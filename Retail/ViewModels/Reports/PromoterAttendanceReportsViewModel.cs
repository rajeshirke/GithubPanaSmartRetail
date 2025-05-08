using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Resx;
using Retail.ViewModels.MonthYearPickerViewModel;
using Xamarin.Forms;

namespace Retail.ViewModels.Reports
{
    public class PromoterAttendanceReportsViewModel:BaseViewModel
    {
        public bool IsExpanded { get; set; }
        public PromoterAttendanceReportsViewModel(INavigation navigation) : base(navigation)
        {
            IsExpanded = true;
            SelectedDate = DateTime.Now.ToString("yyyy-MM-dd");
            obAttendanceView = new ObservableCollection<AttendanceView>();
        }

        public async Task GetPromoterAttendanceReport(ReportCreateUpdateRequest reportCreateUpdate)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    obAttendanceView = new ObservableCollection<AttendanceView>();
                    ReportsManagementSL reportsManagementSL = new ReportsManagementSL();
                    List<AttendanceView> attendanceView = await reportsManagementSL.GetPromoterAttendanceNew(reportCreateUpdate);
                    if (attendanceView != null && attendanceView.Count != 0)
                    {
                        var list = attendanceView.OrderBy(nm => nm.FirstName).ToList();
                        obAttendanceView = new ObservableCollection<AttendanceView>(list);
                    }
                    else
                    {
                        obAttendanceView = new ObservableCollection<AttendanceView>();
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

        public Command ShowReportsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //await GetPromoterAttendanceReport();
                });
            }
        }

        private ObservableCollection<AttendanceView> _obAttendanceView;
        public ObservableCollection<AttendanceView> obAttendanceView
        {
            get { return _obAttendanceView; }
            set
            {
                _obAttendanceView = value;
                OnPropertyChanged(nameof(obAttendanceView));
            }
        }

        private string _SelectedDate;
        public string SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
    }
}
