using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public partial class SalesAchievedvsProductCat
    {
        public ObservableCollection<SalesTargetProductCategoryReportView> salesTargetProductCatReportViews { get; set; }
        public List<lstTotalTargets> listTotalTargets { get; set; }
        public List<lstBalanceTargets> listBalanceTargets { get; set; }
        public List<lstAchievedTargets> listAchievedTargets { get; set; }
        public List<Color> Colors
        {
            get;
            set;
        }

        public double device_height { get; set; }
        public double device_width { get; set; }

        public SalesAchievedvsProductCat(ObservableCollection<SalesTargetProductCategoryReportView> _salesTargetProductCatReportViews)
        {
            InitializeComponent();
            salesTargetProductCatReportViews = _salesTargetProductCatReportViews;
           
            getSalesAchievedPlotProductCat();
            DependencyService.Get<IOrientationService>().Landscape();

            listTotalTargets = new List<lstTotalTargets>();
            listBalanceTargets = new List<lstBalanceTargets>();
            listAchievedTargets = new List<lstAchievedTargets>();

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

        public PlotModel Model { get; set; }

        private void getSalesAchievedPlotProductCat()
        {
            if (salesTargetProductCatReportViews != null)
            {
                Colors = new List<Color>();
                listTotalTargets = new List<lstTotalTargets>();
                listBalanceTargets = new List<lstBalanceTargets>();
                listAchievedTargets = new List<lstAchievedTargets>();
                foreach (var item in salesTargetProductCatReportViews)
                {                   
                    string CategoryAccount = "";
                    
                    if (item != null)
                    {
                        if(item.LocationAccountName != null && item.Name!=null)
                        {
                             CategoryAccount = item.LocationAccountName.ToString() + "-" + item.Name;
                        }
                        listTotalTargets.Add(new lstTotalTargets(CategoryAccount, Convert.ToDecimal((double)item.TotalTargets)));
                        listBalanceTargets.Add(new lstBalanceTargets(CategoryAccount, Convert.ToDecimal((double)item.BalanceTargets)));
                        listAchievedTargets.Add(new lstAchievedTargets(CategoryAccount, Convert.ToDecimal((double)item.AcheivedTargets)));

                    }
                }

                if (listAchievedTargets.Count == 1)
                {
                    colAchievedTargets.Spacing = 0.4;
                }
                else if(listAchievedTargets.Count >= 1 && listAchievedTargets.Count <= 4)
                {
                    colAchievedTargets.Spacing = 0.2;
                }
                else
                {
                    colAchievedTargets.Spacing = 0.01;
                }

                colAchievedTargets.ItemsSource = listAchievedTargets;
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            DependencyService.Get<IOrientationService>().Portrait();
        }
    }
}

public class lstTotalTargets
{
    public string CategoryAccNm { get; set; }

    public decimal? TotalTargetsValue { get; set; }

    public lstTotalTargets(string name, decimal? targetsvalue)
    {
        this.CategoryAccNm = name;
        this.TotalTargetsValue = (decimal)targetsvalue;
    }
}

public class lstBalanceTargets
{
    public string CategoryAccNm { get; set; }

    public decimal? BalanceTargetsValue { get; set; }

    public lstBalanceTargets(string name, decimal? balancevalue)
    {
        this.CategoryAccNm = name;
        this.BalanceTargetsValue = (decimal)balancevalue;
    }
}

public class lstAchievedTargets
{
    public string CategoryAccNm { get; set; }

    public decimal? AchievedTargetsValue { get; set; }

    public lstAchievedTargets(string name, decimal? achievedvalue)
    {
        this.CategoryAccNm = name;
        this.AchievedTargetsValue = (decimal)achievedvalue;
    }
}