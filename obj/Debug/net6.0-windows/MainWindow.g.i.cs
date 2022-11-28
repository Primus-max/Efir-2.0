﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8BAD2B610EDAC9060BD299FF59326D3F1C534A58"
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
        
        
        #line 144 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EventListOnTuesday;
        
        #line default
        #line hidden
        
        
        #line 166 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridView GridOfDay;
        
        #line default
        #line hidden
        
        
        #line 186 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn NameEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 191 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn DescriptionEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn OptionEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 641 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ListEventtoMedia;
        
        #line default
        #line hidden
        
        
        #line 651 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToLectionTextBox;
        
        #line default
        #line hidden
        
        
        #line 669 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfLectionTextBlock;
        
        #line default
        #line hidden
        
        
        #line 678 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentLection;
        
        #line default
        #line hidden
        
        
        #line 695 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToFilmTextBox;
        
        #line default
        #line hidden
        
        
        #line 714 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfFilmTextBlock;
        
        #line default
        #line hidden
        
        
        #line 720 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentFilm;
        
        #line default
        #line hidden
        
        
        #line 740 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToSeriesTextBox;
        
        #line default
        #line hidden
        
        
        #line 759 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfSeriesTextBlock;
        
        #line default
        #line hidden
        
        
        #line 766 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentSeries;
        
        #line default
        #line hidden
        
        
        #line 786 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToPreventionTextBox;
        
        #line default
        #line hidden
        
        
        #line 805 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfPreventionlTextBlock;
        
        #line default
        #line hidden
        
        
        #line 812 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentPrevent;
        
        #line default
        #line hidden
        
        
        #line 926 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToTvShowTextBox;
        
        #line default
        #line hidden
        
        
        #line 945 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfTvShowTextBlock;
        
        #line default
        #line hidden
        
        
        #line 952 "..\..\..\MainWindow.xaml"
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
            
            #line 24 "..\..\..\MainWindow.xaml"
            ((Efir.MainWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.MainWindow_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.EventListOnTuesday = ((System.Windows.Controls.ListView)(target));
            
            #line 159 "..\..\..\MainWindow.xaml"
            this.EventListOnTuesday.LostFocus += new System.Windows.RoutedEventHandler(this.TEST);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 162 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.TESET);
            
            #line default
            #line hidden
            return;
            case 4:
            this.GridOfDay = ((System.Windows.Controls.GridView)(target));
            return;
            case 5:
            this.NameEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 6:
            this.DescriptionEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 7:
            this.OptionEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 8:
            this.ListEventtoMedia = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.FilePathToLectionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            
            #line 663 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenLectionDialog_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.CountOfLectionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.ProgressDownLoadingContentLection = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 13:
            this.FilePathToFilmTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            
            #line 707 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenFilmsDialog_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.CountOfFilmTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 16:
            this.ProgressDownLoadingContentFilm = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 17:
            this.FilePathToSeriesTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 18:
            
            #line 753 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenSeriesDialog_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            this.CountOfSeriesTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 20:
            this.ProgressDownLoadingContentSeries = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 21:
            this.FilePathToPreventionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 22:
            
            #line 799 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenPreventionDialog_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            this.CountOfPreventionlTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 24:
            this.ProgressDownLoadingContentPrevent = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 25:
            this.FilePathToTvShowTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 26:
            
            #line 938 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenTvShowDialog_Click);
            
            #line default
            #line hidden
            return;
            case 27:
            this.CountOfTvShowTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 28:
            this.ProgressDownLoadingContentTvShow = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

