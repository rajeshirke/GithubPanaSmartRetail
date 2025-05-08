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
    public partial class CategorywiseContributionChart
    {
        public ObservableCollection<SalesEntryReportView> obSalesEntryReportView { get; set; }
        public PlotModel Model { get; set; }
        public List<CategoryWiseContributionGraph> categoryWiseContributions { get; set; }
        public List<Color> Colors { get; set; }

        public double device_height { get; set; }
        public double device_width { get; set; }

        public CategorywiseContributionChart(ObservableCollection<SalesEntryReportView> salesEntryReportViews)
        {
            InitializeComponent();
            obSalesEntryReportView = new ObservableCollection<SalesEntryReportView>();
            obSalesEntryReportView = salesEntryReportViews;
            categoryWiseContributions = new List<CategoryWiseContributionGraph>();
            getPromoterAchievedPlotPercentage();
            DependencyService.Get<IOrientationService>().Landscape();

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

        private void getPromoterAchievedPlotPercentage()
        {
            Colors = new List<Color>();
            if (obSalesEntryReportView != null && obSalesEntryReportView.Count != 0)
            {

                foreach (var item in obSalesEntryReportView)
                {                  
                    if (item != null)
                    {
                        if (item.ProductSubCategoryName != null)
                        {
                            categoryWiseContributions.Add(new CategoryWiseContributionGraph(item.ProductSubCategoryName.ToString(), item.SalesValueOnly));

                            var random = new Random();                            
                        }
                    }
                }

                if (categoryWiseContributions.Count == 1)
                {
                    CategorywiseContribution.Spacing = 0.4;
                }
                else if (categoryWiseContributions.Count >= 1 && categoryWiseContributions.Count <= 4)
                {
                    CategorywiseContribution.Spacing = 0.2;
                }
                else
                {
                    CategorywiseContribution.Spacing = 0;
                }

                CategorywiseContribution.ItemsSource = categoryWiseContributions;
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            DependencyService.Get<IOrientationService>().Portrait();
        }
    }
}

public class CategoryWiseContributionGraph
{
    public string Categories { get; set; }

    public decimal? SalesValue { get; set; }

    public CategoryWiseContributionGraph(string name, decimal? salesvalue)
    {
        this.Categories = name;
        this.SalesValue = (decimal)salesvalue;
    }
}