using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfBusyIndicator.XForms;
using System.Threading.Tasks;

namespace Retail.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoaderAnimView : ContentView
    {
        public LoaderAnimView()
        {
            InitializeComponent();
            //LoaderAnim();
            LoaderAnimVisible();

            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                busyindicator.IsVisible = false;
                return false;
            });
        }

        public static readonly BindableProperty IsVisiblesProperty =
          BindableProperty.Create(nameof(IsVisibles), typeof(bool), typeof(LoaderAnimView), false);

        public bool IsVisibles
        {
            get => (bool)GetValue(IsVisiblesProperty);
            set => SetValue(IsVisiblesProperty, value);
        }

        public void LoaderAnim()
        {
            SfBusyIndicator busyIndicator = new SfBusyIndicator()
            {
                AnimationType = AnimationTypes.Box,
                ViewBoxHeight = 150,
                ViewBoxWidth = 150,                
                TextColor = Color.FromHex("1E55A5"),
            };

            this.Content = busyIndicator;
        }

        public void LoaderAnimVisible()
        {
            MessagingCenter.Subscribe<string>(this, "ShowLoaderAnim", (selectedItems) =>
            {
                if (selectedItems == "Show")
                {
                    busyindicator.IsVisible = true;
                }
                else
                {
                    busyindicator.IsVisible = false;
                }
            });
        }

    }
}
