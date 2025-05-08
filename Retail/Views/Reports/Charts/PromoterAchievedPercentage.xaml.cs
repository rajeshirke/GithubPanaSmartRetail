using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Retail.DependencyServices;
using Retail.Infrastructure.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfChart.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.Views.Reports.Charts
{
    public partial class PromoterAchievedPercentage
    {
        public ObservableCollection<SalesTargetPromoterAchievementReportView> salesTargetPromoterAchievementReportViews { get; set; }
        public List<PromoterAchievements> lstPromoterAchievements { get; set; }
        public List<Color> Colors
        {
            get;
            set;
        }
        public double device_height { get; set; }
        public double device_width { get; set; }

        public PromoterAchievedPercentage(ObservableCollection<SalesTargetPromoterAchievementReportView> _salesTargetPromoterAchievementReportViews)
        {
            InitializeComponent();
            salesTargetPromoterAchievementReportViews = _salesTargetPromoterAchievementReportViews;

            getPromoterAchievedPlotPercentage();
            DependencyService.Get<IOrientationService>().Landscape();
            lstPromoterAchievements = new List<PromoterAchievements>();

            device_height = DeviceDisplay.MainDisplayInfo.Height;
            device_width = DeviceDisplay.MainDisplayInfo.Width;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DependencyService.Get<IOrientationService>().Landscape();


            device_height = DeviceDisplay.MainDisplayInfo.Height;
            device_width = DeviceDisplay.MainDisplayInfo.Width;

            OnSizeAllocated(device_width, device_height);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            DependencyService.Get<IOrientationService>().Portrait();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called

            if (width > height)
            {
                DependencyService.Get<IOrientationService>().Landscape();
            }
            else
            {
                DependencyService.Get<IOrientationService>().Landscape();
            }
        }

        public static Random random = new Random();
        public static Color[] colors =
        {
            Color.FromRgb(random.Next(256), random.Next(256), random.Next(256))

            //Color.FromHex("#0099CC"), Color.FromHex("#00C78C"),
            //Color.FromHex("#008080"),Color.FromHex("#00CED1"),Color.FromHex("#33A1DE"),
            //Color.FromHex("#3D59AB"),Color.FromHex("#88AAFF"),Color.FromHex("#A1ECCD")

        };
        private void getPromoterAchievedPlotPercentage()
        {
            Colors = new List<Color>();
            if (salesTargetPromoterAchievementReportViews != null)
            {
                lstPromoterAchievements = new List<PromoterAchievements>();
                foreach (var item in salesTargetPromoterAchievementReportViews)
                {
                    var random = new Random();
                    Colors.Add(colors[random.Next(colors.Length)]);

                    if (item.PersonName != null)
                    {
                        lstPromoterAchievements.Add(new PromoterAchievements(item.PersonName.ToString(), item.PercentageAchieved));
                    }
                }
                if (lstPromoterAchievements.Count == 1)
                {
                    PromoterAchievementgraph.Spacing = 0.4;
                }
                else if (lstPromoterAchievements.Count >= 1 && lstPromoterAchievements.Count <= 4)
                {
                    PromoterAchievementgraph.Spacing = 0.2;
                }
                else
                {
                    PromoterAchievementgraph.Spacing = 0.01;
                }

                PromoterAchievementgraph.ItemsSource = lstPromoterAchievements;
                //PromoterAchievementgraph.ColorModel.Palette = ChartColorPalette.Custom;
                //PromoterAchievementgraph.ColorModel.CustomBrushes = Colors;
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            DependencyService.Get<IOrientationService>().Portrait();

        }
    }
}


public class PromoterAchievements
{
    public string PersonName { get; set; }

    public decimal? AchievedValue { get; set; }

    public PromoterAchievements(string name, decimal? value)
    {
        this.PersonName = name;
        this.AchievedValue = (decimal)value;
    }
}