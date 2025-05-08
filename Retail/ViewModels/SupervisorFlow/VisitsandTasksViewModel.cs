using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Retail.Database.SupervisorWorkflow;
using Retail.Enums;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Models.SupervisorWorkflow;
using Retail.ViewModels.SupervisorFlow.SupervisorWorkflow;
using Retail.Views.MyVisits;
using Retail.Views.SupervisorFlow;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.SupervisorFlow
{
    public class VisitsandTasksViewModel : BaseViewModel
    {
        public event EventHandler ToolBarITem;
        public Command SelectDateCommand { get; }
        public Command SwipeLeftCommand { get; }
        public Command SwipeRightCommand { get; }

        VisitScheduleDb supVisitScheduleDb;
        VisitScheduleLocationDb supVisitScheduleLocationStoreDb;
        VisitLocationTaskDb supVisitLocationTaskDb;
        SupLocationStoreDb supLocationStoreDb;
        FileInfoDb fileInfoDb;
        AnswerUploadedFileDb answerUploadedFileDb;
        TaskQuestionAnswerDb taskQuestionAnswerDb;
        AnswerOptionDb answerOptionDb;
        AnswerSelectedOptionDb answerSelectedOptionDb;
        bool SupWorkflowOffline = false;

        VisitsDataManagementSL visitsDataManagementSL;
        SupervisorSubmitSyncing supervisorSubmitSyncing;

        //QuestionMasterDb questionMasterDb;
        //TaskMasterDb taskMasterDb;
        //TaskMasterQuestionDb taskMasterQuestionDb;

        public VisitsandTasksViewModel(INavigation navigation) :base(navigation)
        {
            IsBusy = true;
            //SelectedDate = DateTime.Now.datet;
            loadDates(DateTime.Today);
            SelectDateCommand = new Command<DateItem>(async (model) => await ExecuteSelectDateCommand(model));
            SwipeLeftCommand = new Command<string>((model) => ExecuteSwipeLeftRightCommand(model));
            SwipeRightCommand = new Command<string>((model) => ExecuteSwipeLeftRightCommand(model));
            Device.BeginInvokeOnMainThread(async () => {
                await GetScheduledVisits();
                IsBusy = false;
            });

            supVisitScheduleDb = new VisitScheduleDb();
            supVisitScheduleLocationStoreDb = new VisitScheduleLocationDb();
            supVisitLocationTaskDb = new VisitLocationTaskDb();
            supLocationStoreDb = new SupLocationStoreDb();
            taskQuestionAnswerDb = new TaskQuestionAnswerDb();
            fileInfoDb = new FileInfoDb();
            answerUploadedFileDb = new AnswerUploadedFileDb();
            answerOptionDb = new AnswerOptionDb();
            answerSelectedOptionDb = new AnswerSelectedOptionDb();
            visitsDataManagementSL = new VisitsDataManagementSL();
            supervisorSubmitSyncing = new SupervisorSubmitSyncing();
        }

        private void ExecuteSwipeLeftRightCommand(string model)
        {
            var today = CurrentDate;
            var month = new DateTime(today.Year, today.Month, 1);

            if (model == "+1")
            {
                var first = month.AddMonths(1);
                loadDates(first);
            }
            else if (model == "-1")
            {
                var first = month.AddMonths(-1);
                loadDates(first);
            }
        }

        private async Task ExecuteSelectDateCommand(DateItem model)
        {
            var Dates1 = new ObservableCollection<DateItem>();
            Dates1 = Dates;

            Dates1.ToList().ForEach((item) =>
            {
                item.selected = false;
                item.backgroundColor = "#FFFFFF";
                item.textColor = "#000000";
            });

            var index = Dates1.ToList().FindIndex(p => p.day == model.day && p.dayWeek == model.dayWeek);
            if (index > -1)
            {
                Dates1[index].backgroundColor = "#1E55A5";
                Dates1[index].textColor = "#FFFFFF";
                Dates1[index].selected = true;
            }

            Dates = new ObservableCollection<DateItem>();
            Dates = Dates1;

            await GetScheduledVisits();          
        }

        public void loadDates(DateTime today)
        {
            try
            {
                CurrentDate = today;
                var Dates1 = new ObservableCollection<DateItem>();
                int thisYear = today.Year;
                int thisMonth = today.Month;
                var dateInit = new DateTime(thisYear, thisMonth, 1);
                var dateEnd = new DateTime(dateInit.Year, dateInit.Month, DateTime.DaysInMonth(dateInit.Year, dateInit.Month));
                Heading = string.Format("{0} {1}", dateInit.ToString("MMM").FirstLetterUpperCase(), dateInit.ToString("yy"));

                for (int i = 1; i <= dateEnd.Day; i++)
                {
                    string myDay = string.Format("{0:00}", i);
                    Dates1.Add(new DateItem()
                    {
                        day = myDay,
                        month = dateInit.ToString("MMM").FirstLetterUpperCase(),
                        dayWeek = new DateTime(dateInit.Year, dateInit.Month, i).DayOfWeek.ToString().Substring(0, 3),
                        selected = i == DateTime.Today.Day,
                        backgroundColor = i == DateTime.Today.Day ? "#1E55A5" : "#FFFFFF",
                        textColor = i == DateTime.Today.Day ? "#FFFFFF" : "#000000",
                        dateTime = new DateTime(thisYear, thisMonth, Convert.ToInt32(myDay))
                    });

                    if (Dates1[i - 1].dateTime == DateTime.Today)
                    {
                        SelectedDate = Dates1[i - 1];
                    }
                }
                Dates = Dates1;

            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }
        
        public async Task GetScheduledVisits()
        {
            try
            {
                IsBusy = true;

                //Removing previous Schedules
                //var test1 = supVisitScheduleDb.GetvisitSchedules();
                var test2 = supVisitScheduleLocationStoreDb.GetvisitScheduleLocations();
                //supVisitScheduleDb.DeletePreviousVisitScheduleStatusByNotInVisitScheduleStatusId((int) TaskStatusEnum.Closed);//submitted
                supVisitScheduleLocationStoreDb.DeletePreviousVisitScheduleStatusByNotIsNotEditable();

                VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>();
                string VisitDate = SelectedDate != null ? SelectedDate.dateTime.ToString("yyyy-MM-dd") : "";

                EventHandler handler = ToolBarITem;
                if (VisitDate == DateTime.Today.ToString("yyyy-MM-dd"))
                {
                    //ToolBarIcon = "noteadd.png";
                    BtnSubmitSupWorkflowOffline = true;
                    handler?.Invoke(this, new ToolBarEventArgs { IconImage = true });
                }
                else
                {
                    //ToolBarIcon = " ";
                    BtnSubmitSupWorkflowOffline = false;
                    handler?.Invoke(this, new ToolBarEventArgs { IconImage = false });
                }
                
                int PersonID = (int)CommonAttribute.CustomeProfile.PersonId;
                VisitsDataManagementSL visitsDataManagementSL = new VisitsDataManagementSL();
                List<VisitScheduleLocationResponse> visitScheduleLocationResponsesList = new List<VisitScheduleLocationResponse>();

                //if (NetworkAvailable)
                //{
                //    Application.Current.Properties["SupWorkflowOffline"] = true; //false . everytime true only. while selecting store only. it should check based on IsNotEditable

                //    VisitScheduleResponse visitSchedule = await visitsDataManagementSL.GetVisitScheduleByPersonIdDate(PersonID, VisitDate, (int)TaskTypeEnum.SupervisorTask);

                //    if (visitSchedule != null)
                //    {
                //        List<VisitScheduleLocationResponse> visitScheduleLocationResponse = visitSchedule.VisitScheduleLocations.ToList();
                //        visitScheduleLocationResponsesList.AddRange(visitScheduleLocationResponse);
                //    }
                //    else
                //    {
                //        Application.Current.Properties["SupWorkflowOffline"] = true;
                //    }
                //    IsBusy = false;
                //}
                //else
                //{
                    Application.Current.Properties["SupWorkflowOffline"] = true;
                //}

                #region SupWorkflowOffline check
                if (Application.Current.Properties.ContainsKey("SupWorkflowOffline"))
                {
                    SupWorkflowOffline = Convert.ToBoolean(Application.Current.Properties["SupWorkflowOffline"].ToString());
                    //BtnSubmitSupWorkflowOffline = SupWorkflowOffline;
                }
                #endregion

                //else
                if (SupWorkflowOffline)
                {
                    //working in offline mode
                    Application.Current.Properties["SupWorkflowOffline"] = true;

                    var VisitScheduleForTestinpurpose = supVisitScheduleDb.GetvisitSchedules().ToList();
                    //var test1 = supVisitScheduleDb.GetvisitSchedules();

                    //var visitSchedules = supVisitScheduleDb.GetvisitScheduleByVisitScheduleNotInStatusId((int)TaskStatusEnum.Submitted, VisitDate).ToList();
                    var visitSchedules = supVisitScheduleDb.GetvisitScheduleByVisitScheduleAllToday(VisitDate).ToList();

                    if (visitSchedules != null && visitSchedules.Count() > 0)
                    {
                        foreach (var itemSch in visitSchedules)
                        {
                            var locationStores = new List<VisitScheduleLocationResponse>();
                            var tblLocationStores = (List<TblVisitScheduleLocationResponse>)supVisitScheduleLocationStoreDb.GetvisitScheduleLocationByPVisitScheduleIdAndCreatedDate(itemSch.PVisitScheduleId, VisitDate).ToList();

                            if (tblLocationStores != null && tblLocationStores.Count() > 0)
                            {
                                int i = -1;
                                foreach (var itemi in tblLocationStores)
                                {
                                    i = i + 1;
                                    var visitLocationTasksSchedule = supVisitLocationTaskDb.GetvisitLocationTasksByPVisitScheduleLocationIdAndVisitDate(itemi.PVisitScheduleLocationId, VisitDate).ToList();
                                    var locationStoresSchedule = supLocationStoreDb.GetlocationStoresByLocationId(itemi.LocationId).ToList();

                                    int TaskStatusId = (int)TaskStatusEnum.Open;
                                    string TaskStatusName = "Open";
                                    int completedTasks = 0;

                                    if (visitLocationTasksSchedule == null || locationStoresSchedule == null)
                                        return;

                                    int j = -1;
                                    foreach (var itemj in visitLocationTasksSchedule)
                                    {
                                        j = j + 1;
                                        // if not matching then inprogress
                                        if (j > 0 && TaskStatusId != itemj.TaskStatusId)
                                        {
                                            TaskStatusId = (int)TaskStatusEnum.InProgress;
                                            TaskStatusName = "InProgress";
                                            break;
                                        }

                                        if (itemj.TaskStatusId == (int)TaskStatusEnum.Open)
                                        {
                                            TaskStatusId = (int)TaskStatusEnum.Open;
                                            TaskStatusName = "Open";
                                        }
                                        else if (itemj.TaskStatusId == (int)TaskStatusEnum.Closed)
                                        {
                                            TaskStatusId = (int)TaskStatusEnum.Closed;
                                            TaskStatusName = "Closed";
                                            completedTasks += completedTasks;
                                        }
                                        else if (itemj.TaskStatusId == (int)TaskStatusEnum.InProgress)
                                        {
                                            TaskStatusId = (int)TaskStatusEnum.InProgress;
                                            TaskStatusName = "InProgress";
                                        }
                                    }
                                    int visitLocationTaskCount = visitLocationTasksSchedule.Count();

                                    locationStores.Add(new VisitScheduleLocationResponse()
                                    {
                                        //VisitScheduleLocationId = itemi.VisitScheduleLocationId,
                                        VisitScheduleId = itemi.VisitScheduleId,
                                        LocationId = itemi.LocationId,
                                        VisitLocationTotalTaskCount = visitLocationTaskCount,//itemi.VisitLocationTotalTaskCount,
                                        VisitLocationTasksStatusId = TaskStatusId,//itemi.VisitLocationTasksStatusId,
                                        VisitLocationTasksStatusName = TaskStatusName,//itemi.VisitLocationTasksStatusName,
                                        VisitLocationTasksCountStatus = completedTasks + "/" + visitLocationTaskCount,//itemi.VisitLocationTasksCountStatus,
                                        StatusId = itemi.StatusId,
                                        OfflineScheduleDraftLocation = true,
                                        OfflineScheduleStatusTextLocation = itemSch.VisitScheduleStatusId != (int)TaskStatusEnum.Closed ? "." : ".",
                                        IsNotEditable = itemi.IsNotEditable,
                                        //Location = itemi.Location,
                                        //VisitLocationTasks = itemi.VisitLocationTasks,
                                    });

                                    if (locationStoresSchedule != null && locationStoresSchedule.Count > 0)
                                    {
                                        var locationStore = locationStoresSchedule[0];
                                        locationStores[i].Location = new LocationResponse()
                                        {
                                            LocationId = locationStore.LocationId,
                                            LocationCode = locationStore.LocationCode,
                                            LocationStoreName = locationStore.LocationStoreName,
                                            Area = locationStore.Area,
                                            Longitude = locationStore.Longitude,
                                            Latitude = locationStore.Latitude,
                                            City = locationStore.City,
                                            Distance = locationStore.Distance,
                                        };
                                    }

                                    if (visitLocationTasksSchedule != null && visitLocationTasksSchedule.Count > 0)
                                    {
                                        var VisitLocationTasks = new List<VisitLocationTaskResponse>();
                                        foreach (var itemk in visitLocationTasksSchedule)
                                        {
                                            VisitLocationTasks.Add(new VisitLocationTaskResponse()
                                            {
                                                TaskMasterId = itemk.TaskMasterId,
                                                TaskStatusId = itemk.TaskStatusId,
                                                TaskCompletionDate = itemk.TaskCompletionDate,
                                                AssignedToPersonId = itemk.AssignedToPersonId
                                            });
                                        }
                                        locationStores[i].VisitLocationTasks = VisitLocationTasks;
                                    }
                                }

                                //VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>(locationStores);
                                visitScheduleLocationResponsesList.AddRange(locationStores);

                            }

                        }
                    }
                }

                foreach (var item in visitScheduleLocationResponsesList)
                {
                    if (item.IsNotEditable)
                    {
                        item.StatusId = (int)TaskStatusEnum.Submitted;
                    }
                }

                //for current date the data is displayed
                if (VisitDate == DateTime.Today.ToString("yyyy-MM-dd"))
                {
                    VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>(visitScheduleLocationResponsesList);
                    if (VisitScheduleLocationResponses != null && VisitScheduleLocationResponses.Count > 0)
                    {
                        if (CommonAttribute.UserLocation != null)
                        {
                            Location sourceCoordinates = new Location(CommonAttribute.UserLocation.Latitude, CommonAttribute.UserLocation.Longitude);
                            foreach (var item in VisitScheduleLocationResponses)
                            {
                                Location destinationCoordinates = new Location(Convert.ToDouble(item.Location.Latitude), Convert.ToDouble(item.Location.Longitude));
                                double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                                item.Location.Distance = Math.Round(distance, 2);
                            }
                        }
                    }
                }
                else
                {
                    VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>();
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

        public Command StoreSelectCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //if (SupWorkflowOffline)
                        await PopupNavigation.Instance.PushAsync(new StoreSelection(VisitScheduleLocationResponses));
                    //else
                        //showToasterMessage("Final submit already done by you, you can't make any changes today");
                });
            }
        }
        
        public Command CountCommand
        {
            get
            {
                return new Command<VisitScheduleLocationResponse>((item) =>
                {
                    long VisitScheduleLocationId = item.VisitScheduleLocationId;
                    if (!item.OfflineScheduleDraftLocation)
                    {
                        Application.Current.Properties["SupWorkflowOffline"] = false;
                    }
                    else
                    {
                        Application.Current.Properties["SupWorkflowOffline"] = true;
                    }

                    #region SupWorkflowOffline check
                    if (Application.Current.Properties.ContainsKey("SupWorkflowOffline"))
                    {
                        SupWorkflowOffline = Convert.ToBoolean(Application.Current.Properties["SupWorkflowOffline"].ToString());
                        //BtnSubmitSupWorkflowOffline = SupWorkflowOffline;
                    }
                    #endregion

                    if (SupWorkflowOffline)
                    {
                        string VisitDate = SelectedDate != null ? SelectedDate.dateTime.ToString("yyyy-MM-dd") : "";
                        var visitScheduleLocations = supVisitScheduleLocationStoreDb.GetvisitScheduleLocationByLocationId(item.LocationId, VisitDate).ToList();
                        if (visitScheduleLocations != null && visitScheduleLocations.Count > 0)
                        {
                            VisitScheduleLocationId = visitScheduleLocations[0].PVisitScheduleLocationId;
                        }
                    }

                    if (item.Location != null)
                    {
                        Navigation.PushAsync(
                            new VisitTasksView(
                                item.Location.LocationStoreName,
                                item.Location.Area,
                                item.Location.Distance,
                                VisitScheduleLocationId,CheckList: true)
                            );
                    }
                });
            }
        }

        public async Task FinalSubmitSupWorkflowOffline()
        {
            //alert confirmation
            var res = false;
            res = await YesorNoDisplayAlert("Alert!", "After final submit you can't make any changes on the selected stores, Do you want to continue? ");
            if (!res)
                return;

            //update schedule status to completed by id
            var selectAllForTestingPurpose = supVisitScheduleDb.GetvisitSchedules().ToList();
            var visitSchedules = supVisitScheduleDb.GetvisitScheduleByVisitScheduleStatusId((int)TaskStatusEnum.Open).ToList();
            if (visitSchedules != null && visitSchedules.Count() > 0)
            {
                foreach (var itemSch in visitSchedules)
                {
                    // updated to visit schedule closed
                    supVisitScheduleDb.UpdateVisitScheduleStatusByPVisitScheduleId(itemSch.PVisitScheduleId, (int)TaskStatusEnum.Closed);
                    // update visit schedule location not editable
                    supVisitScheduleLocationStoreDb.UpdateVisitScheduleLocationEditableByPVisitScheduleId(itemSch.PVisitScheduleId);
                }

                // call syncing
                if (NetworkAvailable)
                {
                    IsBusy = true;
                    Device.BeginInvokeOnMainThread(async () => {
                        try
                        {
                            IsBusy = true;
                            await supervisorSubmitSyncing.SyncingFinalSubmitSupWorkflow(showAlert: true);
                        }
                        catch (Exception ex)
                        {
                            Debugger.Log(0, null, ex.ToString());
                        }
                        finally
                        {
                            IsBusy = false;
                        }
                    });
                }

                //alert message
                await DisplayAlert("Success", "Submitted successfully");
                await Shell.Current.GoToAsync($"//MainRoute/HomeRoute");
            }
            else
            {
                showToasterMessage("Nothing to submit");
            }
        }

        private ObservableCollection<VisitScheduleLocationResponse> _VisitScheduleLocationResponses;
        public ObservableCollection<VisitScheduleLocationResponse> VisitScheduleLocationResponses
        {
            get { return _VisitScheduleLocationResponses; }
            set
            {
                _VisitScheduleLocationResponses = value;
                OnPropertyChanged(nameof(VisitScheduleLocationResponses));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime _CurrentDate;
        public DateTime CurrentDate
        {
            set
            {
                if (value != null)
                {
                    _CurrentDate = value;
                    NotifyPropertyChanged(nameof(CurrentDate));
                }
            }
            get
            {
                return _CurrentDate;
            }
        }
        private DateItem _selectedDate;
        public DateItem SelectedDate
        {
            get => _selectedDate;
            set => SetProperty(ref _selectedDate, value);
        }

        private ObservableCollection<DateItem> _Dates;
        public ObservableCollection<DateItem> Dates
        {
            get => _Dates;
            set => SetProperty(ref _Dates, value);
        }


        private string _Heading;
        public string Heading
        {
            get => _Heading;
            set => SetProperty(ref _Heading, value);
        }

        private string _ToolBarIcon;
        public string ToolBarIcon
        {
            get => _ToolBarIcon;
            set => SetProperty(ref _ToolBarIcon, value);
        }

        private bool _BtnSubmitSupWorkflowOffline;
        public bool BtnSubmitSupWorkflowOffline
        {
            get => _BtnSubmitSupWorkflowOffline;
            set => SetProperty(ref _BtnSubmitSupWorkflowOffline, value);
        }
    }
}
