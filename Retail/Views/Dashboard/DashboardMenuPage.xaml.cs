using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Models;
using Retail.ViewModels.Dashboard;
using Xamarin.Forms;

namespace Retail.Views.Dashboard
{
    public partial class DashboardMenuPage : Shell
    {
        public DashboardMenuPageViewModel viewModel { get; set; }

        public DashboardMenuPage()
        {
            Device.SetFlags(new[] {
                "Brush_Experimental",
            });
            InitializeComponent();

            BindingContext = viewModel = new DashboardMenuPageViewModel(Navigation);

            if (CommonAttribute.CustomeProfile.ProfilePictureFileInfo != null)
            {
                if (Application.Current.Properties.ContainsKey("Photoaction") && Application.Current.Properties["Photoaction"] != null)
                {
                    if (Application.Current.Properties["Photoaction"].ToString() == "Take Photo")
                        imgUserPic.Rotation = 90;

                }

                imgUserPic.Source = CommonAttribute.ImageBaseUrl + CommonAttribute.CustomeProfile.ProfilePictureFileInfo.Path;
            }
            else
                imgUserPic.Source = "userdashbaord";

            Device.BeginInvokeOnMainThread(() => {
                LogoutDataModel.LogoutUser = viewModel.CheckValidUser();

                #region not valid user
                if (LogoutDataModel.LogoutUser > 0)
                {
                    LogoutDataModel.LogoutUser = (int)LogoutDataEnum.SqlLiteConnectionIssue;
                    viewModel.LogoutWithoutConfirmCommand.Execute(LogoutDataModel.LogoutUser);
                }
                #endregion
            });

            int menuCount = CurrentShell.Items.Count;
            for (int i = 0; i < menuCount; i++)
            {
                MenuSetup();
            }

        }

        public void MenuSetup()
        {
            foreach (ShellItem item in CurrentShell.Items.ToList())
            {

                if (item.Title == "Visits & Tasks") //for promoter visits and tasks
                {
                    if (!viewModel.IsVisitsandTasks)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }
                }

                if (item.Title == "Supervisor Visits") // for supervisor visits and tasks
                {
                    if (!viewModel.IsSupervisorVisits)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }
                }

                if (item.Title == "Sales Target")
                {
                    if (!viewModel.IsSalesTarget)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }

                }
                else if (item.Title == "Sales Entry")
                {
                    if (!viewModel.IsSalesEntry)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }

                }
                else if (item.Title == "Sales Return")
                {
                    if (!viewModel.IsSalesReturn)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }

                }
                else if (item.Title == "Out of Stock")//promoter- only "out of stock"
                {
                    if (!viewModel.IsOutOfStock)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }

                }
                else if (item.Title == "Stock Entry" )//supervisor-both (inventory stock and out of stock)
                {
                    if (!viewModel.IsInventory)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }

                }
                else if (item.Title == "Product Catalogue")
                {
                    if (!viewModel.IsProductCatalogue)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }

                }
              else if (item.Title == "Attendance")
                {
                    if (!viewModel.IsAttendance)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }

                }
                else if (item.Title == "Attendance Tracking")
                {
                    if (!viewModel.IsAttendanceTracker)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }

                }
                else if (item.Title == "Targets Overview")
                {
                    if (!viewModel.IsTargetsOverview)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }
                }
                else if (item.Title == "Reports")
                {
                    if (!viewModel.IsReports)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }
                }
                //else if(item.Title== "Supervisor Visits")
                //{
                //    if (!viewModel.IsVisitsandTasks)
                //    {
                //        CurrentShell.Items.Remove(item);
                //        break;
                //    }
                //}
                else if(item.Title== "Market Insights")
                {
                    if (!viewModel.IsMarketInsights)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }
                }
                else if (item.Title == "Price Tracker Entry")
                {
                    if (!viewModel.IsPriceTracker)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }
                }

                else if (item.Title == "Display Tracker Entry")
                {
                    if (!viewModel.IsDisplayTracker)
                    {
                        CurrentShell.Items.Remove(item);
                        break;
                    }
                }

                //List<int> lstRoleIds = CommonAttribute.CustomeProfile.PersonAssignedRoles.Select(x => x.PersonRoleId).ToList();

                //if ((!lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Manager)) && (!lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                //{
                //    if (item.Title == "Attendance Tracking")
                //    {
                //        CurrentShell.Items.Remove(item);
                //    }
                //    if (item.Title == "Targets Overview")
                //    {
                //        CurrentShell.Items.Remove(item);
                //    }
                //    if (item.Title == "Supervisor Visits")
                //    {
                //        CurrentShell.Items.Remove(item);
                //    }
                //    if (item.Title == "Stock Entry")
                //    {
                //        CurrentShell.Items.Remove(item);
                //    }
                //}
                //else if ((lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) || (lstRoleIds.Contains((int)PersonRoleEnum.Manager)) || (lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
                //{
                //    if (item.Title == "Sales Target")
                //    {
                //        CurrentShell.Items.Remove(item);
                //        break;
                //    }
                //    if (item.Title == "Sales Entry")
                //    {
                //        CurrentShell.Items.Remove(item);
                //        break;
                //    }
                //    if (item.Title == "Sales Return")
                //    {
                //        CurrentShell.Items.Remove(item);
                //        break;
                //    }
                //    if (item.Title == "Inventory Stock")
                //    {
                //        CurrentShell.Items.Remove(item);
                //    }
                //    ////did according to Akash sir                   
                //    //if (item.Title == "Inventory Stock")
                //    //{
                //    //    CurrentShell.Items.Remove(item);
                //    //    break;
                //    //}
                //    //if (item.Title == "Product Catalogue")
                //    //{
                //    //    CurrentShell.Items.Remove(item);
                //    //    break;
                //    //}

                //    if (item.Title == "Attendance")
                //    {
                //        CurrentShell.Items.Remove(item);
                //        break;
                //    }

                //    //if (item.Title == "Visits & Tasks")
                //    //{
                //    //    CurrentShell.Items.Remove(item);
                //    //    break;
                //    //}

                //    ////did according to Akash sir
                //    //if (item.Title == "Market Insights")
                //    //{
                //    //    CurrentShell.Items.Remove(item);
                //    //    break;
                //    //}
                //}
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void BtnMenu_Clicked(object sender, EventArgs e)
        {
            if (FlyoutIsPresented)
                FlyoutIsPresented = false;

            FlyoutIsPresented = true;
        }
        void OnNavigating(object sender, ShellNavigatingEventArgs e)
        {

        }

        void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {

        }
    }
}
