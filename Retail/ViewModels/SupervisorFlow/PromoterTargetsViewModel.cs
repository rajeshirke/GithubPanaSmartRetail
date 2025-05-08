using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Retail.Controls;
using Retail.DependencyServices;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Views.SalesTargetViews;
using Rg.Plugins.Popup.Services;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using static Retail.Models.ReportsResponses;
using static Retail.Views.CommonPages.MultiselectPopupView;

namespace Retail.ViewModels.SupervisorFlow
{
    public class PromoterTargetsViewModel : BaseViewModel
    {
        public List<int> _PersonIds { get; set; }        
        public List<int> _LocationIds { get; set; }
        public List<string> _LocationNames { get; set; }
        public ICommand Command { get; }
        public ICommand SelectedSubCatCommand { get; }
        public ICommand SelectedSubcategoryItemCommand { get; }
        public int Month { get; set; }
        public int Year { get; set; }    
        private List<EntityKeyValueResponse> _Location;
        public List<EntityKeyValueResponse> Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged("Location");
            }
        }

        public PromoterTargetsViewModel(INavigation navigation):base(navigation)
        {
            SelectedMandY = DateTime.Now;
            Month = SelectedMandY.Month;
            Year = SelectedMandY.Year;

            Command = CommandFactory.Create<MasterCategoryTarget>(sender =>
            {
                if (!sender.IsExpanded)
                    return;

                foreach (var item in MasterCategoryTargets)
                    item.IsExpanded = sender == item;
            });

            SelectedSubCatCommand = CommandFactory.Create<MasterCategoryTarget>(sender =>
            {
                //if (SubCategoryTarget == null)
                //    return;

                Navigation.PushAsync(new SalesDataEntry());
                //SubCategoryTarget = null;
            });

            SelectedSubcategoryItemCommand = new Command(SelectedSubcategoryItemHandler);
        }

        private async void SelectedSubcategoryItemHandler(object obj)
        {
            var salesTargetSummary = (SalesTargetSummaryByCategory)obj;
            await PopupNavigation.Instance.PushAsync(new SubcategorySalesEntriesPopup(salesTargetSummary.CategoryId, SelectedMandY, salesTargetSummary.CategoryName));
        }


        public async Task GetPromoterTargetSummary(ObservableCollection<Cities> cities, ObservableCollection<SelectedItems> Persons ,DateTime Selectedmonthyear)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    if ((cities != null || cities.Count != 0) && (Persons != null || Persons.Count != 0) && Selectedmonthyear != null)
                    {
                        _PersonIds = new List<int>(); _LocationIds = new List<int>(); _LocationNames = new List<string>();
                        foreach (var item in Persons)
                        {
                            _PersonIds.Add(item.ID);
                        }
                        foreach (var item in cities)
                        {
                            _LocationIds.Add(item.ID); _LocationNames.Add(item.Name);
                        }

                        _PromoterDetailsList = new PromoterDetails()
                        {
                            PersonIds = _PersonIds,
                            LocationIds = _LocationIds,
                            LocationNames = _LocationNames,
                            Month = Selectedmonthyear.Month,
                            Year = Selectedmonthyear.Year
                        };
                        if (_PromoterDetailsList != null)
                        {
                            SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                            PromoterTargetsOverviewResponse promoterTargetsOverview = await salesTargetManagementSL.GetPromoterTargets(_PromoterDetailsList);
                            MasterCategoryTargets = new ObservableCollection<MasterCategoryTarget>();
                            ObservableCollection<MasterCategoryTarget> MasterCategoryTargets1 = new ObservableCollection<MasterCategoryTarget>(promoterTargetsOverview.MasterCategoryTargets.ToList());
                            MasterCategoryTargets = MasterCategoryTargets1;

                            Targets = promoterTargetsOverview.MainTargetSummary.TotalTargets;
                            AchievedTargets = promoterTargetsOverview.MainTargetSummary.AchievedTargets;
                            BalanceTargets = promoterTargetsOverview.MainTargetSummary.BalanceTargets;
                            AchievedTargetPercentage = promoterTargetsOverview.MainTargetSummary.AchievedTargetPercentage;


                        }
                    }
                    IsBusy = false;

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

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    SalesTargetManagementSL salesTargetManagement = new SalesTargetManagementSL();
                    //SalesTargetWithSummaryResponse salesTargetWithSummary = await salesTargetManagement.GetSalesTargetWithSummaryByPersonId(PersonId);

                    SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                    PromoterTargetsOverviewResponse promoterTargetsOverview = await salesTargetManagementSL.GetPromoterTargets(_PromoterDetailsList);

                    var masterCategoryTargets = promoterTargetsOverview.MasterCategoryTargets.ToList();
                    //var lstSubcategoryTarget = new List<SalesTargetSummary>();

                    foreach (var item in masterCategoryTargets)
                    {

                        var _Subcategory = new ListView()
                        {
                            ItemsSource = item.SubCategoryTargets,
                            ItemTemplate = new DataTemplate(typeof(ListDataViewCell))
                        };
                        _Subcategory.ItemTapped += _Subcategory_ItemTappe; ;
                        var _Maincategory = new AccordionSource()
                        {
                            Maincategory = item.MasterTargetSummary.CategoryName + "                    " + item.MasterTargetSummary.TotalTargets + "       " +
                            item.MasterTargetSummary.AchievedTargets + "       " + item.MasterTargetSummary.BalanceTargets + "       " + item.MasterTargetSummary.AchievedTargetPercentage,

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
            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
            finally
            {
                IsBusy = false;
            }

            return vResult;
        }

        private async void _Subcategory_ItemTappe(object sender, ItemTappedEventArgs e)
        {
            var vListItem = e.Item as SalesTargetSummaryByCategory;
            var vMessage = "You Clicked on " + vListItem.CategoryName;
            await ErrorDisplayAlert(vMessage);
        }

        public class ListDataViewCell : ViewCell
        {
            public ListDataViewCell()
            {

                var CategoryName = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Default),
                    WidthRequest = 100,
                    TextColor = Color.Black
                };
                var TotalTargets = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Default),
                    WidthRequest = 70,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    TextColor = Color.Black
                };
                var AchievedTargets = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Default),
                    WidthRequest = 70,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    TextColor = Color.Green
                };
                var BalanceTargets = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Default),
                    WidthRequest = 70,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    TextColor = Color.Red
                };
                var AchievedTargetPercentage = new Label()
                {
                    Font = Font.SystemFontOfSize(NamedSize.Default),
                    WidthRequest = 70,
                    HorizontalOptions = LayoutOptions.EndAndExpand,
                    TextColor = Color.Blue
                };
                CategoryName.SetBinding(Label.TextProperty, new Binding("CategoryName"));
                TotalTargets.SetBinding(Label.TextProperty, new Binding("TotalTargets"));
                AchievedTargets.SetBinding(Label.TextProperty, new Binding("AchievedTargets"));
                BalanceTargets.SetBinding(Label.TextProperty, new Binding("BalanceTargets"));
                AchievedTargetPercentage.SetBinding(Label.TextProperty, new Binding("AchievedTargetPercentage"));


                View = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                    HorizontalOptions = LayoutOptions.StartAndExpand,
                    Padding = new Thickness(12, 8), //12,8                    
                    Children = { CategoryName, TotalTargets, AchievedTargets, BalanceTargets, AchievedTargetPercentage }
                };
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

        private ObservableCollection<MasterCategoryTarget> _MasterCategoryTarget;
        public ObservableCollection<MasterCategoryTarget> MasterCategoryTargets
        {
            get { return _MasterCategoryTarget; }
            set
            {
                _MasterCategoryTarget = value;
                OnPropertyChanged(nameof(MasterCategoryTargets));
            }
        }

        private PromoterDetails _PromoterDetails;
        public PromoterDetails _PromoterDetailsList

        {
            get { return _PromoterDetails; }
            set
            {
                _PromoterDetails = value;
                OnPropertyChanged("_PromoterDetailsList");
            }
        }

        private int _PersonId;
        public int PersonId

        {
            get { return _PersonId; }
            set
            {
                _PersonId = value;
                OnPropertyChanged("PersonId");
            }
        }

        private int _LocationId;
        public int LocationId

        {
            get { return _LocationId; }
            set
            {
                _LocationId = value;
                OnPropertyChanged("LocationId");
            }
        }

        private DateTime  _SelectedMandY;
        public DateTime SelectedMandY

        {
            get { return _SelectedMandY; }
            set
            {
                _SelectedMandY = value;
                OnPropertyChanged("SelectedMandY");
            }
        }
    }
}

