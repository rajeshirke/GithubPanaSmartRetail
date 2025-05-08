using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Views.InputMethods;
using AndroidX.Core.Content;
using Retail.Controls;
using Retail.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryWithoutDoneBtn), typeof(EntryWithoutDoneBtnRenderer))]
namespace Retail.Droid.Renderers
{
    [Obsolete]
    public class EntryWithoutDoneBtnRenderer : EntryRenderer
    {
        #region Private fields and properties

        private BorderRenderer _renderer;
        private const GravityFlags DefaultGravity = GravityFlags.CenterVertical;

        #endregion

        #region Parent override

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                //Control.ImeOptions = ImeAction.ImeNull;
                Control.ImeOptions = ImeAction.Next;

                Control.EditorAction += (sender, args) =>
                {
                    if (args.ActionId == ImeAction.Next)
                    {
                        var entry = (EntryWithoutDoneBtn)Element;
                        if (entry.ReturnType != Controls.ReturnType.Next)
                            entry.Unfocus();

                        // Call all the methods attached to base_entry event handler Completed  
                        entry.InvokeCompleted();
                    }
                };
            }
            if (e.OldElement != null || this.Element == null)
                return;
            Control.Gravity = DefaultGravity;
            //this.Control.ImeOptions = Android.Views.InputMethods.ImeAction.ImeNull;
            Control.SetRawInputType(Android.Text.InputTypes.ClassNumber);
            var entryEx = Element as EntryWithoutDoneBtn;
            
            UpdateBackground(entryEx);
            UpdatePadding(entryEx);
            UpdateTextAlighnment(entryEx);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Control.ImeOptions = Android.Views.InputMethods.ImeAction.ImeNull;
            Control.SetRawInputType(Android.Text.InputTypes.ClassNumber);

            base.OnElementPropertyChanged(sender, e);
            if (Element == null)
                return;
            var entryEx = Element as EntryWithoutDoneBtn;
            if (e.PropertyName == EntryWithoutDoneBtn.BorderWidthProperty.PropertyName ||
                e.PropertyName == EntryWithoutDoneBtn.BorderColorProperty.PropertyName ||
                e.PropertyName == EntryWithoutDoneBtn.BorderRadiusProperty.PropertyName ||
                e.PropertyName == EntryWithoutDoneBtn.BackgroundColorProperty.PropertyName)
            {
                UpdateBackground(entryEx);
            }
            else if (e.PropertyName == EntryWithoutDoneBtn.LeftPaddingProperty.PropertyName ||
                e.PropertyName == EntryWithoutDoneBtn.RightPaddingProperty.PropertyName)
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

        private void UpdateBackground(EntryWithoutDoneBtn entryEx)
        {
            if (_renderer != null)
            {
                _renderer.Dispose();
                _renderer = null;
            }
            _renderer = new BorderRenderer();

            Control.Background = _renderer.GetBorderBackground(entryEx.BorderColor, entryEx.BackgroundColor, entryEx.BorderWidth, entryEx.BorderRadius);
            this.Control.ImeOptions = Android.Views.InputMethods.ImeAction.ImeNull;
        }

        private void UpdatePadding(EntryWithoutDoneBtn entryEx)
        {
            Control.SetPadding((int)Forms.Context.ToPixels(entryEx.LeftPadding), 0,
                (int)Forms.Context.ToPixels(entryEx.RightPadding), 0);
            this.Control.ImeOptions = Android.Views.InputMethods.ImeAction.ImeNull;
        }

        private void UpdateTextAlighnment(EntryWithoutDoneBtn entryEx)
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
            this.Control.ImeOptions = Android.Views.InputMethods.ImeAction.ImeNull;
        }

        #endregion
    }
}

