﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CProProcessMonitor.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CProProcessMonitor.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to set datafile separator &quot;\t&quot;
        ///set terminal png size 1024,768
        ///set title &quot;CPro Process Monitor&quot;
        ///set ylabel &quot;Value&quot;
        ///set xlabel &quot;Time&quot;
        ///set xdata time
        ///set timefmt &quot;%d.%m.%Y %H:%M:%S&quot;
        ///set format x &quot;%H:%M&quot;
        ///set key bottom center outside
        ///set grid
        ///plot &quot;[log_path]&quot; every ::1 using 1:4 with lines lw 2 lt 2 title &apos;CLR Memory [MB]&apos; 
        ///.
        /// </summary>
        public static string plotsettings_clrmemory_template {
            get {
                return ResourceManager.GetString("plotsettings_clrmemory_template", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to set datafile separator &quot;\t&quot;
        ///set terminal png size 1024,768
        ///set title &quot;CPro Process Monitor&quot;
        ///set ylabel &quot;Value&quot;
        ///set xlabel &quot;Time&quot;
        ///set xdata time
        ///set timefmt &quot;%d.%m.%Y %H:%M:%S&quot;
        ///set format x &quot;%H:%M&quot;
        ///set key bottom center outside
        ///set grid
        ///plot &quot;[log_path]&quot; every ::1 using 1:2 with lines lw 2 lt 3 title &apos;CPU [%]&apos;
        ///.
        /// </summary>
        public static string plotsettings_cpu_template {
            get {
                return ResourceManager.GetString("plotsettings_cpu_template", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to set datafile separator &quot;\t&quot;
        ///set terminal png size 1024,768
        ///set title &quot;CPro Process Monitor&quot;
        ///set ylabel &quot;Value&quot;
        ///set xlabel &quot;Time&quot;
        ///set xdata time
        ///set timefmt &quot;%d.%m.%Y %H:%M:%S&quot;
        ///set format x &quot;%H:%M&quot;
        ///set key bottom center outside
        ///set grid
        ///plot &quot;[log_path]&quot; every ::1 using 1:3 with lines lw 2 lt 1 title &apos;Memory [MB]&apos;
        ///     
        ///.
        /// </summary>
        public static string plotsettings_memory_template {
            get {
                return ResourceManager.GetString("plotsettings_memory_template", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to set datafile separator &quot;\t&quot;
        ///set terminal png size 1024,768
        ///set title &quot;CPro Process Monitor&quot;
        ///set ylabel &quot;Value&quot;
        ///set xlabel &quot;Time&quot;
        ///set xdata time
        ///set timefmt &quot;%d.%m.%Y %H:%M:%S&quot;
        ///set format x &quot;%H:%M&quot;
        ///set key bottom center outside
        ///set grid
        ///plot &quot;[log_path]&quot; every ::1 using 1:2 with lines lw 2 lt 3 title &apos;CPU [%]&apos;, \
        ///     &quot;[log_path]&quot; every ::1 using 1:3 with lines lw 2 lt 1 title &apos;Memory [MB]&apos;, \
        ///     &quot;[log_path]&quot; every ::1 using 1:4 with lines lw 2 lt 2 title &apos;CLR Memory [MB]&apos; 
        ///.
        /// </summary>
        public static string plotsettings_template {
            get {
                return ResourceManager.GetString("plotsettings_template", resourceCulture);
            }
        }
    }
}
