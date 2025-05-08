using System;
using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using Retail.Controls;
using Retail.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ImageEntry), typeof(ImageEntryRenderer))]
namespace Retail.iOS.Renderers
{
    public class ImageEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (ImageEntry)this.Element;
            var textField = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case (Controls.ImageAlignment)ImageAlignment.Left:
                        UIView uIView = new UIView(new CGRect(0, 0, 40, 40));
                        textField.LeftViewMode = UITextFieldViewMode.Always;
                        textField.LeftView = uIView;//GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                       // textField.LeftView.Frame = new CGRect(2, 0, 0, 0);
                        break;
                    case (Controls.ImageAlignment)ImageAlignment.Right:
                        textField.RightViewMode = UITextFieldViewMode.Always;
                        textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                       // textField.LeftView.Frame = new CGRect(2, 0, 0, 0);
                        break;
                }
            }
            if(element.LeftImageSpace){
                UIView uIView = new UIView(new CGRect(0, 0, 40, 40));
                textField.LeftViewMode = UITextFieldViewMode.Always;
                textField.LeftView = uIView;
            }
            textField.BorderStyle = UITextBorderStyle.None;
            //CALayer bottomBorder = new CALayer
            //{
            //    Frame = new CGRect(0.0f, element.HeightRequest - 1, UIScreen.MainScreen.Bounds.Width, 1.0f),
            //    BorderWidth = 2.0f,
            //    BorderColor = element.LineColor.ToCGColor()
            //};

            //textField.Layer.AddSublayer(bottomBorder);
            //textField.Layer.MasksToBounds = true;
            
            textField.BorderStyle = UITextBorderStyle.None;
          //  textField.Layer.CornerRadius = 8;
          //  textField.Layer.BorderWidth = .5f;
          //  textField.Layer.BorderColor = UIColor.FromRGB(200, 200, 200).CGColor;

           
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
    }
}
