using System;
using System.Collections.ObjectModel;
using Retail.Views.PriceTracker;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using Xamarin.Forms;
using Retail.Infrastructure.Enums;
using Retail.Infrastructure.RequestModels;
using Retail.Infrastructure.ResponseModels;
using Retail.Models;
using System.IO;
using Xamarin.Essentials;
using Retail.DependencyServices;
using System.Threading.Tasks;
using Retail.Hepler;
using Retail.Infrastructure.ServiceLayer;
using System.Collections.Generic;
using Retail.Database;
using Retail.Infrastructure.Models;
using System.Linq;
using Retail.Controls;
using System.Diagnostics;
using Retail.Views.CommonPages;
using Syncfusion.Data.Extensions;

namespace Retail.ViewModels.PriceTracker
{
    public class DisplayTrackerViewModel : BaseViewModel
    {

        public DisplayTrackerViewModel(INavigation navigation) : base(navigation)
        {
            IsBusy = true;
            this.TotalCompetitorUnits = 0;
            this.TotalPanasonicUnits = 0;
            this.PanaListRowHeight = 30;
            this.PanaDetailsHeight = 150;
            this.ObProductModelResponses = new ObservableCollection<DisplayTrackerProductModelEntryRequest>();
            this.ObDispalyModelsResponse = new ObservableCollection<BrandNameRequest>();

            if (CommonAttribute.UploadedobDisplayTrackerEntryImageRequest != null && CommonAttribute.UploadedobDisplayTrackerEntryImageRequest.Count > 0)
            {
                obDisplayTrackerEntryImageRequest = new ObservableCollection<DisplayTrackerEntryImageRequest>();
                obDisplayTrackerEntryImageRequest = CommonAttribute.UploadedobDisplayTrackerEntryImageRequest;
            }
            else
            {
                obDisplayTrackerEntryImageRequest = new ObservableCollection<DisplayTrackerEntryImageRequest>();
            }

            ImageButtonCommand = new Command(ImageButtonClick);

            SelectedDate = DateTime.Now.Date.ToString("dd,MMMM,yyyy");
            if (!(String.IsNullOrEmpty(CommonAttribute.PrvSelectedDateDisplay)))
            {
                SelectedDate = CommonAttribute.PrvSelectedDateDisplay;
            }
            Device.BeginInvokeOnMainThread(async () =>
            {
                //await GetCategoryForDisplayTracker();
                await GetLocation();
                IsBusy = false;
            });
        }

        #region Methods

        public async Task GetLocation()
        {
            StoreDropDown = new ObservableCollection<DropDownModel>();

            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                    Locations = new List<EntityKeyValueResponse>();
                    Locations = await priceDisplayTrackerManagement.GetStoreForDisplayTracker((int)CommonAttribute.CustomeProfile.PersonId, (int)CommonAttribute.CustomeProfile?.CountryId);
                    if (Locations.Count != 0)
                    {
                        foreach (var item in Locations)
                        {
                            StoreDropDown.Add(new DropDownModel { Id = item.Id, Title = item.Name });
                        }

                        var list = StoreDropDown.OrderBy(snm => snm.Title);
                        StoreDropDown = new ObservableCollection<DropDownModel>(list);
                    }
                    else
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            bool gpsStatus = DependencyService.Get<ILocSettings>().isGpsAvailable();
                            if (!gpsStatus)
                                await ErrorDisplayAlert("Your gps location is not accurate");
                        }
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

        public async Task GetNoOfUnitsForModels()
        {
            try
            {
                IsBusy = true;
                int Total = 0;
                int TotalOther = 0;

                if (string.IsNullOrEmpty(SelectedDate))
                {
                    await ErrorDisplayAlert("Please select date.");
                    return;
                }
                int SelectedStoreID = 0;
                if (SelectedStore != null)
                {
                    SelectedStoreID = SelectedStore.Id;
                }
                if (SelectedStoreID == 0)
                {
                    await ErrorDisplayAlert("Please select store.");
                    return;
                }
                long SelectedCatgoryID = 0; long SelectedSubCategoryId = 0;

                long PersonId = (long)(CommonAttribute.CustomeProfile?.PersonId);

                long CountryId = (long)(CommonAttribute.CustomeProfile?.CountryId);

                if (SelectedCategory != null)
                    SelectedCatgoryID = (long)(SelectedCategory.Id);

                if (SelectedSubCategory != null)
                    SelectedSubCategoryId = (long)(SelectedSubCategory.Id);

                if(SelectedCatgoryID == 0)
                {
                    await ErrorDisplayAlert("Please select category.");
                    return;
                }

                if (SelectedSubCategoryId == 0)
                {
                    await ErrorDisplayAlert("Please select subcategory.");
                    return;
                }

                DisplayTrackerRequest displayTrackerRequest = new DisplayTrackerRequest();
                displayTrackerRequest.CategoryId = (int)SelectedCatgoryID;
                displayTrackerRequest.SubCategoryId = (int)SelectedSubCategoryId;
                displayTrackerRequest.PersonId = (int)PersonId;
                displayTrackerRequest.CountryId = (int)CountryId;
                displayTrackerRequest.EntryDate = Convert.ToDateTime(SelectedDate);
                displayTrackerRequest.LocationId = (int)(SelectedStore?.Id);

                PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                var ProductModelForDisplayTracker = await priceDisplayTrackerManagement.GetProductModelForDisplayTracker(displayTrackerRequest);

                if (ProductModelForDisplayTracker != null)
                {
                    TotalOtherCompetitorUnits = ProductModelForDisplayTracker.OtherBrandQty.ToString();

                    if (ProductModelForDisplayTracker.ProductModels != null && ProductModelForDisplayTracker.ProductModels.Count > 0)
                    {
                        //panasonic models
                        var PanasonicModels = ProductModelForDisplayTracker.ProductModels.ToList();
                        ObProductModelResponses = new ObservableCollection<DisplayTrackerProductModelEntryRequest>(PanasonicModels);
                        if (ObProductModelResponses != null && ObProductModelResponses.Count > 0)
                        {
                            var total = ObProductModelResponses.Select(s => Convert.ToInt32(s.Qty)).Sum();
                            TotalPanasonicUnits = (int)total;
                            PanaListRowHeight = ObProductModelResponses.Count * 60;
                        }
                        else
                        {
                            TotalPanasonicUnits = 0;
                            PanaListRowHeight = 40;
                        }
                        PanaDetailsHeight = PanaListRowHeight + 150;
                    }
                    if (ProductModelForDisplayTracker.DisplayTrackerListResponse != null && ProductModelForDisplayTracker.DisplayTrackerListResponse.Count > 0)
                    {
                        //history
                        ObDisplayTrackerListResponse = new ObservableCollection<DisplayTrackerListResponse>(ProductModelForDisplayTracker.DisplayTrackerListResponse);
                    }
                    if (ProductModelForDisplayTracker.BrandNameRequest != null && ProductModelForDisplayTracker.BrandNameRequest.Count > 0)
                    {
                        //competitor models
                        var CompetitorModels = ProductModelForDisplayTracker.BrandNameRequest.ToList();
                        ObDispalyModelsResponse = new ObservableCollection<BrandNameRequest>(CompetitorModels);
                        if (ObDispalyModelsResponse != null && ObDispalyModelsResponse.Count > 0)
                        {
                            var ListCompetitorModels = ObDispalyModelsResponse.Select(s => s.CompetitorModels).ToList();

                            Total = ListCompetitorModels.Select(d => d.Sum(s => Convert.ToInt32(s.Qty))).ToList().Sum();
                            TotalOther = ObDispalyModelsResponse.Select(s => Convert.ToInt32(s.OtherQty)).ToList().Sum();

                            TotalCompetitorUnits = (int)(Total + TotalOther) + ProductModelForDisplayTracker.OtherBrandQty;
                        }
                    }

                    if (ObProductModelResponses.Count == 0 && ObDispalyModelsResponse.Count == 0)
                    {
                        await ErrorDisplayAlert("No records found");
                        ObProductModelResponses = new ObservableCollection<DisplayTrackerProductModelEntryRequest>();
                        ObDispalyModelsResponse = new ObservableCollection<BrandNameRequest>();
                    }

                    TotalDisplay = (int)(Total + TotalOther) + TotalPanasonicUnits + Convert.ToInt32(TotalOtherCompetitorUnits); // TotalDisplay + item.OtherQty;

                }

                IsBusy = false;
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

        public async Task SaveDisplayTracker()
        {
            
            try
            {
                IsBusy = true;
              //  await Task.Delay(500);

                long SelectedCatgoryID = 0; long SelectedSubCategoryId = 0;
                int SelectedStoreID = 0;

                if (SelectedStore != null)
                {
                    SelectedStoreID = SelectedStore.Id;
                }
                if (SelectedStoreID == 0)
                {
                    await ErrorDisplayAlert("Please select store.");
                    return;
                }
                if (string.IsNullOrEmpty(SelectedDate))
                {
                    await ErrorDisplayAlert("Please select date.");
                    return;
                }
                if (SelectedCategory != null)
                    SelectedCatgoryID = (long)(SelectedCategory.Id);

                if (SelectedSubCategory != null)
                    SelectedSubCategoryId = (long)(SelectedSubCategory.Id);

                if (SelectedCatgoryID == 0)
                {
                    await ErrorDisplayAlert("Please select category.");
                    return;
                }

                if (SelectedSubCategoryId == 0)
                {
                    await ErrorDisplayAlert("Please select subcategory.");
                    return;
                }
                IsDisplayTrackerSaveEnable = false;

                PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                List<DisplayTrackerProductModelEntryRequest> productModelResponses = new List<DisplayTrackerProductModelEntryRequest>();
                List<BrandNameRequest> displayTrackerCompetitorModelEntryRequests = new List<BrandNameRequest>();
                List<DisplayTrackerEntryImageRequest> displayTrackerEntryImageRequests = new List<DisplayTrackerEntryImageRequest>();

                if (ObProductModelResponses != null && ObProductModelResponses.Count > 0)
                {
                    productModelResponses = new List<DisplayTrackerProductModelEntryRequest>(ObProductModelResponses);
                }
                if(ObDispalyModelsResponse != null && ObDispalyModelsResponse.Count>0)
                {
                    displayTrackerCompetitorModelEntryRequests = new List<BrandNameRequest>(ObDispalyModelsResponse);
                }
                if (CommonAttribute.UploadedobDisplayTrackerEntryImageRequest != null && CommonAttribute.UploadedobDisplayTrackerEntryImageRequest.Count > 0)
                {
                    displayTrackerEntryImageRequests = new List<DisplayTrackerEntryImageRequest>(CommonAttribute.UploadedobDisplayTrackerEntryImageRequest);
                }

                DisplayTrackerEntryRequest displayTrackerEntryRequest = new DisplayTrackerEntryRequest();
                displayTrackerEntryRequest.Entrydate = Convert.ToDateTime(SelectedDate);
                displayTrackerEntryRequest.LocationId = SelectedStoreID;
                displayTrackerEntryRequest.Comment = DisplayTrackerComment;
                displayTrackerEntryRequest.OtherBrandQty = TotalOtherCompetitorUnits.ToString();
                displayTrackerEntryRequest.PersonId= (long)(CommonAttribute.CustomeProfile?.PersonId);
                if(SelectedCategory != null && SelectedCategory.Id != 0)
                {
                    displayTrackerEntryRequest.Category = SelectedCategory?.Title;
                    displayTrackerEntryRequest.ProductCategoryId= (int)(SelectedCategory?.Id);
                }
                if (SelectedSubCategory != null && SelectedSubCategory.Id != 0)
                {
                    displayTrackerEntryRequest.SubCategory = SelectedSubCategory?.Title;
                    displayTrackerEntryRequest.ProductSubCategoryId = (int)(SelectedSubCategory?.Id);
                }
                displayTrackerEntryRequest.DisplaysTrackerProductModelEntryResponse = productModelResponses;
                displayTrackerEntryRequest.DisplayTrackerCompetitorModelEntryRequests = displayTrackerCompetitorModelEntryRequests;
                displayTrackerEntryRequest.DisplayTrackerEntryImageRequests = displayTrackerEntryImageRequests;

                var receiveContext = await priceDisplayTrackerManagement.SaveDisplayTrackerEntry(displayTrackerEntryRequest);
                if (receiveContext?.Status == Convert.ToInt16(APIResponseEnum.Success))
                {
                    CommonAttribute.UploadedobDisplayTrackerEntryImageRequest = new ObservableCollection<DisplayTrackerEntryImageRequest>();
                    await Navigation.PushAsync(new FeedBackSuccessPage("Display Tracker"));
                }
                else if (receiveContext != null)
                {
                    IsDisplayTrackerSaveEnable = true;
                    await ErrorDisplayAlert(receiveContext.ErrorMessage);
                }
                else
                {
                    IsDisplayTrackerSaveEnable = true;
                    await ErrorDisplayAlert(Resx.AppResources.lblErrorTitle);
                }
                IsBusy = false;

            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
                IsDisplayTrackerSaveEnable = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void ImageButtonClick(object obj)
        {
            FileData fileData = await TakePhotoAsync();
            DisplayOrientation orientation = DeviceDisplay.MainDisplayInfo.Orientation;
            if (fileData != null && !string.IsNullOrEmpty(fileData.FileName))
            {
                ImageSource DisplayPhoto = ImageSource.FromStream(() =>
                new MemoryStream(Convert.FromBase64String(fileData.string64baseData)));


                var newFile = Path.Combine(FileSystem.CacheDirectory, fileData.FileName);

                if (Photoaction != "Gallery")
                {
                    obDisplayTrackerEntryImageRequest.Add
                    (new DisplayTrackerEntryImageRequest
                    {
                        FileInfo=new FileInfoResponse
                        {
                            FileName = fileData.FileName,
                            FileTypeId = (int)FileTypeEnum.DisplayTrackerEntryImage,
                            Base64StringImage = fileData.string64baseData,
                            Rotation = (orientation == DisplayOrientation.Landscape) ? 0 : 90
                        }                        
                    });
                }
                else
                {
                    obDisplayTrackerEntryImageRequest.Add
                    (new DisplayTrackerEntryImageRequest
                    {
                        FileInfo = new FileInfoResponse
                        {
                            FileName = fileData.FileName,
                            FileTypeId = (int)FileTypeEnum.DisplayTrackerEntryImage,
                            Base64StringImage = fileData.string64baseData,
                            Rotation = 0
                        }                        
                    });
                }
            }
        }

        public async Task ValidateModelNumber()
        {
            if (NetworkAvailable)
            {
                MasterDataManagementSL dataManagementSL = new MasterDataManagementSL();
                if (!string.IsNullOrEmpty(ModelNumber) && ModelNumber.Length > 3)
                {
                    ModelNumberValide = false;

                    return;
                }
                long CountryID = (long)CommonAttribute.CustomeProfile?.CountryId;
                List<string> modelNumberSearchResponses = await dataManagementSL.SearchModelNumberByInitials(ModelNumber, CountryID);
                if (modelNumberSearchResponses != null)
                {
                    var exstingItem = modelNumberSearchResponses.Where(u => u.ToLower() == ModelNumber.ToLower()).ToList();
                    if (exstingItem.Count > 0)
                        ModelNumberValide = true;
                    else
                        ModelNumberValide = false;
                }
            }
            else
            {
                OfflineSearchModelNos();
            }

        }

        public void OfflineSearchModelNos()
        {
            try
            {
                var allModelNumbers = c.conn.Table<TblAllModelNumbers>().ToList();
                List<string> modelNumberSearchResponses = new List<string>();
                modelNumberSearchResponses = new List<string>(allModelNumbers.Select(c => c.ProductModelNumber).Where(m => m.ToLower().StartsWith(ModelNumber.ToLower())).ToList());
                if (modelNumberSearchResponses != null)
                {
                    var exstingItem = modelNumberSearchResponses.Where(u => u.ToLower() == ModelNumber.ToLower()).ToList();
                    if (exstingItem.Count > 0)
                        ModelNumberValide = true;
                    else
                        ModelNumberValide = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task GetCategoryForDisplayTracker()
        {
            try
            {
                IsBusy = true;
                if (NetworkAvailable)
                {
                    long PersonID = (long)(CommonAttribute.CustomeProfile?.PersonId);
                    long CountryID = (long)(CommonAttribute.CustomeProfile?.CountryId);
                    int SelectedStoreID = 0;

                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    //if (SelectedStoreID == 0)
                    //{
                    //  await ErrorDisplayAlert("Please select store.");
                    //  return;
                    //}
                    CategoryDropDown = new ObservableCollection<DropDownModelForDisplayCategory>();
                    PriceDisplayTrackerManagement masterDataManagementSL = new PriceDisplayTrackerManagement();
                    List<CatSubDisplayTrackerResponse> entityKeyValueResponse = await masterDataManagementSL.GetCategoryForDisplayTracker(PersonID, CountryID, SelectedStoreID);
                    if (entityKeyValueResponse.Count != 0)
                    {
                        foreach (var item in entityKeyValueResponse)
                        {
                            CategoryDropDown.Add(new DropDownModelForDisplayCategory { Id = item.Id, Title = item.Name, Flag = item.Flag });
                        }

                        var list = CategoryDropDown.OrderBy(c => c.Title).ToList();
                        CategoryDropDown = new ObservableCollection<DropDownModelForDisplayCategory>(list);
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

        public Task GetSubCategoryForDisplayTracker(long categoryId)
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    long PersonID = (long)(CommonAttribute.CustomeProfile?.PersonId);
                    long CountryID = (long)(CommonAttribute.CustomeProfile?.CountryId);
                    int SelectedStoreID = 0;

                    if (SelectedStore != null)
                    {
                        SelectedStoreID = SelectedStore.Id;
                    }
                    var SubCategoryDropDownNew = new ObservableCollection<DropDownModelForDisplaySubCategory>();
                    SubCategoryDropDown = SubCategoryDropDownNew;
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        ObservableCollection<DropDownModelForDisplaySubCategory> SubCategoryDropDown1 = new ObservableCollection<DropDownModelForDisplaySubCategory>();
                        PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
                        List<CatSubDisplayTrackerResponse> EntityKeyValueResponses = await priceDisplayTrackerManagement.GetSubCategoryForDisplayTracker(PersonID, categoryId, CountryID, SelectedStoreID);
                        if (EntityKeyValueResponses.Count != 0)
                        {
                            foreach (var item in EntityKeyValueResponses)
                            {
                                SubCategoryDropDown1.Add(new DropDownModelForDisplaySubCategory { Id = item.Id, Title = item.Name, Flag = item.Flag });
                            }

                            var list = SubCategoryDropDown1.OrderBy(c => c.Title).ToList();
                            SubCategoryDropDown1 = new ObservableCollection<DropDownModelForDisplaySubCategory>(list);
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

            return Task.CompletedTask;
        }

        //public async Task GetProductModelsForPriceDisplayTracker()
        //{
        //    try
        //    {
        //        IsBusy = true;

        //        if (NetworkAvailable)
        //        {
        //            if (SelectedCategory == null && SelectedSubCategory == null)
        //            {
        //                await ErrorDisplayAlert("Please select category");
        //                IsBusy = false;
        //                return;
        //            }

        //            List<EntityKeyValueResponse> entityKeyValueResponses = new List<EntityKeyValueResponse>();

        //            PriceDisplayTrackerManagement priceDisplayTrackerManagement = new PriceDisplayTrackerManagement();
        //            if (SelectedCategory != null && SelectedSubCategory != null)
        //            {
        //                entityKeyValueResponses = await priceDisplayTrackerManagement.GetProductModelForPriceDisplayTracker((long)(CommonAttribute.CustomeProfile?.PersonId), (long)(CommonAttribute.CustomeProfile?.CountryId), SelectedCategory.Id, SelectedSubCategory.Id);
        //            }
        //            else if (SelectedCategory != null)
        //            {
        //                entityKeyValueResponses = await priceDisplayTrackerManagement.GetProductModelForPriceDisplayTracker((long)(CommonAttribute.CustomeProfile?.PersonId), (long)(CommonAttribute.CustomeProfile?.CountryId), SelectedCategory.Id, 0);
        //            }
        //            if (entityKeyValueResponses != null && entityKeyValueResponses.Count != 0)
        //            {
        //                obEntityKeyValueResponse = new ObservableCollection<EntityKeyValueResponse>(entityKeyValueResponses);
                        
        //            }
        //            else
        //            {
        //                showToasterMessage("No data found");
        //                IsBusy = false;
        //                return;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debugger.Log(0, null, ex.ToString());
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}

        #region Photo
        public async Task<FileData> TakePhotoAsync()
        {
            FileData fileData = new FileData();
            try
            {
                GC.Collect();

                string action = await Application.Current.MainPage.DisplayActionSheet("Photo Upload", "Cancel", null, "Take Photo", "Gallery");
                FileResult photo = null;

                if (action != null && action == "Gallery")
                {
                    Photoaction = action;
                    photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
                    {

                    });
                }
                else if (action != null && action == "Take Photo")
                {
                    Photoaction = action;
                    if (!MediaPicker.IsCaptureSupported)
                    {
                        await ErrorDisplayAlert("Camera not supported");
                        return fileData;
                    }

                    photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
                    {

                    });

                }

                if (photo != null)
                {
                    Application.Current.Properties["Photoaction"] = Photoaction;
                    await Application.Current.SavePropertiesAsync();
                    return fileData = await LoadPhotoAsync(photo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                //return null;
            }

            return fileData;
        }

        async Task<FileData> LoadPhotoAsync(FileResult photo)
        {
            FileData fileData = new FileData();
            try
            {

                if (photo == null)
                {
                    return null;
                }
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                fileData.FileType = photo.ContentType;
                if (Device.RuntimePlatform == Device.Android)
                {
                    using (var stream = await photo.OpenReadAsync())
                    {

                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);

                        string string64base = Retail.Hepler.Extensions.ConvertToBase64(stream);
                        fileData.string64baseData = string64base;

                    }
                }
                else
                {
                    using (var stream = await photo.OpenReadAsync())
                    {
                        using (var newStream = File.OpenWrite(newFile))
                            await stream.CopyToAsync(newStream);

                        var originalImageByteArray = new Byte[(int)stream.Length];

                        stream.Seek(0, SeekOrigin.Begin);
                        stream.Read(originalImageByteArray, 0, (int)stream.Length);


                        var resizer = DependencyService.Get<IImageResizer>();
                        var resizedBytes = resizer.ResizeImage(originalImageByteArray, 1024, 1024);

                        string string64base = Convert.ToBase64String(resizedBytes);
                        fileData.string64baseData = string64base;

                    }
                }
                fileData.FileName = photo.FileName;

            }
            catch (Exception ex)
            {
                //return null;
            }

            return fileData;
        }
        #endregion

        #endregion

        #region Commands

        public Command ImageButtonCommand { get; set; }

        public Command SelectStoreCommand
        {
            get
            {
                return new Command<DropDownModel>(async (obj) =>
                {
                    if (obj != null)
                    {
                        SelectedStore = obj;
                        CommonAttribute.PrvSelectedStoreDisplay = obj;
                        MessagingCenter.Send<object, DropDownModel>(this, "SelectedStoreDisplay", SelectedStore);
                        SelectedStoreText = SelectedStore.Title;

                        await PopupNavigation.Instance.PopAsync();
                    }
                    else
                        SelectedStore.Id = 0;
                });

            }
        }

        public Command SearchStoreCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    StoreDropDown = new ObservableCollection<DropDownModel>(StoreDropDown.Where(x => x.Title.ToLower().Contains(item.ToString().ToLower())).ToList());

                    IsBusy = false;
                });
            }
        }

        public ICommand SearchPanaCompModelsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;
                    await GetNoOfUnitsForModels();
                    IsBusy = false;
                });
            }
        }

        public ICommand SaveDisplayTrackerEntries
        {
            get
            {
                return new Command(async () =>
                {
                    await SaveDisplayTracker();
                });
            }
        }

        public ICommand CalculateTotalPanasonicUnits
        {
            get
            {
                return new Command<DisplayTrackerProductModelEntryRequest>((item) =>
                {
                    //var length = item.Qty.ToString().Length;
                    if (!(string.IsNullOrEmpty(item.Qty)))
                    {
                        var total = ObProductModelResponses.Select(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty)).Sum();

                        TotalPanasonicUnits = (int)total;//TotalPanasonicUnits + item.Qty;

                        var CompetitorModels = ObDispalyModelsResponse.Select(s => s.CompetitorModels).ToList();

                        var totalComp = CompetitorModels.Select(d => d.Sum(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty))).ToList().Sum();
                        var totalOther = ObDispalyModelsResponse.Select(s => Convert.ToInt32(s.OtherQty == "" ? s.OtherQty = "0" : s.OtherQty)).ToList().Sum();

                        TotalDisplay = (int)(totalComp + totalOther) + TotalPanasonicUnits + Convert.ToInt32(TotalOtherCompetitorUnits);
                    }
                    else
                    {
                        item.Qty = "0";
                        var total = ObProductModelResponses.Select(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty)).Sum();
                        TotalPanasonicUnits = (int)total;//TotalPanasonicUnits + item.Qty;

                        var CompetitorModels = ObDispalyModelsResponse.Select(s => s.CompetitorModels).ToList();

                        var totalComp = CompetitorModels.Select(d => d.Sum(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty))).ToList().Sum();
                        var totalOther = ObDispalyModelsResponse.Select(s => Convert.ToInt32(s.OtherQty == "" ? s.OtherQty = "0" : s.OtherQty)).ToList().Sum();

                        TotalDisplay = (int)(totalComp + totalOther) + TotalPanasonicUnits + Convert.ToInt32(TotalOtherCompetitorUnits);
                    }//TotalCompetitorUnits + total;//TotalDisplay + item.Qty;
                });
            }
        }

        public ICommand CalculateTotalCompUnits
        {
            get
            {
                return new Command<DisplayTrackerCompetitorModelEntryRequest>((item) =>
                {
                    if (!(string.IsNullOrEmpty(item.Qty)))
                    {
                        var CompetitorModels = ObDispalyModelsResponse.Select(s => s.CompetitorModels).ToList();

                        var total = CompetitorModels.Select(d => d.Sum(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty))).ToList().Sum();
                        var totalOther = ObDispalyModelsResponse.Select(s => Convert.ToInt32(s.OtherQty == "" ? s.OtherQty = "0" : s.OtherQty)).ToList().Sum();

                        TotalCompetitorUnits = (int)(total + totalOther) + Convert.ToInt32(TotalOtherCompetitorUnits); //TotalCompetitorUnits + item.Qty;
                        TotalDisplay = (int)(total + totalOther) + TotalPanasonicUnits + Convert.ToInt32(TotalOtherCompetitorUnits);
                    }
                    else
                    {
                        item.Qty = "0";
                        var CompetitorModels = ObDispalyModelsResponse.Select(s => s.CompetitorModels).ToList();

                        var total = CompetitorModels.Select(d => d.Sum(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty))).ToList().Sum();
                        var totalOther = ObDispalyModelsResponse.Select(s => Convert.ToInt32(s.OtherQty == "" ? s.OtherQty = "0" : s.OtherQty)).ToList().Sum();

                        TotalCompetitorUnits = (int)(total + totalOther) + Convert.ToInt32(TotalOtherCompetitorUnits); //TotalCompetitorUnits + item.Qty;
                        TotalDisplay = (int)(total + totalOther) + TotalPanasonicUnits + Convert.ToInt32(TotalOtherCompetitorUnits);
                    }
                     //TotalDisplay + item.Qty;
                });
            }
        }

        public ICommand CalculateTotalCompUnitsOthers
        {
            get
            {
                return new Command<BrandNameRequest>((item) =>
                {
                    if (!(string.IsNullOrEmpty(item.OtherQty)))
                    {
                        var CompetitorModels = ObDispalyModelsResponse.Select(s => s.CompetitorModels).ToList();

                        var total = CompetitorModels.Select(d => d.Sum(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty))).Sum();
                        var totalOther = ObDispalyModelsResponse.Select(s => Convert.ToInt32(s.OtherQty)).ToList().Sum();

                        TotalCompetitorUnits = (int)(total + totalOther) + Convert.ToInt32(TotalOtherCompetitorUnits); //TotalCompetitorUnits + item.OtherQty;
                        TotalDisplay = (int)(total + totalOther) + TotalPanasonicUnits + Convert.ToInt32(TotalOtherCompetitorUnits);
                    }
                    else
                    {
                        item.OtherQty = "0";
                        var CompetitorModels = ObDispalyModelsResponse.Select(s => s.CompetitorModels).ToList();

                        var total = CompetitorModels.Select(d => d.Sum(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty))).Sum();
                        var totalOther = ObDispalyModelsResponse.Select(s => Convert.ToInt32(s.OtherQty)).ToList().Sum();

                        TotalCompetitorUnits = (int)(total + totalOther) + Convert.ToInt32(TotalOtherCompetitorUnits); //TotalCompetitorUnits + item.OtherQty;
                        TotalDisplay = (int)(total + totalOther) + TotalPanasonicUnits + Convert.ToInt32(TotalOtherCompetitorUnits);
                    }
                     // TotalDisplay + item.OtherQty;
                });
            }
        }

        public Command TotalOtherCompetitorUnitsCommand
        {
            get
            {
                return new Command(() =>
                {
                    if (!(string.IsNullOrEmpty(TotalOtherCompetitorUnits)))
                    {
                        var CompetitorModels = ObDispalyModelsResponse.Select(s => s.CompetitorModels).ToList();

                        var total = CompetitorModels.Select(d => d.Sum(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty))).Sum();
                        var totalOther = ObDispalyModelsResponse.Select(s => Convert.ToInt32(s.OtherQty == "" ? s.OtherQty = "0" : s.OtherQty)).ToList().Sum();

                        TotalCompetitorUnits = (int)(total + totalOther) + Convert.ToInt32(TotalOtherCompetitorUnits);
                        TotalDisplay = (int)(total + totalOther) + TotalPanasonicUnits + Convert.ToInt32(TotalOtherCompetitorUnits);
                    }
                    else
                    {
                        TotalOtherCompetitorUnits = "0";
                        var CompetitorModels = ObDispalyModelsResponse.Select(s => s.CompetitorModels).ToList();

                        var total = CompetitorModels.Select(d => d.Sum(s => Convert.ToInt32(s.Qty == "" ? s.Qty = "0" : s.Qty))).Sum();
                        var totalOther = ObDispalyModelsResponse.Select(s => Convert.ToInt32(s.OtherQty == "" ? s.OtherQty = "0" : s.OtherQty)).ToList().Sum();

                        TotalCompetitorUnits = (int)(total + totalOther) + Convert.ToInt32(TotalOtherCompetitorUnits);
                        TotalDisplay = (int)(total + totalOther) + TotalPanasonicUnits + Convert.ToInt32(TotalOtherCompetitorUnits);
                    }
                    
                });
            }
        }

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
                        await GetNoOfUnitsForModels();                        
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
                        await GetSubCategoryForDisplayTracker((long)SelectedCategory.Id);
                    }
                    else
                        SelectedCategory.Id = 0;
                });
            }
        }

        //public Command SelectStoreCommand
        //{
        //    get
        //    {
        //        return new Command<DropDownModel>(async (obj) =>
        //        {
        //            if (obj != null)
        //            {
        //                SelectedStore = obj;
        //                await GetCategoryForDisplayTracker();
        //            }
        //            else
        //                SelectedStore.Id = 0;
        //        });
        //    }
        //}

        public Command ModelsPopupCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (NetworkAvailable)
                    {
                        IsBusy = true;
                        DropDownPopupPage dropDownPopup = new DropDownPopupPage();
                        dropDownPopup.Title = "Model Number";

                        if (!string.IsNullOrEmpty(ModelNumber))
                        {
                            dropDownPopup.SetSearchKey(ModelNumber);
                            await dropDownPopup.GetModelNumber(ModelNumber);
                        }

                        dropDownPopup.DropDownSelectedItem += DropDownPopup_DropDownSelectedItem;
                        IsBusy = false;
                        await PopupNavigation.Instance.PushAsync(dropDownPopup);
                    }
                    else
                    {
                        //showToasterNoNetwork();
                        IsBusy = true;
                        DropDownPopupPage dropDownPopup = new DropDownPopupPage();
                        dropDownPopup.Title = "Model Number";

                        if (!string.IsNullOrEmpty(ModelNumber))
                        {
                            dropDownPopup.SetSearchKey(ModelNumber);
                            await dropDownPopup.GetModelNumber(ModelNumber);
                        }

                        dropDownPopup.DropDownSelectedItem += DropDownPopup_DropDownSelectedItem;
                        IsBusy = false;
                        await PopupNavigation.Instance.PushAsync(dropDownPopup);
                    }
                });
            }
        }

        private void DropDownPopup_DropDownSelectedItem(object sender, EventArgs e)
        {
            DropDownPopupPage control = sender as DropDownPopupPage;
            ModelNumber = control.SelectedItem.Title.ToUpper();
            //  ModelNumberValide=true
            ModelNumberValide = true;
            modelNumberSearchResponse = new ModelNumberSearchResponse() { ProductClassificationId = control.SelectedItem.Id, ModelName = control.SelectedItem.Title, ModelDescreption = control.SelectedItem.Desc };

        }

        public ICommand DeletePhotoCommand
        {
            get
            {
                return new Command<DisplayTrackerEntryImageRequest>((item) =>
                {
                    obDisplayTrackerEntryImageRequest.Remove(item);

                });

            }
        }

        public Command SearchByCategoryCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //await PopupNavigation.Instance.PushAsync(new PriceTrackerCategorySubcategoryPopup());
                });
            }
        }

        public Command UploadPhotoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await PopupNavigation.Instance.PushAsync(new DisplayTrackerUploadPhotoPopup());
                });
            }
        }

        public Command CancelCommand
        {
            get
            {
                return new Command(async () =>
                {
                    CommonAttribute.UploadedobDisplayTrackerEntryImageRequest = new ObservableCollection<DisplayTrackerEntryImageRequest>();
                    await Navigation.PopAsync();
                });

            }
        }

        #endregion

        #region Properties

        Connection c;

        public SalesDataEntryDb SalesDataEntryDb;

        public TmpSalesDataEntry TmpSalesData;

        public LocationStoreDb locationStoreDb;

        public ModelNumberSearchResponse modelNumberSearchResponse { get; set; }

        private string _SelectedStoreText;
        public string SelectedStoreText
        {
            get { return _SelectedStoreText; }
            set
            {
                _SelectedStoreText = value;
                OnPropertyChanged(nameof(SelectedStoreText));
            }
        }

        private int _PanaDetailsHeight;
        public int PanaDetailsHeight
        {
            get { return _PanaDetailsHeight; }
            set
            {
                _PanaDetailsHeight = value;
                OnPropertyChanged(nameof(PanaDetailsHeight));
            }
        }

        private int _PanaListRowHeight;
        public int PanaListRowHeight
        {
            get { return _PanaListRowHeight; }
            set
            {
                _PanaListRowHeight = value;
                OnPropertyChanged(nameof(PanaListRowHeight));
            }
        }

        private int _RowHeight;
        public int RowHeight
        {
            get { return _RowHeight; }
            set
            {
                _RowHeight = value;
                OnPropertyChanged(nameof(RowHeight));
            }
        }

        private string _TotalOtherCompetitorUnits;
        public string TotalOtherCompetitorUnits
        {
            get { return _TotalOtherCompetitorUnits; }
            set
            {
                _TotalOtherCompetitorUnits = value;
                
                //TotalCompetitorUnits = TotalCompetitorUnits + value;
                //TotalDisplay = TotalDisplay + value;

                OnPropertyChanged(nameof(TotalOtherCompetitorUnits));
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

        private int _TotalPanasonicUnits;
        public int TotalPanasonicUnits
        {
            get { return _TotalPanasonicUnits; }
            set
            {
                _TotalPanasonicUnits = value;
                OnPropertyChanged(nameof(TotalPanasonicUnits));
            }
        }

        private int _TotalCompetitorUnits;
        public int TotalCompetitorUnits
        {
            get { return _TotalCompetitorUnits; }
            set
            {
                _TotalCompetitorUnits = value;
                OnPropertyChanged(nameof(TotalCompetitorUnits));
            }
        }

        private int _TotalDisplay;
        public int TotalDisplay
        {
            get { return _TotalDisplay; }
            set
            {
                _TotalDisplay = value;
                OnPropertyChanged(nameof(TotalDisplay));
            }
        }

        private string _DisplayTrackerComment;
        public string DisplayTrackerComment
        {
            get { return _DisplayTrackerComment; }
            set
            {
                _DisplayTrackerComment = value;
                OnPropertyChanged(nameof(DisplayTrackerComment));
            }
        }

        private bool _IsDisplayTrackerSaveEnable;
        public bool IsDisplayTrackerSaveEnable
        {
            get { return _IsDisplayTrackerSaveEnable; }
            set
            {
                _IsDisplayTrackerSaveEnable = value;
                OnPropertyChanged(nameof(IsDisplayTrackerSaveEnable));
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

        private ObservableCollection<EntityKeyValueResponse> _obEntityKeyValueResponse;
        public ObservableCollection<EntityKeyValueResponse> obEntityKeyValueResponse
        {
            get { return _obEntityKeyValueResponse; }
            set
            {
                _obEntityKeyValueResponse = value;
                OnPropertyChanged(nameof(obEntityKeyValueResponse));
            }
        }

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

        private List<EntityKeyValueResponse> _Locations;
        public List<EntityKeyValueResponse> Locations
        {
            get { return _Locations; }
            set
            {
                _Locations = value;
                OnPropertyChanged(nameof(Locations));
            }
        }

        private ObservableCollection<DropDownModel> _StoreDropDown;
        public ObservableCollection<DropDownModel> StoreDropDown
        {
            get { return _StoreDropDown; }
            set
            {
                _StoreDropDown = value;
                OnPropertyChanged(nameof(StoreDropDown));
            }
        }

        private bool _ModelNumberValide;
        public bool ModelNumberValide
        {
            get { return _ModelNumberValide; }
            set
            {

                _ModelNumberValide = value;
                OnPropertyChanged(nameof(ModelNumberValide));
            }
        }

        private string _ModelNumber;
        public string ModelNumber
        {
            get { return _ModelNumber; }
            set
            {
                ModelNumberValide = true;
                _ModelNumber = value;
                OnPropertyChanged(nameof(ModelNumber));
            }
        }

        public string Photoaction { get; set; }

        private ObservableCollection<DisplayTrackerEntryImageRequest> _obDisplayTrackerEntryImageRequest;
        public ObservableCollection<DisplayTrackerEntryImageRequest> obDisplayTrackerEntryImageRequest

        {
            get { return _obDisplayTrackerEntryImageRequest; }
            set
            {
                _obDisplayTrackerEntryImageRequest = value;
                OnPropertyChanged(nameof(obDisplayTrackerEntryImageRequest));
            }
        }

        private string _SelectedDate;
        public string SelectedDate
        {
            get { return _SelectedDate; }
            set
            {
                _SelectedDate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private ObservableCollection<DisplayTrackerCompetitorModelEntryRequest> _ObDisplayTrackerCompetitorModelEntryRequest;
        public ObservableCollection<DisplayTrackerCompetitorModelEntryRequest> ObDisplayTrackerCompetitorModelEntryRequest
        {
            get { return _ObDisplayTrackerCompetitorModelEntryRequest; }
            set
            {
                _ObDisplayTrackerCompetitorModelEntryRequest = value;
                OnPropertyChanged(nameof(ObDisplayTrackerCompetitorModelEntryRequest));
            }
        }

        private ObservableCollection<BrandNameRequest> _ObDispalyModelsResponser;
        public ObservableCollection<BrandNameRequest> ObDispalyModelsResponse
        {
            get { return _ObDispalyModelsResponser; }
            set
            {
                _ObDispalyModelsResponser = value;
                OnPropertyChanged(nameof(ObDispalyModelsResponse));
            }
        }

        private ObservableCollection<DisplayTrackerProductModelEntryRequest> _ObProductModelResponses;
        public ObservableCollection<DisplayTrackerProductModelEntryRequest> ObProductModelResponses
        {
            get { return _ObProductModelResponses; }
            set
            {
                _ObProductModelResponses = value;
                OnPropertyChanged(nameof(ObProductModelResponses));
            }
        }

        private ObservableCollection<DisplayTrackerListResponse> _ObDisplayTrackerListResponse;
        public ObservableCollection<DisplayTrackerListResponse> ObDisplayTrackerListResponse
        {
            get { return _ObDisplayTrackerListResponse; }
            set
            {
                _ObDisplayTrackerListResponse = value;
                OnPropertyChanged(nameof(ObDisplayTrackerListResponse));
            }
        }

        #endregion
    }
}
