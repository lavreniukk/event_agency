﻿#pragma checksum "..\..\..\Views\GuestWin.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9CA22B4CDAE93388927A1CD5174DECD66423D6977F9EE08D8264B0F0050E8694"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using EventAgency.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace EventAgency.Views {
    
    
    /// <summary>
    /// GuestWin
    /// </summary>
    public partial class GuestWin : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Views\GuestWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox firstNameField;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Views\GuestWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox lastNameField;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\Views\GuestWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ageField;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\GuestWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox phoneNumField;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Views\GuestWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailField;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\GuestWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UpdateGuestButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Views\GuestWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteGuestButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Views\GuestWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddGuestButton;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Views\GuestWin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView GuestData;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EventAgency;component/views/guestwin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\GuestWin.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.firstNameField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.lastNameField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ageField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.phoneNumField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.emailField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.UpdateGuestButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Views\GuestWin.xaml"
            this.UpdateGuestButton.Click += new System.Windows.RoutedEventHandler(this.UpdateGuestButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DeleteGuestButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\Views\GuestWin.xaml"
            this.DeleteGuestButton.Click += new System.Windows.RoutedEventHandler(this.DeleteGuestButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.AddGuestButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Views\GuestWin.xaml"
            this.AddGuestButton.Click += new System.Windows.RoutedEventHandler(this.AddGuestButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.GuestData = ((System.Windows.Controls.ListView)(target));
            
            #line 26 "..\..\..\Views\GuestWin.xaml"
            this.GuestData.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.GuestData_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

