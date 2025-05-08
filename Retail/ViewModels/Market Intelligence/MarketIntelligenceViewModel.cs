using System;
 
using Retail.Views.MarketIntelligence;
using Retail.Views.ProductCatalogue;
using Xamarin.Forms;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using System.Threading.Tasks;
using Retail.Infrastructure.ServiceLayer;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.Models;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Diagnostics;
using Retail.DependencyServices;

namespace Retail.ViewModels.MarketIntelligence
{
    public class MarketIntelligenceViewModel : BaseViewModel
    {
        public MarketIntelligenceViewModel(INavigation navigation):base(navigation)
        {
            
        }

        public Command QRCodeCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var qrcode = await Retail.Hepler.Extensions.QrScanning();
                        if (qrcode != null)
                        {
                            SerialNo = qrcode;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }

        public Command MarketInsightsCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new MarketInsights());
                });
            }
        }

        public Command QuestionnaireCommand
        {
            get
            {
                return new Command(() =>
                {
                    NavigateCommandSubmit((int)MarketIntelTypeEnum.Questionnaire);
                });
            }
        }

        public Command ProductTestCommand
        {
            get
            {
                return new Command(() =>
                {
                    NavigateCommandSubmit((int)MarketIntelTypeEnum.ProductTest);
                });
            }
        }

        public Command SurveyCommand
        {
            get
            {
                return new Command(() =>
                {
                    NavigateCommandSubmit((int)MarketIntelTypeEnum.Survey);
                });
            }
        }

        private async void NavigateCommandSubmit(int MarketIntelTypeId)
        {
            await Navigation.PushAsync(new ActiveMarketIntelList(MarketIntelTypeId));
            //bool result = await CheckPersonHasAttendedAnyMarketIntel(MarketIntelTypeId);
            //if (result)
            //{
            //    if (marketIntel != null && marketIntel.MarketIntelId > 0)
            //    {
            //        await Navigation.PushAsync(new Questionnaire(marketIntel, MarketIntelTypeId));
            //    }
            //}
        }

        public Command ScanBarcodeCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new Barcode());
                });
            }
        }

        public Command ScanQRCodeCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new QRCode());
                });
            }
        }

        private string _SerialNo;
        public string SerialNo
        {
            get { return _SerialNo; }
            set
            {
                _SerialNo = value.ToUpper();

                OnPropertyChanged("SerialNo");
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

    }
}

