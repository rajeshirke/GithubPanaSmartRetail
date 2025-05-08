
using System;
using CoreGraphics;
using Retail.Controls;
using Retail.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(EntryWithRoundedCorner), typeof(EntryWithRoundedCornerRenderer))]

namespace Retail.iOS.Renderers
{
    public class EntryWithRoundedCornerRenderer : EntryRenderer
    {
        #region Parent override

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;
            Control.BorderStyle = UITextBorderStyle.None;
            UpdateBorderWidth();
            UpdateBorderColor();
            UpdateBorderRadius();
            UpdateLeftPadding();
            UpdateRightPadding(); 
            Control.ClipsToBounds = true;
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null)
                return;
            if (e.PropertyName == EntryWithRoundedCorner.BorderWidthProperty.PropertyName)
            {
                UpdateBorderWidth();
            }
            else if (e.PropertyName == EntryWithRoundedCorner.BorderColorProperty.PropertyName)
            {
                UpdateBorderColor();
            }
            else if (e.PropertyName == EntryWithRoundedCorner.BorderRadiusProperty.PropertyName)
            {
                UpdateBorderRadius();
            }
            else if (e.PropertyName == EntryWithRoundedCorner.LeftPaddingProperty.PropertyName)
            {
                UpdateLeftPadding();
            }
            else if (e.PropertyName == EntryWithRoundedCorner.RightPaddingProperty.PropertyName)
            {
                UpdateRightPadding();
            }
        }

        #endregion

        #region Utility methods

        private void UpdateBorderWidth()
        {
            var entryEx = this.Element as EntryWithRoundedCorner;
            Control.Layer.BorderWidth = entryEx.BorderWidth;
        }

        private void UpdateBorderColor()
        {
            var entryEx = this.Element as EntryWithRoundedCorner;
            Control.Layer.BorderColor = entryEx.BorderColor.ToUIColor().CGColor;
        }

        private void UpdateBorderRadius()
        {
            var entryEx = this.Element as EntryWithRoundedCorner;
            Control.Layer.CornerRadius = (nfloat)entryEx.BorderRadius;
        }

        private void UpdateLeftPadding()
        {
            var entryEx = this.Element as EntryWithRoundedCorner;
            var leftPaddingView = new UIView(new CGRect(0, 0, entryEx.LeftPadding, 0));
            Control.LeftView = leftPaddingView;
            Control.LeftViewMode = UITextFieldViewMode.Always;
        }

        private void UpdateRightPadding()
        {
            var entryEx = this.Element as EntryWithRoundedCorner;
            var rightPaddingView = new UIView(new CGRect(0, 0, entryEx.RightPadding, 0));
            Control.RightView = rightPaddingView;
            Control.RightViewMode = UITextFieldViewMode.Always;
        }

        #endregion
    }
}
