﻿#pragma checksum "D:\Users\ishan\Documents\Visual Studio 2013\Projects\spot your train 2\spot your train 2\SeatAvailResult.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "84CB28449BA23179D9E18D76336FFA59"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Advertising.Mobile.UI;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace spot_your_train_2 {
    
    
    public partial class SeatAvailResult : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock tname;
        
        internal System.Windows.Controls.TextBlock tnum;
        
        internal System.Windows.Controls.TextBlock cls;
        
        internal System.Windows.Controls.TextBlock qut;
        
        internal System.Windows.Controls.TextBlock f;
        
        internal System.Windows.Controls.TextBlock t;
        
        internal Microsoft.Phone.Controls.LongListSelector availlist;
        
        internal System.Windows.Controls.ProgressBar pbsa;
        
        internal Microsoft.Advertising.Mobile.UI.AdControl ad1sa;
        
        internal Microsoft.Advertising.Mobile.UI.AdControl ad2sa;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/spot%20your%20train%202;component/SeatAvailResult.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.tname = ((System.Windows.Controls.TextBlock)(this.FindName("tname")));
            this.tnum = ((System.Windows.Controls.TextBlock)(this.FindName("tnum")));
            this.cls = ((System.Windows.Controls.TextBlock)(this.FindName("cls")));
            this.qut = ((System.Windows.Controls.TextBlock)(this.FindName("qut")));
            this.f = ((System.Windows.Controls.TextBlock)(this.FindName("f")));
            this.t = ((System.Windows.Controls.TextBlock)(this.FindName("t")));
            this.availlist = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("availlist")));
            this.pbsa = ((System.Windows.Controls.ProgressBar)(this.FindName("pbsa")));
            this.ad1sa = ((Microsoft.Advertising.Mobile.UI.AdControl)(this.FindName("ad1sa")));
            this.ad2sa = ((Microsoft.Advertising.Mobile.UI.AdControl)(this.FindName("ad2sa")));
        }
    }
}

