using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.ViewModel
{
    public class ProductListViewModel : ViewModelBase
    {
        public ObservableCollection<ProductViewModel> Products { get; set; }

        private ProductViewModel _selectedProduct;

        public ProductViewModel SelectedProduct 
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; RaisePropertyChanged() }
        }
    }
}
