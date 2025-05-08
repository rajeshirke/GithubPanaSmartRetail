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
using Xamarin.Forms.Internals;

namespace Retail.Views.Reports.Charts
{
    public partial class DailySalesTrendBySubcategoryChart
    {
        public ObservableCollection<SalesEntryDailyBySubCategoryView> salesEntryDailyBySubCategoryViews { get; set; }
        public PlotModel Model { get; set; }
        public List<lstDailySalesBySubCategory> lstDailySalesBySubCategories { get; set; }
        public List<Color> Colors { get; set; }

        public double device_height { get; set; }
        public double device_width { get; set; }

        public DailySalesTrendBySubcategoryChart(ObservableCollection<SalesEntryDailyBySubCategoryView> salesEntryDailyBySubCategories)
        {
            InitializeComponent();
            salesEntryDailyBySubCategoryViews = new ObservableCollection<SalesEntryDailyBySubCategoryView>();
            salesEntryDailyBySubCategoryViews = salesEntryDailyBySubCategories;

            getDailySalesTrenBySubcatgeory();
            DependencyService.Get<IOrientationService>().Landscape();
            lstDailySalesBySubCategories = new List<lstDailySalesBySubCategory>();

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

        private void getDailySalesTrenBySubcatgeory()
        {
            lstDailySalesBySubCategories = new List<lstDailySalesBySubCategory>();
            if (salesEntryDailyBySubCategoryViews != null && salesEntryDailyBySubCategoryViews.Count != 0)
            {
               
                foreach (var item in salesEntryDailyBySubCategoryViews)
                {
                    if (item != null)
                    {
                        lstDailySalesBySubCategories.Add(new lstDailySalesBySubCategory
                           (Convert.ToDateTime(item.EntryDate).ToString("yyyy-MM-dd"), item.TotalTargets, item.AchievedPercentage));

                    }
                }

                //colTotalTargets.ItemsSource = lstDailySalesBySubCategories;
                //colTotalTargets.ColorModel.Palette = ChartColorPalette.Custom;
                //colTotalTargets.ColorModel.CustomBrushes = Colors;

                colAchievedTargets.ItemsSource = lstDailySalesBySubCategories;
            }

        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            DependencyService.Get<IOrientationService>().Portrait();
        }
    }
}

public class lstDailySalesBySubCategory
{
    public string EntryDate { get; set; }

    public decimal? TotalTargetsValue { get; set; }

    public decimal? AchievedTargetsValue { get; set; }

    public lstDailySalesBySubCategory(string eDate, decimal? targetsvalue,decimal? achievedvalue)
    {
        this.EntryDate = eDate;
        this.TotalTargetsValue = (decimal)targetsvalue;
        this.AchievedTargetsValue = (decimal)achievedvalue;
    }
}