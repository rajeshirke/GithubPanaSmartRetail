using System;
using Foundation;
using Retail.Controls;
using Retail.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePickerCtrl), typeof(DatePickerCtrlRenderer))]
namespace Retail.iOS.Renderers
{
    public class DatePickerCtrlRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);
            if (this.Control == null)
                return;
            var element = e.NewElement as DatePickerCtrl;
            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
            }
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 2))
            {
                UIDatePicker picker = (UIDatePicker)Control.InputView;
                picker.PreferredDatePickerStyle = UIDatePickerStyle.Wheels;
            }
            // Control.BorderStyle = UITextBorderStyle.RoundedRect;
            //  Control.Layer.BorderColor = UIColor.FromWhiteAlpha(1, 0.5f);// .CGColor;   
            Control.Layer.CornerRadius = 5;
            Control.Layer.BorderWidth = 0f;
            Control.AdjustsFontSizeToFitWidth = true;
            Control.BorderStyle = UITextBorderStyle.None;
            Control.TextColor = Color.FromHex("#C1C1C1").ToUIColor(); //UIColor.Clear.Fr("#515151",14f);
            Control.BackgroundColor = UIColor.White;
            string placeholderColor = element.PlaceholderColor;
            UIColor color = UIColor.FromRGB(193, 193, 193);

            var placeholderAttributes = new NSAttributedString("#C1C1C1", new UIStringAttributes()
            { ForegroundColor = color });

            Control.AttributedPlaceholder = placeholderAttributes;
            //Control.AttributedPlaceholder = new NSAttributedString(Control.AttributedPlaceholder.Value, foregroundColor: UIColor.Red);
            Control.ShouldEndEditing += (textField) => {
                var seletedDate = (UITextField)textField;
                var text = seletedDate.Text;
                if (text == element.Placeholder)
                {
                    Control.AttributedPlaceholder = placeholderAttributes;
                    Control.TextColor = Color.FromHex("#515151").ToUIColor();
                    Control.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                else
                {
                    Control.AttributedPlaceholder = placeholderAttributes;
                    Control.TextColor = Color.FromHex("#515151").ToUIColor();
                }
                return true;
            };
        }

        private float GetRed(string color)
        {
            Color c = Color.FromHex(color);
            return (float)c.R;
        }

        private float GetGreen(string color)
        {
            Color c = Color.FromHex(color);
            return (float)c.G;
        }

        private float GetBlue(string color)
        {
            Color c = Color.FromHex(color);
            return (float)c.B;
        }

        private void OnCanceled(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }
        private void OnDone(object sender, EventArgs e)
        {
            Control.ResignFirstResponder();
        }
    }
}
