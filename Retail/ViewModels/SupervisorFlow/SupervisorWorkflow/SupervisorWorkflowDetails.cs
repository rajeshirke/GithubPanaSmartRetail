using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Retail.Database;
using Retail.Database.SupervisorWorkflow;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models.SupervisorWorkflow;
using Xamarin.Forms;

namespace Retail.ViewModels.SupervisorFlow.SupervisorWorkflow
{
    public class SupervisorWorkflowDetails
    {
        Connection c;

        //table creation for supervisor
        public TblLocationResponse TmpLocationModels;
        public TblTaskMasterStoreLinkAccount TmpTaskMasterStoreLinkAccountModels;
        public TblTaskMasterResponse TmpTaskMasterModels;
        public TblTaskQuestionAnswerResponse TmpTaskQuestionAnswerModels;
        public TblTaskMasterQuestionResponse TmpTaskMasterQuestionModels;
        public TblQuestionMasterResponse TmpQuestionMasterModels;
        public TblAnswerOptionResponse TmpAnswerOptionModels;
        public TblAnswerSelectedOptionResponse TmpAnswerSelectedOptionModels;
        public TblAnswerUploadedFileResponse TmpAnswerUploadFileModels;
        public TblFileInfoResponse TmpFileInfoModels;
        public TblVisitScheduleLocationResponse TmpScheduleLocationModels;
        public TblVisitLocationTaskResponse TmpVisitLocationModels;

        //supervisor location
        MasterDataManagementSL locationsWithDetailsCountrySL;
        SupLocationStoreDb supLocationStoreDb;

        // scheduled location
        VisitsDataManagementSL visitsDataManagementSL;
        AnswerOptionDb answerOptionDb;
        AnswerSelectedOptionDb answerSelectedOptionDb;
        AnswerUploadedFileDb answerUploadedFileDb;
        FileInfoDb fileInfoDb;
        QuestionMasterDb questionMasterDb;
        TaskMasterDb taskMasterDb;
        TaskMasterStoreLinkAccountDb taskMasterStoreLinkAccountDb;
        TaskMasterQuestionDb taskMasterQuestionDb;
        TaskQuestionAnswerDb taskQuestionAnswerDb;

        VisitScheduleLocationDb visitScheduleLocationDb;
        VisitLocationTaskDb visitLocationTaskDb;

        public SupervisorWorkflowDetails()
        {
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();

            //supervisor location
            locationsWithDetailsCountrySL = new MasterDataManagementSL();
            supLocationStoreDb = new SupLocationStoreDb();

            // scheduled location
            visitsDataManagementSL = new VisitsDataManagementSL();
            visitScheduleLocationDb = new VisitScheduleLocationDb();
            taskMasterDb = new TaskMasterDb();
            taskMasterStoreLinkAccountDb = new TaskMasterStoreLinkAccountDb();
            taskMasterQuestionDb = new TaskMasterQuestionDb();
            questionMasterDb = new QuestionMasterDb();
            answerOptionDb = new AnswerOptionDb();
        }

        // fetch from server store location
        public async Task<bool> StoreLocationSyncing()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                supLocationStoreDb.DeleteAllEntries();

                //List<LocationResponse> locations = await locationsWithDetailsCountrySL.GetLocationsWithDetailsCountryId((int)CommonAttribute.CustomeProfile.CountryId);

                List<LocationResponse> locations = await locationsWithDetailsCountrySL.GetLocationsWithDetailsPersonId((int)CommonAttribute.CustomeProfile.PersonId);
                if (locations != null && locations.Count != 0)
                {
                    foreach (LocationResponse item in locations)
                    {
                        TmpLocationModels = new TblLocationResponse
                        {
                            LocationId = item.LocationId,
                            LocationCode = item.LocationCode,
                            LocationStoreName = item.LocationStoreName,
                            Area = item.Area,
                            Longitude = item.Longitude,
                            Latitude = item.Latitude,
                            City = item.City,
                            Distance = item.Distance,
                            AccountId = item.LocationAccountID
                        };
                        supLocationStoreDb.AddlocationStore(TmpLocationModels);
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

        // fetch from server GetVisitLocationTasksQAOfflineByPersonIdSyncing
        public async Task<bool> GetVisitLocationTasksQAOfflineByPersonIdSyncing()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                int PersonID = (int)CommonAttribute.CustomeProfile.PersonId;
                //string VisitDate = DateTime.Today.ToString("yyyy-MM-dd");

                List<VisitLocationTaskResponse> visitLocationTaskResponses = await visitsDataManagementSL.GetVisitLocationTasksQAOfflineByPersonId(PersonID);

                if (visitLocationTaskResponses != null && visitLocationTaskResponses.Count > 0)
                {
                    ////visitScheduleLocationDb.DeleteAllEntries();
                    taskMasterStoreLinkAccountDb.DeleteAllEntries();

                    ////commented below lines because blank space is showing after updating task in web console
                    //taskMasterDb.DeleteAllEntries();
                    //taskMasterQuestionDb.DeleteAllEntries();
                    //questionMasterDb.DeleteAllEntries();

                    answerOptionDb.DeleteAllEntries();

                    

                    // adding to table TblVisitLocationTaskResponse
                    int i = -1;
                    foreach (VisitLocationTaskResponse itemi in visitLocationTaskResponses)
                    {
                        i = i + 1;
                        TmpVisitLocationModels = new TblVisitLocationTaskResponse
                        {
                            TaskMasterId = itemi.TaskMasterId,
                            TaskStatusId = itemi.TaskStatusId,
                            TaskCompletionDate = itemi.TaskCompletionDate,
                            AssignedToPersonId = itemi.AssignedToPersonId,
                            //VisitLocationTaskId = itemi.VisitLocationTaskId,
                            //VisitScheduleLocationId = itemi.VisitScheduleLocationId,
                            //TaskMaster = itemi.TaskMaster,
                            //VisitScheduleLocation = itemi.VisitScheduleLocation,
                            //TaskQuestionAnswers = itemi.TaskQuestionAnswers
                        };
                        visitScheduleLocationDb.AddvisitScheduleLocation(TmpScheduleLocationModels);
                       
                        // adding to table TaskMaster
                        var taskMaster = itemi.TaskMaster;

                        if (taskMaster != null)
                        {
                            TmpTaskMasterModels = new TblTaskMasterResponse
                            {
                                TaskMasterId = taskMaster.TaskMasterId,
                                Title = taskMaster.Title,
                                Description = taskMaster.Description,
                                IsActive = taskMaster.IsActive,
                                SerialNo = taskMaster.SerialNo,
                            };

                            #region TaskStoreLinkWithAccount
                            //insert into store account table
                            TmpTaskMasterStoreLinkAccountModels = new TblTaskMasterStoreLinkAccount
                            {
                                AccountId = itemi.LocationAccountID,
                                TaskMasterId = taskMaster.TaskMasterId
                            };
                            taskMasterStoreLinkAccountDb.AddtaskMasterStoreLinkAccount(TmpTaskMasterStoreLinkAccountModels);
                            #endregion

                            // already inserted or not
                            var taskMasters = taskMasterDb.GettaskMastersByTaskMasterId(taskMaster.TaskMasterId).ToList();
                            if (taskMasters != null && taskMasters.Count() > 0)
                            {
                                continue;
                            }

                            taskMasterDb.AddtaskMaster(TmpTaskMasterModels);

                            // adding to table TaskMasterQuestion
                            var taskMasterQuestion = taskMaster.TaskMasterQuestions;
                            if (taskMasterQuestion != null
                                && taskMasterQuestion.Count > 0)
                            {
                                int j = -1;
                                foreach (var itemj in taskMasterQuestion)
                                {
                                    j = j + 1;
                                    TmpTaskMasterQuestionModels = new TblTaskMasterQuestionResponse
                                    {
                                        TaskQuestionId = itemj.TaskQuestionId,
                                        TaskMasterId = itemj.TaskMasterId,
                                        QuestionMasterId = itemj.QuestionMasterId,
                                    };
                                    taskMasterQuestionDb.AddtaskMasterQuestion(TmpTaskMasterQuestionModels);

                                    //adding to table QuestionMaster
                                    var questionMaster = itemj.QuestionMaster;
                                    if(questionMaster != null)
                                    {
                                        TmpQuestionMasterModels = new TblQuestionMasterResponse
                                        {
                                            QuestionMasterId = questionMaster.QuestionMasterId,
                                            QuestionTitle = questionMaster.QuestionTitle,
                                            Description = questionMaster.Description,
                                            AnswerTypeId = questionMaster.AnswerTypeId,
                                            IsActive = questionMaster.IsActive,
                                            IsMandatory = questionMaster.IsMandatory,
                                        };

                                        // already inserted or not
                                        var questionMasters = questionMasterDb.GetquestionMastersByQuestionMasterId(questionMaster.QuestionMasterId).ToList();
                                        if (questionMasters != null && questionMasters.Count() > 0)
                                        {
                                            continue;
                                        }

                                        questionMasterDb.AddquestionMaster(TmpQuestionMasterModels);

                                        //adding to table AnswerOptions
                                        var answerOptions = questionMaster.AnswerOptions;
                                        if (answerOptions != null && answerOptions.Count > 0)
                                        {
                                            int k = -1;
                                            foreach (var itemk in answerOptions)
                                            {
                                                k = k + 1;
                                                TmpAnswerOptionModels = new TblAnswerOptionResponse
                                                {
                                                    OptionId = itemk.OptionId,
                                                    OptionName = itemk.OptionName,
                                                    OptionSelected = itemk.OptionSelected,
                                                    QuestionMasterId = itemk.QuestionMasterId,
                                                };
                                                answerOptionDb.AddanswerOption(TmpAnswerOptionModels);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    ////fetch all data after sync prj
                    //var taskmaster = taskMasterDb.GettaskMasters();
                    //var taskmasterquestion = taskMasterQuestionDb.GettaskMasterQuestions();
                    //var questionmaster = questionMasterDb.GetquestionMasters();
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
