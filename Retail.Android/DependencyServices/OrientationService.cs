using System;
using Android.App;
using Android.Content.PM;
using Retail.DependencyServices;
using Retail.Droid.DependencyServices;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(OrientationService))]
namespace Retail.Droid.DependencyServices
{
    public class OrientationService : IOrientationService
    {
        [Obsolete]
        public void Landscape()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Landscape;
        }

        [Obsolete]
        public void Portrait()
        {
            ((Activity)Forms.Context).RequestedOrientation = ScreenOrientation.Portrait;
        }

    }
}
