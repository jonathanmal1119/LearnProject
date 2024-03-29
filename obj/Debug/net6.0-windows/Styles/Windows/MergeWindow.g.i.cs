﻿#pragma checksum "..\..\..\..\..\Styles\Windows\MergeWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5EC8D942D1ECF98D49D591FB4E0738136173177B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Learn.Styles.Templates;
using Learn.Styles.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Learn.Styles.Windows {
    
    
    /// <summary>
    /// MergeWindow
    /// </summary>
    public partial class MergeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid winGrid;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label pageName;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeBtn;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image closeBtnI;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SetGroupingTB;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel setsSP;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label errorLB;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Learn;component/styles/windows/mergewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.winGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 20 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
            ((System.Windows.Controls.Border)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Border_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.pageName = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.closeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
            this.closeBtn.Click += new System.Windows.RoutedEventHandler(this.closeBtn_Click);
            
            #line default
            #line hidden
            
            #line 23 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
            this.closeBtn.MouseEnter += new System.Windows.Input.MouseEventHandler(this.closeBtn_MouseEnter);
            
            #line default
            #line hidden
            
            #line 23 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
            this.closeBtn.MouseLeave += new System.Windows.Input.MouseEventHandler(this.closeBtn_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 5:
            this.closeBtnI = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.SetGroupingTB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.setsSP = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 8:
            
            #line 59 "..\..\..\..\..\Styles\Windows\MergeWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.merge_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.errorLB = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

