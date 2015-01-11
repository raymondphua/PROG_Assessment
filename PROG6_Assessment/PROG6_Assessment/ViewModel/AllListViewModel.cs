using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PROG6_Assessment.ViewModel
{
    public class AllListViewModel : ViewModelBase
    {
        private AfdelingViewModel _selectedAfdeling;
        private ProductViewModel _selectedProduct;
        private ProductOverzicht productOverzichtWindow;
        private MerkOverzicht merkOverzichtWindow;

        public ObservableCollection<ProductViewModel> ProductenGekozenAfdeling { get; set; }

        public MerkListViewModel MerkList { get; set; }
        public ProductListViewModel ProductList { get; set; }
        public AfdelingListViewModel AfdelingList { get; set; }
        public KortingListViewModel KortingList { get; set; }

        public AfdelingViewModel SelectedAfdeling 
        { 
            get { return  _selectedAfdeling; }
            set { _selectedAfdeling = value; RaisePropertyChanged(); } 
        }

        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; RaisePropertyChanged(); }
        }

        public ICommand ShowProductenCommand { get; set; }
        public ICommand ShowMerkenCommand { get; set; }

        public AllListViewModel()
        {
            _selectedAfdeling = new AfdelingViewModel();
            _selectedProduct = new ProductViewModel();

            ProductenGekozenAfdeling = new ObservableCollection<ProductViewModel>();
            AfdelingList = new AfdelingListViewModel();
            MerkList = new MerkListViewModel();
            ProductList = new ProductListViewModel();
            KortingList = new KortingListViewModel();

            ShowProductenCommand = new RelayCommand(ShowProductOverzicht, canShowProductOverzicht);
            ShowMerkenCommand = new RelayCommand(ShowMerkenOverzicht, canShowMerkOverzicht);
        }

        // ---------------- Product Overzicht ---------------- //
        private void ShowProductOverzicht()
        {
            ProductList.GekozenAfdelingShow(SelectedAfdeling.AfdelingId);
            productOverzichtWindow.Show();
        }
        private bool canShowProductOverzicht()
        {
            productOverzichtWindow = new ProductOverzicht();
            return productOverzichtWindow.IsVisible == false;
        }

        // -------------- Merken Overzicht ----------------//
        private void ShowMerkenOverzicht()
        {
            MerkList.GekozenMerkShow(SelectedProduct.ProductId);
            merkOverzichtWindow.Show();
        }

        private bool canShowMerkOverzicht()
        {
            merkOverzichtWindow = new MerkOverzicht();
            return merkOverzichtWindow.IsVisible == false;
        }
    }
}
