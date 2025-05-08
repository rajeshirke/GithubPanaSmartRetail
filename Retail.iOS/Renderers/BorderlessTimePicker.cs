using System;
using System.ComponentModel;
using System.Globalization;
using Retail.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TimePicker), typeof(BorderlessTimePicker))]
namespace Retail.iOS.Renderers
{
    public class BorderlessTimePicker: TimePickerRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            try
            {
                if (Control != null)
                {
                    Control.Layer.BorderWidth = 0;

                    Control.BorderStyle = UITextBorderStyle.None;
                    UITextField entry = Control;
                    UIDatePicker picker = (UIDatePicker)entry.InputView;
                    picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
                    //TimeSpan timeSpan = new TimeSpan(0, 0, 0);
                    // if(Element.Time != timeSpan)
                  //   Control.Text = Element.Time.ToString("hh:mm tt"); // +" "+DateTime.Today.Add(Element.Time).ToString("tt", CultureInfo.CreateSpecificCulture("hr-HR"));
                    string ttstr = " AM";
                    var hours = Element.Time.Hours;
                    var minutes = Element.Time.Minutes;
                    var amPmDesignator = "AM";
                    if (hours == 0)
                        hours = 12;
                    else if (hours == 12)
                        amPmDesignator = "PM";
                    else if (hours > 12)
                    {
                        hours -= 12;
                        amPmDesignator = "PM";
                    }
                    var formattedTime =
                      String.Format("{0}:{1:00} {2}", hours, minutes, amPmDesignator);
                    Control.Text = formattedTime;
                    //if (Element.Time != new TimeSpan(0, 0, 0))
                    //{
                    //    string dt = Element.Time.ToString("hh:mm tt");
                    //    // string dt = Element.Time.ToString("hh:mm");
                    //    Control.Text = dt;// Element.Time.ToString();//// DateTime.Today.Add(Element.Time).ToString("tt", CultureInfo.CreateSpecificCulture("hr-HR"));
                    //}
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
