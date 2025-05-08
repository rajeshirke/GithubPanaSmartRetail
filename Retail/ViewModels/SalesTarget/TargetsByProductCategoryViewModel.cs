using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Retail.ViewModels.SalesTarget
{
    public class TargetsByProductCategoryViewModel : BaseViewModel
    {
        public TargetsByProductCategoryViewModel(INavigation navigation, string MachineName) : base(navigation)
        {
            ModelName = MachineName;


            TargetByProductData.Add(new TargetData { TargetDate = DateTime.Now, TargetModelName = "NA-F130A5WRN", Quantity = "20", Amount = "20,000 AED" });
            TargetByProductData.Add(new TargetData { TargetDate = DateTime.Now, TargetModelName = "KA-F155A5WRN", Quantity = "50", Amount = "15,000 AED" });
            TargetByProductData.Add(new TargetData { TargetDate = DateTime.Now, TargetModelName = "MA-F250A5WRN", Quantity = "60", Amount = "25,000 AED" });
            TargetByProductData.Add(new TargetData { TargetDate = DateTime.Now, TargetModelName = "BA-F250A5WRN", Quantity = "60", Amount = "25,000 AED" });

        }

        public async Task BindingProductCategoryData()
        {
            
        }

        public void SearchTargetByProduct (string ProductName)
        {
            if (!string.IsNullOrEmpty(ProductName))
            {
                var SearchTargetByProduct = SearchByProductData.Where(u => u.TargetModelName.Contains(ProductName));
                TargetByProductData = new ObservableCollection<TargetData>(SearchTargetByProduct);
            }
            else
                TargetByProductData = SearchByProductData;
        }

        public ObservableCollection<TargetData> TargetByProductData { get; set; } =
           new ObservableCollection<TargetData>();

        public ObservableCollection<TargetData> SearchByProductData { get; set; } =
           new ObservableCollection<TargetData>();


        private string _ModelName;
        public string ModelName

        {
            get { return _ModelName; }
            set
            {
                _ModelName = value;
                OnPropertyChanged("ModelName");
            }
        }

        private string _Quantity;
        public string Quantity

        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }

        private string _Amount;
        public string Amount

        {
            get { return _Amount; }
            set
            {
                _Amount = value;
                OnPropertyChanged("Amount");
            }
        }
    }
}

public class TargetData
{
    public DateTime TargetDate { get; set; }
    public string TargetModelName { get; set; }
    public string Quantity { get; set; }
    public string Amount { get; set; }
}

