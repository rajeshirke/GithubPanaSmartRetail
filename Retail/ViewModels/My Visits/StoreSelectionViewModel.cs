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
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models.SupervisorWorkflow;
using Retail.Views.SupervisorFlow;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.MyVisits
{
    public class StoreSelectionViewModel : BaseViewModel
    {
        #region supervisor workflow
        bool SupWorkflowOffline = false;
        //queries
        VisitScheduleLocationDb visitScheduleLocationDb;
        VisitLocationTaskDb visitLocationTaskDb;
        TaskMasterDb taskMasterDb;
        SupLocationStoreDb supLocationStoreDb;
        TaskMasterStoreLinkAccountDb taskMasterStoreLinkAccountDb;
        VisitScheduleDb supVisitScheduleDb;
        VisitScheduleLocationDb supVisitScheduleLocationStoreDb;

        // tables
        public TblVisitScheduleResponse TmpScheduleModels;
        public TblVisitScheduleLocationResponse TmpScheduleLocationModels;
        public TblVisitLocationTaskResponse TmpVisitLocationModels;
        public TblTaskMasterResponse TmpTaskMasterModels;
        #endregion

        public StoreSelectionViewModel(INavigation navigation, ObservableCollection<VisitScheduleLocationResponse> locationResponses) :base(navigation)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () => {
                await GetLocation();
                IsBusy = false;
            });
            VisitScheduleLocationResponses = new ObservableCollection<VisitScheduleLocationResponse>();
            if (locationResponses != null && locationResponses.Count > 0)
            {
                VisitScheduleLocationResponses = locationResponses;
            }
            

            supVisitScheduleDb = new VisitScheduleDb();
            supLocationStoreDb = new SupLocationStoreDb();
            visitScheduleLocationDb = new VisitScheduleLocationDb();
            taskMasterStoreLinkAccountDb = new TaskMasterStoreLinkAccountDb();
            taskMasterDb = new TaskMasterDb();
            visitLocationTaskDb = new VisitLocationTaskDb();
            supVisitScheduleLocationStoreDb = new VisitScheduleLocationDb();

            #region SupWorkflowOffline check
            //if (Application.Current.Properties.ContainsKey("SupWorkflowOffline"))
            //{
            //    SupWorkflowOffline = Convert.ToBoolean(Application.Current.Properties["SupWorkflowOffline"].ToString());
            //}
            SupWorkflowOffline = true; // always work in offline.
            #endregion
        }

        public async Task GetLocation()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable
                      && SupWorkflowOffline == false)
                {
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    //List<LocationResponse> locations = await masterDataManagementSL.GetLocationsWithDetailsCountryId((int)CommonAttribute.CustomeProfile.PersonId);
                    List<LocationResponse> locations = await masterDataManagementSL.GetLocationsWithDetailsPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                    if (locations != null && locations.Count > 0)
                    {
                        var list = locations.OrderBy(s => s.LocationStoreName).ToList();
                        Locations = new ObservableCollection<LocationResponse>(list);
                    }
                }
                else if (SupWorkflowOffline)
                {
                    var tblLocationStores = (List<TblLocationResponse>)supLocationStoreDb.GetlocationStores().ToList();

                    if (tblLocationStores != null && tblLocationStores.Count() > 0)
                    {
                        var locationStores = new List<LocationResponse>();
                        foreach (var item in tblLocationStores)
                        {
                            locationStores.Add(new LocationResponse() {
                                LocationId = item.LocationId,
                                LocationCode = item.LocationCode,
                                LocationStoreName = item.LocationStoreName,
                                Area = item.Area,
                                Longitude = item.Longitude,
                                Latitude = item.Latitude,
                                City = item.City,
                                Distance = item.Distance
                            });
                        }

                        var list = locationStores.OrderBy(s => s.LocationStoreName).ToList();
                        Locations = new ObservableCollection<LocationResponse>(list);
                        //Locations = new ObservableCollection<LocationResponse>(locationStores);
                    }
                }
                else
                {
                    showToasterNoNetwork();
                }

                if (CommonAttribute.UserLocation != null && Locations != null)
                {
                    Location sourceCoordinates = new Location(CommonAttribute.UserLocation.Latitude, CommonAttribute.UserLocation.Longitude);
                    foreach (var item in Locations)
                    {
                        Location destinationCoordinates = new Location(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude));
                        double distance = Location.CalculateDistance(sourceCoordinates, destinationCoordinates, DistanceUnits.Kilometers);
                        item.Distance = Math.Round(distance, 2);
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

        public Command SearchLocationCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    Locations = new ObservableCollection<LocationResponse>
                    (Locations.Where(x => x.LocationStoreName.ToLower().Contains(item.ToString().ToLower())).ToList());
                    IsBusy = false;
                });
            }
        }

        public ICommand SelectStoreCommand
        {
            get
            {
                return new Command<LocationResponse>(async (item) =>
                {
                   try
                    {
                        IsBusy = true;

                        long PersonId = CommonAttribute.CustomeProfile.PersonId;
                        DateTime dateTime = DateTime.Now;
                        string dateTimeString = DateTime.Now.ToString("yyyy-MM-dd");

                        VisitScheduleCreateUpdateRequest visitScheduleCreateUpdateRequest = new VisitScheduleCreateUpdateRequest();
                        visitScheduleCreateUpdateRequest.PersonId = PersonId;
                        visitScheduleCreateUpdateRequest.CreationDate = dateTime;
                        visitScheduleCreateUpdateRequest.VisitScheduleStatusId = (int)VisitStatusEnum.Assigned;
                        visitScheduleCreateUpdateRequest.VisitScheduleDate = DateTime.Now;
                        List<VisitScheduleLocationRequest> VisitScheduleLocationRequestToSave = new List<VisitScheduleLocationRequest>();

                        VisitScheduleLocationRequest obj = new VisitScheduleLocationRequest();
                        obj.LocationId = item.LocationId;
                        VisitScheduleLocationRequestToSave.Add(obj);

                        visitScheduleCreateUpdateRequest.VisitScheduleLocations = VisitScheduleLocationRequestToSave;

                        if (NetworkAvailable
                            && SupWorkflowOffline == false)
                        {
                            VisitsDataManagementSL visitsDataManagementSL = new VisitsDataManagementSL();
                            var aPIResponse = await visitsDataManagementSL.CreateSupervisorVisitSchedule(visitScheduleCreateUpdateRequest);
                            if (aPIResponse?.Status == Convert.ToInt16(APIResponseEnum.Success))
                            {
                                MessagingCenter.Send<object, int>(this, "SelectedStore", item.LocationId);
                                await PopupNavigation.Instance.PopAsync();
                            }
                            else
                            {
                                await DisplayAlert("Alert!", "No tasks available for the user.");
                                await PopupNavigation.Instance.PopAsync();
                            }
                        }
                        else if (SupWorkflowOffline)
                        {
                            // insert into visit schedule location
                            int TaskStatusId = (int)TaskStatusEnum.Open;

                            //validate location already added
                            int LocationId = obj.LocationId;
                            string CreatedDate = dateTimeString;
                            //var TEstLocations = visitScheduleLocationDb.GetvisitScheduleLocations();

                            //for checking store is already exists or not in the database offline
                            var DBlocationExistsToday = visitScheduleLocationDb.ValidateLocationExistsToday(LocationId, CreatedDate).ToList();
                            if(DBlocationExistsToday !=null && DBlocationExistsToday.Count() >0)
                            {
                                await ErrorDisplayAlert("Store already added");
                                //showToasterMessage("Store already added");
                                return;
                            }
                            else
                            {
                                //for checking store is already exists or not in the current list online/offline
                                var locationExistsToday = VisitScheduleLocationResponses.Where(l => l.LocationId == LocationId).ToList();
                                if (locationExistsToday != null && locationExistsToday.Count() > 0)
                                {
                                    await ErrorDisplayAlert("Store already added");
                                    //showToasterMessage("Store already added");
                                    return;
                                }
                            }

                            //for checking store not submitted or not 
                            var IsSubmitted = supVisitScheduleLocationStoreDb.GetvisitScheduleLocationByIsNotEditable();
                            if (IsSubmitted != null && IsSubmitted.ToList().Count > 0)
                            {
                                await ErrorDisplayAlert("Unable to perform the action.Please submit the existing checklist.");
                                return;
                            }

                            //schedule
                            TmpScheduleModels = new TblVisitScheduleResponse()
                            {
                                PersonId = PersonId,
                                CreationDate = dateTimeString,
                                VisitScheduleDate = dateTimeString,
                                VisitScheduleStatusId = TaskStatusId
                            };
                            //var test1 = supVisitScheduleDb.GetvisitSchedules();
                            //add schedule
                            long PVisitScheduleId = supVisitScheduleDb.AddvisitScheduleQuery(TmpScheduleModels, (int)TaskStatusEnum.Open);
                            //var test2 = supVisitScheduleDb.GetvisitSchedules();

                            //schedule location
                            TmpScheduleLocationModels = new TblVisitScheduleLocationResponse()
                            {
                                PVisitScheduleId = PVisitScheduleId,
                                VisitScheduleId = 0,
                                LocationId = LocationId,
                                StatusId = TaskStatusId,
                                CreatedDate = CreatedDate
                            };
                            var currentLocation = supLocationStoreDb.GetlocationStoresByLocationId(LocationId).FirstOrDefault();

                            // fetch data from task master
                            //var taskMasters = taskMasterDb.GettaskMasters().ToList();
                            var taskMastersLink = taskMasterStoreLinkAccountDb.GettaskMasterStoreLinkAccountsByAccountId(currentLocation.AccountId);
                            if (taskMastersLink != null && taskMastersLink.Count() > 0)
                            {
                                //add schedule location
                                long PVisitScheduleLocationId = visitScheduleLocationDb.AddvisitScheduleLocationQuery(TmpScheduleLocationModels);

                                foreach (var itemTask in taskMastersLink)
                                {
                                    // insert into visit location tasks
                                    TmpVisitLocationModels = new TblVisitLocationTaskResponse()
                                    {
                                        TaskMasterId = itemTask.TaskMasterId,
                                        PVisitScheduleLocationId = PVisitScheduleLocationId,
                                        TaskStatusId = TaskStatusId,
                                        TaskCompletionDate = null,
                                        AssignedToPersonId = (int)CommonAttribute.CustomeProfile.PersonId,
                                        CreatedDate = DateTime.Today.ToString("yyyy-MM-dd")
                                    };

                                    visitLocationTaskDb.AddvisitLocationTask(TmpVisitLocationModels);
                                }
                            }
                            else
                            {
                                await DisplayAlert("Alert!", "No tasks available for the selected store.");
                                return;
                            }

                            if (Convert.ToInt16(APIResponseEnum.Success) == 1)
                            {
                                MessagingCenter.Send<object, int>(this, "SelectedStore", item.LocationId);
                                await PopupNavigation.Instance.PopAsync();
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
                });
            }
        }

        public ICommand BackCommand
        {

            get
            {
                return new Command(async() =>
                {
                    await PopupNavigation.Instance.PopAsync();
                });
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

        private ObservableCollection<LocationResponse> _Locations;
        public ObservableCollection<LocationResponse> Locations
        {
            get { return _Locations; }
            set
            {
                _Locations = value;
                OnPropertyChanged(nameof(Locations));
            }
        }
    }
}
