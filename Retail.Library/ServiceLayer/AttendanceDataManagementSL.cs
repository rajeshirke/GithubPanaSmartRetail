using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Infrastructure.DataServices;
using Retail.Infrastructure.Hepler;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using APIResponse = Retail.Infrastructure.RequestModels.APIResponse;

namespace Retail.Infrastructure.ServiceLayer
{
    public class AttendanceDataManagementSL
    {
        public async Task<APIResponse> AttendanceCheckIn(AttendanceRequest AttendanceRequestCheckIn)
        {
            var result = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.AttendanceCheckIn, AttendanceRequestCheckIn);
            return result;
        }

        public async Task<List<AttendanceResponse>> GetDailyAttendanceByLocationDate(int LocationId,string CheckDate)
        {
            var result = await WebServiceUtils<List<AttendanceResponse>>.GetData(string.Format(ServiceEndPoints.GetDailyAttendanceByLocationDate, LocationId,CheckDate));
            return result;
        }

        public async Task<APIResponse> AttendanceCheckOut(int AttendanceID,AttendanceRequest AttendanceRequestCheckOut)
        {
            var result = await WebServiceUtils<APIResponse>.PutData(string.Format(ServiceEndPoints.AttendanceCheckOut, AttendanceID), AttendanceRequestCheckOut);
            return result;
        }

        public async Task<APIResponse> AttendanceCreateUpdateAttendance(AttendanceRequest AttendanceRequestCheckOut)
        {
            var result = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.AttendanceCreateUpdateAttendance, AttendanceRequestCheckOut);
            return result;
        }

        public async Task<List<AttendanceResponse>> GetAttendanceDetailsByDateLocationPersonID(int LocationId,int PersonId,string CheckDate, int CountryId)
        {
            var result = await WebServiceUtils<List<AttendanceResponse>>.GetData(string.Format(ServiceEndPoints.GetAttendanceDetailsByDateLocationPersonID, LocationId, PersonId, CheckDate, CountryId));
            return result;
        }

        public async Task<List<PersonDailyAttendanceResponse>> GetPersonMonthlyAttendance( int PersonId,int Month, int Year)
        {
            var result = await WebServiceUtils<List<PersonDailyAttendanceResponse>>.GetData(string.Format(ServiceEndPoints.GetPersonMonthlyAttendance, PersonId, Month, Year));
            return result;
        }

        //new updated 
        public async Task<List<AttendanceResponse>> GetDailyAttendanceByLocationIdsDate(AttendanceDailySearchRequest attendanceDailySearchRequest)
        {
            var result = await WebServiceUtils<List<AttendanceResponse>>.PostData(ServiceEndPoints.GetDailyAttendanceByLocationIdsDate, attendanceDailySearchRequest);
            return result;
        }
    }
}
