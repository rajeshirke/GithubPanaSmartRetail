using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Hepler;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.ServiceLayer;
using Retail.Views.Reports.Charts;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Retail.ViewModels.Reports
{
    public class CategorywiseContributionViewModel : BaseViewModel
    {
        public bool IsExpanded { get; set; }

        public CategorywiseContributionViewModel(INavigation navigation) : base(navigation)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                IsExpanded = true;
                CurrentDate = DateTime.Now.ToString("yyyy-MM-dd");
                IsBusy = false;
            });
        }

        public async Task GetCategoryWiseContribution(string SelectedDate)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    if (CommonAttribute.CustomeProfile?.PersonId!=null)
                    {   
                        ReportsManagementSL reportsManagementSL = new ReportsManagementSL();
                        List<SalesEntryReportView> lstSalesEntryReportView = await reportsManagementSL.GetCategoryWiseContribution((long)CommonAttribute.CustomeProfile?.PersonId,SelectedDate);
                        if (lstSalesEntryReportView != null && lstSalesEntryReportView.Count != 0)
                        {
                            obSalesEntryReportView = new ObservableCollection<SalesEntryReportView>(lstSalesEntryReportView.OrderByDescending(d => d.EntryDate).ToList());
                        }
                        else
                        {
                            obSalesEntryReportView = new ObservableCollection<SalesEntryReportView>();
                            //await DisplayAlert("Info", "No Records Found");
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

        public Command ChartClick
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new CategorywiseContributionChart(obSalesEntryReportView));
                    
                });
            }
        }


        private ObservableCollection<SalesEntryReportView> _obSalesEntryReportView;
        public ObservableCollection<SalesEntryReportView> obSalesEntryReportView
        {
            get { return _obSalesEntryReportView; }
            set
            {
                _obSalesEntryReportView = value;
                OnPropertyChanged(nameof(obSalesEntryReportView));
            }
        }

        private string _CurrentDate;
        public string CurrentDate
        {
            get { return _CurrentDate; }
            set
            {
                _CurrentDate = value;
                OnPropertyChanged(nameof(CurrentDate));
            }
        }
    }
}

