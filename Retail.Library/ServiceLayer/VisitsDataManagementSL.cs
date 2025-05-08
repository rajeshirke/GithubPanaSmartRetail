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
    public class VisitsDataManagementSL
    {
        //Planned Visits 
        public async Task<VisitScheduleResponse> GetVisitScheduleByPersonIdDate(int PersonId,string selecteddate,int ScheduleType)
        {
            var result= await WebServiceUtils<VisitScheduleResponse>.GetData(string.Format(ServiceEndPoints.GetVisitScheduleByPersonIdDate, PersonId, selecteddate, ScheduleType));
            return result;
        }

        //new testing 30Nov21
        public async Task<VisitScheduleResponse> GetVisitScheduleByPersonIdDates(int PersonId, string selecteddate, int ScheduleType)
        {
            var result = await WebServiceUtils<VisitScheduleResponse>.GetData(string.Format(ServiceEndPoints.GetVisitScheduleByPersonIdDates, PersonId, selecteddate, ScheduleType));
            return result;
        }

        //new API for multiple visits at same time
        public async Task<List<VisitScheduleResponse>> GetAllVisitScheduleByPersonIdDate(int PersonId, string selecteddate, int ScheduleType)
        {
            var result = await WebServiceUtils<List<VisitScheduleResponse>>.GetData(string.Format(ServiceEndPoints.GetAllVisitScheduleByPersonIdDate, PersonId, selecteddate, ScheduleType));
            return result;
        }

        //Visit Tasks
        public async Task<List<VisitLocationTaskResponse>> GetVisitLocationTasksByVisitScheduleLocationId(int LocationID)
        {
            var result = await WebServiceUtils<List<VisitLocationTaskResponse>>.GetData(string.Format(ServiceEndPoints.GetVisitLocationTasksByVisitScheduleLocationId, LocationID));
            return result;
        }
        //Task Summary
        public async Task<List<TaskQuestionAnswerResponse>> GetTaskQuestionAnswerByTaskMasterID(int PersonId,int VisitLocationId)
        {
            var result = await WebServiceUtils<List<TaskQuestionAnswerResponse>>.GetData(string.Format(ServiceEndPoints.GetTaskQuestionAnswerByTaskMasterID, PersonId, VisitLocationId));
            return result;
        }

        //Task Summary
        public async Task<List<TaskQuestionAnswerToSubmitResponse>> GetLocationTaskQuestionAnswer(int TaskMasterId, int VisitLocationTaskID)
        {
            var result = await WebServiceUtils<List<TaskQuestionAnswerToSubmitResponse>>.GetData(string.Format(ServiceEndPoints.GetLocationTaskQuestionAnswer, TaskMasterId, VisitLocationTaskID));
            return result;
        }

        //Task Summary
        public async Task<APIResponse> PostLocationTaskQuestionAnswer(List<TaskQuestionAnswerToSubmitResponse> LocationTaskQuestionAnswer)
        {
            var result = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.SaveTaskActionAnswers, LocationTaskQuestionAnswer);
            return result;
        }

        //public async Task<ActionResult<VisitScheduleResponse>> CreateSupervisorVisitSchedule(VisitScheduleCreateUpdateRequest req)

        public async Task<APIResponse> CreateSupervisorVisitSchedule(VisitScheduleCreateUpdateRequest visitScheduleCreateUpdateRequest)
        {
            var result = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.CreateSupervisorVisitSchedule, visitScheduleCreateUpdateRequest);
            return result;
        }

        public async Task<VisitLocationTaskResponse> GetVisitLocationTask(int VisitLocationTaskId)
        {
            var result = await WebServiceUtils<VisitLocationTaskResponse>.GetData(string.Format(ServiceEndPoints.GetVisitLocationTask, VisitLocationTaskId));
            return result;
        }

        //offline
        public async Task<List<VisitLocationTaskResponse>> GetVisitLocationTasksQAOfflineByPersonId(int PersonId)
        {
            var result = await WebServiceUtils<List<VisitLocationTaskResponse>>.GetData(string.Format(ServiceEndPoints.GetVisitLocationTasksQAOfflineByPersonId, PersonId));
            return result;
        }

        //offline
        public async Task<APIResponse> SaveTaskActionAnswersOffline(VisitScheduleResponse LocationTaskQuestionAnswer)
        {
            var result = await WebServiceUtils<APIResponse>.PostData(ServiceEndPoints.SaveTaskActionAnswersOffline, LocationTaskQuestionAnswer);
            return result;
        }
    }
}
