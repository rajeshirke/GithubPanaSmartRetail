using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Retail.Database;
using Retail.DependencyServices;
using Retail.Hepler;
using Retail.Infrastructure.ResponseModels;
using Retail.Infrastructure.ServiceLayer;
using Retail.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Retail.ViewModels.SalesTarget
{
    public class OfflineDetails
    {
        Connection c;
        public OfflineDetails()
        {
            c = new Connection();
            c.conn = DependencyService.Get<ISQLite>().GetConnection();
            c.conn.CreateTable<TblRecentlyUsedModel>();
            c.conn.CreateTable<TblProductCategories>();
            c.conn.CreateTable<TblSubcategories>();
            c.conn.CreateTable<TblAllModelNumbers>();
        }


        #region OfflineData
        public async Task<bool> GetOfflineProductbyCategory()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                if (true)
                {
                    c.conn.DeleteAll<TblProductCategories>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<EntityKeyValueResponse> entityKeyValueResponse = await masterDataManagementSL.GetProductMasterCategories();
                    if (entityKeyValueResponse.Count != 0 && entityKeyValueResponse != null)
                    {
                        TblProductCategories tbl = new TblProductCategories();
                        foreach (var item in entityKeyValueResponse)
                        {
                            tbl.Id = item.Id;
                            tbl.Name = item.Name;

                            c.conn.Insert(tbl);
                        }
                    }
                    else
                    {
                        return false;
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

            return IsBusy;
        }

        public async Task<bool> GetOfflineRecentlyUsedModelNos()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                if (true)
                {
                    c.conn.DeleteAll<TblRecentlyUsedModel>();
                    List<string> lstModelNumbers = new List<string>();
                    SalesTargetManagementSL salesTargetManagementSL = new SalesTargetManagementSL();
                    if(CommonAttribute.CustomeProfile!=null)
                    {
                        lstModelNumbers = await salesTargetManagementSL.GetRecentTargetModelEntriesByPerson(CommonAttribute.CustomeProfile.PersonId);
                        if (lstModelNumbers.Count != 0 && lstModelNumbers != null)
                        {
                            TblRecentlyUsedModel tbl = new TblRecentlyUsedModel();
                            foreach (string modelno in lstModelNumbers)
                            {
                                tbl.Name = modelno;
                                c.conn.Insert(tbl);
                            }
                        }
                        else
                        {
                            return false;
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

            return IsBusy;
        }

        public async Task<bool> GetOfflineSubcategories()
        {
            bool IsBusy = false;

            try
            {
                IsBusy = true;
                if (true)
                {
                    c.conn.DeleteAll<TblSubcategories>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    List<ProductCategoryResponse> productCategoryResponse = await masterDataManagementSL.GetAllSubcategoriesforOffline();
                    if (productCategoryResponse != null && productCategoryResponse.Count != 0)
                    {
                        TblSubcategories tbl = new TblSubcategories();
                        foreach (var item in productCategoryResponse)
                        {
                            tbl.ProductCategoryId = item.ProductCategoryId;
                            tbl.Name = item.Name;
                            tbl.IsActive = item.IsActive;
                            tbl.ParentCategoryId = item.ParentCategoryId;
                            tbl.Description = item.Description;

                            c.conn.Insert(tbl);
                        }
                    }
                    else
                    {
                        return false;
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

            return IsBusy;
        }

        public async Task<bool> GetModelNumbersActiveByCountryIdOffline()
        {
            bool IsBusy = false;

            try
            {
                if (true)
                {
                    List<ProductModelWithCategoryResponse> lstAllModelNumbers = new List<ProductModelWithCategoryResponse>();
                    c.conn.DeleteAll<TblAllModelNumbers>();
                    MasterDataManagementSL masterDataManagementSL = new MasterDataManagementSL();
                    if (CommonAttribute.CustomeProfile != null)
                    {
                        lstAllModelNumbers = await masterDataManagementSL.GetModelNumbersActiveByCountryIdOffline((long)CommonAttribute.CustomeProfile?.CountryId);
                        if (lstAllModelNumbers.Count != 0 && lstAllModelNumbers != null)
                        {
                            TblAllModelNumbers tbl = new TblAllModelNumbers();
                            foreach (var item in lstAllModelNumbers)
                            {
                                tbl.ProductModelId = item.ProductModelId;
                                tbl.ProductModelNumber = item.ProductModelNumber;
                                tbl.CountryId = item.CountryId;
                                tbl.ProductCategoryId = item.ProductCategoryId;
                                tbl.ProductSubCategoryId = item.ProductSubCategoryId;
                                tbl.Category = item.Category;
                                tbl.SubCategory = item.SubCategory;

                                c.conn.Insert(tbl);
                            }
                        }
                        else
                        {
                            return false;
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

            return IsBusy;
        }

        #endregion
    }
}
