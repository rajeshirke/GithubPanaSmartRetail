using System;
using Xamarin.Forms;

namespace Retail.Infrastructure.ResponseModels
{
    public class ProductModelForPriceDisplayResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Flag { get; set; }
        public Color ModelNoStatus
        {
            get
            {
                if(Flag == 1)
                {
                    return Color.Green;
                }
                else
                {
                    return Color.Black;
                }

            }
        }
        public Color ModelNoStatusText
        {
            get
            {
                if (Flag == 1)
                {
                    return Color.Green;
                }
                else
                {
                    return Color.Black;
                }

            }
        }
    }
}

