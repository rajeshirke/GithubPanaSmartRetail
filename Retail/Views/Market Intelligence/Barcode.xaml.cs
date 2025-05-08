using System;
using System.Collections.Generic;
using Retail.ViewModels.MarketIntelligence;
using Xamarin.Forms;

namespace Retail.Views.MarketIntelligence
{
    public partial class Barcode : ContentPage
    {
        public Barcode()
        {
            InitializeComponent();
            BindingContext = new MarketIntelligenceViewModel(Navigation);
        }
    }
}
