using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Retail.Database.SupervisorWorkflow;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.Models.SupervisorWorkflow;
using Retail.Views.MyVisits;
using Retail.Views.SupervisorFlow;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.SupervisorFlow.SupervisorWorkflow
{
    public class SupervisorSubmitSyncing
    {
        VisitScheduleDb supVisitScheduleDb;
        VisitScheduleLocationDb supVisitScheduleLocationStoreDb;
        VisitLocationTaskDb supVisitLocationTaskDb;
        SupLocationStoreDb supLocationStoreDb;
        FileInfoDb fileInfoDb;
        AnswerUploadedFileDb answerUploadedFileDb;
        TaskQuestionAnswerDb taskQuestionAnswerDb;
        AnswerOptionDb answerOptionDb;
        AnswerSelectedOptionDb answerSelectedOptionDb;

        VisitsDataManagementSL visitsDataManagementSL;

        public SupervisorSubmitSyncing()
        {

            supVisitScheduleDb = new VisitScheduleDb();
            visitsDataManagementSL = new VisitsDataManagementSL();
            supVisitScheduleLocationStoreDb = new VisitScheduleLocationDb();
            supVisitLocationTaskDb = new VisitLocationTaskDb();
            supLocationStoreDb = new SupLocationStoreDb();
            taskQuestionAnswerDb = new TaskQuestionAnswerDb();
            fileInfoDb = new FileInfoDb();
            answerUploadedFileDb = new AnswerUploadedFileDb();
            answerOptionDb = new AnswerOptionDb();
            answerSelectedOptionDb = new AnswerSelectedOptionDb();
        }

        public async Task<bool> SyncingFinalSubmitSupWorkflow(bool showAlert = false)
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                #region fetch from sqlite
                var _VisitScheduleResponse = new VisitScheduleResponse(); // parent table
                var visitSchedules = supVisitScheduleDb.GetvisitScheduleByVisitScheduleStatusId((int)TaskStatusEnum.Closed).ToList();

                if (visitSchedules != null && visitSchedules.Count() > 0)
                {
                    foreach (var itemSch in visitSchedules)
                    {

                        var visitScheduleLocations = supVisitScheduleLocationStoreDb.GetvisitScheduleLocationByPVisitScheduleId(itemSch.PVisitScheduleId).ToList();
                        if (visitScheduleLocations != null && visitScheduleLocations.Count > 0)
                        {
                            var _VisitScheduleLocationResponses = new List<VisitScheduleLocationResponse>();
                            int i = -1;
                            foreach (var itemi in visitScheduleLocations)
                            {
                                i = i + 1;
                                _VisitScheduleLocationResponses.Add(new VisitScheduleLocationResponse()
                                {
                                    //VisitScheduleLocationId = itemi.VisitScheduleLocationId,
                                    VisitScheduleId = itemi.VisitScheduleId,
                                    LocationId = itemi.LocationId,
                                    VisitLocationTotalTaskCount = itemi.VisitLocationTotalTaskCount,
                                    VisitLocationTasksStatusId = itemi.VisitLocationTasksStatusId,
                                    VisitLocationTasksStatusName = itemi.VisitLocationTasksStatusName,
                                    VisitLocationTasksCountStatus = itemi.VisitLocationTasksCountStatus,
                                    PersonLocationLatitude = itemi.PersonLocationLatitude,
                                    PersonLocationLongitude = itemi.PersonLocationLongitude,
                                    ProximityRange = itemi.ProximityRange,
                                    StatusId = itemi.StatusId,
                                    VisitLocationTasks = new List<VisitLocationTaskResponse>()
                                });

                                // visit location task fetch 
                                var visitLocationTasks = supVisitLocationTaskDb.GetvisitLocationTasksByPVisitScheduleLocationId(itemi.PVisitScheduleLocationId).ToList();
                                if (visitLocationTasks != null && visitLocationTasks.Count() > 0)
                                {
                                    var _visitScheduleLocation = _VisitScheduleLocationResponses[i];
                                    int j = -1;
                                    //var _VisitLocationTasks = new List<VisitLocationTaskResponse>();
                                    foreach (var itemj in visitLocationTasks)
                                    {
                                        j = j + 1;
                                        var _VisitLocationTask = new VisitLocationTaskResponse()
                                        {
                                            //VisitLocationTaskId = itemj.VisitLocationTaskId,
                                            TaskMasterId = itemj.TaskMasterId,
                                            //VisitScheduleLocationId = itemj.VisitScheduleLocationId,
                                            TaskStatusId = itemj.TaskStatusId,
                                            TaskCompletionDate = itemj.TaskCompletionDate,
                                            AssignedToPersonId = itemj.AssignedToPersonId,
                                            //AssignedToPersonId = itemj.AssignedToPersonId,

                                        };

                                        // task question answers
                                        //var taskQuestionAnsers = taskQuestionAnswerDb.GettaskQuestionAnswerByTaskMasterId(itemj.TaskMasterId).ToList();
                                        var taskQuestionAnsers = taskQuestionAnswerDb.GettaskQuestionAnswerByVisitLocationTaskMasterId(itemj.PVisitLocationTaskId).ToList();

                                        if (taskQuestionAnsers != null && taskQuestionAnsers.Count() > 0)
                                        {
                                            var _TaskQuestionAnswers = new List<TaskQuestionAnswerResponse>();
                                            int k = -1;
                                            foreach (var itemk in taskQuestionAnsers)
                                            {
                                                k = k + 1;
                                                _TaskQuestionAnswers.Add(new TaskQuestionAnswerResponse()
                                                {
                                                    //TaskQuestionAnswerId = itemk.TaskQuestionAnswerId,
                                                    AnswerText = itemk.AnswerText,
                                                    AnswerSign = itemk.AnswerSign,
                                                    AnswerBarcode = itemk.AnswerBarcode,
                                                    AnswerYesNo = itemk.AnswerYesNo,
                                                    AnswerNumber = itemk.AnswerNumber,
                                                    AnswerDate = itemk.AnswerDate,
                                                    AnswerPrice = itemk.AnswerPrice,
                                                    AnswerQrcode = itemk.AnswerQrcode,
                                                    AnswerCurrency = itemk.AnswerCurrency,
                                                    AnswerSelectedOptionId = itemk.AnswerSelectedOptionId,
                                                    AnswerSelectedOptionText = itemk.AnswerSelectedOptionText,
                                                    StatusId = itemk.StatusId,
                                                    AnswerEntryByPersonId = itemk.AnswerEntryByPersonId,
                                                    AnswerEntryDate = itemk.AnswerEntryDate,
                                                    VisitLocationTaskId = itemk.VisitLocationTaskId,
                                                    QuestionMasterId = itemk.QuestionMasterId,
                                                    TaskMasterId = itemk.TaskMasterId,
                                                    //AnswerSelectedOptions = new List<AnswerSelectedOptionResponse>(),
                                                    AnswerUploadedFiles = new List<AnswerUploadedFileResponse>(),
                                                });

                                                // answer selected options
                                                var answerSelectedOption = answerSelectedOptionDb.GetanswerSelectedOptionByPTaskQuestionAnswerId(itemk.PTaskQuestionAnswerId).ToList();
                                                if (answerSelectedOption != null && answerSelectedOption.Count() > 0)
                                                {
                                                    var _AnswerSelectedOptions = new List<AnswerSelectedOptionResponse>();
                                                    int m = -1;
                                                    foreach (var itemm in answerSelectedOption)
                                                    {
                                                        m = m + 1;
                                                        _AnswerSelectedOptions.Add(new AnswerSelectedOptionResponse()
                                                        {
                                                            OptionId = itemm.OptionId,
                                                            OptionName = itemm.OptionName,
                                                            QuestionMasterId = itemm.QuestionMasterId
                                                        });
                                                    }

                                                    _TaskQuestionAnswers[k].AnswerSelectedOptions = _AnswerSelectedOptions;
                                                }

                                                //answer uploaded files
                                                var answerUploaedFiles = answerUploadedFileDb.GetanswerUploaedFileByPTaskQuestionAnswerId(itemk.PTaskQuestionAnswerId).ToList();
                                                if (answerUploaedFiles != null && answerUploaedFiles.Count() > 0)
                                                {
                                                    var _AnswerUploadedFiles = new List<AnswerUploadedFileResponse>();
                                                    int l = -1;
                                                    foreach (var iteml in answerUploaedFiles)
                                                    {
                                                        l = l + 1;
                                                        _AnswerUploadedFiles.Add(new AnswerUploadedFileResponse()
                                                        {
                                                            AnswerUploadedFilesId = iteml.AnswerUploadedFilesId,
                                                            //FileInfoId = iteml.FileInfoId,
                                                            //TaskQuestionAnswerId = iteml.TaskQuestionAnswerId,
                                                            FileInfo = new FileInfoResponse()
                                                        });

                                                        var fileInfos = fileInfoDb.GetfileInfoByPFileInfoId(iteml.PFileInfoId).ToList();
                                                        if (fileInfos != null && fileInfos.Count() > 0)
                                                        {
                                                            var fileInfo = fileInfos[0];
                                                            _AnswerUploadedFiles[l].FileInfo = new FileInfoResponse()
                                                            {
                                                                FileInfoId = fileInfo.FileInfoId,
                                                                FileName = fileInfo.FileName,
                                                                FileTypeId = fileInfo.FileTypeId,
                                                                MimeType = fileInfo.MimeType,
                                                                Path = fileInfo.Path,
                                                                Base64StringImage = fileInfo.Base64StringImage,
                                                            };
                                                        }
                                                    }

                                                    _TaskQuestionAnswers[k].AnswerUploadedFiles = _AnswerUploadedFiles;
                                                }
                                            }

                                            _VisitLocationTask.TaskQuestionAnswers = _TaskQuestionAnswers;
                                        }

                                        _visitScheduleLocation.VisitLocationTasks.Add(_VisitLocationTask);
                                    }
                                }
                            }

                            _VisitScheduleResponse.VisitScheduleLocations = _VisitScheduleLocationResponses;
                            _VisitScheduleResponse.CreationDate = !string.IsNullOrEmpty(itemSch.CreationDate) ? Convert.ToDateTime(itemSch.CreationDate) : DateTime.Today;
                            //_VisitScheduleResponse.CompletionDate = Convert.ToDateTime(itemSch.compl);
                            _VisitScheduleResponse.VisitScheduleDate = !string.IsNullOrEmpty(itemSch.VisitScheduleDate) ? Convert.ToDateTime(itemSch.VisitScheduleDate) : DateTime.Today;
                            _VisitScheduleResponse.VisitScheduleStatusId = (int)TaskStatusEnum.Closed;//(int)itemSch.VisitScheduleStatusId;
                            _VisitScheduleResponse.PersonId = (int)itemSch.PersonId;
                            _VisitScheduleResponse.CompletionDate = DateTime.Now;
                        }

                        // submitted visit schedule posting

                        var response = (Retail.Infrastructure.RequestModels.APIResponse)await visitsDataManagementSL.SaveTaskActionAnswersOffline(_VisitScheduleResponse);
                        if (response.Status == (int)APIResponseEnum.Success)
                        {
                            //clear all sqlite table based on PVisitScheduleId except masters
                            supVisitScheduleDb.UpdateVisitScheduleStatusByPVisitScheduleId(itemSch.PVisitScheduleId, (int)TaskStatusEnum.Submitted);

                            //supVisitScheduleDb.DeleteAllEntries();
                            //supVisitScheduleLocationStoreDb.DeleteAllEntries();
                            //supVisitLocationTaskDb.DeleteAllEntries();
                            //taskQuestionAnswerDb.DeleteAllEntries();
                            //fileInfoDb.DeleteAllEntries();
                            //answerSelectedOptionDb.DeleteAllEntries();
                            //answerUploadedFileDb.DeleteAllEntries();

                            if (showAlert)
                            {
                                //await DisplayAlert("Success", "Saved successfully");
                                //await Navigation.PopAsync();
                            }
                        }
                        else if (showAlert)
                        {
                            //await ErrorDisplayAlert("Error while saving data");
                        }

                    }
                }
                #endregion

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
