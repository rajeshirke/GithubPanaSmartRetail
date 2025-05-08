using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Database.SupervisorWorkflow;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models.SupervisorWorkflow;
using Retail.ViewModels.SupervisorFlow.SupervisorWorkflow;
using Retail.Views.MyVisits;
using Retail.Views.SupervisorFlow;
using Xamarin.Forms;

namespace Retail.ViewModels.MyVisits
{
    public class VisitTasksViewModel : BaseViewModel
    {
        bool SupWorkflowOffline = false;
        public int _VisitLocationID;
        //queries
        public VisitLocationTaskDb supVisitLocationTaskDb;
        TaskMasterDb taskMasterDb;
        VisitScheduleDb supVisitScheduleDb;
        VisitScheduleLocationDb supVisitScheduleLocationStoreDb;
        SupervisorSubmitSyncing supervisorSubmitSyncing;


        public VisitTasksViewModel(INavigation navigation, string StoreName_, string StoreAddress_, double Distance_, long VisitLocationID_, bool CheckList_) : base(navigation)
        {
            StoreName = StoreName_; StoreAddress = StoreAddress_; Distance = Distance_; CheckList = CheckList_;
            _VisitLocationID = (int)VisitLocationID_;
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () => {
                await GetTaskDetails();
                IsBusy = false;
            });
            supVisitLocationTaskDb = new VisitLocationTaskDb();
            taskMasterDb = new TaskMasterDb();

            #region SupWorkflowOffline check
            if (Application.Current.Properties.ContainsKey("SupWorkflowOffline"))
            {
                SupWorkflowOffline = Convert.ToBoolean(Application.Current.Properties["SupWorkflowOffline"].ToString());
            }
            #endregion

            #region PageName based on Login User/ Role
            List<int> lstRoleIds = CommonAttribute.CustomeProfile?.PersonAssignedRoles?.Select(x => x.PersonRoleId).ToList();

            //if(lstRoleIds.Contains((int)PersonRoleEnum.Promoter))
            //{
            //    PageName = "Location Tasks";
            //}
            //else if ((lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) || (lstRoleIds.Contains((int)PersonRoleEnum.Manager)) || (lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
            //{
            //    PageName = "Supervisor Checklist";
            //}

            supVisitScheduleDb = new VisitScheduleDb();
            supVisitScheduleLocationStoreDb = new VisitScheduleLocationDb();
            supVisitLocationTaskDb = new VisitLocationTaskDb();
            supervisorSubmitSyncing = new SupervisorSubmitSyncing();

            if (CheckList)
            {
                PageName = "Supervisor Checklist";

                var IsSubmitted = supVisitScheduleLocationStoreDb.GetvisitScheduleLocationIsNotEditableByPVisitScheduleLocationId(_VisitLocationID);
                //var IsSubmitted = supVisitScheduleLocationStoreDb.GetvisitScheduleLocationByIsNotEditable();
                var test1 = supVisitScheduleLocationStoreDb.GetvisitScheduleLocations();

                if (IsSubmitted != null && IsSubmitted.ToList().Count > 0)
                {
                    BtnSubmitSupWorkflowOffline = true;
                }


            }
            else
            {
                PageName = "Location Tasks";
                BtnSubmitSupWorkflowOffline = false;
            }


            #endregion
        }

        public async Task GetTaskDetails()
        {
            try
            {
                IsBusy = true;
                List<VisitLocationTaskResponse> visitLocationTaskResponse = new List<VisitLocationTaskResponse>();

                if (NetworkAvailable && SupWorkflowOffline == false)
                {
                    VisitsDataManagementSL visitsDataManagementSL = new VisitsDataManagementSL();
                    visitLocationTaskResponse = await visitsDataManagementSL.GetVisitLocationTasksByVisitScheduleLocationId(_VisitLocationID);
                }
                else if (SupWorkflowOffline)
                {
                    // fetch data from task master
                    var visitLocationTasksSchedule = supVisitLocationTaskDb.GetvisitLocationTasksByPVisitScheduleLocationId(_VisitLocationID).ToList();

                    if (visitLocationTasksSchedule != null && visitLocationTasksSchedule.Count > 0)
                    {
                        int i = -1;
                        var VisitLocationTasks = new List<VisitLocationTaskResponse>();
                        foreach (var itemi in visitLocationTasksSchedule)
                        {
                            i = i + 1;
                            VisitLocationTasks.Add(new VisitLocationTaskResponse()
                            {
                                AssignedToPersonId = itemi.AssignedToPersonId,
                                TaskStatusId = itemi.TaskStatusId,
                                TaskMasterId = itemi.TaskMasterId,
                                TaskCompletionDate = itemi.TaskCompletionDate,
                                PVisitLocationTaskId = itemi.PVisitLocationTaskId
                            });

                            var taskMasters = taskMasterDb.GettaskMastersByTaskMasterId(itemi.TaskMasterId).ToList();
                            if (taskMasters != null && taskMasters.Count > 0)
                            {
                                var taskMaster = taskMasters[0];
                                var taskMasterResponse = new TaskMasterResponse()
                                {
                                    TaskMasterId = taskMaster.TaskMasterId,
                                    Title = taskMaster.Title,
                                    Description = taskMaster.Description,
                                    IsActive = taskMaster.IsActive,
                                    SerialNo = taskMaster.SerialNo,
                                };
                                VisitLocationTasks[i].TaskMaster = taskMasterResponse;
                            }
                        }

                        visitLocationTaskResponse = VisitLocationTasks;
                    }
                }
                else
                {
                    showToasterNoNetwork();
                }

                if (visitLocationTaskResponse != null)
                {
                    VisitLocationTaskResponses = new ObservableCollection<VisitLocationTaskResponse>(visitLocationTaskResponse);
                    SearchVisitLocationTaskResponses = new List<VisitLocationTaskResponse>(VisitLocationTaskResponses);
                    int SrNo = 0;
                    foreach (var item in VisitLocationTaskResponses)
                    {
                        SrNo = SrNo + 1;
                        item.TaskMaster.SerialNo = SrNo;
                    }

                    if (visitLocationTaskResponse.Count == 0)
                    {
                        //showToasterMessage("No data found");
                        IsBusy = false;
                        return;
                    }
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

        public Command SearchTaskCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    if (VisitLocationTaskResponses != null && VisitLocationTaskResponses.Count > 0)
                    {
                        var Result = SearchVisitLocationTaskResponses.Where(x => x.TaskMaster.Title.ToLower().Contains(item.ToString().ToLower())).ToList();
                        VisitLocationTaskResponses = new ObservableCollection<VisitLocationTaskResponse>(Result);
                    }
                    IsBusy = false;
                });
            }
        }

        public Command BackButtonCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    try
                    {
                        MessagingCenter.Send<string>("", "SupPromVisitBack");
                        await Navigation.PopAsync();
                    }
                    catch (Exception ex)
                    {
                        Debugger.Log(0, null, ex.ToString());
                    }
                    finally
                    {
                        //IsBusy = false;
                    }
                });
            }
        }

        public Command TaskItemCommand
        {
            get
            {
                return new Command<VisitLocationTaskResponse>((item) =>
                {
                    if (SupWorkflowOffline)
                    {
                        if (item.PVisitLocationTaskId > 0)
                        {
                            item.VisitLocationTaskId = item.PVisitLocationTaskId;
                        }
                    }
                    Navigation.PushAsync(new TaskSummaryView(item, StoreAddress, CheckList));
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

                    //update geocoordiantes 
                    await supVisitScheduleLocationStoreDb.updateVisitCheduleLocationGeoCoordinates(itemSch.PVisitScheduleId);
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

        private bool _BtnSubmitSupWorkflowOffline;
        public bool BtnSubmitSupWorkflowOffline
        {
            get => _BtnSubmitSupWorkflowOffline;
            set => SetProperty(ref _BtnSubmitSupWorkflowOffline, value);
        }

        private ObservableCollection<VisitLocationTaskResponse> _VisitLocationTaskResponses;
        public ObservableCollection<VisitLocationTaskResponse> VisitLocationTaskResponses
        {
            get { return _VisitLocationTaskResponses; }
            set
            {
                _VisitLocationTaskResponses = value;
                OnPropertyChanged(nameof(VisitLocationTaskResponses));
            }
        }

        private List<VisitLocationTaskResponse> _SearchVisitLocationTaskResponses;
        public List<VisitLocationTaskResponse> SearchVisitLocationTaskResponses
        {
            get { return _SearchVisitLocationTaskResponses; }
            set
            {
                _SearchVisitLocationTaskResponses = value;
                OnPropertyChanged("SearchVisitLocationTaskResponses");
            }
        }

        private string _StoreName;
        public string StoreName

        {
            get { return _StoreName; }
            set
            {
                _StoreName = value;
                OnPropertyChanged("StoreName");
            }
        }
        private string _StoreAddress;
        public string StoreAddress

        {
            get { return _StoreAddress; }
            set
            {
                _StoreAddress = value;
                OnPropertyChanged("StoreAddress");
            }
        }
        private double _Distance;
        public double Distance

        {
            get { return _Distance; }
            set
            {
                _Distance = value;
                OnPropertyChanged("Distance");
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

        private bool _CheckList;
        public bool CheckList

        {
            get { return _CheckList; }
            set
            {
                _CheckList = value;
                OnPropertyChanged("CheckList");
            }
        }
    }
}
