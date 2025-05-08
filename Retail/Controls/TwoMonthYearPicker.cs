using System;
using System.Diagnostics;
using System.Windows.Input;
using Retail.DependencyServices;
using Retail.ViewModels.MonthYearPickerViewModel;
using Xamarin.Forms;

namespace Retail.Controls
{
    public class TwoMonthYearPicker : View
    {
        public TwoMonthYearPicker()
        {
            BindingContext = new ItemsViewModel(DependencyService.Get<INumberPickerDialog>());
            this.HeightRequest = 20;
        }

        #region FontSize

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
             propertyName: nameof(FontSize),
             returnType: typeof(double),
             declaringType: typeof(TwoMonthYearPicker),
             defaultValue: (double)24,
             defaultBindingMode: BindingMode.TwoWay);

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        #endregion FontSize

        #region TextColor

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
            propertyName: nameof(TextColor),
            returnType: typeof(Color),
            declaringType: typeof(TwoMonthYearPicker),
            defaultValue: Color.FromHex("#515151"),
            defaultBindingMode: BindingMode.TwoWay);

        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        #endregion TextColor      

        public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
          propertyName: nameof(TextFont),
          returnType: typeof(Font),
          declaringType: typeof(TwoMonthYearPicker),
          defaultBindingMode: BindingMode.TwoWay);

        public Font TextFont
        {
            get => (Font)GetValue(FontFamilyProperty);
            set => SetValue(FontFamilyProperty, value);
        }


        public static readonly BindableProperty FontAttributeProperty = BindableProperty.Create(
          propertyName: nameof(FontAttributes),
          returnType: typeof(FontAttributes),
          declaringType: typeof(TwoMonthYearPicker),
          defaultBindingMode: BindingMode.TwoWay);

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)GetValue(FontAttributeProperty);
            set => SetValue(FontAttributeProperty, value);
        }

        #region InfiniteScroll

        public static readonly BindableProperty InfiniteScrollProperty = BindableProperty.Create(
            propertyName: nameof(InfiniteScroll),
            returnType: typeof(bool),
            declaringType: typeof(TwoMonthYearPicker),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay);

        public bool InfiniteScroll
        {
            get => (bool)GetValue(InfiniteScrollProperty);
            set => SetValue(InfiniteScrollProperty, value);
        }

        #endregion InfiniteScroll

        #region Date

        public static readonly BindableProperty DateProperty = BindableProperty.Create(
            propertyName: nameof(Date),
            returnType: typeof(DateTime),
            declaringType: typeof(TwoMonthYearPicker),
            defaultValue: new DateTime(1990, 01, 01),
            defaultBindingMode: BindingMode.TwoWay, propertyChanged: MonthYearChanged);

        private static async void MonthYearChanged(BindableObject bindable, object oldValue, object newValue)
        {
            try
            {
                DateTime CurrentDate = DateTime.Today;
                var lastDayOfMonth = CurrentDate.AddMonths(-2).AddDays(1);

                if ((lastDayOfMonth.Date <= Convert.ToDateTime(newValue)) == false)
                {
                    MessagingCenter.Send<string>("1/1/0001", "OnlyTwoMonthYearChangeDate");
                }
                else
                {
                    MessagingCenter.Send<string>(newValue.ToString(), "OnlyTwoMonthYearChangeDate");
                }

            }
            catch (Exception ex)
            {
                Debugger.Log(0, null, ex.ToString());
            }
        }

        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set => SetValue(DateProperty, value);
        }

        #endregion Date

        #region MaxDate

        public static readonly BindableProperty MaxDateProperty = BindableProperty.Create(
            propertyName: nameof(MaxDate),
            returnType: typeof(DateTime?),
            declaringType: typeof(TwoMonthYearPicker),
            defaultValue: new DateTime(2099, 12, 31), //ymd
            defaultBindingMode: BindingMode.TwoWay);

        public DateTime? MaxDate
        {
            get => (DateTime?)GetValue(MaxDateProperty);
            set => SetValue(MaxDateProperty, value);
        }

        #endregion MaxDate

        #region MinDate

        public static readonly BindableProperty MinDateProperty = BindableProperty.Create(
            propertyName: nameof(MinDate),
            returnType: typeof(DateTime?),
            declaringType: typeof(TwoMonthYearPicker),
            defaultValue: new DateTime(2000, 01, 01),
            defaultBindingMode: BindingMode.TwoWay);

        public DateTime? MinDate
        {
            get => (DateTime?)GetValue(MinDateProperty);
            set => SetValue(MinDateProperty, value);
        }

        #endregion MinDate

        public int ImageWidth
        {
            get { return (int)GetValue(ImageWidthProperty); }
            set { SetValue(ImageWidthProperty, value); }
        }

        public int ImageHeight
        {
            get { return (int)GetValue(ImageHeightProperty); }
            set { SetValue(ImageHeightProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public ImageAlignment ImageAlignment
        {
            get { return (ImageAlignment)GetValue(ImageAlignmentProperty); }
            set { SetValue(ImageAlignmentProperty, value); }
        }
        public bool LeftImageSpace
        {
            get { return (bool)GetValue(LeftImageSpaceProperty); }
            set { SetValue(LeftImageSpaceProperty, value); }
        }

        public static readonly BindableProperty ImageProperty =
          BindableProperty.Create(nameof(Image), typeof(string), typeof(TwoMonthYearPicker), string.Empty);

        public static readonly BindableProperty ImageHeightProperty =
          BindableProperty.Create(nameof(ImageHeight), typeof(int), typeof(TwoMonthYearPicker), 20);

        public static readonly BindableProperty ImageWidthProperty =
            BindableProperty.Create(nameof(ImageWidth), typeof(int), typeof(TwoMonthYearPicker), 20);

        public static readonly BindableProperty ImageAlignmentProperty =
            BindableProperty.Create(nameof(ImageAlignment), typeof(ImageAlignment), typeof(TwoMonthYearPicker), ImageAlignment.Left);

        public static readonly BindableProperty LeftImageSpaceProperty =
          BindableProperty.Create(nameof(LeftImageSpace), typeof(bool), typeof(TwoMonthYearPicker), false);


        public event EventHandler ItemTapped = (e, a) => { };

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(TwoMonthYearPicker), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(TwoMonthYearPicker), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
    }
}
