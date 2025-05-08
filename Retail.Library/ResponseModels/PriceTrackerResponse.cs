using System;
using Newtonsoft.Json;

namespace Retail.Infrastructure.ResponseModels
{
    public class PriceTrackerResponse
    {
        public int CompetitorProductModelId { get; set; }
        public string ProductModelName { get; set; }
        public string BrandName { get; set; }
        public string ProductModelNumberWithOutBrandName { get; set; }
        //public decimal? RRP { get; set; }
        //public decimal? NetRRP { get; set; }
        //public decimal? Promo { get; set; }
        public string RRP { get; set; } = "0";
        public string NetRRP { get; set; } = "0";
        public string Promo { get; set; } = "0";
        public bool IsNonEditableFlg { get; set; } = false;
        public int TxtId { get; set; }
        public bool PanaFlag { get; set; }
        public bool IsVisibleDeletePriceTrakcer
        {
            get
            {
                if (IsNonEditableFlg == true)
                {
                    if (!(string.IsNullOrEmpty(RRP)) && RRP != "0" && NetRRP != "0" && !(string.IsNullOrEmpty(NetRRP))) //// for default 0

                    //if (!(string.IsNullOrEmpty(RRP)) && !(string.IsNullOrEmpty(NetRRP)) && PanaFlag==false) ////for blank
                    {
                        return true;
                    }
                }
                
                return false;
            }
        }

        public bool IsEditPriceTrakcer
        {
            get
            {
                if(IsNonEditableFlg == true)
                {
                    if (!(string.IsNullOrEmpty(RRP)) && RRP != "0" && NetRRP != "0" && !(string.IsNullOrEmpty(NetRRP))) //// for default 0

                    //if (!(string.IsNullOrEmpty(RRP)) && !(string.IsNullOrEmpty(NetRRP))) ////for blank
                    {
                        return false;
                    }
                }
                
                return true;
            }
        }
    }
}

