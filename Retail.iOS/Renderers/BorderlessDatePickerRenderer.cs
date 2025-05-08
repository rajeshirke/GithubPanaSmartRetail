using System;
using System.ComponentModel;
using Retail.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePicker), typeof(BorderlessDatePickerRenderer))]
namespace Retail.iOS.Renderers
{
    public class BorderlessDatePickerRenderer : DatePickerRenderer
    {
       
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                Control.Layer.BorderWidth = 0;
                Control.BorderStyle = UITextBorderStyle.None;
                if (UIDevice.CurrentDevice.CheckSystemVersion(13, 2))
                {
                    UIDatePicker picker = (UIDatePicker)Control.InputView;
                    picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
                }
            }
        }
    }
}
