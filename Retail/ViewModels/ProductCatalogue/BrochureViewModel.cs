using System;
using System.Collections.ObjectModel;
using Retail.DependencyServices;
using Xamarin.Forms;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using Retail.Infrastructure.ServiceLayer;
using System.Linq;
using Retail.Views.ProductCatalogue;
using Retail.Controls;
using System.Windows.Input;
using Retail.Infrastructure.Enums;
using System.Diagnostics;

namespace Retail.ViewModels.ProductCatalogue
{
    public class BrochureViewModel : BaseViewModel
    {
        
        public BrochureViewModel(INavigation navigation) : base(navigation)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await GetProductCatalogues();
                IsBusy = false;
            });
        }       

        public async Task GetProductCatalogues()
        {
            try
            {
                IsBusy = true;

                if (NetworkAvailable)
                {
                    listProductCatalogueByCategoryResponse = new List<ProductCatalogueByCategoryResponse>();

                    ProductCataloguesManagementSL productCataloguesManagementSL = new ProductCataloguesManagementSL();
                    listProductCatalogueByCategoryResponse = await productCataloguesManagementSL.GetProductCataloguesByCountry((int)CommonAttribute.CustomeProfile?.CountryId);
                    if (listProductCatalogueByCategoryResponse != null && listProductCatalogueByCategoryResponse.Count != 0)
                    {
                        obProductCatalogueByCategoryResponse = new ObservableCollection<ProductCatalogueByCategoryResponse>(listProductCatalogueByCategoryResponse);

                        var item = obProductCatalogueByCategoryResponse.ToList();
                        foreach (var item1 in item)
                        {
                            foreach (var product in item1.ProductCatalogues)
                            {
                                if (product.FileInfo != null && !string.IsNullOrEmpty(product.FileInfo?.Path))
                                {
                                    if (product.FileInfo?.FileTypeId == (int)FileTypeEnum.ProductCatalogue)
                                    {
                                        if (product.FileInfo.Path.ToString().ToLower().Contains(".mp4"))
                                        {
                                            product.FileFullPathMedia = CommonAttribute.ImageBaseUrl + product.FileInfo?.Path;
                                        }
                                        //else if (product.FileInfo.Path.ToString().ToLower().Contains(".pdf"))
                                        //{
                                        //    product.FileFullPathMedia = CommonAttribute.ImageBaseUrl + product.FileInfo?.Path;
                                        //}                                      
                                        else
                                        {
                                            product.FileFullPath = CommonAttribute.ImageBaseUrl + product.FileInfo?.Path;
                                        }
                                    }
                                }
                            }
                        }

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
        }

        public bool isfileview { get; set; }

        public ICommand SelectedItemCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {

                    isfileview = false;
                    IsBusy = true;
                    if (item != null)
                    {
                        if (item.ToLower().Contains(".pdf"))
                        {
                            string fileUrl = item;
                            await Navigation.PushAsync(new PDFViewerPage(fileUrl, "PDF Viewer", "pdf"));
                        }
                    }
                    else
                    {
                        string currentUrl = item;
                        await Navigation.PushModalAsync(new ImagePopupView(currentUrl), true);

                    }


                    //string fileUrl = CommonAttribute.ImageBaseUrl+item.ProductCatalogues;
                    //if (item.ProductCatalogues.FirstOrDefault().FileInfo.FileName.ToLower().Contains(".pdf"))
                    //{
                    //    await Navigation.PushAsync(new PDFViewerPage(fileUrl, item.ProductCategoryName, "pdf"));
                    //}
                    
                    IsBusy = false;

                });
            }
        }

        private string _ProductCatalogue;
        public string ProductCatalogue
        {
            get { return _ProductCatalogue; }
            set
            {
                _ProductCatalogue = value;
                OnPropertyChanged("ProductCatalogue");
            }
        }

        private List<ProductCatalogueByCategoryResponse> _listProductCatalogueByCategoryResponse;
        public List<ProductCatalogueByCategoryResponse> listProductCatalogueByCategoryResponse
        {
            get { return _listProductCatalogueByCategoryResponse; }
            set
            {
                _listProductCatalogueByCategoryResponse = value;
                OnPropertyChanged("listProductCatalogueByCategoryResponse");
            }
        }
        private ObservableCollection<ProductCatalogueByCategoryResponse> _obProductCatalogueByCategoryResponse;
        public ObservableCollection<ProductCatalogueByCategoryResponse> obProductCatalogueByCategoryResponse
        {
            get { return _obProductCatalogueByCategoryResponse; }
            set
            {
                _obProductCatalogueByCategoryResponse = value;
                OnPropertyChanged("obProductCatalogueByCategoryResponse");
            }
        }
    }
}
