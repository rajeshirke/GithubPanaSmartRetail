using System;
using System.Diagnostics;
using System.Drawing;
using CoreGraphics;
using Retail.Controls;
using Retail.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(RoundedCornerView), typeof(RoundedCornerViewRenderer))]
namespace Retail.iOS.Renderers
{
    public class RoundedCornerViewRenderer : FrameRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (this.Element == null) return;


            this.Element.PropertyChanged += (sender, e1) => {
                try
                {
                    if (NativeView != null)
                    {
                        NativeView.SetNeedsDisplay();
                        NativeView.SetNeedsLayout();
                    }
                }
                catch (Exception exp)
                {
                    Debug.WriteLine("Handled Exception in RoundedCornerViewDemoRenderer. Just warngin : " + exp.Message);
                }
            };
        }
        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);
            this.LayoutIfNeeded();
            RoundedCornerView rcv = (RoundedCornerView)Element;
            //rcv.HasShadow = false;      
            rcv.Padding = new Thickness(0, 0, 0, 0);
            //this.BackgroundColor = rcv.FillColor.ToUIColor();      
            this.ClipsToBounds = true;
            this.Layer.BackgroundColor = rcv.FillColor.ToCGColor();
            this.Layer.MasksToBounds = true;
            this.Layer.CornerRadius = (nfloat)rcv.RoundedCornerRadius;
            if (rcv.MakeCircle)
            {
                this.Layer.CornerRadius = (int)(Math.Min(Element.Width, Element.Height) / 2);
            }
            this.Layer.BorderWidth = 0;

            if (rcv.BorderWidth > 0 && rcv.BorderColor.A > 0.0)
            {
                this.Layer.BorderWidth = rcv.BorderWidth;
                this.Layer.BorderColor =
                     new UIKit.UIColor(
                     (nfloat)rcv.BorderColor.R,
                     (nfloat)rcv.BorderColor.G,
                     (nfloat)rcv.BorderColor.B,
                         (nfloat)rcv.BorderColor.A).CGColor;
            }
            if (rcv.Shadow)
            {
                Layer.ShadowRadius = rcv.ShadowRadius;
                Layer.ShadowColor = UIColor.Gray.CGColor;
                Layer.ShadowOffset = new CGSize(2, 2);
                Layer.ShadowOpacity = 0.50f;
                Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
                Layer.MasksToBounds = false;
            }
        }
    }
}
