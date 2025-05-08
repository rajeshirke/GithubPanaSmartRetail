using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using KeyboardOverlap.Forms.Plugin.iOSUnified;
using Retail.Hepler;
using Retail.iOS.DependencyServices;
using Retail.ViewModels;
using UIKit;
using UserNotifications;
using Xamarin.Forms;
using Shiny;
using Octane.Xamarin.Forms.VideoPlayer.iOS;
using Retail.DependencyServices;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.SfRadialMenu.XForms.iOS;
using Plugin.Iconize;
using Syncfusion.SfPicker.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfNumericTextBox.XForms.iOS;
using Syncfusion.XForms.Pickers.iOS;
using Syncfusion.XForms.iOS.TextInputLayout;

namespace Retail.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        [System.Runtime.InteropServices.DllImport(ObjCRuntime.Constants.ObjectiveCLibrary, EntryPoint = "objc_msgSend")]
        internal extern static IntPtr IntPtr_objc_msgSend(IntPtr receiver, IntPtr selector, UISemanticContentAttribute arg1);
        SQLiteDb c = new SQLiteDb();
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            this.ShinyFinishedLaunching(new ShinyStartup());
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTg3MjY2QDMxMzkyZTM0MmUzMFFTNnZlWEF4Q0xDK01sUzRZVHB0dkNaVVFIUkhqTkttYm9KaklMTWV2YWs9");

            CommonAttribute.ScreenHeight = (int)UIScreen.MainScreen.Bounds.Height;
            CommonAttribute.ScreenWidth = (int)UIScreen.MainScreen.Bounds.Width;

            UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            {
                TextColor = UIColor.White,
                Font = UIFont.FromName("calibribold", 24)
            });

            try
            {
                // Color of the selected tab icon:
                var appearanceTabBar = new UITabBarAppearance()
                {
                    //BackgroundColor = UIColor.FromRGB(red: 87, green: 85, blue: 205),
                    //ShadowColor = UIColor.FromRGB(red: 87, green: 85, blue: 205),
                    BackgroundColor = UIColor.FromRGB(red: 30, green: 85, blue: 165),
                    ShadowColor = UIColor.FromRGB(red: 30, green: 85, blue: 165),
                };
                UITabBar.Appearance.StandardAppearance = appearanceTabBar;

                var appearanceNavigation = new UINavigationBarAppearance()
                {
                    //BackgroundColor = UIColor.FromRGB(red: 87, green: 85, blue: 205),
                    //ShadowColor = UIColor.FromRGB(red: 87, green: 85, blue: 205),
                    BackgroundColor = UIColor.FromRGB(red: 30, green: 85, blue: 165),
                    ShadowColor = UIColor.FromRGB(red: 30, green: 85, blue: 165),
                };
                UINavigationBar.Appearance.StandardAppearance = appearanceNavigation;

                //if (Convert.ToInt32(UIDevice.CurrentDevice.SystemVersion.Split(".")[0]) >= 15)
                //{
                //    UITabBar.Appearance.ScrollEdgeAppearance = appearanceTabBar;
                //    UINavigationBar.Appearance.ScrollEdgeAppearance = appearanceNavigation;
                //}

                // NavigationController.NavigationBar.SetBackgroundImage(, UIBarMetrics.Default);
                // NavigationController.NavigationBar.Alpha = alpha;
            }
            catch (Exception ex)
            {

            }

            global::Xamarin.Forms.Forms.SetFlags("Brush_Experimental");
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            XF.Material.iOS.Material.Init();

            UNUserNotificationCenter.Current.Delegate = new iOSNotificationReceiver();
            DependencyService.Register<IAppTrackingTransparencyPermission, AppTrackingTransparencyPermission>();

            //if (System.IO.File.Exists(c.PathToDb))
            //{
            //    LoadApplication(new App());
            //}
            //else
            //{
            c.GetConnection();
                LoadApplication(new App());
            //}
            Rg.Plugins.Popup.Popup.Init();
            OxyPlot.Xamarin.Forms.Platform.iOS.PlotViewRenderer.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            FormsVideoPlayer.Init();
            ImageCircleRenderer.Init();
            KeyboardOverlapRenderer.Init();
            SfCardViewRenderer.Init();
            SfRadialMenuRenderer.Init();
            SfDatePickerRenderer.Init();
            SfTextInputLayoutRenderer.Init();
            Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer.Init();
            Syncfusion.XForms.iOS.Border.SfBorderRenderer.Init();
            Syncfusion.XForms.iOS.Buttons.SfButtonRenderer.Init();
            Syncfusion.XForms.iOS.Graphics.SfGradientViewRenderer.Init();
            Syncfusion.XForms.iOS.Expander.SfExpanderRenderer.Init();
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            Syncfusion.XForms.iOS.Accordion.SfAccordionRenderer.Init();
            Syncfusion.SfDataGrid.XForms.iOS.SfDataGridRenderer.Init();
            new Syncfusion.SfAutoComplete.XForms.iOS.SfAutoCompleteRenderer();
            new SfNumericTextBoxRenderer();
            Iconize.Init();
            SfPickerRenderer.Init();
            //SfBusyIndicatorRenderer.Init();
            new SfBusyIndicatorRenderer();

            MessagingCenter.Unsubscribe<BaseViewModel>(this, "Lang");
            MessagingCenter.Subscribe<BaseViewModel>(this, "Lang", (sender) =>
            {
                ObjCRuntime.Selector selector = new ObjCRuntime.Selector("setSemanticContentAttribute:");

                if (CommonAttribute.flowDirection == FlowDirection.LeftToRight)
                    AppDelegate.IntPtr_objc_msgSend(UIView.Appearance.Handle, selector.Handle, UISemanticContentAttribute.ForceLeftToRight);
                else
                    AppDelegate.IntPtr_objc_msgSend(UIView.Appearance.Handle, selector.Handle, UISemanticContentAttribute.ForceRightToLeft);

            });
           


            return base.FinishedLaunching(app, options);
        }
    }
}
