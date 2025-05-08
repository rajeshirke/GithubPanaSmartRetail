using System;
using System.ComponentModel;
using Android.Views;
using Retail.Controls;
using Retail.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // Type or member is obsolete
[assembly: ExportRenderer(typeof(EntryWithRoundedCorner), typeof(EntryWithRoundedCornerRenderer))]
#pragma warning restore CS0612 // Type or member is obsolete
namespace Retail.Droid.Renderers
{
    [Obsolete]
    public class EntryWithRoundedCornerRenderer : EntryRenderer
    {
        #region Private fields and properties

        private BorderRenderer _renderer;
        private const GravityFlags DefaultGravity = GravityFlags.CenterVertical;

        #endregion

        #region Parent override

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;
            Control.Gravity = DefaultGravity;
            var entryEx = Element as EntryWithRoundedCorner;
            UpdateBackground(entryEx);
            UpdatePadding(entryEx);
            UpdateTextAlighnment(entryEx);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null)
                return;
            var entryEx = Element as EntryWithRoundedCorner;
            if (e.PropertyName == EntryWithRoundedCorner.BorderWidthProperty.PropertyName ||
                e.PropertyName == EntryWithRoundedCorner.BorderColorProperty.PropertyName ||
                e.PropertyName == EntryWithRoundedCorner.BorderRadiusProperty.PropertyName ||
                e.PropertyName == EntryWithRoundedCorner.BackgroundColorProperty.PropertyName)
            {
                UpdateBackground(entryEx);
            }
            else if (e.PropertyName == EntryWithRoundedCorner.LeftPaddingProperty.PropertyName ||
                e.PropertyName == EntryWithRoundedCorner.RightPaddingProperty.PropertyName)
            {
                UpdatePadding(entryEx);
            }
            else if (e.PropertyName == Entry.HorizontalTextAlignmentProperty.PropertyName)
            {
                UpdateTextAlighnment(entryEx);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_renderer != null)
                {
                    _renderer.Dispose();
                    _renderer = null;
                }
            }
        }

        #endregion

        #region Utility methods

        private void UpdateBackground(EntryWithRoundedCorner entryEx)
        {
            if (_renderer != null)
            {
                _renderer.Dispose();
                _renderer = null;
            }
            _renderer = new BorderRenderer();

            Control.Background = _renderer.GetBorderBackground(entryEx.BorderColor, entryEx.BackgroundColor, entryEx.BorderWidth, entryEx.BorderRadius);
        }

        private void UpdatePadding(EntryWithRoundedCorner entryEx)
        {
            Control.SetPadding((int)Forms.Context.ToPixels(entryEx.LeftPadding), 0,
                (int)Forms.Context.ToPixels(entryEx.RightPadding), 0);
        }

        private void UpdateTextAlighnment(EntryWithRoundedCorner entryEx)
        {
            var gravity = DefaultGravity;
            switch (entryEx.HorizontalTextAlignment)
            {
                case Xamarin.Forms.TextAlignment.Start:
                    gravity |= GravityFlags.Start;
                    break;
                case Xamarin.Forms.TextAlignment.Center:
                    gravity |= GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    gravity |= GravityFlags.End;
                    break;
            }
            Control.Gravity = gravity;
        }

        #endregion
    }
}
