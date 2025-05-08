using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Views.InventoryStock;
using Xamarin.Forms;

namespace Retail.ViewModels.InventoryStock
{
    public class InventoryStockViewModel : BaseViewModel
    {
        public int StockEntry { get; set; }

        public InventoryStockViewModel(INavigation navigation, int InventoryEntry) : base(navigation)
        {
            StockEntry = InventoryEntry;
            listInventoryStockEntryResponse = new List<InventoryStockEntryResponse>();
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await GetLocation();

                //if (SelectedStore != null)
                //    await GetInventoryDetails(SelectedStore.Id);
                IsBusy = false;
            });
        }

        public async Task GetInventoryDetails(int LocationId)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    InventoryManagementSL inventoryManagementSL = new InventoryManagementSL();
                    listInventoryStockEntryResponse = await inventoryManagementSL.GetInventoryStockEntryByLocationPerson(LocationId);
                    if (listInventoryStockEntryResponse != null && listInventoryStockEntryResponse.Count > 0)
                    {
                        if (StockEntry == 1)
                        {
                            var StockEntries = listInventoryStockEntryResponse.Where(s => s.StockEntryTypeId == (int)StockEntryTypeEnum.InventoryStockEntry).ToList();
                            listInventoryStockEntryResponse = StockEntries;
                        }
                        else if (StockEntry == 2)
                        {
                            var OutOfStockEntries = listInventoryStockEntryResponse.Where(s => s.StockEntryTypeId == (int)StockEntryTypeEnum.OutOfStockEntry).ToList();
                            listInventoryStockEntryResponse = OutOfStockEntries;
                        }
                    }    
                }
                else
                {
                    //showToasterNoNetwork();
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task GetLocation()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    StoreDropDown = new ObservableCollection<DropDownModel>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    Locations = new List<EntityKeyValueResponse>();
                    Locations = await masterDataManagementSL.GetLocationsByPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                    if (Locations.Count != 0)
                    {
                        foreach (var item in Locations)
                        {
                            StoreDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                        }

                        var list = StoreDropDown.OrderBy(snm => snm.Title);
                        StoreDropDown = new ObservableCollection<DropDownModel>(list);
                    }
                    else
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            bool gpsStatus = DependencyService.Get<ILocSettings>().isGpsAvailable();
                            if (!gpsStatus)
                                await ErrorDisplayAlert("Your gps location is not accurate");
                        }
                    }
                }
                else
                {
                    //showToasterNoNetwork();
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }
        }

        public Command SelectStoreCommand
        {
            get
            {
                return new Command<DropDownModel>(async (obj) =>
                {
                    if (obj != null)
                        SelectedStore = obj;
                    else
                        SelectedStore.Id = 0;

                    int SelectedStoreID = 0;
                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    await GetInventoryDetails(SelectedStoreID);
                });
            }
        }


        public Command AddInventoryCommand
        {
            get
            {
                return new Command(async () =>
                {
                   await Navigation.PushAsync(new AddInventoryStock());
                });
            }
        }

        private List<EntityKeyValueResponse> _Locations;
        public List<EntityKeyValueResponse> Locations
        {
            get { return _Locations; }
            set
            {
                _Locations = value;
                OnPropertyChanged("Locations");
            }
        }

        private ObservableCollection<DropDownModel> _StoreDropDown;
        public ObservableCollection<DropDownModel> StoreDropDown
        {
            get { return _StoreDropDown; }
            set
            {
                _StoreDropDown = value;
                OnPropertyChanged("StoreDropDown");
            }
        }

        private DropDownModel _SelectedStore;
        public DropDownModel SelectedStore
        {
            get { return _SelectedStore; }
            set
            {
                _SelectedStore = value;
                OnPropertyChanged("SelectedStore");
            }
        }

        private List<InventoryStockEntryResponse> _listInventoryStockEntryResponse;
        public List<InventoryStockEntryResponse> listInventoryStockEntryResponse
        {
            get { return _listInventoryStockEntryResponse; }
            set
            {
                _listInventoryStockEntryResponse = value;
                OnPropertyChanged("listInventoryStockEntryResponse");
            }
        }

    }
}
