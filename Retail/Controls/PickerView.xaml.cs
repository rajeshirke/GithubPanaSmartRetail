using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Retail.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerView : ContentView
    {
        public ObservableCollection<string> pickerSource { get; set; }

        public PickerView(ObservableCollection<string> source)
        {
            InitializeComponent();

            picker.ItemsSource = source;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var stack = this.Parent as StackLayout;

            stack.Children.Remove(this);
        }
    }
}
