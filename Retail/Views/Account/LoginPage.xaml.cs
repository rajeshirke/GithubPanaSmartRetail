using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Retail.Hepler;
using Retail.Models;
using Retail.ViewModels.Account;
using Xamarin.Forms;

namespace Retail.Views.Account
{
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel viewModel { get; set; }
        public LoginPage()
        {
            InitializeComponent();
            CommonAttribute.selectedLang = new LanguageModel() { LongName = "English", LongCode = "en", lid = 1 };
            BindingContext = viewModel = new LoginViewModel(Navigation);
#if DEBUG
            {
                viewModel.Email = "Praveenr@ae.panasonic.com";// Current.Properties["email"].ToString();
               
                viewModel.Password = "Pana@17985";// Current.Properties["password"].ToString();
            }

#else
            
#endif
            //Task.Run(AnimateBackgroud);
        }

        private async void AnimateBackgroud()
        {
            Action<double> forward = input => bgGradient.AnchorY = input;
            Action<double> backward = input => bgGradient.AnchorX = input;

            while (true)
            {
                bgGradient.Animate(name: "forward", callback: forward, start: 0, end: 1, length: 5000, easing: Easing.SinIn);
                await Task.Delay(1000);
                bgGradient.Animate(name: "backward", callback: backward, start: 1, end: 0, length: 5000, easing: Easing.SinIn);
                await Task.Delay(1000);

            }
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Picker langPicker = sender as Picker;
            //var selectedItem = langPicker.SelectedItem as LanguageModel;
            //CommonAttribute.selectedLang = selectedItem;
            ////lblLong.Text = selectedItem.LongName;
            //viewModel.ChangeLangugeCommand.Execute(selectedItem.LongCode);
            
            //if (CommonAttribute.selectedLang.LongCode == "ur")
            //{
            //    viewModel.flowDirection = FlowDirection.RightToLeft;
            //    //  Device.FlowDirection
            //    // Application.Current.MainPage.FlowDirection = FlowDirection.RightToLeft;
            //    // Application.Current.MainPage =new  LoginPage();
            //}
            //else
            //    viewModel.flowDirection = FlowDirection.LeftToRight;


            //CommonAttribute.flowDirection = viewModel.flowDirection;
            //// Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        protected async override void OnAppearing()
        {
            if (Application.Current.Properties.ContainsKey("email"))
                viewModel.Email = Application.Current.Properties["email"].ToString();
            if (Application.Current.Properties.ContainsKey("password"))
                viewModel.Password = Application.Current.Properties["password"].ToString();
            base.OnAppearing();


        }
        private void ToolbarItem_Clicked(System.Object sender, System.EventArgs e)
        {

            Device.InvokeOnMainThreadAsync(() => {
                if (pkLong.IsFocused)
                    pkLong.Unfocus();

                pkLong.Focus();
            });


        }

    }
}
