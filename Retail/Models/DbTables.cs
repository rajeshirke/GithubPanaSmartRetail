using System;
using SQLite;

namespace Retail.Models
{
    #region Sales entry
    public class TmpSalesDataEntry
    {
        [PrimaryKey, AutoIncrement]
        public long? SalesTargetEntryId { get; set; }

        public long? ProductModelId { get; set; }
        public string ProductModelNumber { get; set; }
        public long EntryByPersonId { get; set; }
        public DateTime EntryDate { get; set; }
        public int LocationId { get; set; }
        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int? CurrencyId { get; set; }
        public int TargetInOutStatusId { get; set; }
        public int? CountryId { get; set; } //new 24Aug21 prj
        public int? SalesEntrySubmittedStatus { get; set; }
    }

    public class TblRecentlyUsedModel
    {
        //[PrimaryKey, AutoIncrement]
        //public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TblProductCategories
    {
        //[PrimaryKey, AutoIncrement]
        //public int CategoryID { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TblSubcategories
    {
        //[PrimaryKey, AutoIncrement]
        //public int SubcategoryID { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
    }

    public class TblAllModelNumbers
    {
        //[PrimaryKey,AutoIncrement]
        //public int ID { get; set; }
        public long ProductModelId { get; set; }
        public string ProductModelNumber { get; set; }
        public int CountryId { get; set; }
        public int ProductCategoryId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }

    #endregion

    #region Attendance
    public class TblPromoterAttendance
    {
        [PrimaryKey, AutoIncrement]
        public int? ID { get; set; }
       
        public long AttendanceId { get; set; }
        public long PersonId { get; set; }
        public string AttendanceDate { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckoutDate { get; set; }
        public int? LocationId { get; set; }
        public decimal? PersonLocationLongitude { get; set; }
        public decimal? PersonLocationLatitude { get; set; }
        public bool? IsOffDay { get; set; }
        public decimal? ProximityRange { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        
    }
    #endregion

    #region Inventory stock
    public class TblInventoryStockEntryRequest
    {
        [PrimaryKey, AutoIncrement]
        public int? InventoryStockEntryId { get; set; }
        [Indexed]
        public long? ProductModelId { get; set; }
        public string ProductModelNumber { get; set; }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public string ProductSubCategoryName { get; set; }

        public int LocationId { get; set; }
        public DateTime EntryDate { get; set; }
        public long EntryByPersonId { get; set; }
        public string Comments { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? Quantity { get; set; }
        public int? StockEntryTypeId { get; set; }
        public int? CountryId { get; set; } //new 24Aug21 prj
        public int InventoryEntrySubmittedStatus { get; set; }
    }
    #endregion

    public class TblLocationStore
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class TblPromoterLocationResponse
    {
        public int LocationId { get; set; }
        public string LocationCode { get; set; }
        public string LocationStoreName { get; set; }
        public string Area { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string City { get; set; }
        public double Distance { get; set; }
    }
}
