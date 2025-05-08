using System;
using Xamarin.Forms;

namespace Retail.Models
{
    public class DropDownModel
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Desc { get; set; }
    }

    public class DropDownModelForDisplayCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int Flag { get; set; }

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

    public class DropDownModelForDisplaySubCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public int Flag { get; set; }

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
