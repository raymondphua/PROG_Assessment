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
        private ReceptViewModel _selectedRecept;
        private ProductOverzicht productOverzichtWindow;
        private ReceptProductLijst receptProductLijstWindow;
        private MerkOverzicht merkOverzichtWindow;
        private ReceptenProductenWindow receptProductWindow;

        public ObservableCollection<ProductViewModel> ProductenGekozenAfdeling { get; set; }

        public MerkListViewModel MerkList { get; set; }
        public ProductListViewModel ProductList { get; set; }
        public AfdelingListViewModel AfdelingList { get; set; }
        public KortingListViewModel KortingList { get; set; }
        public BoodschappenlijstjeViewModel Lijstje { get; set; }
        public ReceptListViewModel ReceptList { get; set; }
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

        public ReceptViewModel SelectedRecept
        {

            get { return _selectedRecept; }
            set { _selectedRecept = value; RaisePropertyChanged(); }

        }

        public ICommand ShowProductenCommand { get; set; }
        public ICommand ShowProductenWindowCommand { get; set; }
        public ICommand ShowProductListCommand { get; set; }
        public ICommand ShowMerkenCommand { get; set; }

        public AllListViewModel()
        {
            _selectedAfdeling = new AfdelingViewModel();
            _selectedProduct = new ProductViewModel();
            _selectedRecept = new ReceptViewModel();

            ProductenGekozenAfdeling = new ObservableCollection<ProductViewModel>();
            AfdelingList = new AfdelingListViewModel();
            MerkList = new MerkListViewModel();
            ProductList = new ProductListViewModel();
            KortingList = new KortingListViewModel();
            Lijstje = new BoodschappenlijstjeViewModel();
            ReceptList = new ReceptListViewModel();

            DomainModel.Model.Product swag = new DomainModel.Model.Product();
            swag.ProductNaam = "Aardbei";

            Lijstje.Producten.Add(swag);

            ShowProductenCommand = new RelayCommand(ShowProductOverzicht, canShowProductOverzicht);
            ShowMerkenCommand = new RelayCommand(ShowMerkenOverzicht, canShowMerkOverzicht);
            ShowProductListCommand = new RelayCommand(ShowProductOverzichtRecept, canShowProductOverzichtRecept);
            ShowProductenWindowCommand = new RelayCommand(ShowProductOverzichtRecept2, canShowProductOverzichtRecept2);

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

        // ---------------- Product Overzicht ---------------- //
        private void ShowProductOverzichtRecept()
        {
            ReceptList.GekozenRecept(SelectedRecept.ReceptId, ProductList.Products.ToList());
            receptProductLijstWindow.Show();
        }
        private bool canShowProductOverzichtRecept()
        {
            receptProductLijstWindow = new ReceptProductLijst();
            return receptProductLijstWindow.IsVisible == false;
        }

        // ---------------- Product Overzicht ---------------- //
        private void ShowProductOverzichtRecept2()
        {
            ReceptList.GekozenRecept(SelectedRecept.ReceptId, ProductList.Products.ToList());
            receptProductWindow.Show();
        }
        private bool canShowProductOverzichtRecept2()
        {
            receptProductWindow = new ReceptenProductenWindow();
            return receptProductWindow.IsVisible == false;
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
