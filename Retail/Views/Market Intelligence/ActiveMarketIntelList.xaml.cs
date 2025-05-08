using System;
using System.Collections.Generic;
using Retail.ViewModels.MarketIntelligence;
using Xamarin.Forms;

namespace Retail.Views.MarketIntelligence
{
    public partial class ActiveMarketIntelList : ContentPage
    {
        public ActiveMarketIntelListViewModel viewModel { get; set; }


        public ActiveMarketIntelList(int MarketIntelTypeId)
        {
            InitializeComponent();
            BindingContext = viewModel = new ActiveMarketIntelListViewModel(Navigation, MarketIntelTypeId);
        }
    }
}
