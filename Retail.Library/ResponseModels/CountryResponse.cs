using System;
namespace Retail.Infrastructure.ResponseModels
{

        public class CountryResponse
        {
            public int CountryId { get; set; }
            public string Iso { get; set; }
            public string Name { get; set; }
            public string Iso3 { get; set; }
            public int? NumCode { get; set; }
            public int PhoneCode { get; set; }
            public string CurrencyCode { get; set; }
            public int? CurrencyId { get; set; }
            public bool? IsActive { get; set; }

            public virtual CurrencyMasterResponse CurrencyMasterResponse { get; set; }
            public virtual CountryLevelSettingResponse CountryLevelSettingResponse { get; set; }
            //public virtual ICollection<PaymentModeCountry> PaymentModeCountries { get; set; }  

        }

        public class CurrencyMasterResponse
        {
            public int CurrencyId { get; set; }
            public string Name { get; set; }
            public decimal ExchangeRate { get; set; }
            public string Code { get; set; }
            public int CountryId { get; set; }
        }
        public class CountryLevelSettingResponse
        {
            public int CountryLevelSettingId { get; set; }
            public int CountryId { get; set; }
            public int OrderReturnDays { get; set; }
            public bool? IsCarePlusWarranty { get; set; }
            public decimal? TaxPercentage { get; set; }

        }
    }

