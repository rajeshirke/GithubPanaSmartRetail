using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Retail.Hepler;
using Retail.Infrastructure.Hepler;
using Retail.Models;
using Retail.ViewModels;
using Retail.ViewModels.Dashboard;
using Syncfusion.SfRadialMenu.XForms;
using Xamarin.Forms;

namespace Retail.Views.Dashboard
{
    public partial class DashboardMasterPage : ContentPage
    {
        DashboardMasterPageViewModel viewModel;
      //  public BaseViewModel baseViewModel;
        public ToolbarItem toolbarItem { get; set; }
        double height, width;
        double height1, width1;
        public ObservableCollection<string> list;

        public DashboardMasterPage()
        {
            Console.WriteLine("RIAA_Mob DashboardMasterPage Start :" + DateTime.Now);
            InitializeComponent();
            BindingContext = viewModel = new DashboardMasterPageViewModel(Navigation);
           // baseViewModel = new BaseViewModel(Navigation);
            currentdate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            toolbarItem = new ToolbarItem
            {
                Text = "dashbord",
                IconImageSource = ImageSource.FromFile("info.png"),
                Order = ToolbarItemOrder.Primary,
                Command = viewModel.UserNotificationsCommand,
                Priority = 0
            };

            this.ToolbarItems.Add(toolbarItem);
            list = new ObservableCollection<string>();

        }

        void Handle_DragBegin(object sender, Syncfusion.SfRadialMenu.XForms.DragBeginEventArgs e)
        {
            list.Add("RadialMenu is began to drag" + "\n Start Position :" + " \t X = " + e.Position.X + " \t Y =" + e.Position.Y);
        }

        void Handle_DragEnd(object sender, Syncfusion.SfRadialMenu.XForms.DragEndEventArgs e)
        {
            if (radialMenu.CenterButtonPlacement == SfRadialMenuCenterButtonPlacement.Center)

            {
                if (e.NewValue.X >= (radialMenu.Width / 2 - radialMenu.CenterButtonRadius / 2) || e.NewValue.X <= -(radialMenu.Width / 2 - radialMenu.CenterButtonRadius / 2))
                {
                    e.Handled = true;
                }
                if (e.NewValue.Y >= (radialMenu.Height / 2 - radialMenu.CenterButtonRadius / 2) || e.NewValue.Y <= -(radialMenu.Height / 2 - radialMenu.CenterButtonRadius / 2))
                {
                    e.Handled = true;
                }
            }

            list.Add("RadialMenu is stopped dragging" + "\n Start Position :" + " \t X = " + e.OldValue.X + " \t Y =" + e.OldValue.Y + "\n End Position :" + "\t X = " + e.NewValue.X + "\t Y =" + e.NewValue.Y);
        }

        HubConnection hubConnection;
        private async Task HubConnection()
        {
            //if (!IsConnected)
            //{
                hubConnection = new HubConnectionBuilder()
               .WithUrl(ServiceEndPoints.WebAppUri + "chatter").Build();
                hubConnection.On<ChatMessage>("ReceiveMessage", async (chatMessage) =>
                {
                    if (chatMessage.UserName == CommonAttribute.CustomeProfile?.Email)
                    {
                        await viewModel.LogoutUser((int)LogoutDataEnum.ConcurrencyTimeout);
                    }
                });

                await Connect();
            //}
            
        }

        public async Task Connect()
        {
            try
            {
                if (!IsConnected)
                {
                    await hubConnection.StartAsync();
                    
                    IsConnected = true;
                }
            }
            catch (Exception e)
            {
                IsConnected = false;                
            }
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                _isConnected = value;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //kumar Adabala Edit
           // await HubConnection();
            Console.WriteLine("RIAA_Mob OnAppearing End :" + DateTime.Now);
        }

        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Alert!", "Do you really want to exit the application?", "Yes", "No");
                if (result)
                {
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                    }
                }
            });
            return true;
        }

        private void BtnMenu_Clicked(object sender, EventArgs e)
        {            
            Shell.Current.FlyoutIsPresented = true;
        }

    }
}

