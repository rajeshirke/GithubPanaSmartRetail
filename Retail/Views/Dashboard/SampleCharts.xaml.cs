using System.Collections.Generic;
using Entry = Microcharts.ChartEntry;
using Xamarin.Forms;
using SkiaSharp;
using Microcharts;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Retail.Controls;
using System.Collections.ObjectModel;
using System;

namespace Retail.Views.Dashboard
{
    public partial class SampleCharts : ContentPage
    {
        List<Entry> Entries = new List<Entry>
        {
            new Entry(70)
            {
                Color=SKColor.Parse("#FF1493"),
                Label="May",
                ValueLabel="50"
            },
            new Entry(90)
            {
                Color=SKColor.Parse("#FF1493"),
                Label="JUNE",
                ValueLabel="100"
            },

            new Entry(110)
            {
                Color=SKColor.Parse("#FF1493"),
                Label="July",
                ValueLabel="150"
            }
        };
        public SampleCharts()
        {
            InitializeComponent();

            Chart1.Chart = new BarChart { Entries = Entries };

            
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var source = new ObservableCollection<string>() { "111", "222", "333" };

            pickerStack.Children.Add(new PickerView(source));
        }
    }
}
