using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Retail.Infrastructure.ResponseModels;
using Retail.ViewModels.MyVisits.TaskSummary;
using SignaturePad.Forms;
using Xamarin.Forms;

namespace Retail.Views.MyVisits.TaskSummary
{
    public partial class ManagerSignature : ContentPage
    {
        public ManagerSignatureViewModel managerSignatureViewModel { get; set; }
        public TaskQuestionAnswerToSubmitResponse TaskQuestionAnswer1 { get; set; }


        public ManagerSignature(ObservableCollection<AnswerUploadedFileResponse> AnswerUploadedFiles1,
            TaskQuestionAnswerToSubmitResponse _TaskQuestionAnswer1)
        {
            InitializeComponent();

            TaskQuestionAnswer1 = _TaskQuestionAnswer1;
            BindingContext = managerSignatureViewModel = new ManagerSignatureViewModel(Navigation, AnswerUploadedFiles1);
            managerSignatureViewModel.SignaturePadViewItem += ManagerSignatureViewModel_SignaturePadViewItem;
        }

        private void ManagerSignatureViewModel_SignaturePadViewItem(object sender, EventArgs e)
        {
            try
            {
                MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
                MessagingCenter.Send<ObservableCollection<AnswerUploadedFileResponse>>(managerSignatureViewModel.AnswerUploadedFiles, "AnswerUploadedFileSignature");
                Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
        {
            try
            {
                if (!spForms.IsBlank)
                {
                    var img = await spForms.GetImageStreamAsync(SignatureImageFormat.Png);
                    var signatureMemoryStream = new MemoryStream();
                    img.CopyTo(signatureMemoryStream);
                    byte[] data = signatureMemoryStream.ToArray();
                    managerSignatureViewModel.signatureBase64string = Convert.ToBase64String(data);

                    MessagingCenter.Send<TaskQuestionAnswerToSubmitResponse>(TaskQuestionAnswer1, "AnswerGeneral");
                    managerSignatureViewModel.ImageButtonCommand.Execute("submit");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        void TapGestureRecognizer_Tapped_Cancel(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
