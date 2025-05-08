using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Retail.Controls;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MyVisits;
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Retail.Views.MyVisits
{
    public partial class TaskSummaryView : ContentPage
    {
        public  ListView listView;

        TaskSummaryViewModel taskSummaryViewModel { get; set; }
        public int GeneralItemSrNo { get; set; } = 0;

        public TaskSummaryView(VisitLocationTaskResponse visitLocationTaskResponse, string StoreAddress, bool CheckList)
        {
            InitializeComponent();
            BindingContext= taskSummaryViewModel = new TaskSummaryViewModel(Navigation, visitLocationTaskResponse, StoreAddress, CheckList);
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => {
                #region for Photo
                MessagingCenter.Subscribe<ObservableCollection<AnswerUploadedFileResponse>>(this, "AnswerUploadedFilePhoto", (PhotoUploadedFiles) =>
                {
                    if(PhotoUploadedFiles != null)
                    {
                        var PhotoUploadedFiles1 = (ObservableCollection<AnswerUploadedFileResponse>)PhotoUploadedFiles;
                        var TaskQuestionAnswerToSubmit = taskSummaryViewModel.TaskQuestionAnswerToSubmitResponses;

                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == GeneralItemSrNo);
                            if (result != null)
                            {
                                if (GeneralItemSrNo > 0 && GeneralItemSrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    if (TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].AnswerTypeId == (int)AnswerTypeEnum.Image)
                                    {
                                        TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].AnswerUploadedFiles = PhotoUploadedFiles1;
                                        taskSummaryViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for Video
                MessagingCenter.Subscribe<ObservableCollection<AnswerUploadedFileResponse>>(this, "AnswerUploadedFileVideo", (VideoUploadedFiles) =>
                {
                    if (VideoUploadedFiles != null)
                    {
                        var VideoUploadedFiles1 = (ObservableCollection<AnswerUploadedFileResponse>)VideoUploadedFiles;
                        var TaskQuestionAnswerToSubmit = taskSummaryViewModel.TaskQuestionAnswerToSubmitResponses;

                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == GeneralItemSrNo);
                            if (result != null)
                            {
                                if (GeneralItemSrNo > 0 && GeneralItemSrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    if (TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].AnswerTypeId == (int)AnswerTypeEnum.Video)
                                    {
                                        TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].AnswerUploadedFiles = VideoUploadedFiles1;
                                        taskSummaryViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for QR Code
                MessagingCenter.Subscribe<TaskQuestionAnswerToSubmitResponse>(this, "AnsweredQrcode", (TaskQuestionAnswer) =>
                {
                    if (TaskQuestionAnswer != null)
                    {
                        var TaskQuestionAnswer1 = (TaskQuestionAnswerToSubmitResponse)TaskQuestionAnswer;
                        var TaskQuestionAnswerToSubmit = taskSummaryViewModel.TaskQuestionAnswerToSubmitResponses;

                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == TaskQuestionAnswer.SrNo);
                            if (result != null)
                            {
                                if (TaskQuestionAnswer.SrNo > 0 && TaskQuestionAnswer.SrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    if (TaskQuestionAnswerToSubmit[TaskQuestionAnswer.SrNo - 1].AnswerTypeId == (int)AnswerTypeEnum.QRcode)
                                    {
                                        TaskQuestionAnswerToSubmit[TaskQuestionAnswer.SrNo - 1] = TaskQuestionAnswer1;
                                        taskSummaryViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for Bar Code
                MessagingCenter.Subscribe<TaskQuestionAnswerToSubmitResponse>(this, "AnsweredBarcode", (TaskQuestionAnswer) =>
                {
                    if (TaskQuestionAnswer != null)
                    {
                        var TaskQuestionAnswer1 = (TaskQuestionAnswerToSubmitResponse)TaskQuestionAnswer;
                        var TaskQuestionAnswerToSubmit = taskSummaryViewModel.TaskQuestionAnswerToSubmitResponses;

                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == TaskQuestionAnswer.SrNo);
                            if (result != null)
                            {
                                if (TaskQuestionAnswer.SrNo > 0 && TaskQuestionAnswer.SrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    if (TaskQuestionAnswerToSubmit[TaskQuestionAnswer.SrNo - 1].AnswerTypeId == (int)AnswerTypeEnum.Barcode)
                                    {
                                        TaskQuestionAnswerToSubmit[TaskQuestionAnswer.SrNo - 1] = TaskQuestionAnswer1;
                                        taskSummaryViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for Selection
                MessagingCenter.Subscribe<TaskQuestionAnswerToSubmitResponse>(this, "SelectedAnswerOption", (TaskQuestionAnswer) =>
                {
                    if (TaskQuestionAnswer != null)
                    {
                        var TaskQuestionAnswer1 = (TaskQuestionAnswerToSubmitResponse)TaskQuestionAnswer;
                        var TaskQuestionAnswerToSubmit = taskSummaryViewModel.TaskQuestionAnswerToSubmitResponses;

                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == TaskQuestionAnswer.SrNo);
                            if (result != null)
                            {
                                if (TaskQuestionAnswer.SrNo > 0 && TaskQuestionAnswer.SrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    if (TaskQuestionAnswerToSubmit[TaskQuestionAnswer.SrNo - 1].AnswerTypeId == (int)AnswerTypeEnum.Selection)
                                    {
                                        TaskQuestionAnswerToSubmit[TaskQuestionAnswer.SrNo - 1] = TaskQuestionAnswer1;
                                        taskSummaryViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for Signature
                MessagingCenter.Subscribe<ObservableCollection<AnswerUploadedFileResponse>>(this, "AnswerUploadedFileSignature", (SignUploadedFiles) =>
                {
                    if (SignUploadedFiles != null)
                    {
                        var SignUploadedFiles1 = (ObservableCollection<AnswerUploadedFileResponse>)SignUploadedFiles;
                        var TaskQuestionAnswerToSubmit = taskSummaryViewModel.TaskQuestionAnswerToSubmitResponses;

                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == GeneralItemSrNo);
                            if (result != null)
                            {
                                if (GeneralItemSrNo > 0 && GeneralItemSrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    if (TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].AnswerTypeId == (int)AnswerTypeEnum.Sign)
                                    {
                                        TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].AnswerUploadedFiles = SignUploadedFiles1;
                                        taskSummaryViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for general
                MessagingCenter.Subscribe<TaskQuestionAnswerToSubmitResponse>(this, "AnswerGeneral", (GeneralItem) =>
                {
                    try
                    {
                        if (GeneralItem != null)
                        {
                            GeneralItemSrNo = GeneralItem.SrNo;
                            var TaskQuestionAnswerToSubmit = taskSummaryViewModel.TaskQuestionAnswerToSubmitResponses;

                            if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                            {
                                var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == GeneralItem.SrNo);
                                if (result != null)
                                {
                                    if (GeneralItem.SrNo > 0 && GeneralItem.SrNo <= TaskQuestionAnswerToSubmit.Count)
                                    {
                                        //TaskQuestionAnswerToSubmit[GeneralItem.SrNo - 1] = TaskQuestionAnswerToSubmit[GeneralItem.SrNo - 1];
                                        taskSummaryViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debugger.Log(0, null, ex.ToString());
                    }
                });
                #endregion

            });
        }

        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            var answers = sender as TaskQuestionAnswerToSubmitResponse;
            taskSummaryViewModel.SubmittedAnswers(answers);
        }

    }
}

public class AddControls
{
    public Label Label { get; set; }
    public Entry Entry { get; set; }
    public Button Button { get; set; }
}
