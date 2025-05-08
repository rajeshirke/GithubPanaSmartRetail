using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Retail.Infrastructure.Models;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using System.IO;
using Xamarin.Essentials;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.ServiceLayer;
using System.Collections.Generic;
using Retail.Database;
using System.Linq;
using Retail.Controls;
using Rg.Plugins.Popup.Services;

namespace Retail.ViewModels.PriceTracker
{
	public class CategorySubCategoryDisplayTrackerViewModel : BaseViewModel
    {
		public CategorySubCategoryDisplayTrackerViewModel (INavigation navigation, DropDownModel _SelectedStore) : base(navigation)
        {
            IsBusy = true;
            if (_SelectedStore != null)
                SelectedStore = _SelectedStore;
            Device.InvokeOnMainThreadAsync(async () =>
            {
            });
        }

        #region Methods
        public async Task GetCategoryForDisplayTracker()
        {
            try
            {
                IsBusy = true;
                if (NetworkAvailable)
                {
                    long PersonID = (long)(CommonAttribute.CustomeProfile?.PersonId);
                    long CountryID = (long)(CommonAttribute.CustomeProfile?.CountryId);
                    CategoryDropDown = new ObservableCollection<DropDownModelForDisplayCategory>();
                    PriceDisplayTrackerManagement masterDataManagementSL = new PriceDisplayTrackerManagement();
                    List<CatSubDisplayTrackerResponse> entityKeyValueResponse = await masterDataManagementSL.GetCategoryForDisplayTracker(PersonID, CountryID, SelectedStore.Id);
                    if (entityKeyValueResponse.Count != 0)
                    {
                        foreach (var item in entityKeyValueResponse)
                        {
                            CategoryDropDown.Add(new DropDownModelForDisplayCategory { Id = item.Id, Title = item.Name, Flag = item.Flag });
                        }

                        //var list = CategoryDropDown.OrderBy(c => c.Title).ToList();
                        //CategoryDropDown = new ObservableCollection<DropDownModelForDisplayCategory>(list);
                    }
                    else
                    {
                        IsBusy = false;
                        return;
                    }
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

        public void GetSubCategoryForDisplayTracker(long categoryId)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    long PersonID = (long)(CommonAttribute.CustomeProfile?.PersonId);
                    long CountryID = (long)(CommonAttribute.CustomeProfile?.CountryId);

                    var SubCategoryDropDownNew = new ObservableCollection<DropDownModelForDisplaySubCategory>();
                    SubCategoryDropDown = SubCategoryDropDownNew;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        ObservableCollection<DropDownModelForDisplaySubCategory> SubCategoryDropDown1 = new ObservableCollection<DropDownModelForDisplaySubCategory>();
                        PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                        List<CatSubDisplayTrackerResponse> EntityKeyValueResponses = await priceDisplayTrackerManagement.GetSubCategoryForDisplayTracker(PersonID, categoryId, CountryID, SelectedStore.Id);
                        if (EntityKeyValueResponses.Count != 0)
                        {
                            foreach (var item in EntityKeyValueResponses)
                            {
                                SubCategoryDropDown1.Add(new DropDownModelForDisplaySubCategory { Id = item.Id, Title = item.Name, Flag = item.Flag });
                            }

                            //var list = SubCategoryDropDown1.OrderBy(c => c.Title).ToList();
                            //SubCategoryDropDown1 = new ObservableCollection<DropDownModelForDisplaySubCategory>(list);
                        }
                        else
                        {
                            IsBusy = false;
                            SubCategoryDropDown1 = new ObservableCollection<DropDownModelForDisplaySubCategory>();
                            SubCategoryDropDown = new ObservableCollection<DropDownModelForDisplaySubCategory>();
                            return;
                        }

                        if (SubCategoryDropDown1 != null)
                            SubCategoryDropDown = SubCategoryDropDown1;

                    });
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

            //return Task.CompletedTask;
        }
        #endregion


        #region Commands

        public Command SelectSubCategoryCommand
        {
            get
            {
                return new Command<DropDownModelForDisplaySubCategory>(async (obj) =>
                {
                    SelectedSubCategory = new DropDownModelForDisplaySubCategory();
                    if (obj != null)
                    {
                        SelectedSubCategory = obj;
                        MessagingCenter.Send<object, DropDownModelForDisplaySubCategory>(this, "SelectedSubCategoryDisplay", SelectedSubCategory);
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                        SelectedSubCategory.Id = 0;
                });
            }
        }

        public Command SelectCategoryCommand
        {
            get
            {
                return new Command<DropDownModelForDisplayCategory>(async (obj) =>
                {
                    if (obj != null)
                    {
                        SelectedCategory = obj;
                        MessagingCenter.Send<object, DropDownModelForDisplayCategory>(this, "SelectedbcategoryDisplay", SelectedCategory);
                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                        SelectedCategory.Id = 0;
                });
            }
        }

        public Command SearchCatgeoryCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    CategoryDropDown = new ObservableCollection<DropDownModelForDisplayCategory>(CategoryDropDown.Where(x => x.Title.ToLower().Contains(item.ToString().ToLower())).ToList());

                    IsBusy = false;
                });
            }
        }

        public Command SearchSubcategoryCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    SubCategoryDropDown = new ObservableCollection<DropDownModelForDisplaySubCategory>(SubCategoryDropDown.Where(x => x.Title.ToLower().Contains(item.ToString().ToLower())).ToList());

                    IsBusy = false;
                });
            }
        }

        #endregion

        #region Properties

        private DropDownModel _SelectedStore;
        public DropDownModel SelectedStore
        {
            get { return _SelectedStore; }
            set
            {
                _SelectedStore = value;
                OnPropertyChanged(nameof(SelectedStore));
            }
        }

        private string _SelectedCategoryText;
        public string SelectedCategoryText
        {
            get { return _SelectedCategoryText; }
            set
            {
                _SelectedCategoryText = value;
                OnPropertyChanged(nameof(SelectedCategoryText));
            }
        }

        private string _SelectedSubCategoryText;
        public string SelectedSubCategoryText
        {
            get { return _SelectedSubCategoryText; }
            set
            {
                _SelectedSubCategoryText = value;
                OnPropertyChanged(nameof(SelectedSubCategoryText));
            }
        }

        private bool _IsCatgeory;
        public bool IsCatgeory
        {
            get { return _IsCatgeory; }
            set
            {
                _IsCatgeory = value;
                OnPropertyChanged(nameof(IsCatgeory));
            }
        }

        private bool _IsSubCatgeory;
        public bool IsSubCatgeory
        {
            get { return _IsSubCatgeory; }
            set
            {
                _IsSubCatgeory = value;
                OnPropertyChanged(nameof(IsSubCatgeory));
            }
        }

        private ObservableCollection<DropDownModelForDisplayCategory> _CategoryDropDown;
        public ObservableCollection<DropDownModelForDisplayCategory> CategoryDropDown
        {
            get { return _CategoryDropDown; }
            set
            {
                _CategoryDropDown = value;
                OnPropertyChanged(nameof(CategoryDropDown));
            }
        }

        private DropDownModelForDisplayCategory _SelectedCategory;
        public DropDownModelForDisplayCategory SelectedCategory
        {
            get { return _SelectedCategory; }
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        private DropDownModelForDisplaySubCategory _SelectedSubCategory;
        public DropDownModelForDisplaySubCategory SelectedSubCategory
        {
            get { return _SelectedSubCategory; }
            set
            {
                _SelectedSubCategory = value;
                OnPropertyChanged(nameof(SelectedSubCategory));
            }
        }


        private ObservableCollection<DropDownModelForDisplaySubCategory> _SubCategoryDropDown;
        public ObservableCollection<DropDownModelForDisplaySubCategory> SubCategoryDropDown
        {
            get { return _SubCategoryDropDown; }
            set
            {
                _SubCategoryDropDown = value;
                OnPropertyChanged(nameof(SubCategoryDropDown));
                //SelectedSubCategory = new DropDownModel();                
            }
        }
        #endregion
    }
}


