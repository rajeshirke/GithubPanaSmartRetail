using System;
using System.Collections.Generic;
using Retail.ViewModels.MyVisits;
using Xamarin.Forms;

namespace Retail.Views.MyVisits
{
    public partial class TaskActionsView : ContentPage
    {
        public TaskActionsView()
        {
            InitializeComponent();
            BindingContext = new TaskAtionsViewModel(Navigation);
        }
    }
}
