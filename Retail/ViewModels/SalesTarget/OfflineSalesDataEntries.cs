using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Xamarin.Forms;

namespace Retail.ViewModels.SalesTarget
{
    public class OfflineSalesDataEntries
    {
        Connection c;
        public List<SalesEntryRequest> SaveSalesEntries { get; set; }
        public OfflineSalesDataEntries()
        {
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            c.conn.CreateTable<TmpSalesDataEntry>();
        }

        public async Task<bool> GetOfflineSalesEntries()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                SaveSalesEntries = new List<SalesEntryRequest>();
                var OfflineSalesEntries = c.conn.Table<TmpSalesDataEntry>().Where(f => f.SalesEntrySubmittedStatus == 1).ToList();
                if (OfflineSalesEntries != null && OfflineSalesEntries.Count != 0)
                {
                    foreach (var item in OfflineSalesEntries)
                    {
                        SaveSalesEntries.Add(new SalesEntryRequest
                        {
                            CountryId = item.CountryId,
                            CurrencyId = item.CurrencyId,
                            ProductModelId = item.ProductModelId,
                            ProductModelNumber = item.ProductModelNumber,
                            EntryDate = item.EntryDate,
                            Quantity = item.Quantity,
                            UnitPrice = item.UnitPrice,
                            TotalPrice = item.TotalPrice,
                            EntryByPersonId = item.EntryByPersonId,
                            ProductCategoryId = item.ProductCategoryId,
                            ProductSubCategoryId = item.ProductSubCategoryId,
                            ProductCategoryName = item.ProductCategoryName,
                            ProductSubCategoryName = item.ProductSubCategoryName,
                            LocationId = item.LocationId,
                            TargetInOutStatusId = item.TargetInOutStatusId,
                        });
                    }

                    if (SaveSalesEntries != null && SaveSalesEntries.Count != 0)
                    {
                        SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                        var receiveContext = await salesTargetManagementSL.SaveSalesDataEntries(SaveSalesEntries);
                        if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Success))
                        {
                            c.conn.Table<TmpSalesDataEntry>().Delete(s => s.SalesEntrySubmittedStatus == 1);
                        }

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

            return IsBusy;
        }
    }   
}

