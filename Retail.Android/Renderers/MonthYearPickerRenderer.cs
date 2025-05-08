using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.App;
using Android.Widget;
using AndroidX.Core.Content;
using Plugin.CurrentActivity;
using Retail.Controls;
using Retail.Droid.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MonthYearPickerView), typeof(MonthYearPickerRenderer))]
namespace Retail.Droid.Renderers
{
    public class MonthYearPickerRenderer : ViewRenderer<MonthYearPickerView, EditText>
    {
        private readonly Context _context;
        private MonthYearPickerDialog _monthYearPickerDialog;
        MonthYearPickerView element;
        public MonthYearPickerRenderer(Context context) : base(context)
        {
            _context = context;
        }

        [Obsolete]
        protected override void OnElementChanged(ElementChangedEventArgs<MonthYearPickerView> e)
        {
            base.OnElementChanged(e);

            CreateAndSetNativeControl();

            Control.KeyListener = null;
            if (Element != null)
            {
                Element.Focused += Element_Focused;

                element = (MonthYearPickerView)this.Element;

                var editText = this.Control;
                if (!string.IsNullOrEmpty(element.Image))
                {
                    switch (element.ImageAlignment)
                    {
                        case (Controls.ImageAlignment)ImageAlignment.Left:
                            editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
                            break;
                        case (Controls.ImageAlignment)ImageAlignment.Right:
                            editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image), null);
                            break;
                    }
                }
            }

        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth + 15, element.ImageHeight + 15, true));
        }

        protected override void Dispose(bool disposing)
        {
            if (Control == null) return;

            Element.Focused -= Element_Focused;

            if (_monthYearPickerDialog != null)
            {
                _monthYearPickerDialog.OnDateTimeChanged -= OnDateTimeChanged;
                _monthYearPickerDialog.OnClosed -= OnClosed;
                _monthYearPickerDialog.Hide();
                _monthYearPickerDialog.Dispose();
                _monthYearPickerDialog = null;
            }

            base.Dispose(disposing);
        }

        #region Private Methods

        private void ShowDatePicker()
        {
            if (_monthYearPickerDialog == null)
            {
                _monthYearPickerDialog = new MonthYearPickerDialog();
                _monthYearPickerDialog.OnDateTimeChanged += OnDateTimeChanged;
                _monthYearPickerDialog.OnClosed += OnClosed;
            }
            _monthYearPickerDialog.Date = Element.Date;
            _monthYearPickerDialog.MinDate = FormatDateToMonthYear(Element.MinDate);
            _monthYearPickerDialog.MaxDate = FormatDateToMonthYear(Element.MaxDate);
            _monthYearPickerDialog.InfiniteScroll = Element.InfiniteScroll;

            var fragmentTag = "fragMonthYearPickerDialog";
            var appcompatActivity = CrossCurrentActivity.Current.Activity as AppCompatActivity;
            var mFragManager = appcompatActivity?.SupportFragmentManager.BeginTransaction();
            if (mFragManager != null)
            {
                _monthYearPickerDialog.Show(mFragManager, nameof(MonthYearPickerDialog));
            }
            else
            {
                mFragManager.Add(Resource.Id.fragment_container_view_tag, new MonthYearPickerDialog(), fragmentTag);
                mFragManager.Commit();
            }
        }

        private void ClearPickerFocus()
        {
            ((IElementController)Element).SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
            Control.ClearFocus();
        }

        private DateTime? FormatDateToMonthYear(DateTime? dateTime) =>
            dateTime.HasValue ? (DateTime?)new DateTime(dateTime.Value.Year, dateTime.Value.Month, 1) : null;

        [Obsolete]
        private void CreateAndSetNativeControl()
        {
            var tv = new EditText(_context);

            tv.SetTextColor(Element.TextColor.ToAndroid());
            tv.TextSize = 15;
            tv.Text = $"{Element.Date.ToString("MMM"):D2} , {Element.Date.Year}";
            tv.Gravity = Android.Views.GravityFlags.Start;
            tv.SetBackgroundColor(Element.BackgroundColor.ToAndroid());
            Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "calibribold.ttf");
            tv.Typeface = font;

            SetNativeControl(tv);
        }

        #endregion

        #region Event Handlers

        private void Element_Focused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                ShowDatePicker();
            }
        }

        private void OnClosed(object sender, DateTime e)
        {
            ClearPickerFocus();
        }

        private void OnDateTimeChanged(object sender, DateTime e)
        {
            Element.Date = e;
            Control.Text = $"{Element.Date.ToString("MMM"):D2} , {Element.Date.Year}";
            ClearPickerFocus();
        }

        #endregion
    }
}
