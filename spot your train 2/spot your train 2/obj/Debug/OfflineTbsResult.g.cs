﻿#pragma checksum "D:\Users\ishan\Documents\Visual Studio 2013\Projects\spot your train 2\spot your train 2\OfflineTbsResult.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "109C00C9AA98FA5308ADC640C5FC3F58"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
    
    
    public partial class OfflineTbsResult : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock ofrom;
        
        internal System.Windows.Controls.TextBlock oto;
        
        internal Microsoft.Phone.Controls.LongListSelector otbslist;
        
        internal System.Windows.Controls.ProgressBar opbtbs;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/spot%20your%20train%202;component/OfflineTbsResult.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.ofrom = ((System.Windows.Controls.TextBlock)(this.FindName("ofrom")));
            this.oto = ((System.Windows.Controls.TextBlock)(this.FindName("oto")));
            this.otbslist = ((Microsoft.Phone.Controls.LongListSelector)(this.FindName("otbslist")));
            this.opbtbs = ((System.Windows.Controls.ProgressBar)(this.FindName("opbtbs")));
        }
    }
}
