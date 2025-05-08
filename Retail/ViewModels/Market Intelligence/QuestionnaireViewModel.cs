using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Xamarin.Forms;

namespace Retail.ViewModels.MarketIntelligence
{
    public class QuestionnaireViewModel : BaseViewModel
    {
        public event EventHandler QuestionnaireViewItem;
        public Command SaveCommand { get; set; }
        public int PMarketIntelId { get; set; }
        public int PMarketIntelTypeId { get; set; }
        public int PMarketIntelCollectionId { get; set; }
        public MarketIntel marketIntel { get; set; }

        public QuestionnaireViewModel(INavigation navigation, MarketIntel _marketIntel, int MarketIntelTypeId) : base(navigation)
        {
            IsBusy = true;
            marketIntel = _marketIntel;
            Device.BeginInvokeOnMainThread(() =>
            {
                PMarketIntelId = marketIntel.MarketIntelId;
                PMarketIntelTypeId = MarketIntelTypeId;
                getCurrespondingTitle();
                IsBusy = false;
            });

            SaveCommand = new Command(() =>
            {
                //obMarketIntelQuestionAnswerToSubmitResponses = MarketIntelQuestionAnswerToSubmitResponses;
            });
        }

        private void TimerEnablingForQuestions()
        {
            int SecondsElapsed = 0;
            int MaxSeconds = Convert.ToInt32(marketIntel.Timer);
            MaxSeconds = MaxSeconds * 60;
            TimeSpan timeSpan;
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (SecondsElapsed < MaxSeconds)
                {
                    SecondsElapsed++;
                    timeSpan = new TimeSpan(0, 0, ((MaxSeconds - SecondsElapsed)));
                    TaskQuestionListTimer = timeSpan.Minutes.ToString() + ":" + timeSpan.Seconds.ToString();
                    GC.SuppressFinalize(this);
                    return true;
                }
                EventHandler handler = QuestionnaireViewItem;
                handler?.Invoke(this, null);
                return false;
            });
        }

        private void getCurrespondingTitle()
        {
            Title = "";
        }

        public void SubmittedAnswers()
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () => {
                //  IsBusy = true;
                await SaveSubmittedAnswers();
                IsBusy = false;
            });
        }

        public async Task SaveSubmittedAnswers()
        {
            try
            {
                //IsBusy = true;

                if (NetworkAvailable)
                {
                    MarketIntelDataManagementSL visitsDataManagementSL = new MarketIntelDataManagementSL();
                    var TaskQuestionAnswerList = new List<MarketIntelQuestionAnswerToSubmitResponse>(MarketIntelQuestionAnswerToSubmitResponses);
                    var response = (Retail.Infrastructure.RequestModels.APIResponse)await visitsDataManagementSL.SaveMarketIntelAnswers(TaskQuestionAnswerList);
                    if (response.Status == (int)APIResponseEnum.Success)
                    {
                        await DisplayAlert("Success", "Saved successfully");
                        //await Navigation.PopAsync();
                    }
                    else
                    {
                        await ErrorDisplayAlert("Error while saving data");
                    }

                    return;
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
                //IsBusy = false;
            }
        }

        public void CreateUpdateMarketIntelStartEnd(int type)
        {
            var marketIntelCollectionCreateUpdateRequest = new MarketIntelCollectionCreateUpdateRequest();
            marketIntelCollectionCreateUpdateRequest.MarketIntelCollectionId = PMarketIntelCollectionId;
            marketIntelCollectionCreateUpdateRequest.MarketIntelId = PMarketIntelId;
            marketIntelCollectionCreateUpdateRequest.AttendedByPersonId = CommonAttribute.CustomeProfile.PersonId;
            marketIntelCollectionCreateUpdateRequest.AttendedDate = DateTime.Today;

            switch (type)
            {
                case 1: // start
                    marketIntelCollectionCreateUpdateRequest.StatusId = (int)TaskStatusEnum.InProgress;
                    marketIntelCollectionCreateUpdateRequest.StartTime = DateTime.Now;
                    marketIntelCollectionCreateUpdateRequest.TimerType = type;
                    IsBusy = true;
                    Device.BeginInvokeOnMainThread(async () => {
                        //  IsBusy = true;
                        await CreateUpdateMarketIntelCollection(marketIntelCollectionCreateUpdateRequest);
                        IsBusy = false;
                    });
                    break;
                case 2: // finish
                    marketIntelCollectionCreateUpdateRequest.StatusId = (int)TaskStatusEnum.Closed;
                    marketIntelCollectionCreateUpdateRequest.EndTime = DateTime.Now;
                    marketIntelCollectionCreateUpdateRequest.TimerType = type;
                    IsBusy = true;
                    Device.BeginInvokeOnMainThread(async () => {
                        //  IsBusy = true;
                        await CreateUpdateMarketIntelCollection(marketIntelCollectionCreateUpdateRequest);
                        await Navigation.PopAsync();
                        IsBusy = false;
                    });
                    break;
            }
        }

        public async Task CreateUpdateMarketIntelCollection(MarketIntelCollectionCreateUpdateRequest MarketIntelCollectionCreateUpdateRequests)
        {
            try
            {
                //IsBusy = true;

                if (NetworkAvailable)
                {
                    //MarketIntelCollectionCreateUpdateRequest.Add(taskQuestionAnswerToSubmitResponse);
                    MarketIntelDataManagementSL visitsDataManagementSL = new MarketIntelDataManagementSL();
                    var response = (Retail.Infrastructure.RequestModels.APIResponse)await visitsDataManagementSL.CreateUpdateMarketIntelCollection(MarketIntelCollectionCreateUpdateRequests);
                    if (response.Status == (int)APIResponseEnum.Success)
                    {
                        if (response.Data != null)
                        {
                            int _Value = Convert.ToInt32(response.Data.ToString());
                            PMarketIntelCollectionId = _Value;

                            // get questions
                            await GetTaskSummaryDetails(PMarketIntelId, PMarketIntelCollectionId);

                            #region timer start and stop
                            if (PMarketIntelTypeId != (int)MarketIntelTypeEnum.Questionnaire && PMarketIntelTypeId != (int)MarketIntelTypeEnum.Survey)
                            {
                                if (MarketIntelCollectionCreateUpdateRequests.TimerType == 1)
                                {
                                    // start timer
                                    TimerEnablingForQuestions();
                                }
                                else if (MarketIntelCollectionCreateUpdateRequests.TimerType == 2)
                                {
                                    // stop timer

                                }
                            }
                            #endregion
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
                //IsBusy = false;
            }
        }

        public async Task GetTaskSummaryDetails(int _MarketIntelId, int _MarketIntelCollectionId)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    int MarketIntelId = (int)_MarketIntelId; //1;
                    int MarketIntelCollectionId = (int)_MarketIntelCollectionId;
                    MarketIntelDataManagementSL visitsDataManagementSL = new MarketIntelDataManagementSL();
                    List<MarketIntelQuestionAnswerToSubmitResponse> questionAnswerResponse = await visitsDataManagementSL.GetMarketIntelCollectionQuestionAnswer(MarketIntelId, MarketIntelCollectionId);
                    if (questionAnswerResponse != null)
                    {
                        MarketIntelQuestionAnswerToSubmitResponses = new ObservableCollection<MarketIntelQuestionAnswerToSubmitResponse>(questionAnswerResponse);

                        if (MarketIntelQuestionAnswerToSubmitResponses.Count == 0)
                        {
                            //await ErrorDisplayAlert("No data found");
                            showToasterMessage("No data found");
                            IsBusy = false;
                            return;
                        }
                    }

                    IsBusy = false;
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


        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PopAsync();
                });

            }
        }

        private ObservableCollection<MarketIntelQuestionAnswerToSubmitResponse> _MarketIntelQuestionAnswerToSubmitResponses;
        public ObservableCollection<MarketIntelQuestionAnswerToSubmitResponse> MarketIntelQuestionAnswerToSubmitResponses
        {
            get { return _MarketIntelQuestionAnswerToSubmitResponses; }
            set
            {
                _MarketIntelQuestionAnswerToSubmitResponses = value;
                OnPropertyChanged(nameof(MarketIntelQuestionAnswerToSubmitResponses));
                OnPropertyChanged(nameof(TaskCount));
                OnPropertyChanged(nameof(TaskQuestionListVisible));
                //timer
            }
        }

        private string _Answer;
        public string Answer

        {
            get { return _Answer; }
            set
            {
                _Answer = value;
                OnPropertyChanged("Answer");
            }
        }

        private Color _TaskStatusColor;
        public Color TaskStatusColor

        {
            get { return _TaskStatusColor; }
            set
            {
                _TaskStatusColor = value;
                OnPropertyChanged(nameof(TaskStatusColor));
            }
        }

        public string _TaskQuestionListTimer;
        public string TaskQuestionListTimer
        {
            get { return _TaskQuestionListTimer; }
            set
            {
                _TaskQuestionListTimer = value;
                OnPropertyChanged(nameof(TaskQuestionListTimer));
            }
        }

        public bool _TaskQuestionListVisible;
        public bool TaskQuestionListVisible
        {
            get
            {
                if (MarketIntelQuestionAnswerToSubmitResponses != null
                    && MarketIntelQuestionAnswerToSubmitResponses.Count > 0)
                {
                    _TaskQuestionListVisible = true;
                }
                return _TaskQuestionListVisible;
            }
        }

        public string _TaskCount;
        public string TaskCount
        {
            get
            {
                if (MarketIntelQuestionAnswerToSubmitResponses != null
                    && MarketIntelQuestionAnswerToSubmitResponses.Count > 0)
                {
                    string _AnsweredQuest = MarketIntelQuestionAnswerToSubmitResponses.Where(m => m.StatusId == (int)AnswerStatusEnum.Answered).ToList().Count().ToString();
                    _TaskCount = string.Format("{0}/{1}", _AnsweredQuest, MarketIntelQuestionAnswerToSubmitResponses.Count().ToString());
                }
                return _TaskCount;
            }
            set
            {
                _TaskCount = value;
                OnPropertyChanged(nameof(TaskCount));
            }
        }

        private string _Title;
        public string Title
        {
            get {
                if(PMarketIntelTypeId == (int)MarketIntelTypeEnum.Questionnaire)
                    _Title = "Questionnaire";
                if (PMarketIntelTypeId == (int)MarketIntelTypeEnum.ProductTest)
                    _Title = "Product Test";
                if (PMarketIntelTypeId == (int)MarketIntelTypeEnum.Survey)
                    _Title = "Survey";
                return _Title;
            }
            set
            {
                OnPropertyChanged(nameof(Title));
            }
        }
    }
}
