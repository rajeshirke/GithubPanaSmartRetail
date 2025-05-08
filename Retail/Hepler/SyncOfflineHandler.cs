using System;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.ViewModels.Attendance;
using Retail.ViewModels.InventoryStock;
using Retail.ViewModels.SalesTarget;
using Retail.ViewModels.SupervisorFlow.SupervisorWorkflow;
using Syncfusion.SfBusyIndicator.XForms;
using Xamarin.Forms;

namespace Retail.Hepler
{
    public interface ISyncOfflineHandler
    {
        void SyncOfflineDatas();
    }

    public class SyncOfflineHandler : ISyncOfflineHandler
    {
        SyncAttendanceDetails syncAttendanceDetails;
        SalesEntryStoreDetails salesEntryStoreDetails;
        RecentlyUsedModelNumberDetails recentlyUsedModelNumberDetails;
        OfflineDetails offlineDetails;
        OfflineSalesDataEntries offlineSalesDataEntries;
        OfflineInventoryEntries offlineInventoryEntries;
        SupervisorWorkflowDetails supervisorWorkflowDetails;
        SupervisorSubmitSyncing supervisorSubmitSyncing;

        public void SyncOfflineDatas()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    IsBusy = true;
                    
                    //busyIndicator.IsVisible = true;
                    //busyIndicator.IsBusy = true;                    
                    //await Task.Delay(5000);

                    // attendance syncing
                    syncAttendanceDetails = new SyncAttendanceDetails();
                    await syncAttendanceDetails.AttendanceSyncing();
                    //sales entry syncing
                    salesEntryStoreDetails = new SalesEntryStoreDetails();
                    await salesEntryStoreDetails.StoreSyncing();
                    await salesEntryStoreDetails.StoreDetailsSyncing();
                    // recently used models
                    recentlyUsedModelNumberDetails = new RecentlyUsedModelNumberDetails();
                    await recentlyUsedModelNumberDetails.RecentlyModelSyncing();
                    // offline details product cat, recently used, sub category, model numbers
                    offlineDetails = new OfflineDetails();
                    await offlineDetails.GetOfflineProductbyCategory();
                    await offlineDetails.GetOfflineRecentlyUsedModelNos();
                    await offlineDetails.GetOfflineSubcategories();
                    await offlineDetails.GetModelNumbersActiveByCountryIdOffline();
                    // offline sales data enry
                    offlineSalesDataEntries = new OfflineSalesDataEntries();
                    await offlineSalesDataEntries.GetOfflineSalesEntries();
                    // offline inventory entires
                    offlineInventoryEntries = new OfflineInventoryEntries();
                    await offlineInventoryEntries.GetOfflineInventoryEntries();
                    //supervisor workflow sync store location, location tasks
                    supervisorWorkflowDetails = new SupervisorWorkflowDetails();
                    await supervisorWorkflowDetails.StoreLocationSyncing();
                    await supervisorWorkflowDetails.GetVisitLocationTasksQAOfflineByPersonIdSyncing();
                    //supervisor final submit checklists
                    supervisorSubmitSyncing = new SupervisorSubmitSyncing();
                    await supervisorSubmitSyncing.SyncingFinalSubmitSupWorkflow();

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    IsBusy = false;
                    //busyIndicator.IsVisible = false;

                }
            });
        }

        SfBusyIndicator busyIndicator = new SfBusyIndicator()
        {
            AnimationType = AnimationTypes.DoubleCircle,
            ViewBoxHeight = 150,
            ViewBoxWidth = 150,
            TextColor = Color.Blue,
            EnableAnimation = true,
            //BackgroundColor = Color.White

        };

        public bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set
            {
                var hud = DependencyService.Get<IEWProgress>();
                if (value)
                {
                    hud.Show("Syncing in progress ...");
                }
                else
                {
                    hud.Dismiss();
                }
                _IsBusy = value;
            }
        }
    }
}
