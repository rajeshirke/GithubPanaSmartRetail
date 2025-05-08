using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Retail.Hepler;
using Xamarin.Forms;

namespace Retail.ViewModels.DemoControls
{
    public class BarcodeViewModel : BaseViewModel
    {
        public event EventHandler SignaturePadViewItem;
        public string signatureBase64string { get; set; }
        public string selectedItem { get; set; }
        public BarcodeViewModel(INavigation navigation): base(navigation)
        {
           
        }
        public ICommand AddControlCmmand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                         ModelLists.Add(new ModelList
                            {
                                ModelNo=ModelNumber,
                                Qty=Quantity,    
                            });
                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new Command<ModelList>(async (item) =>
                {
                    ModelLists.Remove(item);
                });
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return new Command<ModelList>(async (item) =>
                {

                    ModelNumber = item.ModelNo;
                    Quantity = item.Qty;
                    ModelLists.Remove(item);
                });
            }
        }

        public ObservableCollection<ModelList> ModelLists { get; set; } =
            new ObservableCollection<ModelList>();

        

        public Command ModelNoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        var qrcode = await Extensions.QrScanning();
                        if (qrcode != null)
                        {
                            ModelNo = qrcode;
                        }

                    }
                    catch (Exception ex)
                    {

                    }
                });
            }
        }
        
        public Command plusCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                       CartCount= Convert.ToInt32( CartCount) + 1;
                    
                });
            }
        }
        public Command minusCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (CartCount > 0)
                    {
                        CartCount = Convert.ToInt32(CartCount) - 1;
                    }
                });
            }
        }

        private string _ModelNo;
        public string ModelNo
        {
            get { return _ModelNo; }
            set
            {
                
                _ModelNo = value.ToUpper();
                OnPropertyChanged("ModelNo");
            }
        }

        private string _ModelNumber;
        public string ModelNumber
        {
            get { return _ModelNumber; }
            set
            {

                _ModelNumber = value;
                OnPropertyChanged("ModelNumber");
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

        private int _CartCount;
        public int CartCount
        {
            get { return _CartCount; }
            set
            {

                _CartCount = value;
                OnPropertyChanged("CartCount");
            }
        }
    }

}


public class ModelList
{
    public int Id { get; set; }
    public string ModelNo { get; set; }
    public string Qty { get; set; }
    public int Row { get; set; }
    
}