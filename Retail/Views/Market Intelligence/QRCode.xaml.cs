using System;
using System.Collections.Generic;
using Retail.ViewModels.MarketIntelligence;
using Xamarin.Forms;

namespace Retail.Views.MarketIntelligence
{
    public partial class QRCode : ContentPage
    {
        public QRCode()
        {
            InitializeComponent();
            BindingContext = new MarketIntelligenceViewModel(Navigation);
        }
    }
}
