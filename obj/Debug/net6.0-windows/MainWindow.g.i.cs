﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "AE76E4D14F286F0AC934FEDE3D318CC181AC8976"
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
        
        
        #line 92 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Testing;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridView GridOfDay;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn TimeEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 118 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn NameEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 123 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn DescriptionEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn OptionEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 260 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToLectionTextBox;
        
        #line default
        #line hidden
        
        
        #line 278 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfLectionTextBlock;
        
        #line default
        #line hidden
        
        
        #line 287 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentLection;
        
        #line default
        #line hidden
        
        
        #line 304 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToFilmTextBox;
        
        #line default
        #line hidden
        
        
        #line 323 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfFilmTextBlock;
        
        #line default
        #line hidden
        
        
        #line 329 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentFilm;
        
        #line default
        #line hidden
        
        
        #line 349 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToSeriesTextBox;
        
        #line default
        #line hidden
        
        
        #line 368 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfSeriesTextBlock;
        
        #line default
        #line hidden
        
        
        #line 375 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentSeries;
        
        #line default
        #line hidden
        
        
        #line 395 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToPreventionTextBox;
        
        #line default
        #line hidden
        
        
        #line 414 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfPreventionlTextBlock;
        
        #line default
        #line hidden
        
        
        #line 421 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentPrevent;
        
        #line default
        #line hidden
        
        
        #line 535 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToTvShowTextBox;
        
        #line default
        #line hidden
        
        
        #line 554 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfTvShowTextBlock;
        
        #line default
        #line hidden
        
        
        #line 561 "..\..\..\MainWindow.xaml"
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
            this.TimeEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
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
            
            #line 186 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.GenerateEfir_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.FilePathToLectionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            
            #line 272 "..\..\..\MainWindow.xaml"
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
            
            #line 316 "..\..\..\MainWindow.xaml"
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
            
            #line 362 "..\..\..\MainWindow.xaml"
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
            
            #line 408 "..\..\..\MainWindow.xaml"
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
            
            #line 547 "..\..\..\MainWindow.xaml"
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

