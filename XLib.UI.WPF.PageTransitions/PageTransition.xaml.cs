using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Media.Animation;

namespace XLib.UI.WPF
{
	public partial class PageTransition : UserControl 
	{
		Stack<Page> pages = new Stack<Page>();

		public Page CurrentPage { get; set; }

		public static readonly DependencyProperty TransitionTypeProperty = DependencyProperty.Register("TransitionType",
			typeof(PageTransitionType),
			typeof(PageTransition), new PropertyMetadata(PageTransitionType.SlideAndFade));

		public PageTransitionType TransitionType
		{
			get
			{
				return (PageTransitionType)GetValue(TransitionTypeProperty);
			}
			set 
			{
				SetValue(TransitionTypeProperty, value);
			}
		}

		public PageTransition()
		{
			InitializeComponent();
		}		
		
		public void ShowPage(Page newPage)
		{			
			pages.Push(newPage);
            Action action = () => ShowNewPage();
            action.BeginInvoke( null, null );
		}

		void ShowNewPage()
		{
			Dispatcher.Invoke((Action)delegate 
				{
					if (contentPresenter.Content != null)
					{
						Page oldPage = contentPresenter.Content as Page;

						if (oldPage != null)
						{
							oldPage.Loaded -= newPage_Loaded;

							UnloadPage(oldPage);
						}
					}
					else
					{
						ShowNextPage();
					}
					
				});
		}

		void ShowNextPage()
		{
			var newPage = pages.Pop();

			newPage.Loaded += newPage_Loaded;

			contentPresenter.Content = newPage;
		}

		void UnloadPage(Page page)
		{
			Storyboard hidePage = (Resources[string.Format("{0}Out", TransitionType.ToString())] as Storyboard).Clone();

			hidePage.Completed += hidePage_Completed;

			hidePage.Begin(contentPresenter);
		}

		void newPage_Loaded(object sender, RoutedEventArgs e)
		{
			Storyboard showNewPage = Resources[string.Format("{0}In", TransitionType.ToString())] as Storyboard;

			showNewPage.Begin(contentPresenter);

			CurrentPage = sender as Page;
		}		

		void hidePage_Completed(object sender, EventArgs e)
		{
			contentPresenter.Content = null;

			ShowNextPage();
		}		
	}
}
