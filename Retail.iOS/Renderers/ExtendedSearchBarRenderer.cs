using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRendererAttribute(typeof(SearchBar), typeof(ExtendedSearchBarRenderer))]
namespace Retail.iOS.Renderers
{
    public class ExtendedSearchBarRenderer : SearchBarRenderer
    {

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var searchbar = (UISearchBar)Control;
            searchbar.BackgroundColor = UIColor.White;
            //searchbar.SearchBarStyle = UISearchBarStyle.Minimal;
        }
    }
}
