using System;
using System.Collections.Generic;
using Retail.ViewModels.Reports;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace Retail.Views.Reports
{
    public partial class CategorywiseContributionReport : ContentPage
    {
        public CategorywiseContributionViewModel viewModel { get; set; }
        public ToolbarItem toolbarItem { get; set; }
        public string SelectedMonthDate { get; set; }

        public CategorywiseContributionReport()
        {
            InitializeComponent();
            BindingContext = viewModel = new CategorywiseContributionViewModel(Navigation);

            toolbarItem = new ToolbarItem
            {
                Text = "dashbord",
                IconImageSource = ImageSource.FromFile("chart.png"),
                Order = ToolbarItemOrder.Primary,
                Command = viewModel.ChartClick,
                Priority = 0,
            };
            this.ToolbarItems.Add(toolbarItem);

            var firstDayOfMonth = new DateTime(DateTime.Today.Date.Year, DateTime.Today.Date.Month, 1);
            SelectedMonthDate = firstDayOfMonth.ToString();

            var mindate = new DateTime(2000, 01, 01);
            var maxdate = new DateTime(DateTime.Now.Date.Year, DateTime.Now.Date.Month, DateTime.Now.Date.Day);

            monthyearpicker.MinDate = mindate;
            monthyearpicker.MaxDate = maxdate;
        }

        void OnLabelTapped(object sender, EventArgs e)
        {
            Label label = sender as Label;
            Expander expander = label.Parent.Parent.Parent as Expander;

            if (label.FontSize == Device.GetNamedSize(NamedSize.Default, label))
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Large, label);
            }
            else
            {
                label.FontSize = Device.GetNamedSize(NamedSize.Default, label);
            }
            expander.ForceUpdateSize();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<string>(this, "OnlyTwoMonthYearChangeDate", async (selectedItems) =>
            {
                string selecteddate = Convert.ToDateTime(selectedItems).ToString("yyyy-MM-dd");
                var SelectedDate = String.Format("{0:yyyy-MM-dd}", selecteddate);
                SelectedMonthDate = selecteddate.ToString();
            });
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                await viewModel.GetCategoryWiseContribution(SelectedMonthDate);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
