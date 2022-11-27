﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4D2BB44C7066F691968AF7FA2FD124DC1D2D5FFD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ControlzEx;
using ControlzEx.Behaviors;
using ControlzEx.Controls;
using ControlzEx.Theming;
using ControlzEx.Windows.Shell;
using Efir;
using Efir.ViewModels;
using GongSolutions.Wpf.DragDrop;
using MaterialDesignThemes.MahApps;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace Efir {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 146 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Testing;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridView GridOfDay;
        
        #line default
        #line hidden
        
        
        #line 182 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn NameEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 187 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn DescriptionEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn OptionEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 646 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToLectionTextBox;
        
        #line default
        #line hidden
        
        
        #line 664 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfLectionTextBlock;
        
        #line default
        #line hidden
        
        
        #line 673 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentLection;
        
        #line default
        #line hidden
        
        
        #line 690 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToFilmTextBox;
        
        #line default
        #line hidden
        
        
        #line 709 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfFilmTextBlock;
        
        #line default
        #line hidden
        
        
        #line 715 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentFilm;
        
        #line default
        #line hidden
        
        
        #line 735 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToSeriesTextBox;
        
        #line default
        #line hidden
        
        
        #line 754 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfSeriesTextBlock;
        
        #line default
        #line hidden
        
        
        #line 761 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentSeries;
        
        #line default
        #line hidden
        
        
        #line 781 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToPreventionTextBox;
        
        #line default
        #line hidden
        
        
        #line 800 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfPreventionlTextBlock;
        
        #line default
        #line hidden
        
        
        #line 807 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentPrevent;
        
        #line default
        #line hidden
        
        
        #line 921 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToTvShowTextBox;
        
        #line default
        #line hidden
        
        
        #line 940 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfTvShowTextBlock;
        
        #line default
        #line hidden
        
        
        #line 947 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentTvShow;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Efir;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 26 "..\..\..\MainWindow.xaml"
            ((Efir.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MainWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Testing = ((System.Windows.Controls.ListView)(target));
            return;
            case 3:
            this.GridOfDay = ((System.Windows.Controls.GridView)(target));
            return;
            case 4:
            this.NameEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 5:
            this.DescriptionEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 6:
            this.OptionEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 7:
            this.FilePathToLectionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 658 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenLectionDialog_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.CountOfLectionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.ProgressDownLoadingContentLection = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 11:
            this.FilePathToFilmTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            
            #line 702 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenFilmsDialog_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.CountOfFilmTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 14:
            this.ProgressDownLoadingContentFilm = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 15:
            this.FilePathToSeriesTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 16:
            
            #line 748 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenSeriesDialog_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.CountOfSeriesTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 18:
            this.ProgressDownLoadingContentSeries = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 19:
            this.FilePathToPreventionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 20:
            
            #line 794 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenPreventionDialog_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            this.CountOfPreventionlTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 22:
            this.ProgressDownLoadingContentPrevent = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 23:
            this.FilePathToTvShowTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 24:
            
            #line 933 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenTvShowDialog_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            this.CountOfTvShowTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 26:
            this.ProgressDownLoadingContentTvShow = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

