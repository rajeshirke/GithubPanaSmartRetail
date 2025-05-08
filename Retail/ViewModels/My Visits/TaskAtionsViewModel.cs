using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Retail.ViewModels.MyVisits
{
    public class TaskAtionsViewModel : BaseViewModel
    {
        public TaskAtionsViewModel(INavigation navigation):base(navigation)
        {
            TaskActionsData.Add(new TaskActions { TaskActionName = "Take Display photo" });
            TaskActionsData.Add(new TaskActions { TaskActionName = "Number of units in stock" });
            TaskActionsData.Add(new TaskActions { TaskActionName = "Product display positioning" });
            TaskActionsData.Add(new TaskActions { TaskActionName = "Store manager signature" });
        }

        public ObservableCollection<TaskActions> TaskActionsData { get; set; } =
            new ObservableCollection<TaskActions>();

        public Command TaskActionCommand
        {
            get
            {
                return new Command(() =>
                {
                    //Navigation.PushAsync(new VisitTasksView(item.StoreName, item.StoreAddress, item.Distance));
                });
            }
        }

        private string _TaskActionName;
        public string TaskActionName
        {
            get { return _TaskActionName; }
            set
            {
                _TaskActionName = value;
                OnPropertyChanged("TaskActionName");
            }
        }

    }
}

public class TaskActions
{
    public  string TaskActionName { get; set; }
}