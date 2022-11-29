﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C483CF3A2A3FACF401D8E9597242A21520385ECC"
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
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 81 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl TabOfDayWeek;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EfirListOnMonday;
        
        #line default
        #line hidden
        
        
        #line 180 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EfirListOnTuesday;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContextMenu AddEventFromContextMenu;
        
        #line default
        #line hidden
        
        
        #line 211 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridView GridOfDay;
        
        #line default
        #line hidden
        
        
        #line 232 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn NameEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 237 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn DescriptionEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 241 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn OptionEfirOfDay;
        
        #line default
        #line hidden
        
        
        #line 268 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EfirListOnWednesday;
        
        #line default
        #line hidden
        
        
        #line 354 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EfirListOnThursday;
        
        #line default
        #line hidden
        
        
        #line 440 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EfirListOnFriday;
        
        #line default
        #line hidden
        
        
        #line 526 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EfirtListOnSaturday;
        
        #line default
        #line hidden
        
        
        #line 611 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView EfirtListOnSunday;
        
        #line default
        #line hidden
        
        
        #line 762 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ListEventtoMedia;
        
        #line default
        #line hidden
        
        
        #line 772 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToLectionTextBox;
        
        #line default
        #line hidden
        
        
        #line 790 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfLectionTextBlock;
        
        #line default
        #line hidden
        
        
        #line 799 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentLection;
        
        #line default
        #line hidden
        
        
        #line 816 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToFilmTextBox;
        
        #line default
        #line hidden
        
        
        #line 835 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfFilmTextBlock;
        
        #line default
        #line hidden
        
        
        #line 841 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentFilm;
        
        #line default
        #line hidden
        
        
        #line 861 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToSeriesTextBox;
        
        #line default
        #line hidden
        
        
        #line 880 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfSeriesTextBlock;
        
        #line default
        #line hidden
        
        
        #line 887 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentSeries;
        
        #line default
        #line hidden
        
        
        #line 901 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToPreventionTextBox;
        
        #line default
        #line hidden
        
        
        #line 920 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfPreventionlTextBlock;
        
        #line default
        #line hidden
        
        
        #line 927 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressDownLoadingContentPrevent;
        
        #line default
        #line hidden
        
        
        #line 1035 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox FilePathToTvShowTextBox;
        
        #line default
        #line hidden
        
        
        #line 1054 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock CountOfTvShowTextBlock;
        
        #line default
        #line hidden
        
        
        #line 1061 "..\..\..\MainWindow.xaml"
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
            this.TabOfDayWeek = ((System.Windows.Controls.TabControl)(target));
            return;
            case 3:
            this.EfirListOnMonday = ((System.Windows.Controls.ListView)(target));
            return;
            case 4:
            
            #line 110 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPreventionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 111 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTvShowAtList_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 112 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSeriesAtList_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 113 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddFilmsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 114 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 115 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddLectionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 116 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBreakAtList_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 118 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveEvent_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.EfirListOnTuesday = ((System.Windows.Controls.ListView)(target));
            return;
            case 14:
            this.AddEventFromContextMenu = ((System.Windows.Controls.ContextMenu)(target));
            return;
            case 15:
            
            #line 199 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPreventionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 200 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTvShowAtList_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 201 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSeriesAtList_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 202 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddFilmsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 203 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 204 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddLectionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 205 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBreakAtList_Click);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 207 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveEvent_Click);
            
            #line default
            #line hidden
            return;
            case 23:
            this.GridOfDay = ((System.Windows.Controls.GridView)(target));
            return;
            case 25:
            this.NameEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 26:
            this.DescriptionEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 27:
            this.OptionEfirOfDay = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 28:
            this.EfirListOnWednesday = ((System.Windows.Controls.ListView)(target));
            return;
            case 29:
            
            #line 287 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPreventionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 288 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTvShowAtList_Click);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 289 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSeriesAtList_Click);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 290 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddFilmsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 291 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 292 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddLectionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            
            #line 293 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBreakAtList_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            
            #line 295 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveEvent_Click);
            
            #line default
            #line hidden
            return;
            case 38:
            this.EfirListOnThursday = ((System.Windows.Controls.ListView)(target));
            return;
            case 39:
            
            #line 373 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPreventionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 40:
            
            #line 374 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTvShowAtList_Click);
            
            #line default
            #line hidden
            return;
            case 41:
            
            #line 375 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSeriesAtList_Click);
            
            #line default
            #line hidden
            return;
            case 42:
            
            #line 376 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddFilmsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 43:
            
            #line 377 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 44:
            
            #line 378 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddLectionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 45:
            
            #line 379 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBreakAtList_Click);
            
            #line default
            #line hidden
            return;
            case 46:
            
            #line 381 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveEvent_Click);
            
            #line default
            #line hidden
            return;
            case 48:
            this.EfirListOnFriday = ((System.Windows.Controls.ListView)(target));
            return;
            case 49:
            
            #line 459 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPreventionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 50:
            
            #line 460 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTvShowAtList_Click);
            
            #line default
            #line hidden
            return;
            case 51:
            
            #line 461 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSeriesAtList_Click);
            
            #line default
            #line hidden
            return;
            case 52:
            
            #line 462 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddFilmsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 53:
            
            #line 463 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 54:
            
            #line 464 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddLectionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 55:
            
            #line 465 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBreakAtList_Click);
            
            #line default
            #line hidden
            return;
            case 56:
            
            #line 467 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveEvent_Click);
            
            #line default
            #line hidden
            return;
            case 58:
            this.EfirtListOnSaturday = ((System.Windows.Controls.ListView)(target));
            return;
            case 59:
            
            #line 545 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPreventionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 60:
            
            #line 546 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTvShowAtList_Click);
            
            #line default
            #line hidden
            return;
            case 61:
            
            #line 547 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSeriesAtList_Click);
            
            #line default
            #line hidden
            return;
            case 62:
            
            #line 548 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddFilmsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 63:
            
            #line 549 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 64:
            
            #line 550 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddLectionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 65:
            
            #line 551 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBreakAtList_Click);
            
            #line default
            #line hidden
            return;
            case 66:
            
            #line 553 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveEvent_Click);
            
            #line default
            #line hidden
            return;
            case 68:
            this.EfirtListOnSunday = ((System.Windows.Controls.ListView)(target));
            return;
            case 69:
            
            #line 630 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPreventionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 70:
            
            #line 631 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddTvShowAtList_Click);
            
            #line default
            #line hidden
            return;
            case 71:
            
            #line 632 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddSeriesAtList_Click);
            
            #line default
            #line hidden
            return;
            case 72:
            
            #line 633 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddFilmsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 73:
            
            #line 634 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewsAtList_Click);
            
            #line default
            #line hidden
            return;
            case 74:
            
            #line 635 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddLectionAtList_Click);
            
            #line default
            #line hidden
            return;
            case 75:
            
            #line 636 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.AddBreakAtList_Click);
            
            #line default
            #line hidden
            return;
            case 76:
            
            #line 638 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveEvent_Click);
            
            #line default
            #line hidden
            return;
            case 78:
            this.ListEventtoMedia = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 79:
            this.FilePathToLectionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 80:
            
            #line 784 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenLectionDialog_Click);
            
            #line default
            #line hidden
            return;
            case 81:
            this.CountOfLectionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 82:
            this.ProgressDownLoadingContentLection = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 83:
            this.FilePathToFilmTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 84:
            
            #line 828 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenFilmsDialog_Click);
            
            #line default
            #line hidden
            return;
            case 85:
            this.CountOfFilmTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 86:
            this.ProgressDownLoadingContentFilm = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 87:
            this.FilePathToSeriesTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 88:
            
            #line 874 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenSeriesDialog_Click);
            
            #line default
            #line hidden
            return;
            case 89:
            this.CountOfSeriesTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 90:
            this.ProgressDownLoadingContentSeries = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 91:
            this.FilePathToPreventionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 92:
            
            #line 914 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenPreventionDialog_Click);
            
            #line default
            #line hidden
            return;
            case 93:
            this.CountOfPreventionlTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 94:
            this.ProgressDownLoadingContentPrevent = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 95:
            this.FilePathToTvShowTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 96:
            
            #line 1047 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OpenTvShowDialog_Click);
            
            #line default
            #line hidden
            return;
            case 97:
            this.CountOfTvShowTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 98:
            this.ProgressDownLoadingContentTvShow = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 12:
            
            #line 137 "..\..\..\MainWindow.xaml"
            ((MaterialDesignThemes.Wpf.TimePicker)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.ChangeTimeEvent);
            
            #line default
            #line hidden
            break;
            case 24:
            
            #line 227 "..\..\..\MainWindow.xaml"
            ((MaterialDesignThemes.Wpf.TimePicker)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.ChangeTimeEvent);
            
            #line default
            #line hidden
            break;
            case 37:
            
            #line 314 "..\..\..\MainWindow.xaml"
            ((MaterialDesignThemes.Wpf.TimePicker)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.ChangeTimeEvent);
            
            #line default
            #line hidden
            break;
            case 47:
            
            #line 400 "..\..\..\MainWindow.xaml"
            ((MaterialDesignThemes.Wpf.TimePicker)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.ChangeTimeEvent);
            
            #line default
            #line hidden
            break;
            case 57:
            
            #line 486 "..\..\..\MainWindow.xaml"
            ((MaterialDesignThemes.Wpf.TimePicker)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.ChangeTimeEvent);
            
            #line default
            #line hidden
            break;
            case 67:
            
            #line 572 "..\..\..\MainWindow.xaml"
            ((MaterialDesignThemes.Wpf.TimePicker)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.ChangeTimeEvent);
            
            #line default
            #line hidden
            break;
            case 77:
            
            #line 657 "..\..\..\MainWindow.xaml"
            ((MaterialDesignThemes.Wpf.TimePicker)(target)).LostFocus += new System.Windows.RoutedEventHandler(this.ChangeTimeEvent);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

