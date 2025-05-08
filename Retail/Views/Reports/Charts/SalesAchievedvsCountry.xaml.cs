using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Microcharts;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Models;
using Retail.ViewModels.InventoryStock;
using Retail.ViewModels.Reports.Charts;
using Rg.Plugins.Popup.Services;
using SkiaSharp;
using Syncfusion.SfChart.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.Views.Reports.Charts
{
    public partial class SalesAchievedvsCountry
    {
        public SalesAchievedvsCountryViewModel viewModel { get; set; }
        public ObservableCollection<SalesTargetCountryCityAccountReportView> salesTargetCountryCityAccountReportViews { get; set; }
        public List<lstTotalTargetsCountry> listTotalTargets { get; set; }
        public List<lstBalanceTargetsCountry> listBalanceTargets { get; set; }
        public List<lstAchievedTargetsCountry> listAchievedTargets { get; set; }
        public List<Color> Colors
        {
            get;
            set;
        }

        public double device_height { get; set; }
        public double device_width { get; set; }

        public SalesAchievedvsCountry(ObservableCollection<SalesTargetCountryCityAccountReportView> _salesTargetCountryCityAccountReportViews)
        {
            InitializeComponent();
            BindingContext = viewModel = new SalesAchievedvsCountryViewModel(Navigation);
            salesTargetCountryCityAccountReportViews = _salesTargetCountryCityAccountReportViews;

            loadDates(DateTime.Today);
            DependencyService.Get<IOrientationService>().Landscape();

            listTotalTargets = new List<lstTotalTargetsCountry>();
            listBalanceTargets = new List<lstBalanceTargetsCountry>();
            listAchievedTargets = new List<lstAchievedTargetsCountry>();

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

        public ObservableCollection<DateItem> Dates { get; set; }
       
        private void loadDates(DateTime today)
        {
            try
            {
                var today1 = new DateTime(today.Year, today.Month, 1);
                today1 = today1.AddYears(-5);

                var Dates1 = new ObservableCollection<DateItem>();

                for (int i = today1.Year; i < today.Year; i++)
                {
                    today1 = today1.AddYears(1);
                    Dates1.Add(new DateItem()
                    {
                        dateTime = new DateTime(today1.Year, today1.Month, today1.Day),
                        dateTimeYear = today1.Year
                    });
                }
                Dates = Dates1;
                getSalesAchievedPlotCountry();
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        public PlotModel Model { get; set; }
        private void getSalesAchievedPlotCountry()
        {
            listTotalTargets = new List<lstTotalTargetsCountry>();
            listBalanceTargets = new List<lstBalanceTargetsCountry>();
            listAchievedTargets = new List<lstAchievedTargetsCountry>();
            if (Dates != null && salesTargetCountryCityAccountReportViews != null)
            {
                string CountryAccount = "";
                foreach (var item in salesTargetCountryCityAccountReportViews) 
                {
                    if (item != null)
                    {
                        if (item.Country != null && item.LocationAccountName != null)
                        {
                            CountryAccount = item.Country.ToString() + "-" + item.LocationAccountName;
                        }

                        listTotalTargets.Add(new lstTotalTargetsCountry(CountryAccount, Convert.ToDecimal((double)item.TotalTargets)));
                        listBalanceTargets.Add(new lstBalanceTargetsCountry(CountryAccount, Convert.ToDecimal((double)item.BalanceTargets)));
                        listAchievedTargets.Add(new lstAchievedTargetsCountry(CountryAccount, Convert.ToDecimal((double)item.AcheivedTargets)));

                    }
                }

                if (listAchievedTargets.Count == 1)
                {
                    colAchievedTargetsCountry.Spacing = 0.4;
                }
                else if (listAchievedTargets.Count >= 1 && listAchievedTargets.Count <= 4)
                {
                    colAchievedTargetsCountry.Spacing = 0.2;
                }
                else
                {
                    colAchievedTargetsCountry.Spacing = 0.01;
                }

                colAchievedTargetsCountry.ItemsSource = listAchievedTargets;
            }

        }

        private void getSalesAchievedMicroCountry()
        {
             var entries = new[] {new ChartEntry(0){ }};

            if (Dates != null && salesTargetCountryCityAccountReportViews != null)
            {
                var entries1 = new[] { new ChartEntry(0) { }};
                foreach (var item in Dates)
                {
                    var modelTargetCountry = salesTargetCountryCityAccountReportViews.Where(m=>m.TargetYear == item.dateTimeYear).FirstOrDefault();
                    if (modelTargetCountry != null)
                    {
                        entries1 = new[]
                        {
                            new ChartEntry(item.dateTimeYear)
                            {
                                Label = "Target",
                                ValueLabel = modelTargetCountry.TotalTargets.ToString(),
                                Color = SKColor.Parse("#2c3e50"),
                            },
                            new ChartEntry(item.dateTimeYear)
                            {
                                Label = "Achieved",
                                ValueLabel = modelTargetCountry.AcheivedTargets.ToString(),
                                Color = SKColor.Parse("#77d065")
                            },
                            new ChartEntry(item.dateTimeYear)
                            {
                                Label = "Balance",
                                ValueLabel = modelTargetCountry.BalanceTargets.ToString(),
                                Color = SKColor.Parse("#b455b6")
                            }
                        };
                    }
                }
                entries = entries1;
            }

            var chart = new BarChart() { Entries = entries };           
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
            DependencyService.Get<IOrientationService>().Portrait();

        }
    }
}


public class lstTotalTargetsCountry
{
    public string CountryAccNm { get; set; }

    public decimal? TotalTargetsValue { get; set; }

    public lstTotalTargetsCountry(string name, decimal? targetsvalue)
    {
        this.CountryAccNm = name;
        this.TotalTargetsValue = (decimal)targetsvalue;
    }
}

public class lstBalanceTargetsCountry
{
    public string CountryAccNm { get; set; }

    public decimal? BalanceTargetsValue { get; set; }

    public lstBalanceTargetsCountry(string name, decimal? balancevalue)
    {
        this.CountryAccNm = name;
        this.BalanceTargetsValue = (decimal)balancevalue;
    }
}

public class lstAchievedTargetsCountry
{
    public string CountryAccNm { get; set; }

    public decimal? AchievedTargetsValue { get; set; }

    public lstAchievedTargetsCountry(string name, decimal? achievedvalue)
    {
        this.CountryAccNm = name;
        this.AchievedTargetsValue = (decimal)achievedvalue;
    }
}