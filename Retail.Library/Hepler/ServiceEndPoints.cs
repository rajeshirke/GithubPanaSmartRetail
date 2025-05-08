using System;
namespace Retail.Infrastructure.Hepler
{
    public static class ServiceEndPoints
    {
        #region panasonic_production
        //public static Uri StagingServerUri => new Uri("https://smartretail.mea.panasonic.com/RetailAPI/api/");        
        //public static string WebAppUri { get { return "https://smartretail.mea.panasonic.com/"; } }

        #endregion

        #region Panasonic NEW dev
        public static Uri StagingServerUri => new Uri("http://devsrv01.panasonic.ae:91/api/");
        public static string WebAppUri { get { return "http://devsrv01.panasonic.ae:92/"; } }
        #endregion

        public static Uri ServerBaseUri { get { return StagingServerUri; } } // taking from Retail.CommonAttribute.ImageBaseUrl

        #region Account
        public static string Login => "Login";
        public static string ConcurrentLogin => "Login";
        public static string Register => "Login/Register";
        public static string UserToken => "UserToken/VerifyEmailMobileToken";
        public static string SavePersonProfilePicture => "People/SavePersonProfilePicture/";
        //api
        public static string VerifyEmailMobileTokenForgetPassword => "UserToken/VerifyEmailMobileTokenForgetPassword";

        public static string ChangePassword => "Login/ChangePassword";
        public static string ForgotPassword => "Login/ForgotPassword";
        public static string UpdatePersonDetails => "People/UpdatePersonDetails/";
        public static string ResendOTP => "Login/ResendOTP?UserID=";
        public static string ForgotPasswordOTP => "Login/ForgotPasswordOTP";
        public static string GetWarrantyCard => "Products/GetWarrantyCard/";
        ///api/Login/ForgotPasswordOTP
        ///GetWarrantyCard
        #endregion
        #region Notification
        public static string GetUnreadNotifications => "Notifications/GetUnreadNotifications/";
        public static string UpdateNotificationReadDate => "Notifications/UpdateNotificationReadDate/{0}/{1}";
        #endregion
        #region SalesTarget
        public static string GetSalesTargetWithSummaryByPersonId => "SalesTargets/GetSalesTargetWithSummaryByPersonId/{0}";
        public static string GetSalesEntryByPersonIdSubCategoryId => "SalesTargets/GetSalesEntryByPersonIdSubCategoryId/{0}/{1}/{2}/{3}"; //0=PersonId 1=ProductSubCategoryId 2=FromDate 3=ToDate

        #endregion
        #region SalesEntry/ReturnEntry
        public static string SaveSalesDataEntries => "SalesTargets/SaveSalesDataEntries";
        public static string GetRecentTargetModelEntriesByPerson => "SalesTargets/GetRecentTargetModelEntriesByPerson?PersonId={0}";
        public static string BuildSalesEntryReuqestWithModelDetails => "SalesTargets/BuildSalesEntryReuqestWithModelDetails";
        public static string SaveSalesDataReturnEntries => "SalesTargets/SaveSalesDataReturnEntries";
        public static string GetSalesEntryTransactionsByDate =>"SalesTargets/GetSalesEntryTransactionsByDate/{0}/{1}/{2}"; //0=Date,1=Entrystatusid,2=PersonId
        public static string GetOnlyProductModelNumbersActiveByCategoryId => "Products/GetOnlyProductModelNumbersActiveByCategoryId/{0}/{1}/{2}"; //0=CountryId,1=CategoryId
        public static string ValidateModelByModelNumberCountry => "Products/ValidateModelByModelNumberCountry/{0}/{1}"; //0=CountryId,1=ModelNumber
        #endregion

        #region Visits
        public static string GetVisitScheduleByPersonIdDate = "Visits/GetVisitScheduleByPersonIdDate/{0}?Date={1}&ScheduleType={2}"; //0=personid,1=date

        //testing
        public static string GetVisitScheduleByPersonIdDates = "Visits/GetVisitScheduleByPersonIdDate/{0}?Date={1}&ScheduleType={2}&Version=1"; //0=personid,1=date

        //new API for multiple visits at same time
        public static string GetAllVisitScheduleByPersonIdDate = "Visits/GetAllVisitScheduleByPersonIdDate/{0}?Date={1}&ScheduleType={2}&Version=1";


        public static string GetLocationTaskQuestionAnswer = "Visits/GetLocationTaskQuestionAnswer/{0}/{1}"; //TaskMasterId(0),VisitLocationTaskID(1)        
        public static string GetVisitLocationTasksByVisitScheduleLocationId = "Visits/GetVisitLocationTasksByVisitScheduleLocationId/{0}";
        public static string GetTaskQuestionAnswerByTaskMasterID = "Visits/GetTaskQuestionAnswerByTaskMasterID/{0}/{1}";
        public static string SaveTaskActionAnswers = "Visits/SaveTaskActionAnswers";
        public static string CreateSupervisorVisitSchedule => "Visits/CreateSupervisorVisitSchedule";
        public static string GetVisitLocationTask => "Visits/GetVisitLocationTask/{0}"; //0=visitLocationTaskId
        // offline
        public static string GetVisitLocationTasksQAOfflineByPersonId = "Visits/GetVisitLocationTasksQAOfflineByPersonId/{0}"; //0=personid
        public static string SaveTaskActionAnswersOffline = "Visits/SaveTaskActionAnswersOffline";
        #endregion

        #region TargetOverview       
        public static string GetSupervisorTargetsOverview = "SalesTargets/GetSupervisorTargetsOverview";
        public static string GetPromoterTargets = "SalesTargets/GetPromoterTargets";
        #endregion

        #region MasterData

        //Get Countries
        public static string GetMultipleCountryIdByPersonId = "MasterData/GetMultipleCountryIdByPersonId/{0}"; //0=Person ID

        //Get Countries
        public static string GetMultipleCountryId = "MasterData/GetMultipleCountryId"; //0=CountryId

        //Cities
        public static string GetCitiesByCountryId = "MasterData/GetCitiesByCountryId/{0}";

        //cities by multiple country ids
        public static string GetCitiesByMultipleCountryId = "MasterData/GetCitiesByMultipleCountryId/0";

        //accounts by multiple cities
        public static string GetAccountsByMultipleCountryId = "MasterData/GetAccountsByMultipleCountryId/{0}";

        //Locations by multiple country account
        public static string GetLocationByAccountsMultipleCountryId = "MasterData/GetLocationByAccountsMultipleCountryId/{0}";

        //Locations
        public static string GetLocationsByCityIds = "MasterData/GetLocationsByCityIds/{0}";

        //Location by personid
        public static string GetLocationsByPersonId => "MasterData/GetLocationsByPersonId/{0}";

        //Location by personid and selected country ID        
        public static string GetLocationsByPersonIdCountryIds => "MasterData/GetLocationsByPersonIdCountryIds/{0}";

        //location details by countryid
        public static string GetLocationDetailsByPersonId => "MasterData/GetLocationDetailsByPersonId/{0}"; //PersonId

        //Promoters by multiple locations
        public static string GetPromotersByMultiLocation => "MasterData/GetPromotersByMultiLocation";

        //master category
        public static string GetProductMasterCategories => "MasterData/GetProductMasterCategories";

        //All Subcategories for Offline functionality
        public static string GetAllSubcategoriesforOffline => "MasterData/GetAllSubcategoriesforOffline";

        //All ModelNumbers for Offline Functionality
        public static string GetModelNumbersActiveByCountryIdOffline => "Products/GetModelNumbersActiveByCountryIdOffline/{0}"; //CountryId

        //subcategories
        public static string GetProductSubCategoriesByCategoryId => "MasterData/GetProductSubCategoriesByCategoryId/{0}"; //0=CategoryId

        //ProductModelsByCategoryId
        public static string GetProductModelsByCategoryId => "Products/GetProductModelsByCategoryId/{0}/{1}"; //0=CountryId,1=CategoryId

        //locationbycountryid
        public static string GetLocationsByCountryId => "MasterData/GetLocationsByCountryId/{0}"; //0=CountryId

        //GetLocationsWithDetailsCountryId
        public static string GetLocationsWithDetailsCountryId => "MasterData/GetLocationsWithDetailsCountryId/{0}"; //0=CountryId

        //Store list based on Login PersonId
        public static string GetLocationsWithDetailsPersonId => "MasterData/GetLocationsWithDetailsPersonId/{0}"; //0=PersonID

        //Locations by account id
        public static string GetLocationsByAccountIDActive => "MasterData​/GetLocationsByAccountIDActive​/{0}"; //0=AccountId

        //Get Accounts
        public static string GetAccountsByCountryId = "MasterData/GetAccountsByCountryId/{0}";

        //get  Supervisors
        public static string GetSupervisorsByAccountId = "MasterData/GetSupervisorsByAccountId/{0}"; //AccountId

        ////get  Supervisors based on country and account
        public static string GetSupervisorsListByCountryAccountIdMultiple = "Reports/GetSupervisorsListByCountryAccountIdMultiple"; 


        //search model numbers by initials
        public static string SearchModelNumberByInitials => "Products/SearchModelNumberByInitials?ModelNumberinitials={0}&CountryId={1}";
        #endregion


        #region AttendanceTracker
        //DailyAttendance
        public static string GetDailyAttendanceByLocationDate => "Attendances/GetDailyAttendanceByLocationDate/{0}/{1}"; //0=locationid,1=date
        //Checkin
        public static string AttendanceCheckIn => "Attendances/CreateAttendance";
        //checkout
        public static string AttendanceCheckOut => "Attendances/UpdateAttendance/{0}"; //0=checkinID
        //AttendanceHistory
        public static string GetAttendanceDetailsByDateLocationPersonID  => "Attendances/GetAttendanceDetailsByDateLocationPersonID/{0}/{1}/{2}?CountryId={3}"; //0=LocationID,1=PersonID,3=date,4=CountryId
        //Monthly Attendance
        public static string GetPersonMonthlyAttendance => "Attendances/GetPersonMonthlyAttendance/{0}/{1}/{2}"; //0=PersonID,1=Month,2=Year

        //new
        public static string AttendanceCreateUpdateAttendance => "Attendances/CreateUpdateAttendance";

        //new updated
        public static string GetDailyAttendanceByLocationIdsDate => "Attendances/GetDailyAttendanceByLocationIdsDate";
        #endregion


        #region Product Catalogue
        public static string GetProductCataloguesByCountry => "Products/GetProductCataloguesByCountry/{0}"; //0=CountryId
        #endregion

        #region Inventory Stock
        public static string GetInventoryStockEntryByLocationPerson => "Inventory/GetInventoryStockEntryByLocationPerson/{0}"; //0=LocationID
        public static string BuildStockEntryWithModelDetails => "Inventory/BuildStockEntryWithModelDetails";
        public static string CreateInventoryStockEntry => "Inventory/CreateInventoryStockEntry";
        #endregion

        #region MarketInsights
        public static string SaveMarketInsight => "MarketIntel/SaveMarketInsight";
        #endregion

        #region Questionnaire, Product Test, Survey
        public static string CheckPersonHasAttendedAnyMarketIntel => "MarketIntel/CheckPersonHasAttendedAnyMarketIntel/{0}/{1}/{2}";
        public static string GetMarketIntelCollectionQuestionAnswer => "MarketIntel/GetMarketIntelCollectionQuestionAnswer/{0}/{1}";
        public static string SaveMarketIntelAnswers => "MarketIntel/SaveMarketIntelAnswers";
        public static string CreateUpdateMarketIntelCollection => "MarketIntel/CreateUpdateMarketIntelCollection";
        public static string CheckQuestionnaireForAlert => "MarketIntel/CheckQuestionnaireForAlert/{0}/{1}";
        public static string MarketIntelAllActiveDatas => "MarketIntel/MarketIntelAllActiveDatas/{0}/{1}/{2}";//0=CountryId,1=PersonId,2=MarketIntelTypeId
        public static string CheckPersonHasAttendedAnyMarketIntelByMarketIntelId => "MarketIntel/CheckPersonHasAttendedAnyMarketIntelByMarketIntelId/{0}/{1}/{2}";
        #endregion

        #region Reports
        //public static string GetSalesAchievedvsCountry => "Reports/GetSalesAchievedvsCountry/{0}/{1}?AccountId={2}&CityId={3}"; //0=CountryId,1=FromDate,2=CityId,3=AccountId
        //public static string GetSalesAchievedvsProductCategory => "Reports/GetSalesAchievedvsProductCategory/{0}/{1}?AccountId={2}&CityId={3}"; //0=CountryId,1=FromDate,2=CityId,3=AccountId
        //public static string GetSalesTargetPromoterAchievementReport => "Reports/GetSalesTargetPromoterAchievementReport/{0}?LocationId={1}"; //0=AccountId,1=LocationId,2=PersonId
        //public static string DailySalesTrendBySubcategory => "Reports/GetDailySalesTrendBySubcategory/{0}/{1}"; //0=AccountId,1=StartDate
        //public static string GetPromoterAttendance => "Reports/GetPromoterAttendance/{0}"; //0=Date
        //public static string GetVisitChecklistSupervisorReport => "Reports/GetVisitChecklistSupervisorReport/{0}/{1}"; //0=AccountId,1=SupervisorId
        //public static string GetDailyVisitTargetTasksReport = "Reports/GetDailyVisitTargetTasksReport/{0}/{1}"; //0=AccountId,SupervisorId=1
        //public static string GetCategoryWiseContribution => "Reports/GetCategoryWiseContribution/{0}"; //0=personid

        public static string GetSalesAchievedvsCountry => "Reports/GetSalesAchievedvsCountry"; //0=CountryId,1=FromDate,2=CityId,3=AccountId
        public static string GetSalesAchievedvsProductCategory => "Reports/GetSalesAchievedvsProductCategory"; //0=CountryId,1=FromDate,2=CityId,3=AccountId
        public static string GetSalesTargetPromoterAchievementReportByCountryAccountLocationMultiple => "Reports/GetSalesTargetPromoterAchievementReportByCountryAccountLocationMultiple"; //0=AccountId,1=LocationId,2=PersonId
        public static string GetDailySalesTrendBySubcategoryAll => "Reports/GetDailySalesTrendBySubcategoryAll"; //0=AccountId,1=StartDate
        public static string GetPromoterAttendanceNew => "Reports/GetPromoterAttendanceNew";
        public static string GetVisitChecklistSupervisorReport => "Reports/GetVisitChecklistSupervisorReport/{0}/{1}"; //0=AccountId,1=SupervisorId
        public static string GetDailyVisitTargetTasksReportAll = "Reports/GetDailyVisitTargetTasksReportAll"; //0=AccountId,SupervisorId=1
        public static string GetCategoryWiseContribution => "Reports/GetCategoryWiseContribution/{0}?Date={1}"; //0=personid,1=SelectedDte
        #endregion

        #region PriceDisplayTracker
        public static string GetCategoryForPriceDisplayTracker = "PriceDisplayTracker/GetCategoryForPriceDisplayTracker/{0}/{1}"; //0=PersonId
        public static string GetSubCategoryForPriceDisplayTracker = "PriceDisplayTracker/GetSubCategoryForPriceDisplayTracker/{0}/{1}/{2}"; //0=PersonId,1=CategoryId        
        public static string GetProductModelForPriceDisplayTracker = "PriceDisplayTracker/GetProductModelForPriceDisplayTracker/{0}/{1}/{2}/{3}/{4}"; //0=PersonId,1=CountryId,2=CategoryId,3=SubCategoryId
        public static string GetCompetitorProductModelByModelIdForPriceDisplayTracker = "PriceDisplayTracker/GetCompetitorProductModelByModelIdForPriceDisplayTracker"; //0=ModelId, 1=PersonID
        public static string PriceTrackerEntry = "PriceDisplayTracker/PriceTrackerEntry";
        public static string GetStoreForPriceDisplayTracker = "PriceDisplayTracker/GetStoreForPriceDisplayTracker/{0}/{1}"; //0=PersonId
        public static string SearchModelNumberByInitialForPriceDisplayTracker = "PriceDisplayTracker/SearchModelNumberByInitialForPriceDisplayTracker/{0}/{1}/{2}"; //0=PersonId,1=CountryId
        public static string GetProductModelForDisplayTracker = "DisplayTracker/GetProductModelForDisplayTracker"; //0=PersonId,1=CountryId,2=CategoryId,3=SubCategoryId
        public static string SaveDisplayTrackerEntry = "DisplayTracker/SaveDisplayTrackerEntry";
        public static string GetStoreForDisplayTracker = "DisplayTracker/GetStoreForDisplayTracker/{0}/{1}";//0=PersonId
        public static string GetCategoryForDisplayTracker = "DisplayTracker/GetCategoryForDisplayTracker/{0}/{1}/{2}"; //0=PersonId
        public static string GetSubCategoryForDisplayTracker = "DisplayTracker/GetSubCategoryForDisplayTracker/{0}/{1}/{2}/{3}"; //0=PersonId , 1=CategoryId
        #endregion
    }

}

