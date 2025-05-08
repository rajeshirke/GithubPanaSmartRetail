using System;
using System.Diagnostics;
using Retail.Controls;
using Retail.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DatePickerCtrl), typeof(DatePickerCtrlRenderer))]
namespace Retail.Droid.Renderers
{
    [Obsolete]
    public class DatePickerCtrlRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            this.Control.SetTextColor(Android.Graphics.Color.Gray);
            this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            this.Control.SetPadding(0, 0, 0, 5);
            this.Control.SetTextColor(Android.Graphics.Color.ParseColor("#C1C1C1"));
            this.Control.TextSize = 15;

            // this.Control.SetBackgroundDrawable(gd);
            //  GradientDrawable gd = new GradientDrawable();
            // SetCornerRadius(25); //increase or decrease to changes the corner look
            // SetColor(Android.Graphics.Color.Transparent);
            // SetStroke(3, Android.Graphics.Color.#533f95;             

            DatePickerCtrl element = Element as DatePickerCtrl;
            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                this.Control.Text = element.Placeholder;
                this.Control.SetTextColor(Android.Graphics.Color.ParseColor("#C1C1C1"));
            }

            this.Control.FocusChange += (sender, arg) =>
            {
                try
                {
                    if (Control.Text == "Select Date")
                    {
                        Control.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                        Control.SetTextColor(Android.Graphics.Color.ParseColor("#515151"));
                    }
                    else
                    {
                        Control.Text = Convert.ToDateTime(Control.Text).ToString("dd-MMM-yyyy");
                        Control.SetTextColor(Android.Graphics.Color.ParseColor("#515151"));
                    }
                }
                catch (Exception ex)
                {
                    Debugger.Log(0, null, ex.ToString());
                }
            };

            this.Control.TextChanged += (sender, arg) =>
            {
                var selectedDate = arg.Text.ToString();
                if (selectedDate == element.Placeholder)
                {
                    Control.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    Control.SetTextColor(Android.Graphics.Color.ParseColor("#515151"));
                }
            };
        }
    }
}
