using System;
using System.Drawing;
using CoreGraphics;
using Retail.Controls;
using Retail.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;



[assembly: ExportRenderer(typeof(TwoMonthYearPicker), typeof(TwoMonthYearPickerRenderer))]
namespace Retail.iOS.Renderers
{
    public class TwoMonthYearPickerRenderer : ViewRenderer<TwoMonthYearPicker, UITextField>
    {
        private DateTime _selectedDate;
        private UITextField _dateLabel;
        private PickerDateModel _pickerModel;

        protected override void OnElementChanged(ElementChangedEventArgs<TwoMonthYearPicker> e)
        {
            base.OnElementChanged(e);
            _dateLabel = new UITextField();

            var dateToday = DateTime.Today;
            SetupPicker(new DateTime(dateToday.Year, dateToday.Month, 1));

            SetNativeControl(_dateLabel);
            if (Element != null)
            {
                Control.EditingChanged += ControlOnEditingChanged;
                Element.PropertyChanged += Element_PropertyChanged;

                var element = (TwoMonthYearPicker)this.Element;
                var textField = this.Control;
                textField.Font = UIFont.FromName("Calibri Bold", 15f);
                if (!string.IsNullOrEmpty(element.Image))
                {
                    switch (element.ImageAlignment)
                    {
                        case (Controls.ImageAlignment)ImageAlignment.Left:
                            UIView uIView = new UIView(new CGRect(0, 0, 40, 40));
                            textField.LeftViewMode = UITextFieldViewMode.Always;
                            textField.LeftView = uIView;//GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                            textField.TextColor = Xamarin.Forms.Color.FromHex("#515151").ToUIColor();
                            textField.Font = UIFont.SystemFontOfSize(15f);
                            textField.Font = UIFont.BoldSystemFontOfSize(14.5f);
                            textField.Font = UIFont.FromName("Calibri Bold", 15f);// textField.LeftView.Frame = new CGRect(2, 0, 0, 0);
                            break;
                        case (Controls.ImageAlignment)ImageAlignment.Right:
                            textField.RightViewMode = UITextFieldViewMode.Always;
                            textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                            // textField.LeftView.Frame = new CGRect(2, 0, 0, 0);
                            textField.TextColor = Xamarin.Forms.Color.FromHex("#515151").ToUIColor();
                            textField.Font = UIFont.SystemFontOfSize(15f);
                            textField.Font = UIFont.BoldSystemFontOfSize(14.5f);
                            textField.Font = UIFont.FromName("Calibri Bold", 15f);
                            break;
                    }
                }
                if (element.LeftImageSpace)
                {
                    UIView uIView = new UIView(new CGRect(0, 0, 40, 40));
                    textField.LeftViewMode = UITextFieldViewMode.Always;
                    textField.LeftView = uIView;
                    textField.TextColor = Xamarin.Forms.Color.FromHex("#515151").ToUIColor();
                    textField.Font = UIFont.SystemFontOfSize(15f);
                    textField.Font = UIFont.BoldSystemFontOfSize(14.5f);
                    textField.Font = UIFont.FromName("Calibri Bold", 15f);
                }
            }

        }

        private UIView GetImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(5, 0, width, height)
            };
            UIView objLeftView = new UIView(new System.Drawing.Rectangle(2, 0, width + 10, height));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }

        private void ControlOnEditingChanged(object sender, EventArgs e)
        {
            var currentDate = $"{Element.Date.ToString("MMM"):D2} , {Element.Date.Year}";
            _dateLabel.Font = UIFont.FromName("Calibri Bold", 15f);
            if (_dateLabel.Text != currentDate)
            {
                _dateLabel.Text = currentDate;
                _dateLabel.TextColor = Xamarin.Forms.Color.FromHex("#515151").ToUIColor();
                _dateLabel.Font = UIFont.SystemFontOfSize(15f);
                _dateLabel.Font = UIFont.BoldSystemFontOfSize(14.5f);
                _dateLabel.Font = UIFont.FromName("Calibri Bold", 15f);
            }
        }

        protected override void Dispose(bool disposing)
        {
            Element.PropertyChanged -= Element_PropertyChanged;
            base.Dispose(disposing);
        }

        private void SetupPicker(DateTime date)
        {
            if (Element != null)
            {
                var datePicker = new UIPickerView();
                _pickerModel = new PickerDateModel(datePicker, date);
                datePicker.ShowSelectionIndicator = true;
                _selectedDate = date;
                _pickerModel.PickerChanged += (sender, e) =>
                {
                    _selectedDate = e;
                };
                datePicker.Model = _pickerModel;

                _pickerModel.MaxDate = Element.MaxDate ?? DateTime.MaxValue;
                _pickerModel.MinDate = Element.MinDate ?? DateTime.MinValue;



                var toolbar = new UIToolbar
                {
                    BarStyle = UIBarStyle.Default,
                    Translucent = true
                };
                toolbar.SizeToFit();

                var doneButton = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done,
                (s, e) =>
                {
                    Element.Date = _selectedDate;

                    DateTime CurrentDate = DateTime.Today;
                    var lastDayOfMonth = CurrentDate.AddMonths(-2).AddDays(1);

                    if ((lastDayOfMonth.Date <= _selectedDate) == false)
                    {
                        _dateLabel.Text = "Only 60 days selection allowed.";
                        _dateLabel.ResignFirstResponder();
                        _dateLabel.TextColor = Xamarin.Forms.Color.FromHex("#515151").ToUIColor();
                        _dateLabel.Font = UIFont.SystemFontOfSize(15f);
                        _dateLabel.Font = UIFont.BoldSystemFontOfSize(14.5f);
                        _dateLabel.Font = UIFont.FromName("Calibri Bold", 15f);
                    }
                    else
                    {
                        _dateLabel.Text = $"{Element.Date.ToString("MMM"):D2} , {Element.Date.Year}";
                        _dateLabel.ResignFirstResponder();
                        _dateLabel.TextColor = Xamarin.Forms.Color.FromHex("#515151").ToUIColor();
                        _dateLabel.Font = UIFont.SystemFontOfSize(15f);
                        _dateLabel.Font = UIFont.BoldSystemFontOfSize(14.5f);
                        _dateLabel.Font = UIFont.FromName("Calibri Bold", 15f);
                    }

                });

                toolbar.SetItems(new[] { new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace), doneButton }, true);

                _dateLabel.InputView = datePicker;
                _dateLabel.Text = $"{Element.Date.ToString("MMM"):D2} , {Element.Date.Year}";
                _dateLabel.InputAccessoryView = toolbar;
                _dateLabel.TextColor = Xamarin.Forms.Color.FromHex("#515151").ToUIColor();
                _dateLabel.Font = UIFont.SystemFontOfSize(15f);
                _dateLabel.Font = UIFont.BoldSystemFontOfSize(14.5f);
                _dateLabel.Font = UIFont.FromName("Calibri Bold", 15f);
            }
        }

        private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == TwoMonthYearPicker.MaxDateProperty.PropertyName)
            {
                _pickerModel.MaxDate = Element.MaxDate ?? DateTime.MinValue;
            }
            else if (e.PropertyName == TwoMonthYearPicker.MinDateProperty.PropertyName)
            {
                _pickerModel.MinDate = Element.MinDate ?? DateTime.MaxValue;
            }
        }
    }
}
