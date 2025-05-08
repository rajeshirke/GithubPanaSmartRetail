using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Retail.Controls;
using Retail.Database.SupervisorWorkflow;
using Retail.Hepler;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models.SupervisorWorkflow;
using Retail.Views.MyVisits;
using Xamarin.Essentials;
using Xamarin.Forms;
using Extensions = Retail.Hepler.Extensions;

namespace Retail.ViewModels.MyVisits
{
    public class TaskSummaryViewModel : BaseViewModel
    {
        public bool TaskCompletionVisible { get; set; } = false;
        public bool IsTaskEditable { get; set; } = true;
        public Command SaveCommand { get; set; }
        public VisitLocationTaskResponse visitLocationTaskResponse { get; set; }
        bool SupWorkflowOffline = false;
        //query
        QuestionMasterDb questionMasterDb;
        TaskMasterQuestionDb taskMasterQuestionDb;

        TaskQuestionAnswerDb taskQuestionAnswerDb;
        VisitScheduleDb supVisitScheduleDb;
        VisitScheduleLocationDb supVisitScheduleLocationStoreDb;
        VisitLocationTaskDb supVisitLocationTaskDb;
        AnswerOptionDb answerOptionDb;

        FileInfoDb fileInfoDb;
        AnswerUploadedFileDb answerUploadedFileDb;
        AnswerSelectedOptionDb answerSelectedOptionDb;
        //table
        public TblTaskQuestionAnswerResponse TmpTaskQuestionAnswerModels;

        public TaskSummaryViewModel(INavigation navigation, VisitLocationTaskResponse _visitLocationTaskResponse, string StoreAddress_, bool CheckList_) : base(navigation)
        {
            #region SupWorkflowOffline check
            if (Application.Current.Properties.ContainsKey("SupWorkflowOffline") && CheckList_)
            {
                SupWorkflowOffline = Convert.ToBoolean(Application.Current.Properties["SupWorkflowOffline"].ToString());
            }
            #endregion

            supVisitScheduleDb = new VisitScheduleDb();
            taskMasterQuestionDb = new TaskMasterQuestionDb();
            questionMasterDb = new QuestionMasterDb();
            fileInfoDb = new FileInfoDb();
            answerUploadedFileDb = new AnswerUploadedFileDb();
            answerSelectedOptionDb = new AnswerSelectedOptionDb();

            taskQuestionAnswerDb = new TaskQuestionAnswerDb();
            supVisitScheduleLocationStoreDb = new VisitScheduleLocationDb();
            supVisitLocationTaskDb = new VisitLocationTaskDb();
            answerOptionDb = new AnswerOptionDb();

            visitLocationTaskResponse = _visitLocationTaskResponse;

            #region save button visible
            ////visible for promoter
            //var personRoles = CommonAttribute.CustomeProfile?.PersonAssignedRoles;
            //if (personRoles.ToList().Count() > 0)
            //{
            //    if ((long)personRoles.ToList()[0].PersonRoleId == Convert.ToInt16(PersonRoleEnum.Promoter))
            //    {
            //        TaskCompletionVisible = true;
            //    }
            //}
            
            //visible based on supervisorflow
            if (SupWorkflowOffline || !CheckList_)
            {
                TaskCompletionVisible = true;

                //save button visibility
                var VisitScheduleIsNotEdiatable = supVisitScheduleLocationStoreDb.GetvisitLocationTasksByPVisitLocationTaskId(visitLocationTaskResponse.PVisitLocationTaskId);
                if (VisitScheduleIsNotEdiatable != null)
                {
                    if (VisitScheduleIsNotEdiatable.IsNotEditable == true)
                    {
                        TaskCompletionVisible = false;
                    }
                    else
                    {
                        TaskCompletionVisible = true;
                    }
                }
            }
            #endregion

            TaskName = _visitLocationTaskResponse?.TaskMaster?.Title;
            SerialNo = (int)_visitLocationTaskResponse?.TaskMaster?.SerialNo;
            StoreAddress = StoreAddress_;
            CheckList = CheckList_;
            TaskStatusColor = (Color)_visitLocationTaskResponse?.StatusColor;
            TaskDescription = _visitLocationTaskResponse?.TaskMaster?.Description;
            TaskStatusName = _visitLocationTaskResponse?.TaskStatusName;

            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () =>
            {
                await GetTaskSummaryDetails(_visitLocationTaskResponse?.TaskMasterId,
                    _visitLocationTaskResponse?.VisitLocationTaskId);
                IsBusy = false;
            });

            SaveCommand = new Command(() =>
            {
                //obTaskQuestionAnswerToSubmitResponses = TaskQuestionAnswerToSubmitResponses;
            });

            #region PageName based on Login User/ Role
            List<int> lstRoleIds = CommonAttribute.CustomeProfile?.PersonAssignedRoles?.Select(x => x.PersonRoleId).ToList();

            //if (lstRoleIds.Contains((int)PersonRoleEnum.Promoter))
            //{
            //    PageName = "Task Summary";
            //}
            //else if ((lstRoleIds.Contains((int)PersonRoleEnum.Supervisor)) || (lstRoleIds.Contains((int)PersonRoleEnum.Manager)) || (lstRoleIds.Contains((int)PersonRoleEnum.Admin)))
            //{
            //    PageName = "Supervisor Checklist Summary";
            //}

            if (CheckList)
            {
                PageName = "Supervisor Checklist Summary";
            }
            else
            {
                PageName = "Task Summary";
            }

            //if(PageName == "Task Summary")
            //{
            //    if(_visitLocationTaskResponse?.TaskStatusId == (int)TaskStatusEnum.Closed)
            //    {
            //        TaskCompletionVisible = false;
            //    }
            //    else
            //    {
            //        TaskCompletionVisible = true;
            //    }
            //}

            #endregion
        }

        public void SubmittedAnswers(TaskQuestionAnswerToSubmitResponse taskQuestionAnswerToSubmitResponse)
        {
            IsBusy = true;
            Device.BeginInvokeOnMainThread(async () => {
                //  IsBusy = true;
                await SaveSubmittedAnswers();
                IsBusy = false;
            });
        }

        async Task SaveSubmittedAnswers()
        {
            try
            {
                //IsBusy = true;
                if (TaskQuestionAnswerToSubmitResponses == null)
                    return;

                var TaskQuestionAnswerList = new List<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswerToSubmitResponses);

                if (NetworkAvailable && SupWorkflowOffline == false)
                {
                    VisitsDataManagementSL visitsDataManagementSL = new VisitsDataManagementSL();
                    var response = (Retail.Infrastructure.RequestModels.APIResponse)await visitsDataManagementSL.PostLocationTaskQuestionAnswer(TaskQuestionAnswerList);
                    if (response.Status == (int)APIResponseEnum.Success)
                    {
                        await DisplayAlert("Success", "Saved successfully");
                        MessagingCenter.Send<VisitLocationTaskResponse>(visitLocationTaskResponse, "TaskSummaryBack");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await ErrorDisplayAlert("Error while saving data");
                    }

                    //IsBusy = false;
                    //return;
                }
                else if (SupWorkflowOffline)
                {
                    if (visitLocationTaskResponse == null)
                        return;

                    taskQuestionAnswerDb.DeletetaskQuestionAnswerByPVisitLocationTaskId(visitLocationTaskResponse.VisitLocationTaskId);

                    int i = -1;
                    foreach (var itemi in TaskQuestionAnswerList)
                    {
                        i = i + 1;

                        TmpTaskQuestionAnswerModels = new TblTaskQuestionAnswerResponse
                        {
                            //TaskQuestionAnswerId = Convert.ToInt64(itemi.TaskQuestionAnswerId),
                            AnswerText = itemi.AnswerText,
                            AnswerSign = itemi.AnswerSign,
                            AnswerBarcode = itemi.AnswerBarcode,
                            AnswerYesNo = itemi.AnswerYesNo,
                            AnswerNo = (itemi.AnswerYesNo == true) ? false : (itemi.AnswerYesNo == false) ? true : null,
                            AnswerNumber = itemi.AnswerNumber,
                            AnswerDate = itemi.AnswerDate,
                            AnswerPrice = itemi.AnswerPrice,
                            AnswerQrcode = itemi.AnswerQrcode,
                            AnswerCurrency = itemi.AnswerCurrency,
                            AnswerSelectedOptionId = itemi.AnswerSelectedOptionId,
                            AnswerSelectedOptionText = itemi.AnswerSelectedOptionText,
                            StatusId = itemi.StatusId == null ? (int)AnswerStatusEnum.Pending : itemi.StatusId,
                            AnswerEntryByPersonId = itemi.AnswerEntryByPersonId,
                            AnswerEntryDate = itemi.AnswerEntryDate != null ? (System.DateTime)itemi.AnswerEntryDate : DateTime.Today,
                            VisitLocationTaskId = itemi.VisitLocationTaskId,
                            QuestionMasterId = itemi.QuestionMasterId,
                            TaskMasterId = itemi.TaskMasterId,
                            PVisitLocationTaskId = visitLocationTaskResponse.VisitLocationTaskId //PVisitLocationTaskId
                        };

                        long primaryKeyQuestionAnswerId = taskQuestionAnswerDb.AddtaskQuestionAnswer(TmpTaskQuestionAnswerModels);

                        // add selected options
                        if (itemi.AnswerSelectedOptions != null && itemi.AnswerSelectedOptions.Count() > 0)
                        {
                            foreach (var itemk in itemi.AnswerSelectedOptions)
                            {
                                answerOptionDb.AddanswerOption(new TblAnswerOptionResponse()
                                {
                                    OptionId = itemk.OptionId,
                                    OptionName = itemk.OptionName,
                                    OptionSelected = true,
                                    QuestionMasterId = itemk.QuestionMasterId,
                                });
                            }
                        }

                        // add answer uploaded files
                        if (itemi.AnswerUploadedFiles != null && itemi.AnswerUploadedFiles.Count() > 0)
                        {
                            int j = -1;
                            foreach (var itemj in itemi.AnswerUploadedFiles)
                            {
                                j = j + 1;

                                long primaryKeyPFileInfoId = fileInfoDb.AddfileInfo(new TblFileInfoResponse
                                {
                                    FileName = itemj.FileInfo.FileName,
                                    FileTypeId = itemj.FileInfo.FileTypeId,
                                    MimeType = itemj.FileInfo.MimeType,
                                    Path = itemj.FileInfo.Path,
                                    Base64StringImage = itemj.FileInfo.Base64StringImage,
                                });
                                answerUploadedFileDb.AddanswerUploaedFile(new TblAnswerUploadedFileResponse
                                {
                                    PFileInfoId = primaryKeyPFileInfoId,
                                    PTaskQuestionAnswerId = primaryKeyQuestionAnswerId,
                                });
                            }
                        }
                    }
                    // saved success
                    if ((int)APIResponseEnum.Success == 1)
                    {
                        long PVisitLocationTaskId = (long)visitLocationTaskResponse?.PVisitLocationTaskId;
                        var visitLocationTasks = supVisitLocationTaskDb.GetvisitLocationTasksByPVisitLocationTaskId(PVisitLocationTaskId).ToList();
                        // tasks
                        if (visitLocationTasks != null && visitLocationTasks.Count() == 1)
                        {
                            var visitLocationTask = visitLocationTasks[0];
                            visitLocationTaskResponse.VisitScheduleLocationId = visitLocationTask.PVisitScheduleLocationId;
                            if (visitLocationTask.PVisitLocationTaskId > 0)
                            {
                                // updating visit location task status
                                supVisitLocationTaskDb.UpdatevisitLocationTaskStatusByPVisitLocationTaskId(PVisitLocationTaskId, TaskStatusId);
                                // fetch all task on visitschedulelocationid
                                var visitLocationTasksOnScheduledLocation = supVisitLocationTaskDb.GetvisitLocationTasksByPVisitScheduleLocationId(visitLocationTaskResponse.VisitScheduleLocationId).ToList();
                                //updating schedule location status
                                await supVisitScheduleLocationStoreDb.updateVisitCheduleLocationStatus(visitLocationTask.PVisitScheduleLocationId, visitLocationTasksOnScheduledLocation);
                            }
                        }

                        //alert message
                        await DisplayAlert("Success", "Saved Checklist Successfully");
                        MessagingCenter.Send<VisitLocationTaskResponse>(visitLocationTaskResponse, "TaskSummaryBack");
                        await Navigation.PopAsync();
                    }
                }
                else
                {
                    showToasterNoNetwork();
                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                //IsBusy = false;
            }
        }

        public async Task GetTaskSummaryDetails(long? _TaskMasterId, long? _VisitLocationTaskId)
        {
            try
            {
                IsBusy = true;
                int TaskMasterId = (int)_TaskMasterId; //1;
                int VisitLocationTaskID = (int)_VisitLocationTaskId; //1;
                List<TaskQuestionAnswerToSubmitResponse> questionAnswerResponse = null;

                if (NetworkAvailable && SupWorkflowOffline == false)
                {
                    VisitsDataManagementSL visitsDataManagementSL = new VisitsDataManagementSL();
                    questionAnswerResponse = await visitsDataManagementSL.GetLocationTaskQuestionAnswer(TaskMasterId, VisitLocationTaskID);
                }
                else if (SupWorkflowOffline)
                {
                    var taskMasterQuestions = taskMasterQuestionDb.GettaskMasterQuestionByTaskMasterId(TaskMasterId);

                    if (taskMasterQuestions != null && taskMasterQuestions.Count() > 0)
                    {
                        int i = -1;
                        var questionMasters1 = new List<TaskQuestionAnswerToSubmitResponse>();
                        foreach (var itemi in taskMasterQuestions)
                        {
                            i = i + 1;
                            var questionMasters = questionMasterDb.GetquestionMastersByQuestionMasterId(itemi.QuestionMasterId).ToList();
                            var taskQuestionAnswers = taskQuestionAnswerDb.GettaskQuestionAnswerByQuestionMasterId(itemi.QuestionMasterId, (long)_VisitLocationTaskId).ToList();

                            if (questionMasters != null && questionMasters.Count() > 0)
                            {
                                //int j = -1;
                                foreach (var itemj in questionMasters)
                                {
                                    //j = j + 1;
                                    var questionMaster = new TaskQuestionAnswerToSubmitResponse()
                                    {
                                        TaskMasterId = itemi.TaskMasterId,
                                        QuestionMasterId = itemj.QuestionMasterId,
                                        QuestionTitle = itemj.QuestionTitle,
                                        Description = itemj.Description,
                                        AnswerTypeId = itemj.AnswerTypeId,
                                        SrNo = (i + 1),
                                        IsActive = itemj.IsActive,
                                        IsMandatory = itemj.IsMandatory,
                                        SerialNo = (i + 1),
                                        IsTaskEditable = TaskCompletionVisible,
                                        //AnswerText = itemj.AnswerText,
                                        AnswerOptions = new List<AnswerOptionResponse>()
                                    };

                                    //fetch selected options
                                    var AnswerOptions = answerOptionDb.GetanswerOptionByQuestionMasterId(itemj.QuestionMasterId).ToList();
                                    if (AnswerOptions != null && AnswerOptions.Count() > 0)
                                    {
                                        foreach (var itemm in AnswerOptions)
                                        {
                                            questionMaster.AnswerOptions.Add(new AnswerOptionResponse()
                                            {
                                                OptionId = itemm.OptionId,
                                                OptionName = itemm.OptionName,
                                                OptionSelected = itemm.OptionSelected,
                                                QuestionMasterId = itemm.QuestionMasterId
                                            });
                                        }
                                    }

                                    if (taskQuestionAnswers != null && taskQuestionAnswers.Count > 0)
                                    {
                                        var taskQuestionAnswer = taskQuestionAnswers[0];
                                        questionMaster.VisitLocationTaskId = taskQuestionAnswer.PVisitLocationTaskId;
                                        questionMaster.AnswerText = taskQuestionAnswer.AnswerText;
                                        questionMaster.AnswerSign = taskQuestionAnswer.AnswerSign;
                                        questionMaster.AnswerBarcode = taskQuestionAnswer.AnswerBarcode;
                                        questionMaster.AnswerYesNo = taskQuestionAnswer.AnswerYesNo;
                                        questionMaster.AnswerNo = (taskQuestionAnswer.AnswerYesNo == true) ? false : (taskQuestionAnswer.AnswerYesNo == false) ? true : null;
                                        questionMaster.AnswerNumber = taskQuestionAnswer.AnswerNumber;
                                        questionMaster.AnswerDate = taskQuestionAnswer.AnswerDate;
                                        questionMaster.AnswerPrice = taskQuestionAnswer.AnswerPrice;
                                        questionMaster.AnswerQrcode = taskQuestionAnswer.AnswerQrcode;
                                        questionMaster.AnswerCurrency = taskQuestionAnswer.AnswerCurrency;
                                        questionMaster.AnswerSelectedOptionId = taskQuestionAnswer.AnswerSelectedOptionId;
                                        questionMaster.AnswerSelectedOptionText = taskQuestionAnswer.AnswerSelectedOptionText;
                                        questionMaster.AnswerEntryByPersonId = taskQuestionAnswer.AnswerEntryByPersonId;
                                        questionMaster.AnswerEntryDate = taskQuestionAnswer.AnswerEntryDate;
                                        questionMaster.StatusId = taskQuestionAnswer.StatusId;//(int)AnswerStatusEnum.Answered;
                                        questionMaster.AnswerUploadedFiles = new List<AnswerUploadedFileResponse>();
                                        questionMaster.AnswerSelectedOptions = new List<AnswerSelectedOptionResponse>();

                                        //fetch answer selected options
                                        var selectedOptions = answerSelectedOptionDb.GetanswerSelectedOptionByQuestionMasterId(taskQuestionAnswer.QuestionMasterId).ToList();
                                        if (selectedOptions != null && selectedOptions.Count() > 0)
                                        {
                                            foreach (var itemn in selectedOptions)
                                            {
                                                questionMaster.AnswerSelectedOptions.Add(new AnswerSelectedOptionResponse()
                                                {
                                                    OptionId = itemn.OptionId,
                                                    OptionName = itemn.OptionName,
                                                    QuestionMasterId = itemn.QuestionMasterId
                                                });
                                            }
                                        }

                                        //fetch answer uploaded files
                                        var answerUploadedFiles = answerUploadedFileDb.GetanswerUploaedFileByPTaskQuestionAnswerId(taskQuestionAnswer.PTaskQuestionAnswerId).ToList();
                                        if (answerUploadedFiles != null && answerUploadedFiles.Count() > 0)
                                        {
                                            int k = -1;
                                            foreach (var itemk in answerUploadedFiles)
                                            {
                                                k = k + 1;
                                                questionMaster.AnswerUploadedFiles.Add(new AnswerUploadedFileResponse()
                                                {
                                                    AnswerUploadedFilesId = itemk.AnswerUploadedFilesId,
                                                    //FileInfoId = itemk.FileInfoId,
                                                    //TaskQuestionAnswerId =itemk.TaskQuestionAnswerId,
                                                    FileInfo = new FileInfoResponse(),
                                                });

                                                var _AnswerUploadedFiles = questionMaster.AnswerUploadedFiles.ToList();

                                                // fetch file info upload file
                                                var fileInfos = fileInfoDb.GetfileInfoByPFileInfoId(itemk.PFileInfoId).ToList();
                                                if (fileInfos != null && fileInfos.Count() > 0)
                                                {
                                                    var fileInfo = fileInfos[0];
                                                    _AnswerUploadedFiles[k].FileInfo = new FileInfoResponse()
                                                    {
                                                        FileInfoId = Convert.ToInt32(fileInfo.FileTypeId),
                                                        FileName = fileInfo.FileName,
                                                        FileTypeId = fileInfo.FileTypeId,
                                                        MimeType = fileInfo.MimeType,
                                                        Path = fileInfo.Path,
                                                        Base64StringImage = fileInfo.Base64StringImage,
                                                    };
                                                }
                                            }
                                        }
                                    }

                                    questionMasters1.Add(questionMaster);
                                }
                            }
                        }
                        questionAnswerResponse = questionMasters1;
                    }
                }
                else
                {
                    showToasterNoNetwork();
                }

                if (questionAnswerResponse != null)
                {
                    TaskQuestionAnswerToSubmitResponses = new ObservableCollection<TaskQuestionAnswerToSubmitResponse>(questionAnswerResponse);
                }
                if (TaskQuestionAnswerToSubmitResponses.Count == 0)
                {
                    //await ErrorDisplayAlert("No data found");
                    showToasterMessage("No data found");
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
        }


        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await Navigation.PopAsync();
                });

            }
        }

        private ObservableCollection<TaskQuestionAnswerToSubmitResponse> _TaskQuestionAnswerToSubmitResponses;
        public ObservableCollection<TaskQuestionAnswerToSubmitResponse> TaskQuestionAnswerToSubmitResponses
        {
            get { return _TaskQuestionAnswerToSubmitResponses; }
            set
            {
                _TaskQuestionAnswerToSubmitResponses = value;
                OnPropertyChanged(nameof(TaskQuestionAnswerToSubmitResponses));
                OnPropertyChanged(nameof(TaskCount));
            }
        }

        private string _Answer;
        public string Answer

        {
            get { return _Answer; }
            set
            {
                _Answer = value;
                OnPropertyChanged("Answer");
            }
        }

        private string _TaskName;
        public string TaskName

        {
            get { return _TaskName; }
            set
            {
                _TaskName = value;
                OnPropertyChanged("TaskName");
            }
        }

        private string _TaskDescription;
        public string TaskDescription
        {
            get { return _TaskDescription; }
            set
            {
                _TaskDescription = value;
                OnPropertyChanged("TaskDescription");
            }
        }

        private string _TaskStatusName;
        public string TaskStatusName
        {
            get { return _TaskStatusName; }
            set
            {
                _TaskStatusName = value;
                OnPropertyChanged(nameof(TaskStatusName));
            }
        }

        private int _TaskStatusId;
        public int TaskStatusId
        {
            get { return _TaskStatusId; }
            set
            {
                _TaskStatusId = value;
                OnPropertyChanged(nameof(TaskStatusId));
            }
        }

        private string _StoreAddress;
        public string StoreAddress

        {
            get { return _StoreAddress; }
            set
            {
                _StoreAddress = value;
                OnPropertyChanged("StoreAddress");
            }
        }

        private int _SerialNo;
        public int SerialNo

        {
            get { return _SerialNo; }
            set
            {
                _SerialNo = value;
                OnPropertyChanged(nameof(SerialNo));
            }
        }

        private Color _TaskStatusColor;
        public Color TaskStatusColor

        {
            get { return _TaskStatusColor; }
            set
            {
                _TaskStatusColor = value;
                OnPropertyChanged(nameof(TaskStatusColor));
            }
        }

        public string _TaskCount;
        public string TaskCount
        {
            get
            {
                if (TaskQuestionAnswerToSubmitResponses != null
                    && TaskQuestionAnswerToSubmitResponses.Count > 0)
                {
                    int _AnsweredQuests = TaskQuestionAnswerToSubmitResponses.Where(m => m.StatusId == (int)AnswerStatusEnum.Answered).ToList().Count();
                    int _TaskCounts = TaskQuestionAnswerToSubmitResponses.Count();
                    _TaskCount = string.Format("{0}/{1}", _AnsweredQuests.ToString(), _TaskCounts.ToString());
                    if (_AnsweredQuests == 0)
                    {
                        TaskStatusName = "Open";
                        TaskStatusColor = Color.Red;
                        TaskStatusId = (int)TaskStatusEnum.Open;
                    }
                    else if (_AnsweredQuests >= 1 && _AnsweredQuests != _TaskCounts)
                    {
                        TaskStatusName = "In Progress";
                        TaskStatusColor = Color.Blue;
                        TaskStatusId = (int)TaskStatusEnum.InProgress;
                    }
                    else if (_AnsweredQuests == _TaskCounts)
                    {
                        TaskStatusName = "Closed";
                        TaskStatusColor = Color.Green;
                        TaskStatusId = (int)TaskStatusEnum.Closed;
                    }
                }
                return _TaskCount;
            }
            set
            {
                _TaskCount = value;
                OnPropertyChanged(nameof(TaskCount));
            }
        }

        private string _PageName;
        public string PageName
        {
            get { return _PageName; }
            set
            {
                _PageName = value;
                OnPropertyChanged(nameof(PageName));
            }
        }

        private bool _CheckList;
        public bool CheckList

        {
            get { return _CheckList; }
            set
            {
                _CheckList = value;
                OnPropertyChanged("CheckList");
            }
        }
    }
}


