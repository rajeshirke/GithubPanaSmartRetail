using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Views.MarketIntelligence;
using Xamarin.Forms;

namespace Retail.ViewModels.MarketIntelligence
{
    public class ActiveMarketIntelListViewModel : BaseViewModel
    {
        public int SelectedMarketIntelTypeId { get; set; }

        public ActiveMarketIntelListViewModel(INavigation navigation,int MarketIntelTypeId) : base(navigation)
        {
            SelectedMarketIntelTypeId = MarketIntelTypeId;
            if (SelectedMarketIntelTypeId==(int)MarketIntelTypeEnum.ProductTest)
            {
                PageName = "Active Product Tests";
            }
            else if(SelectedMarketIntelTypeId == (int)MarketIntelTypeEnum.Survey)
            {
                PageName = "Active Surveys";
            }
            else if (SelectedMarketIntelTypeId == (int)MarketIntelTypeEnum.Questionnaire)
            {
                PageName = "Active Questionnaires";
            }

            IsBusy = true;

            Device.BeginInvokeOnMainThread(async () =>
            {
                IsBusy = false;
                await ActiveMarketIntelList();
            });
        }

        async Task ActiveMarketIntelList()
        {

            try
            {
                //IsBusy = true;
                if (NetworkAvailable)
                {
                    int CountryId = (int)CommonAttribute.CustomeProfile?.CountryId; //1;
                    int PersonId = (int)CommonAttribute.CustomeProfile?.PersonId; //1;
                    int MarketIntelTypeId = (int)SelectedMarketIntelTypeId; //1;
                    obMarketIntel = new ObservableCollection<MarketIntel>();

                    MarketIntelDataManagementSL marketIntelDataManagementSL = new MarketIntelDataManagementSL();
                    List<MarketIntel> marketIntels = await marketIntelDataManagementSL.MarketIntelAllActiveDatas(CountryId, PersonId, MarketIntelTypeId);
                    if (marketIntels != null && marketIntels.Count > 0)
                    {
                        obMarketIntel = new ObservableCollection<MarketIntel>(marketIntels);
                    }
                }
                else
                {
                    showToasterNoNetwork();
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

        public ICommand ActiveMarketIntelCommand
        {
            get
            {
                return new Command<MarketIntel>((item) =>
                {
                    NavigateCommandSubmit(item);
                });
            }
        }

        private async void NavigateCommandSubmit(MarketIntel marketIntel)
        {
            bool result = await CheckPersonHasAttendedAnyMarketIntel(marketIntel.MarketIntelId);
            if (result)
            {
                if (marketIntel != null && marketIntel.MarketIntelId > 0)
                {
                    await Navigation.PushAsync(new Questionnaire(marketIntel, marketIntel.MarketIntelTypeId));
                }
            }
        }

        public async Task<bool> CheckPersonHasAttendedAnyMarketIntel(int _MarketIntelTypeId)
        {
            bool result = false;

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    int CountryId = (int)CommonAttribute.CustomeProfile.CountryId; //1;
                    int PersonId = (int)CommonAttribute.CustomeProfile.PersonId; //1;
                    int MarketIntelTypeId = (int)_MarketIntelTypeId; //1;
                    MarketIntelDataManagementSL visitsDataManagementSL = new MarketIntelDataManagementSL();
                    APIResponse aPIResponse = await visitsDataManagementSL.CheckPersonHasAttendedAnyMarketIntelByMarketIntelId(CountryId, PersonId, MarketIntelTypeId);
                    if (aPIResponse != null)
                    {
                        if (aPIResponse.Status == (int)APIResponseEnum.Success)
                        {
                            result = true;
                            if (aPIResponse.Data != null)
                            {
                                marketIntel = new MarketIntel();
                                var converter = new ExpandoObjectConverter();
                                dynamic data1 = JsonConvert.DeserializeObject<ExpandoObject>(aPIResponse.Data.ToString(), converter);
                                foreach (KeyValuePair<string, object> kvp in data1)
                                {
                                    Console.WriteLine(kvp.Key + ": " + kvp.Value);
                                    if (kvp.Key == "marketIntelId")
                                    {
                                        if (kvp.Value != null)
                                        {
                                            int _MarketIntelId = Convert.ToInt32(kvp.Value.ToString());
                                            marketIntel.MarketIntelId = _MarketIntelId;
                                        }
                                    }
                                    if (kvp.Key == "timer")
                                    {
                                        if (kvp.Value != null)
                                        {
                                            int _Timer = Convert.ToInt32(kvp.Value.ToString());
                                            marketIntel.Timer = _Timer;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            await ErrorDisplayAlert(!string.IsNullOrEmpty(aPIResponse.ErrorMessage.ToString()) ? aPIResponse.ErrorMessage.ToString() : aPIResponse.Message.ToString());
                        }
                    }
                }
                else
                {
                    showToasterNoNetwork();
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

            return result;
        }

        public MarketIntel marketIntel { get; set; }

        private ObservableCollection<MarketIntel> _obMarketIntel;
        public ObservableCollection<MarketIntel> obMarketIntel

        {
            get { return _obMarketIntel; }
            set
            {
                _obMarketIntel = value;
                OnPropertyChanged(nameof(obMarketIntel));
            }
        }


        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set
            {
                _PageName = value;

                OnPropertyChanged(nameof(PageName));
            }
        }
    }
}

