   M S B A M L   `   `   `  ����  LPKF History Assistant'    !LPKFHistoryAssistant.BugtrackForm   5      W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      -   5          N� Title$  BugtrackForm��5      $	��534��6   $	��700��6'   -  'System.Windows.Input.KeyboardNavigation   	IsTabStop .  .��5      �  .  .+�5   
   � $��	10,10,0,0q�6   $
��Left=�6"   $	��690��6=    J� IsHitTestVisible .  +� IsFocusScope .    TabNavigation$ Cycle=�6�      DirectionalNavigation$ Cycle=�6�   .+�5      �� -   5        $��_btOk��5   p   $��23��6   $��Right=�6!      TabIndex$ 2��6=   $��	0,0,12,12q�6]   $��Bottom=�6}   $	��107��6�    �� 	IsDefault . .��6�   Ok6�   �� 5      -   5        $��_btSettings��5   �   $��23��6/   $ 3��6;   $
��Left=�6[   $��	12,0,0,12q�6v   $��Bottom=�6�   $	��107��6�   .��6�   Settings...6�   � 5
      -   5        $��grid1��5
   (   $��12,12,12,61q�6   $ Continue=�65   $ Continue=�6a    � RowDefinitions 5      �� 5      $	��44* �6$   $��44��61   $��44��6@   �� 5      $
��397* �6$   	 � ColumnDefinitions	 5      �� 5      .+�5      �� -   5        $��_txbHead��5      
 �� AcceptsReturn
 .  �� 
AcceptsTab . ��.  ���#DBF0DB
�������$��0q�6.   $��14(�6B   $��44��6P   $	��900"�6\    �� 
IsReadOnly .  �� TextDecorations$
 None{�6�   $	��Top=�6�   $
��Left=�6�   $��Center=�6�   $��5q�6  $��Trebuchet MS)�6  .��6(  ,*Please enter Bugtrack Id(s) or Entry Title6T  � 5      -   5        $��grid2��5   $   $	��6,0q�6   $��1��61   $ Continue=�6>   $ Continue=�6j    5      �� 5      $	��40* �6(   �� 5      $	��58* �6(   �� 5      $��12.008* �6(   �� 5      $	��40* �6(   �� 5      $��246.958* �6(   	 5      �� 5      $
��150* �6+   �� 5      $
��510* �6+   .+�5       �� -   5        $��
rectangle1��5    F   ���	#FFDBF0DB$��0,6,0,7.022q�61   6F   �� 5!      -   5        $��label1��5!   v   $��Trebuchet MS)�6   $
��Bold"�66   $��45,6,0,7q�6H   $��Right=�6Z   .��6�   Bugtrack Id(s)6�   �� 5"      -	   5        $��_txbIds��5"   Y   $��Trebuchet MS)�6   $��1��68   $��6,6,0,7q�6H   $ 0��6h   $��Center=�6�   6�   �� 5#      -
   5        $��
rectangle2��5#   F   ���	#FFDBF0DB$��0,0,0,7.022q�61   $��1��6X   �� 5$      -   5        $��	_txbTitle��5$   M   $��Trebuchet MS)�6   $��6,0,0,7.022q�68   $	��Top=�6^   $ 1��6}   $��1��6�   $��1��6�   " �� VerticalScrollBarVisibility$
 Auto=�6�   
 . 6  �� 5%      -   5        $��label2��5%   [   $��Trebuchet MS)�6   $
��Bold"�66   $��	6,39,0,14q�6H   $��Right=�6i   $��2��6�   .��6�   Entry Title6�   5&      5'      5(      5)      5*      