using System;
using System.Collections.Generic;
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
    public class SalesEntryStoreDetails
    {
        Connection c;
        MasterDataManagementSL masterDataManagementSL;

        List<EntityKeyValueResponse> Locations;
        public TblLocationStore TmpLocationStore;

        LocationStoreDb locationStoreDb;
        PromoterLocationStoreDb promoterLocationStoreDb;

        public SalesEntryStoreDetails()
        {
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            c.conn.CreateTable<TblLocationStore>();
            masterDataManagementSL = new MasterDataManagementSL();

            Locations = new List<EntityKeyValueResponse>();

            locationStoreDb = new LocationStoreDb();
            promoterLocationStoreDb = new PromoterLocationStoreDb();
        }

        // fetch from server
        public async Task<bool> StoreSyncing()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                locationStoreDb.DeleteAllEntries();

                Locations = await masterDataManagementSL.GetLocationsByPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                if (Locations.Count != 0)
                {
                    foreach (var item in Locations)
                    {
                        TmpLocationStore = new TblLocationStore
                        {
                            Id = item.Id,
                            Name = item.Name,
                        };
                        locationStoreDb.AddlocationStore(TmpLocationStore);
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

        // fetch from server
        public async Task<bool> StoreDetailsSyncing()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                promoterLocationStoreDb.DeleteAllEntries();

                var PromoterLocations = await masterDataManagementSL.GetLocationDetailsByPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                if (PromoterLocations.Count != 0)
                {
                    foreach (var item in PromoterLocations)
                    {
                        var promoterLocation = new TblPromoterLocationResponse
                        {
                            LocationId = item.LocationId,
                            LocationStoreName = item.LocationStoreName,
                            Area = item.Area,
                            Longitude = item.Longitude,
                            Latitude = item.Latitude,
                            Distance = item.Distance,
                        };
                        promoterLocationStoreDb.AddlocationStore(promoterLocation);
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
