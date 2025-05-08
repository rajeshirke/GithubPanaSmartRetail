using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Retail.DependencyServices;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Models.SupervisorWorkflow;
using SQLite;
using Xamarin.Forms;

namespace Retail.Database.SupervisorWorkflow
{
    public class VisitScheduleLocationDb
    {
        private SQLiteConnection conn;
        SupLocationStoreDb supLocationStoreDb;
        VisitScheduleDb supVisitScheduleDb;

        //CREATE
        public VisitScheduleLocationDb()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<TblVisitScheduleLocationResponse>();
            supLocationStoreDb = new SupLocationStoreDb();
            supVisitScheduleDb = new VisitScheduleDb();
        }

        //READ  
        public IEnumerable<TblVisitScheduleLocationResponse> GetvisitScheduleLocations()
        {
            var visitScheduleLocations = (from mem in conn.Table<TblVisitScheduleLocationResponse>() select mem);
            return visitScheduleLocations.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleLocationResponse> GetvisitScheduleLocationByPVisitScheduleLocationId(long PVisitScheduleLocationId)
        {
            var visitScheduleLocation = new List<TblVisitScheduleLocationResponse>();
            string query = "select * From TblVisitScheduleLocationResponse where PVisitScheduleLocationId=" + PVisitScheduleLocationId;
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitScheduleLocation = result;
            return visitScheduleLocation.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleLocationResponse> GetvisitScheduleLocationByPVisitScheduleId(long PVisitScheduleId)
        {
            var visitScheduleLocation = new List<TblVisitScheduleLocationResponse>();
            string query = "select * From TblVisitScheduleLocationResponse where PVisitScheduleId=" + PVisitScheduleId;
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitScheduleLocation = result;
            return visitScheduleLocation.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleLocationResponse> GetvisitScheduleLocationByLocationId(long LocationId, string CreatedDate)
        {
            //string TodaysDate = DateTime.Today.Date.ToString("yyyy-MM-dd");
            var visitScheduleLocation = new List<TblVisitScheduleLocationResponse>();
            string query = "select * From TblVisitScheduleLocationResponse where LocationId=" + LocationId+ " and CreatedDate='" + CreatedDate + "'";
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitScheduleLocation = result;
            return visitScheduleLocation.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleLocationResponse> GetvisitScheduleLocationByCreatedDate(string CreatedDate)
        {
            var visitScheduleLocation = new List<TblVisitScheduleLocationResponse>();
            string query = "select * From TblVisitScheduleLocationResponse where CreatedDate='" + CreatedDate + "'";
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitScheduleLocation = result;
            return visitScheduleLocation.ToList();
        }


        //READ  
        public IEnumerable<TblVisitScheduleLocationResponse> GetvisitScheduleLocationByIsNotEditable()
        {
            var visitScheduleLocation = new List<TblVisitScheduleLocationResponse>();
            string query = "select * From TblVisitScheduleLocationResponse where IsNotEditable = false";
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitScheduleLocation = result;
            return visitScheduleLocation.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleLocationResponse> GetvisitScheduleLocationIsNotEditableByPVisitScheduleLocationId(int PVisitScheduleLocationId)
        {
            var visitScheduleLocation = new List<TblVisitScheduleLocationResponse>();
            string query = "select * From TblVisitScheduleLocationResponse where IsNotEditable = false and PVisitScheduleLocationId='"+ PVisitScheduleLocationId + "'";
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitScheduleLocation = result;
            return visitScheduleLocation.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleLocationResponse> GetvisitScheduleLocationByPVisitScheduleIdAndCreatedDate(long PVisitScheduleId, string CreatedDate)
        {
            var visitScheduleLocation = new List<TblVisitScheduleLocationResponse>();
            string query = "select * From TblVisitScheduleLocationResponse where PVisitScheduleId=" + PVisitScheduleId + " and CreatedDate='" + CreatedDate + "'";
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitScheduleLocation = result;
            return visitScheduleLocation.ToList();
        }

        //READ  
        public IEnumerable<TblVisitScheduleLocationResponse> ValidateLocationExistsToday(long LocationId, string CreatedDate)
        {
            var visitScheduleLocation = new List<TblVisitScheduleLocationResponse>();
            string query = "select * From TblVisitScheduleLocationResponse where LocationId=" + LocationId + " and CreatedDate='" + CreatedDate + "'";
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitScheduleLocation = result;
            return visitScheduleLocation.ToList();
        }

        //UPDATE
        public IEnumerable<TblVisitScheduleLocationResponse> UpdateVisitScheduleLocationEditableByPVisitScheduleId(long PVisitScheduleId)
        {
            var visitSchedules = new List<TblVisitScheduleLocationResponse>();
            string query = "update TblVisitScheduleLocationResponse set IsNotEditable=true where PVisitScheduleId = " + PVisitScheduleId;
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitSchedules = result;
            return visitSchedules.ToList();
        }

        //INSERT  
        public string AddvisitScheduleLocation(TblVisitScheduleLocationResponse TblVisitScheduleLocationResponse)
        {
            conn.Insert(TblVisitScheduleLocationResponse);
            return "success";
        }

        //UPDATE  
        public async Task<IEnumerable<TblVisitScheduleLocationResponse>> updateVisitCheduleLocationStatus(long PVisitScheduleLocationId, List<TblVisitLocationTaskResponse> tblVisitLocationTaskResponses)
        {
            int StatusId = (int)TaskStatusEnum.Open;
            if (tblVisitLocationTaskResponses != null && tblVisitLocationTaskResponses.Count() > 0)
            {
                int j = -1;
                foreach (var itemj in tblVisitLocationTaskResponses)
                {
                    j = j + 1;
                    // if not matching then inprogress
                    if (j > 0 && StatusId != itemj.TaskStatusId)
                    {
                        StatusId = (int)TaskStatusEnum.InProgress;
                        break;
                    }

                    if (itemj.TaskStatusId == (int)TaskStatusEnum.Open)
                    {
                        StatusId = (int)TaskStatusEnum.Open;
                    }
                    else if (itemj.TaskStatusId == (int)TaskStatusEnum.Closed)
                    {
                        StatusId = (int)TaskStatusEnum.Closed;
                    }
                    else if (itemj.TaskStatusId == (int)TaskStatusEnum.InProgress)
                    {
                        StatusId = (int)TaskStatusEnum.InProgress;
                    }
                }
            }

            //update data            
            var visitLocationTasksByVisit = new List<TblVisitScheduleLocationResponse>();
            string query = "update TblVisitScheduleLocationResponse set StatusId=" + StatusId + " where PVisitScheduleLocationId = " + PVisitScheduleLocationId;
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                visitLocationTasksByVisit = result;
            return visitLocationTasksByVisit.ToList();
        }

        public async Task<IEnumerable<TblVisitScheduleLocationResponse>> updateVisitCheduleLocationGeoCoordinates(long PVisitScheduleId) //PVisitScheduleLocationId
        {
            var visitLocationTasksByVisit = new List<TblVisitScheduleLocationResponse>();
            var visitSchedule= supVisitScheduleDb.GetvisitScheduleByVisitScheduleStatusId((int)TaskStatusEnum.Closed).ToList();
            //foreach (var item in visitSchedule)
            //{
                var VisitScheduleLocation = GetvisitScheduleLocationByPVisitScheduleId(PVisitScheduleId);
                if (VisitScheduleLocation != null)
                {
                    foreach (var itemScheduleLoc in VisitScheduleLocation)
                    {
                        // get select location details
                        var visitScheduleLocation = GetvisitScheduleLocationByPVisitScheduleLocationId(itemScheduleLoc.PVisitScheduleLocationId)?.FirstOrDefault();
                        var locationStore = supLocationStoreDb.GetlocationStoresByLocationId(visitScheduleLocation.LocationId)?.FirstOrDefault();
                        double NearbyDistance = await Retail.Hepler.Extensions.GetDistanceByPersonLocationAsync((double)locationStore.Latitude, (double)locationStore.Longitude);

                        //update data
                        var location = await Retail.Hepler.Extensions.GeoCoordinates();
                        if(location!=null)
                        {
                            string query = "update TblVisitScheduleLocationResponse set PersonLocationLongitude=" + location.Longitude + ",PersonLocationLatitude=" + location.Latitude + ",ProximityRange=" + NearbyDistance + " where PVisitScheduleLocationId = " + itemScheduleLoc.PVisitScheduleLocationId;
                            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
                            if (result != null && result.Count > 0)
                                visitLocationTasksByVisit = result;
                        }                        

                    }
                }
                
            //}
            return visitLocationTasksByVisit.ToList();
        }

        //INSERT  
        public long AddvisitScheduleLocationQuery(TblVisitScheduleLocationResponse TblVisitScheduleLocationResponse)
        {
            long primaryKey = 0;
            conn.Insert(TblVisitScheduleLocationResponse);
            string query = "select * From TblVisitScheduleLocationResponse order by PVisitScheduleLocationId desc limit 1";
            var result = conn.Query<TblVisitScheduleLocationResponse>(query);
            if (result != null && result.Count > 0)
                primaryKey = result[0].PVisitScheduleLocationId;
            return primaryKey;
            //return "success";
        }

        //DELETE  
        public string DeletevisitScheduleLocation(int id)
        {
            conn.Delete<TblVisitScheduleLocationResponse>(id);
            return "success";
        }

        //DELETE ALL
        public string DeleteAllEntries()
        {
            conn.DeleteAll<TblVisitScheduleLocationResponse>();
            return "Success";
        }

        //DELETE ALL
        //DELETE ALL PREVIOUS SCHEDULES NOT SUBMITTED
        public IEnumerable<TblVisitScheduleLocationResponse> DeletePreviousVisitScheduleStatusByNotIsNotEditable()
        {
            string TodaysDate = DateTime.Today.ToString("yyyy-MM-dd");
            var visitScheduleLocs = new List<TblVisitScheduleLocationResponse>();
            string query1 = "select * from TblVisitScheduleLocationResponse where IsNotEditable=false and CreatedDate <> '" + TodaysDate + "'";
            var result1 = conn.Query<TblVisitScheduleLocationResponse>(query1);
            if (result1 != null && result1.Count() > 0)
            {
                visitScheduleLocs = result1;
                foreach (var item in visitScheduleLocs)
                {
                    string query2 = "select * from TblVisitScheduleLocationResponse where PVisitScheduleLocationId = " + item.PVisitScheduleLocationId;
                    var result2 = conn.Query<TblVisitScheduleLocationResponse>(query2);
                    if (result2 != null && result2.Count() > 0)
                    {
                        //delete visit schedule locations
                        string query3 = "delete from TblVisitScheduleLocationResponse where PVisitScheduleLocationId = " + item.PVisitScheduleLocationId;
                        var result3 = conn.Query<TblVisitScheduleLocationResponse>(query3);
                        //delete visit schedule
                        string query4 = "delete from TblVisitScheduleResponse where PVisitScheduleId = " + item.PVisitScheduleId;
                        var result4 = conn.Query<TblVisitScheduleResponse>(query4);
                    }
                }
            }

            return visitScheduleLocs.ToList();
        }



        //READ
        public TblVisitScheduleLocationResponse GetvisitLocationTasksByPVisitLocationTaskId(long PVisitLocationTaskId)
        {
            var visitLocationTasksByVisit = new TblVisitScheduleLocationResponse();

            var visitLocationTasksResponse = new TblVisitLocationTaskResponse();
            string query = "select * From TblVisitLocationTaskResponse where PVisitLocationTaskId = " + PVisitLocationTaskId;
            var result = conn.Query<TblVisitLocationTaskResponse>(query);
            if (result != null && result.Count > 0)
                visitLocationTasksResponse = result.FirstOrDefault();

            if (visitLocationTasksResponse != null && visitLocationTasksResponse.PVisitScheduleLocationId > 0)
            {
                
                string query1 = "select * From TblVisitScheduleLocationResponse where PVisitScheduleLocationId = " + visitLocationTasksResponse.PVisitScheduleLocationId;
                var result1 = conn.Query<TblVisitScheduleLocationResponse>(query1);
                if (result1 != null && result1.Count > 0)
                    visitLocationTasksByVisit = result1.FirstOrDefault();
            }
            return visitLocationTasksByVisit;
        }

    }
}
