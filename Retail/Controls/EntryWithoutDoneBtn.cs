using System;
using Xamarin.Forms;

namespace Retail.Controls
{
    public class EntryWithoutDoneBtn : Entry
    {
        public EntryWithoutDoneBtn() : base()
        {

        }
        public new event EventHandler Completed;
        #region Properties
        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(
           nameof(ReturnType),
           typeof(ReturnType),
           typeof(EntryWithoutDoneBtn),
           ReturnType.Done,
           BindingMode.OneWay
       );
        public static BindableProperty BorderColorProperty = BindableProperty.Create<EntryWithoutDoneBtn, Color>(o => o.BorderColor, Color.Transparent);

        public void InvokeCompleted()
        {
            if (this.Completed != null)
                this.Completed.Invoke(this, null);
        }

        public ReturnType ReturnType
        {
            get { return (ReturnType)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }


        public static BindableProperty BorderWidthProperty = BindableProperty.Create<EntryWithoutDoneBtn, float>(o => o.BorderWidth, 0);

        public float BorderWidth
        {
            get { return (float)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static BindableProperty BorderRadiusProperty = BindableProperty.Create<EntryWithoutDoneBtn, float>(o => o.BorderRadius, 0);

        public float BorderRadius
        {
            get { return (float)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public static BindableProperty LeftPaddingProperty = BindableProperty.Create<EntryWithoutDoneBtn, int>(o => o.LeftPadding, 5);

        public int LeftPadding
        {
            get { return (int)GetValue(LeftPaddingProperty); }
            set { SetValue(LeftPaddingProperty, value); }
        }

        public static BindableProperty RightPaddingProperty = BindableProperty.Create<EntryWithoutDoneBtn, int>(o => o.RightPadding, 5);

        public int RightPadding
        {
            get { return (int)GetValue(RightPaddingProperty); }
            set { SetValue(RightPaddingProperty, value); }
        }

        #endregion
    }

    public enum ReturnType
    {
        Go,
        Next,
        Done,
        Send,
        Search
    }
}