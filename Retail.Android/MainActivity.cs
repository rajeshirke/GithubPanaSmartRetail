using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Android.Widget;
using Android.Database;
using Android.Provider;
using Xamarin.Forms;
using Retail.Droid.Renderers;
using Android.Views;
using Plugin.Media;
using Plugin.CurrentActivity;
using Retail.Droid.DependencyServices;
using SQLite;
using ImageCircle.Forms.Plugin.Droid;
using Retail.Hepler;
using Android.Support.V7.App;
using Retail.DependencyServices;
using ZXing.Net.Mobile;
using Shiny;
using System.Threading.Tasks;
using Octane.Xamarin.Forms.VideoPlayer.Android;
using Syncfusion.XForms.Android.Cards;

namespace Retail.Droid
{
    [Activity(Label = "Retail", Icon = "@mipmap/applogo", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize, LaunchMode = LaunchMode.SingleTop)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static int StatusBarHeight;
        SQLiteDb c = new SQLiteDb();
        public static MainActivity rootActivity { get; set; }
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            this.ShinyOnCreate();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTg3MjY2QDMxMzkyZTM0MmUzMFFTNnZlWEF4Q0xDK01sUzRZVHB0dkNaVVFIUkhqTkttYm9KaklMTWV2YWs9");

            //AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("Brush_Experimental");
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
           
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            Rg.Plugins.Popup.Popup.Init(this);
            OxyPlot.Xamarin.Forms.Platform.Android.PlotViewRenderer.Init();
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            await CrossMedia.Current.Initialize();

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            ImageCircleRenderer.Init();
            FormsVideoPlayer.Init();

            XF.Material.Droid.Material.Init(this, savedInstanceState);


            CommonAttribute.ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            CommonAttribute.ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);
            rootActivity = this;
       
            StatusBarHeight = getStatusBarHeight();
            c.GetConnection();
            if (System.IO.File.Exists(c.PathToDb))
            {
                LoadApplication(new App());
            }
            else
            {
                c.GetConnection();
                LoadApplication(new App());
            }
            CreateNotificationFromIntent(Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            this.ShinyOnNewIntent(intent);
            CreateNotificationFromIntent(intent);
        }

        void CreateNotificationFromIntent(Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(AndroidNotificationManager.TitleKey);
                string message = intent.GetStringExtra(AndroidNotificationManager.MessageKey);
                DependencyService.Get<INotificationManager>().ReceiveNotification(title, message);
            }
        }

        public int getStatusBarHeight()
        {

            int statusBarHeight = 0, totalHeight = 0, contentHeight = 0;
            int resourceId = Resources.GetIdentifier("status_bar_height", "dimen", "android");
            if (resourceId > 0)
            {
                statusBarHeight = Resources.GetDimensionPixelSize(resourceId);

            }
            return statusBarHeight;
        }

        private void TransparentStatusBar()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                // for covering the full screen in android..
                Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);

                // clear FLAG_TRANSLUCENT_STATUS flag:
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);

                Window.SetStatusBarColor(Android.Graphics.Color.Transparent);

            }

        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            //RunOnUiThread(
            //   async () =>
            //   {
            //       var isCloseApp = await AlertAsync(this, "Retail", "Do you want to close this app?", "Yes", "No");

            //       if (isCloseApp)
            //       {
            //           var activity = (Activity)Forms.Context;
            //           activity.FinishAffinity();
            //       }
            //   });
        }

        public Task<bool> AlertAsync(Context context, string title, string message, string positiveButton, string negativeButton)
        {
            var tcs = new TaskCompletionSource<bool>();

            using (var db = new Android.App.AlertDialog.Builder(context))
            {
                db.SetTitle(title);
                db.SetMessage(message);
                db.SetPositiveButton(positiveButton, (sender, args) => { tcs.TrySetResult(true); });
                db.SetNegativeButton(negativeButton, (sender, args) => { tcs.TrySetResult(false); });
                db.Show();
            }

            return tcs.Task;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            // BadgeDrawable.SetBadgeCount(this, menu.GetItem(0),3,Android.Graphics.Color.Red, Android.Graphics.Color.White);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            return base.OnCreateOptionsMenu(menu);
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        //{
        //    Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //    this.ShinyOnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            this.ShinyOnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            MultiMediaPickerService.SharedInstance.OnActivityResult(requestCode, resultCode, data);
        }
    }
}