using System;
using System.Collections.Generic;
using System.Linq;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.ResponseModels;
using Retail.Views.Reports;
using Xamarin.Forms;

namespace Retail.ViewModels.Reports
{
    public class ReportsViewModel : BaseViewModel
    {
        public ReportsViewModel(INavigation navigation) : base(navigation)
        {
            List<MobileSubMenuResponse> mobileSubMenuResponse = (List<MobileSubMenuResponse>)CommonAttribute.CustomeProfile.MobileSubMenus;
            if (mobileSubMenuResponse != null && mobileSubMenuResponse.Count > 0)
            {
                foreach(var ReportsSubMenu in mobileSubMenuResponse)
                {
                    switch(ReportsSubMenu.MobileSubMenuId)
                    {
                        case (int)ReportsEnum.Sales:
                            IsSalesReportVisible = true;
                            break;

                        case (int)ReportsEnum.PromoterwiseAchievement:
                            IsPromoterwiseAchievementReportVisible = true;
                            break;

                        case (int)ReportsEnum.DailySalesByCategory:
                            IsDailySalesByCategoryReportVisible = true;
                            break;

                        case (int)ReportsEnum.PromoterAttendance:
                            IsPromoterAttendanceReportVisible = true;
                            break;

                        case (int)ReportsEnum.VisitTargets:
                            IsVisitTargetsReportVisible = true;
                            break;

                        case (int)ReportsEnum.CategorywiseContribution:
                            IsCategorywiseContributionReportVisible = true;
                            break;
                    }
                }
            }
        }

        public Command SalesReportCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new SalesReports());
                });
            }
        }       

        public Command SalesTargetPromoterAchievementCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new SalesTargetPromoterAchievementView());
                });
            }
        }

        public Command DailySalesTrendBySubcategoryCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new DailySalesTrendBySubcategory());
                });
            }
        }

        public Command PromoterAttendanceCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new PromoterAttendanceReport());
                });
            }
        }

        public Command VisitTargetsCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new VisitTargetsReport());
                });
            }
        }

        public Command CategorywiseContributionCommand
        {
            get
            {
                return new Command(() =>
                {
                    Navigation.PushAsync(new CategorywiseContributionReport());
                });
            }
        }
    }
}
