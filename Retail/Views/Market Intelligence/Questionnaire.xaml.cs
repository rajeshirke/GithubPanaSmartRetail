using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MarketIntelligence;
using Xamarin.Forms;

namespace Retail.Views.MarketIntelligence
{
    public partial class Questionnaire : ContentPage
    {
        QuestionnaireViewModel questionnaireViewModel { get; set; }
        public int GeneralItemSrNo { get; set; } = 0;

        public Questionnaire(MarketIntel marketIntel, int MarketIntelTypeId)
        {
            InitializeComponent();

            BindingContext = questionnaireViewModel = new QuestionnaireViewModel(Navigation, marketIntel, MarketIntelTypeId);
            questionnaireViewModel.QuestionnaireViewItem += TapGestureRecognizer_Tapped_Stop;
        }

        protected override void OnAppearing()
        {
            Device.BeginInvokeOnMainThread(() => {
                #region for Photo
                MessagingCenter.Subscribe<ObservableCollection<AnswerUploadedFileResponse>>(this, "MarketAnswerUploadedFilePhoto", (PhotoUploadedFiles) =>
                {
                    if (PhotoUploadedFiles != null)
                    {
                        var PhotoUploadedFiles1 = (ObservableCollection<AnswerUploadedFileResponse>)PhotoUploadedFiles;
                        var TaskQuestionAnswerToSubmit = questionnaireViewModel.MarketIntelQuestionAnswerToSubmitResponses;

                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == GeneralItemSrNo);
                            if (result != null)
                            {
                                if (GeneralItemSrNo > 0 && GeneralItemSrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    if (TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].AnswerTypeId == (int)AnswerTypeEnum.Image)
                                    {
                                        TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].MarketIntelAnswerUploadedFiles = PhotoUploadedFiles1;
                                        questionnaireViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for Video
                MessagingCenter.Subscribe<ObservableCollection<AnswerUploadedFileResponse>>(this, "MarketAnswerUploadedFileVideo", (VideoUploadedFiles) =>
                {
                    if (VideoUploadedFiles != null)
                    {
                        var VideoUploadedFiles1 = (ObservableCollection<AnswerUploadedFileResponse>)VideoUploadedFiles;
                        var TaskQuestionAnswerToSubmit = questionnaireViewModel.MarketIntelQuestionAnswerToSubmitResponses;
                   
                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == GeneralItemSrNo);
                            if (result != null)
                            {
                                if (GeneralItemSrNo > 0 && GeneralItemSrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    if (TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].AnswerTypeId == (int)AnswerTypeEnum.Video)
                                    {
                                        TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].MarketIntelAnswerUploadedFiles = VideoUploadedFiles1;
                                        questionnaireViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for QR Code
                MessagingCenter.Subscribe<MarketIntelQuestionAnswerToSubmitResponse>(this, "MarketAnsweredQrcode", (TaskQuestionAnswer) =>
                {
                    if (TaskQuestionAnswer != null)
                    {
                        var TaskQuestionAnswer1 = (MarketIntelQuestionAnswerToSubmitResponse)TaskQuestionAnswer;
                        var TaskQuestionAnswerToSubmit = questionnaireViewModel.MarketIntelQuestionAnswerToSubmitResponses;
                   
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
                                        questionnaireViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for Bar Code
                MessagingCenter.Subscribe<MarketIntelQuestionAnswerToSubmitResponse>(this, "MarketAnsweredBarcode", (TaskQuestionAnswer) =>
                {
                    if (TaskQuestionAnswer != null)
                    {
                        var TaskQuestionAnswer1 = (MarketIntelQuestionAnswerToSubmitResponse)TaskQuestionAnswer;
                        var TaskQuestionAnswerToSubmit = questionnaireViewModel.MarketIntelQuestionAnswerToSubmitResponses;
                   
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
                                        questionnaireViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for Selection
                MessagingCenter.Subscribe<MarketIntelQuestionAnswerToSubmitResponse>(this, "MarketSelectedAnswerOption", (TaskQuestionAnswer) =>
                {
                    if (TaskQuestionAnswer != null)
                    {
                        var TaskQuestionAnswer1 = (MarketIntelQuestionAnswerToSubmitResponse)TaskQuestionAnswer;
                        var TaskQuestionAnswerToSubmit = questionnaireViewModel.MarketIntelQuestionAnswerToSubmitResponses;
                   
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
                                        questionnaireViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for Signature
                MessagingCenter.Subscribe<ObservableCollection<AnswerUploadedFileResponse>>(this, "MarketAnswerUploadedFileSignature", (SignUploadedFiles) =>
                {
                    if (SignUploadedFiles != null)
                    {
                        var SignUploadedFiles1 = (ObservableCollection<AnswerUploadedFileResponse>)SignUploadedFiles;
                        var TaskQuestionAnswerToSubmit = questionnaireViewModel.MarketIntelQuestionAnswerToSubmitResponses;
                    
                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == GeneralItemSrNo);
                            if (result != null)
                            {
                                if (GeneralItemSrNo > 0 && GeneralItemSrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    if (TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].AnswerTypeId == (int)AnswerTypeEnum.Sign)
                                    {
                                        TaskQuestionAnswerToSubmit[GeneralItemSrNo - 1].MarketIntelAnswerUploadedFiles = SignUploadedFiles1;
                                        questionnaireViewModel.TaskCount = "0";
                                    }
                                }
                            }
                        }
                    }
                });
                #endregion
                #region for general
                MessagingCenter.Subscribe<MarketIntelQuestionAnswerToSubmitResponse>(this, "AnswerGeneral", (GeneralItem) =>
                {
                    if (GeneralItem != null)
                    {
                        GeneralItemSrNo = GeneralItem.SrNo;
                        var TaskQuestionAnswerToSubmit = questionnaireViewModel.MarketIntelQuestionAnswerToSubmitResponses;

                        if (TaskQuestionAnswerToSubmit != null && TaskQuestionAnswerToSubmit.Count > 0)
                        {
                            var result = TaskQuestionAnswerToSubmit.Where(p => p.SrNo == GeneralItem.SrNo);
                            if (result != null)
                            {
                                if (GeneralItem.SrNo > 0 && GeneralItem.SrNo <= TaskQuestionAnswerToSubmit.Count)
                                {
                                    //TaskQuestionAnswerToSubmit[GeneralItem.SrNo - 1] = TaskQuestionAnswerToSubmit[GeneralItem.SrNo - 1];
                                    questionnaireViewModel.TaskCount = "0";
                                }
                            }
                        }
                    }
                });
                #endregion

            });
        }

        // save
        /// <summary>
        /// seperate save function no need.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            //var answers = sender as MarketIntelQuestionAnswerToSubmitResponse;
            questionnaireViewModel.SubmittedAnswers();
        }

        // finish
        void TapGestureRecognizer_Tapped_Stop(System.Object sender, System.EventArgs e)
        {
            // save task
            // submit answers
            questionnaireViewModel.SubmittedAnswers();
            // finish task
            questionnaireViewModel.CreateUpdateMarketIntelStartEnd(2);
        }

        // start
        void TapGestureRecognizer_Tapped_Start(System.Object sender, System.EventArgs e)
        {
            questionnaireViewModel.CreateUpdateMarketIntelStartEnd(1);
        }
    }
}
