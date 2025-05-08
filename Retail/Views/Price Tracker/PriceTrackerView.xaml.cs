using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OxyPlot;
using Retail.Hepler;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.PriceTracker;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.SfAutoComplete.XForms;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;

namespace Retail.Views.PriceTracker
{
    public partial class PriceTrackerView : ContentPage
    {
        public PriceTrackerViewModel viewModel { get; set; }
        public long ModelId { get; set; }
        public ProductModelForPriceDisplayResponse entityKeyValueResponse { get; set; }

        public PriceTrackerView()
        {
            InitializeComponent();
            BindingContext = viewModel = new PriceTrackerViewModel(Navigation);

            selectDate.Unfocused += SelectDate_Unfocused;

            //CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            //customCulture.NumberFormat.NumberDecimalSeparator = ".";
            //System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            //var mindate = DateTime.Now; //DateTime.Now.AddDays(-4);
            //selectDate.MinimumDate = mindate;

            if (CommonAttribute.PrvSelectedModelNo != null)
            {
                entityKeyValueResponse = CommonAttribute.PrvSelectedModelNo;
                modelnumberAutocomplete.Text = entityKeyValueResponse.Name;
                viewModel.ModelNumber = entityKeyValueResponse.Name;
                viewModel.ModelID = entityKeyValueResponse.Id;
            }
            if (!(String.IsNullOrEmpty(CommonAttribute.PrvSelectedDate)))
            {
                viewModel.SelectedDate = CommonAttribute.PrvSelectedDate;
                selectDate.SelectedDate = Convert.ToDateTime(CommonAttribute.PrvSelectedDate);
            }
            if (CommonAttribute.PrvSelectedStore != null)
            {
                viewModel.SelectedStore = CommonAttribute.PrvSelectedStore;
                viewModel.SelectedStoreText = viewModel.SelectedStore.Title.ToString();
                txtStore.Text = viewModel.SelectedStore.Title.ToString();
            }
            if (CommonAttribute.AllModelNumbers != null && CommonAttribute.AllModelNumbers.Count > 0)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel.GetModelNumbers();
                    CommonAttribute.AllModelNumbers = viewModel.obModelNumbers;
                    modelnumberAutocomplete.DataSource = CommonAttribute.AllModelNumbers;
                });
            }
        }

        

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            MessagingCenter.Subscribe<object, ProductModelForPriceDisplayResponse>(this, "SelectedModelNoPriceTracker", async (obj, entityKeyValueResponse) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel.GetModelNumbers();
                    CommonAttribute.AllModelNumbers = viewModel.obModelNumbers;
                    modelnumberAutocomplete.DataSource = CommonAttribute.AllModelNumbers;
                });

                if (entityKeyValueResponse != null)
                {
                    modelnumberAutocomplete.SelectedValue = "";
                   // modelnumberAutocomplete.SelectedItem = "";
                    modelnumberAutocomplete.Text = "";
                    CommonAttribute.PrvSelectedModelNo = new ProductModelForPriceDisplayResponse();
                    viewModel.ModelNumber = entityKeyValueResponse.Name;
                    viewModel.ModelID = entityKeyValueResponse.Id;
                    CommonAttribute.PrvSelectedModelNo = entityKeyValueResponse;
                    //modelnumberAutocomplete.SelectedItem = entityKeyValueResponse.Name;
                    modelnumberAutocomplete.Text = entityKeyValueResponse.Name.ToString();
                }

                //await viewModel.GetCompModelsForPriceDisplayTracker(viewModel.ModelID);
            });

            MessagingCenter.Subscribe<object, ProductModelForPriceDisplayResponse>(this, "SelectedModelNoPriceTrackerAndroidiOS", async (obj, entityKeyValueResponse) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await viewModel.GetModelNumbers();
                    CommonAttribute.AllModelNumbers = viewModel.obModelNumbers;
                    modelnumberAutocomplete.DataSource = CommonAttribute.AllModelNumbers;
                });

                if (entityKeyValueResponse != null)
                {
                    modelnumberAutocomplete.SelectedValue = "";
                    //modelnumberAutocomplete.SelectedItem = "";
                    modelnumberAutocomplete.Text = "";
                    CommonAttribute.PrvSelectedModelNo = new ProductModelForPriceDisplayResponse();
                    viewModel.ModelNumber = entityKeyValueResponse.Name;
                    viewModel.ModelID = entityKeyValueResponse.Id;
                    CommonAttribute.PrvSelectedModelNo = entityKeyValueResponse;
                    //modelnumberAutocomplete.SelectedItem = entityKeyValueResponse.Name;
                    modelnumberAutocomplete.Text = entityKeyValueResponse.Name.ToString();
                }

                //await viewModel.GetCompModelsForPriceDisplayTracker(viewModel.ModelID);
            });

            MessagingCenter.Subscribe<object, Models.DropDownModel>(this, "SelectedStore", async (obj, SelectedStore) =>
            {
                if(SelectedStore != null)
                {
                    viewModel.SelectedStoreText = SelectedStore.Title.ToString();
                    viewModel.SelectedStore = SelectedStore;
                    await viewModel.GetModelNumbers();
                    modelnumberAutocomplete.Text = "";
                    CommonAttribute.AllModelNumbers = new System.Collections.ObjectModel.ObservableCollection<ProductModelForPriceDisplayResponse>(viewModel.obModelNumbers);
                    viewModel.obModelNumbers = CommonAttribute.AllModelNumbers;
                    modelnumberAutocomplete.DataSource = CommonAttribute.AllModelNumbers;
                    txtStore.Text= SelectedStore.Title.ToString();
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Navigation.RemovePage(this);
        }

        protected override bool OnBackButtonPressed()
        {
            //Shell.Current.GoToAsync("../HomeRoute");

            Shell.Current.GoToAsync($"//MainRoute/HomeRoute");
            return true;
        }

        private void SelectDate_Unfocused(object sender, FocusEventArgs e)
        {
            var dp = sender as DatePicker;            
            viewModel.SelectedDate = dp.Date.ToString("dd,MMMM,yyyy");
            CommonAttribute.PrvSelectedDate = dp.Date.ToString("dd,MMMM,yyyy");
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            Device.InvokeOnMainThreadAsync(() =>
            {
                if (selectDate.IsFocused)
                    selectDate.Unfocus();

                selectDate.Focus();
                var dp = sender as DatePicker;
                CommonAttribute.PrvSelectedDate = dp.Date.ToString("dd,MMMM,yyyy");

            });
        }

        async void txtModelNo_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            await viewModel.ValidateModelNumber();
        }

        void modelnumberAutocomplete_SelectionChanged(System.Object sender, Syncfusion.SfAutoComplete.XForms.SelectionChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.ToString()))
            {
                entityKeyValueResponse = new ProductModelForPriceDisplayResponse();
                var SelectedModelNumber = modelnumberAutocomplete.SelectedItem;
                entityKeyValueResponse = (ProductModelForPriceDisplayResponse)modelnumberAutocomplete.SelectedItem;
                if (entityKeyValueResponse != null)
                {
                    viewModel.ModelID = entityKeyValueResponse.Id;
                    viewModel.ModelNumber = entityKeyValueResponse.Name;
                    CommonAttribute.PrvSelectedModelNo = entityKeyValueResponse;

                    //await viewModel.GetCompModelsForPriceDisplayTracker(viewModel.ModelID);
                }
                else
                {
                    viewModel.ObPriceTrackerResponse = new System.Collections.ObjectModel.ObservableCollection<PriceTrackerResponse>();
                    entityKeyValueResponse = new ProductModelForPriceDisplayResponse();
                    viewModel.ModelID = entityKeyValueResponse.Id;
                    viewModel.ModelNumber = entityKeyValueResponse.Name;
               }
            }
            else
            {

            }
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            IsBusy = true;
            await viewModel.SavePriceTrackerEntry();
            IsBusy = false;
        }

        async void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void txtStore_Focused(System.Object sender, Xamarin.Forms.FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new StoreSelectionPopupPriceTracker());
        }

        async void TapGestureRecognizer_Tapped_2(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushPopupAsync(new StoreSelectionPopupPriceTracker());
        }

        void selectDate_ItemTapped(System.Object sender, System.EventArgs e)
        {
            var dp = sender as DatePicker;
            viewModel.SelectedDate = dp.Date.ToString("dd,MMMM,yyyy");
            CommonAttribute.PrvSelectedDate = dp.Date.ToString("dd,MMMM,yyyy");
        }

        //void txtRRP_Completed(System.Object sender, System.EventArgs e)
        //{
        //    var tabs = PriceTrackerListView.GetTabIndexesOnParentPage(out int count);
        //    var visual = sender as Xamarin.Forms.VisualElement;
        //    var currentIndex = visual.TabIndex;
        //    var nextFocus = PriceTrackerListView.FindNextElement(true, tabs, ref currentIndex);

        //    (nextFocus as Xamarin.Forms.VisualElement)?.Focus();
        //}
        //void TapGestureRecognizer_Tapped_3(System.Object sender, System.EventArgs e)
        //{
        //    modelnumberAutocomplete.SelectedValue = "";
        //    modelnumberAutocomplete.SelectedItem = "";
        //    modelnumberAutocomplete.Text = "";
        //    entityKeyValueResponse = new ProductModelForPriceDisplayResponse();
        //    CommonAttribute.PrvSelectedModelNo = new ProductModelForPriceDisplayResponse();            
        //    viewModel.ObPriceTrackerResponse = new System.Collections.ObjectModel.ObservableCollection<PriceTrackerResponse>();
        //}

    }
}

