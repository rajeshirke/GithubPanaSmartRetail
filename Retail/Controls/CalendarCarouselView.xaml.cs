using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Retail.Models;
using Xamarin.Forms;

namespace Retail.Controls
{
    public partial class CalendarCarouselView : ContentView
    {
        public ObservableCollection<DateItem> ItemsSource { get; set; }

        public static readonly BindableProperty ItemsSourceProperty =
           BindableProperty
           .Create(
               propertyName: "ItemsSource",
               returnType: typeof(ObservableCollection<DateItem>),
               declaringType: typeof(CalendarCarouselView),
               defaultValue: null,
               defaultBindingMode: BindingMode.OneWay,
               propertyChanged: ItemsSourcePropertyChangedAsync);

        public CalendarCarouselView()
        {
            InitializeComponent();
        }

        public static async void ItemsSourcePropertyChangedAsync(BindableObject bindable, object oldValue, object newValue)
        {
            var items = newValue as ObservableCollection<DateItem>;
            var control = (CalendarCarouselView)bindable;

            var index = items.ToList().FindIndex(p => p.selected);

            await Task.Delay(250);

            if (index > -1)
                control.listDates.ScrollTo(index, -1, ScrollToPosition.MakeVisible, true);
        }
    }
}
