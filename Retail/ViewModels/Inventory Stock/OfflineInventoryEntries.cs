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

namespace Retail.ViewModels.InventoryStock
{
    public class OfflineInventoryEntries
    {
        Connection c;
        public List<InventoryStockEntryRequest> lstSaveInventoryStockEntryRequest { get; set; }

        public OfflineInventoryEntries()
        {
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            c.conn.CreateTable<TblInventoryStockEntryRequest>();
        }

        public async Task<bool> GetOfflineInventoryEntries()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                lstSaveInventoryStockEntryRequest = new List<InventoryStockEntryRequest>();
                var OfflineInventoryEntries = c.conn.Table<TblInventoryStockEntryRequest>().Where(f => f.InventoryEntrySubmittedStatus == 1).ToList();
                if (OfflineInventoryEntries != null && OfflineInventoryEntries.Count != 0)
                {
                    foreach (var item in OfflineInventoryEntries)
                    {
                        lstSaveInventoryStockEntryRequest.Add(new InventoryStockEntryRequest
                        {
                            InventoryStockEntryId = 0,
                            ProductModelId = item.ProductModelId,
                            ProductModelNumber = item.ProductModelNumber,
                            ProductCategoryId = item.ProductCategoryId,
                            ProductCategoryName = item.ProductCategoryName,
                            ProductSubCategoryId = item.ProductSubCategoryId,
                            ProductSubCategoryName = item.ProductSubCategoryName,
                            LocationId = item.LocationId,
                            EntryDate = item.EntryDate,
                            EntryByPersonId = item.EntryByPersonId,
                            Comments = item.Comments,
                            CreationDate = item.CreationDate,
                            Quantity = item.Quantity,
                            StockEntryTypeId = item.StockEntryTypeId,
                            CountryId = item.CountryId,
                            InventoryEntrySubmittedStatus = item.InventoryEntrySubmittedStatus,

                        });
                    }
                    if (lstSaveInventoryStockEntryRequest != null && lstSaveInventoryStockEntryRequest.Count != 0)
                    {
                        InventoryManagementSL inventoryManagementSL = new InventoryManagementSL();
                        var receiveContext = await inventoryManagementSL.CreateInventoryStockEntry(lstSaveInventoryStockEntryRequest);
                        if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Success))
                        {
                            c.conn.Table<TblInventoryStockEntryRequest>().Delete(d => d.InventoryEntrySubmittedStatus == 1);
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
