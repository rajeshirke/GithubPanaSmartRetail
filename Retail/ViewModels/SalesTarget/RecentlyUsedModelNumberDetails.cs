using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Xamarin.Forms;

namespace Retail.ViewModels.SalesTarget
{
    public class RecentlyUsedModelNumberDetails
    {
        Connection c;
        SalesTargetManagementSL salesTargetManagementSL;
        public TblRecentlyUsedModel TmpRecentModels;
        RecentlyModelStoreDb recentModelDb;

        public RecentlyUsedModelNumberDetails()
        {
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            c.conn.CreateTable<TblRecentlyUsedModel>();
            salesTargetManagementSL = new SalesTargetManagementSL();
            recentModelDb = new RecentlyModelStoreDb();
        }

        // fetch from server
        public async Task<bool> RecentlyModelSyncing()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                recentModelDb.DeleteAllEntries();

                List<string> lstModelNumbers = await salesTargetManagementSL.GetRecentTargetModelEntriesByPerson(CommonAttribute.CustomeProfile.PersonId);
                if (lstModelNumbers.Count != 0)
                {
                    foreach (string modelno in lstModelNumbers)
                    {
                        TmpRecentModels = new TblRecentlyUsedModel
                        {
                            //Id = item.Id,
                            Name = modelno,
                        };
                        recentModelDb.AddRecentlyModel(TmpRecentModels);
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
