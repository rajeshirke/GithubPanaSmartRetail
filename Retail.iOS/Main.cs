﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AppCenter.Crashes;
using Foundation;
using UIKit;

namespace Retail.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }
    }
}
    