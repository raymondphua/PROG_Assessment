using DomainModel.Model;
using DomainModel.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PROG6_Assessment.ViewModel
{
    public class BoodschappenlijstjeListViewModel : ViewModelBase
    {
        public ObservableCollection<ProductViewModel> Lijstjes { get; set; }

        private BoodschappenlijstjeViewModel _selectedLijstje;
        private ProductViewModel _selectedProduct;
        private IBoodschappenlijstjeRepository lijstRepository;
        private IProductRepository productRepository;

        public BoodschappenlijstjeViewModel SelectedLijstje
        {
            get { return _selectedLijstje; }
            set { _selectedLijstje = value; RaisePropertyChanged(); }
        }

        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; RaisePropertyChanged(); }
        }
        
        // ICommands
        public ICommand AddLijstjeCommand { get; set; }
        public ICommand EditLijstjeCommand { get; set; }
        public ICommand DeleteLijstjeCommand { get; set; }
        public ICommand ClearLijstjeCommand { get; set; }

        // Constructor
        public BoodschappenlijstjeListViewModel()
        {
            _selectedProduct = new ProductViewModel();
            lijstRepository = new BoodschappenlijstjeRepository();
            productRepository = new ProductRepository();

            //var lijstList = productRepository.GetAll().Select(s => new ProductViewModel(s));

            AddLijstjeCommand = new RelayCommand(AddLijst, CanAddLijst);
            //EditLijstjeCommand = new RelayCommand(EditLijst, CanEditLijst);
            //DeleteLijstjeCommand = new RelayCommand(DeleteLijst, CanDeleteLijst);
            //ClearLijstjeCommand = new RelayCommand(ClearLijst, CanClear);
            //ShowProductsCommand = new RelayCommand(ShowProductOverzicht, canShowProductOverzicht);



            SelectedLijstje = new BoodschappenlijstjeViewModel();
        }

        //// ---------------- Add Afdeling ---------------- //
        private void AddLijst()
        {
            var product = Lijstjes.FirstOrDefault(x => x.ProductId == SelectedProduct.ProductId);

            if (product != null)
            {
                product.Aantal++;
            }
            else
            {
                var newProduct = new ProductViewModel();
                newProduct.ProductId = SelectedProduct.ProductId;
                newProduct.ProductNaam = SelectedProduct.ProductNaam;
                newProduct.Prijs = SelectedProduct.Prijs;
                newProduct.Merken = SelectedProduct.Merken;
                newProduct.Kortingen = SelectedProduct.Kortingen;
                newProduct.Aantal = 1;
                Lijstjes.Add(product);
            }
        }

        private bool CanAddLijst()
        {
            if (SelectedProduct == null)
            {
                return false;
            }

            return true;
        }

        //// ---------------- Edit Afdeling ---------------- //
        //private void EditLijst()
        //{
        //    Boodschappenlijstje updateLijst = SelectedLijstje.ConvertToLijst(SelectedLijstje);
        //    lijstRepository.Update(updateLijst);
        //}

        //private bool CanEditLijst()
        //{
        //    if (SelectedLijstje == null)
        //    {
        //        return false;
        //    }
        //    if (SelectedLijstje.Producten.Count == 0)
        //    {
        //        return false;
        //    }
        //    return true;
        //}

        //// ---------------- Delete Afdeling ---------------- //
        //private void DeleteLijst()
        //{

        //    var removeLijst = SelectedLijstje.ConvertToLijst(SelectedLijstje);

        //    lijstRepository.Delete(removeLijst);
        //    Lijstjes.Remove(SelectedLijstje);

        //    SelectedLijstje = new BoodschappenlijstjeViewModel();
        //}

        //private bool CanDeleteLijst()
        //{
        //    return SelectedLijstje != null;
        //}

        //// ---------------- Clear Afdeling ---------------- //
        //private void ClearLijst()
        //{
        //    SelectedLijstje = new BoodschappenlijstjeViewModel();
        //}

        //private bool CanClear()
        //{
        //    return true;
        //}
    }
}
