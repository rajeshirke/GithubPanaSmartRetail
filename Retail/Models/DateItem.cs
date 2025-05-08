using System;
using Retail.ViewModels;
using Xamarin.Forms;

namespace Retail.Models
{
    public class DateItem 
    {
        //public DateItem(INavigation navigation) : base(navigation)
        //{
        //}
        
        public DateItem()
        {

        }

        public string month { get; set; }
        public string day { get; set; }
        public string dayWeek { get; set; }
        public bool selected { get; set; }

        public string backgroundColor { get; set; }
        public string textColor { get; set; }
        public DateTime dateTime { get; set; }
        public int dateTimeYear { get; set; }

        //private string _backgroundColor;
        //public string backgroundColor
        //{
        //    get => _backgroundColor;
        //    set => SetProperty(ref _backgroundColor, value);
        //}

        //private string _textColor;
        //public string textColor
        //{
        //    get => _textColor;
        //    set => SetProperty(ref _textColor, value);
        //}
    }
}
