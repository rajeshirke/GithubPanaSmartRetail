using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Controls;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Retail.ViewModels.MonthYearPickerViewModel;
using Retail.Views.Dashboard;
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;

namespace Retail.ViewModels.SalesTarget
{
    public class MonthlyTargetViewModel :  BaseViewModel
    {
        public List<int> lstPersonId { get; set; }
        public List<int> lstLocationId { get; set; }
        public List<string> lstLocationName { get; set; }       
        public string CategoryName { get; set; }
        public ICommand Command { get; }
        public ICommand SelectedSubCatCommand { get; }
        public ICommand SelectedSubcategoryItemCommand { get; }
        public bool IsExpanded { get; set; }

        public MonthlyTargetViewModel(INavigation navigation ): base(navigation)
        {
            IsExpanded = true;
            lstPersonId = new List<int>(); lstLocationId = new List<int>(); lstLocationName = new List<string>();
            SelectedMY = DateTime.Now;
            Command = CommandFactory.Create<MasterCategoryTarget>(sender =>
            {
                if (!sender.IsExpanded)
                    return;

                foreach (var item in MasterCategoryTargets)
                    item.IsExpanded = sender == item;
            });

            SelectedSubCatCommand = CommandFactory.Create<MasterCategoryTarget>(async sender =>
            {
                await Navigation.PushAsync(new SalesDataEntry());
            });

            SelectedSubcategoryItemCommand = new Command(SelectedSubcategoryItemHandler);

        }
        public string LocationNames { get; set; }
        public ObservableCollection<SelectedItems> selectedLocations { get; set; }
        public async Task SelectedLocationListsbyPerId()
        {
            IsBusy = true;
            string CityNames = "";
            Locations = new ObservableCollection<EntityKeyValueResponse>();
            if(selectedLocations! ==null || selectedLocations.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("", "Please select store.", "Ok");
                return;
            }
            foreach (var item in selectedLocations)
            {
                if (item.ID != 0)
                {
                    Locations.Add(new EntityKeyValueResponse
                    { Id = item.ID, Name = item.Name });

                    CityNames = CityNames + item.Name + ",";
                }

            }
            LocationNames = CityNames.TrimEnd(',');

            IsBusy = true;
           // SelectedMY = monthyearpicker.Date;
            DateTime MonthYear = SelectedMY;
            if (Locations != null && Locations.Count != 0)
                await GetPromoterTargetSummary(Locations, MonthYear);
            else
                await Application.Current.MainPage.DisplayAlert("", "Please select store.", "Ok");
        }
        public Command RefreshSalesTarget
        {
            get
            {
                return new Command(async () =>
                {
                    await GetPromoterTargetSummary(Locations, SelectedMY);
                });
            }
        }

        private async void SelectedSubcategoryItemHandler(object obj)
        {
            var salesTargetSummary = (SalesTargetSummaryByCategory)obj;
            await PopupNavigation.Instance.PushAsync(new SubcategorySalesEntriesPopup(salesTargetSummary.CategoryId, SelectedMY, salesTargetSummary.CategoryName));
        }

        public async Task GetPromoterTargetSummary(ObservableCollection<EntityKeyValueResponse> Locations,DateTime selectedmonthyear)
        {
            try
            {
                IsBusy = true;
               // await Task.Delay(1000);
                if (NetworkAvailable)
                {
                    IsBusy = true;
                    //ItemsViewModel viewModel = new ItemsViewModel(DependencyService.Get<INumberPickerDialog>());
                    //string x = viewModel.Title;
                    if (Locations != null && selectedmonthyear != null)
                    {
                        lstLocationId = new List<int>(); lstLocationName = new List<string>(); lstPersonId = new List<int>();
                        lstPersonId.Add((int)CommonAttribute.CustomeProfile.PersonId);
                        foreach (var item in Locations)
                        {
                            lstLocationId.Add(item.Id); lstLocationName.Add(item.Name);
                        }

                        lstPromoterDetails = new PromoterDetails()
                        {
                            PersonIds = lstPersonId,
                            LocationIds = lstLocationId,
                            LocationNames = lstLocationName,
                            Month = selectedmonthyear.Month,
                            Year = selectedmonthyear.Year
                        };
                        if (lstPromoterDetails != null)
                        {
                            SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                            PromoterTargetsOverviewResponse promoterTargetsOverview = await salesTargetManagementSL.GetPromoterTargets(lstPromoterDetails);
                            MasterCategoryTargets = promoterTargetsOverview.MasterCategoryTargets.ToList();
                            
                            Targets = promoterTargetsOverview.MainTargetSummary.TotalTargets;
                            AchievedTargets = promoterTargetsOverview.MainTargetSummary.AchievedTargets;
                            BalanceTargets = promoterTargetsOverview.MainTargetSummary.BalanceTargets;
                            AchievedTargetPercentage = promoterTargetsOverview.MainTargetSummary.AchievedTargetPercentage;

                            IsBusy = false;
                        }

                    }


                    //SubCategoryHeight = (MasterCategoryTargets.FirstOrDefault().SubCategoryTargets.Count) * 30;

                    //if (MasterCategoryTargets.Count == 0)
                    //{
                    //    await ErrorDisplayAlert("No data found");
                    //    IsBusy = false;
                    //    return;
                    //}
                }
                else
                {
                    //showToasterNoNetwork();
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

        public async Task<List<AccordionSource>> GetSampleDataAsync()
        {
            var vResult = new List<AccordionSource>();

            if (NetworkAvailable)
            {
                SalesTargetManagementSL salesTargetManagement = new SalesTargetManagementSL();
                //SalesTargetWithSummaryResponse salesTargetWithSummary = await salesTargetManagement.GetSalesTargetWithSummaryByPersonId(PersonId);

                SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                PromoterTargetsOverviewResponse promoterTargetsOverview = await salesTargetManagementSL.GetPromoterTargets(lstPromoterDetails);

                var masterCategoryTargets = promoterTargetsOverview.MasterCategoryTargets.ToList();
                //var lstSubcategoryTarget = new List<SalesTargetSummary>();

                foreach (var item in masterCategoryTargets)
                {

                    var _Subcategory = new ListView()
                    {
                        ItemsSource = item.SubCategoryTargets,
                        HeightRequest = item.SubCategoryTargets.Count * 40,
                        //Margin = new Thickness(10, 0, 0, 0),
                        SeparatorColor = Color.LightGray,
                        ItemTemplate = new DataTemplate(typeof(ListDataViewCell))
                    
                    };
                    _Subcategory.ItemTapped += _Subcategory_ItemTapped;
                    var _Maincategory = new AccordionSource()
                    {
                        Maincategory = item.MasterTargetSummary.CategoryName,
                        Target=item.MasterTargetSummary.TotalTargets,
                        Achieved= item.MasterTargetSummary.AchievedTargets,
                        Balance=item.MasterTargetSummary.BalanceTargets,

                        HeaderTextColor = Color.Black,
                        ContentItems = _Subcategory,
                    };
                    vResult.Add(_Maincategory);
                }
            }
            else
            {
                //showToasterNoNetwork();
            }

            return vResult;
        }

        private async void _Subcategory_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vListItem = e.Item as SalesTargetSummaryByCategory;
            //var vMessage = "You Clicked on " + vListItem.CategoryName;
            //await ErrorDisplayAlert(vMessage);
            await Navigation.PushAsync(new SalesDataEntry());
        }
       
        public class ListDataViewCell : ViewCell
        {
            
            public ListDataViewCell()
            {
               

                var CategoryName = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Small),
                    WidthRequest = 120,
                    HeightRequest=25,
                    TextColor = Color.Black,
                    Margin = new Thickness (10,0,0,0),                    
                    HorizontalOptions=LayoutOptions.End,
                    
                };
                var TotalTargets = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Small),
                    WidthRequest = 70,
                    HeightRequest = 25,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    Margin = new Thickness (14,0,0,0),
                    TextColor = Color.Black,
                    
                };
                var AchievedTargets = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Small),
                    WidthRequest =40,
                    HeightRequest = 25,
                    HorizontalOptions =LayoutOptions.EndAndExpand,
                    Margin = new Thickness(10, 0, 4, 0),
                    TextColor = Color.Green,
                    

                };
                var BalanceTargets = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Small),
                    WidthRequest = 70,
                    HeightRequest = 25,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    //Padding = new Thickness(10, 0, 0, 0),
                    TextColor = Color.Red,
                   

                };
                //var AchievedTargetPercentage = new Label()
                //{
                //    Font = Font.SystemFontOfSize(NamedSize.Default),
                //    WidthRequest = 70,
                //    HeightRequest = 45,
                //    HorizontalOptions = LayoutOptions.EndAndExpand,
                //    TextColor = Color.Blue
                //};
                var SalesEntryIcon = new Image()
                {
                    Source= "SalesEntry.png",
                    WidthRequest = 20,
                    HeightRequest=20,
                    HorizontalOptions = LayoutOptions.End,
                    VerticalOptions=LayoutOptions.Start,
                    
                };

                

                CategoryName.SetBinding(Label.TextProperty,new Binding("CategoryName"));
                TotalTargets.SetBinding(Label.TextProperty, new Binding("TotalTargets"));
                AchievedTargets.SetBinding(Label.TextProperty, new Binding("AchievedTargets"));
                BalanceTargets.SetBinding(Label.TextProperty, new Binding("BalanceTargets"));
                //AchievedTargetPercentage.SetBinding(Label.TextProperty, new Binding("AchievedTargetPercentage"));

               
                View = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Margin = new Thickness(10), //12,8                    
                    Children = { CategoryName, TotalTargets, AchievedTargets, BalanceTargets, SalesEntryIcon }
                };
            }
        }

        public string stringFormat(string str, string align, int length)
        {
            if (align == "Right")
                str = String.Format("{0," + length + "}", str);
            else if (align == "Center")
                str = String.Format("{0,-" + length + "}", String.Format("{0," + ((length + str.Length) / 2).ToString() + "}", str));
            else
                str = String.Format("{0,-" + length + "}", str);

            return str + " ";
        }

        public string EmptyViewText
        {
            get => "No data found";
        }

        private ObservableCollection<EntityKeyValueResponse> _Locations;
        public ObservableCollection<EntityKeyValueResponse> Locations
        {
            get { return _Locations; }
            set
            {
                _Locations = value;
                OnPropertyChanged("Locations");
            }
        }

        private PromoterTargetsOverviewResponse _PromoterTargetsOverview;
        public PromoterTargetsOverviewResponse PromoterTargetsOverview
        {
            get { return _PromoterTargetsOverview; }
            set
            {
                _PromoterTargetsOverview = value;
                OnPropertyChanged("PromoterTargetsOverview");
            }
        }
        
        private SalesTargetSummaryByCategory _SubCategoryTarget;
        public SalesTargetSummaryByCategory SubCategoryTarget
        {
            get { return _SubCategoryTarget; }
            set
            {
                _SubCategoryTarget = value;
                OnPropertyChanged(nameof(SubCategoryTarget));
            }
        }

        private List<MasterCategoryTarget> _MasterCategoryTarget;
        public List<MasterCategoryTarget> MasterCategoryTargets
        {
            get { return _MasterCategoryTarget; }
            set
            {
                _MasterCategoryTarget = value;
                OnPropertyChanged(nameof(MasterCategoryTargets));
                //OnPropertyChanged(nameof(Items));
            }
        }

        private int _SubCategoryHeight;
        public int SubCategoryHeight

        {
            get { return _SubCategoryHeight; }
            set
            {
                _SubCategoryHeight = value;
                OnPropertyChanged(nameof(SubCategoryHeight));
            }
        }

        private PromoterDetails _PromoterDetails;
        public PromoterDetails lstPromoterDetails

        {
            get { return _PromoterDetails; }
            set
            {
                _PromoterDetails = value;
                OnPropertyChanged("lstPromoterDetails");
            }
        }

        private int _TotalTargets;
        public int Targets

        {
            get { return _TotalTargets; }
            set
            {
                _TotalTargets = value;
                OnPropertyChanged("Targets");
            }
        }

        private int _AchievedTargets;
        public int AchievedTargets

        {
            get { return _AchievedTargets; }
            set
            {
                _AchievedTargets = value;
                OnPropertyChanged("AchievedTargets");
            }
        }

        private int _BalanceTargets;
        public int BalanceTargets

        {
            get { return _BalanceTargets; }
            set
            {
                _BalanceTargets = value;
                OnPropertyChanged("BalanceTargets");
            }
        }


        private double _AchievedTargetPercentage;
        public double AchievedTargetPercentage

        {
            get { return _AchievedTargetPercentage; }
            set
            {
                _AchievedTargetPercentage = value;
                OnPropertyChanged("AchievedTargetPercentage");
            }
        }

        private DateTime _SelectedMY;
        public DateTime SelectedMY

        {
            get { return _SelectedMY; }
            set
            {
                _SelectedMY = value;
                OnPropertyChanged(nameof(SelectedMY));
            }
        }
    }
}
