using System;
using System.Collections.Generic;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.ViewModels.InventoryStock;
using Xamarin.Forms;

namespace Retail.Views.InventoryStock
{
    public partial class InventoryStockDetails : ContentPage
    {
        InventoryStockViewModel viewModel { get; set; }
        public InventoryStockDetails(int InventoryEntry)
        {
            InitializeComponent();
            BindingContext = viewModel=new InventoryStockViewModel(Navigation, InventoryEntry);
            
        }
       
    }
}
