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
    public class ReceptListViewModel : ViewModelBase
    {
        public ObservableCollection<ReceptViewModel> Recepten { get; set; }
        public ObservableCollection<ProductViewModel> ProductenGekozenRecept { get; set; }

        private ReceptViewModel _selectedRecept;
        private ProductViewModel _selectedProduct;
        private IReceptRepository receptRepository;
        private IProductRepository productRepository;
        private int ReceptId;

        public ReceptViewModel SelectedRecept
        {
            get { return _selectedRecept; }
            set { _selectedRecept = value; RaisePropertyChanged(); }
        }

        public ProductViewModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; RaisePropertyChanged(); }
        }

        // ICommands
        public ICommand AddReceptCommand { get; set; }
        public ICommand EditReceptCommand { get; set; }
        public ICommand DeleteReceptCommand { get; set; }
        public ICommand ClearReceptCommand { get; set; }

        public ICommand AddProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }

        // Constructor
        public ReceptListViewModel()
        {
            receptRepository = new ReceptRepository();
            productRepository = new ProductRepository();

            var receptList = receptRepository.GetAll().Select(s => new ReceptViewModel(s));

            AddReceptCommand = new RelayCommand(AddRecept, CanAddRecept);
            EditReceptCommand = new RelayCommand(EditRecept, CanEditRecept);
            DeleteReceptCommand = new RelayCommand(DeleteRecept, CanDeleteRecept);
            ClearReceptCommand = new RelayCommand(ClearRecept, CanClear);
            AddProductCommand = new RelayCommand(AddProduct, CanAddProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
            //ShowProductsCommand = new RelayCommand(ShowProductOverzicht, canShowProductOverzicht);

            Recepten = new ObservableCollection<ReceptViewModel>(receptList);
            SelectedRecept = new ReceptViewModel();
        }

        // ---------------- Add Afdeling ---------------- //
        private void AddRecept()
        {
            var recept = new ReceptViewModel();

            recept.ReceptNaam = SelectedRecept.ReceptNaam;
            var addRecept = recept.ConvertToRecept(recept);

            receptRepository.Create(addRecept);

            recept.ReceptId = addRecept.ReceptId;
            Recepten.Add(recept);
        }

        private bool CanAddRecept()
        {
            if (SelectedRecept == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedRecept.ReceptNaam))
            {
                return false;
            }

            return true;
        }

        // ---------------- Edit Afdeling ---------------- //
        private void EditRecept()
        {
            Recept updateRecept = SelectedRecept.ConvertToRecept(SelectedRecept);
            receptRepository.Update(updateRecept);
        }

        private bool CanEditRecept()
        {
            if (SelectedRecept == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedRecept.ReceptNaam))
            {
                return false;
            }
            return true;
        }

        // ---------------- Delete Afdeling ---------------- //
        private void DeleteRecept()
        {

            var removeRecept = SelectedRecept.ConvertToRecept(SelectedRecept);

            receptRepository.Delete(removeRecept);
            Recepten.Remove(SelectedRecept);

            SelectedRecept = new ReceptViewModel();
        }

        private bool CanDeleteRecept()
        {
            return SelectedRecept != null;
        }

        // ---------------- Clear Afdeling ---------------- //
        private void ClearRecept()
        {
            SelectedRecept = new ReceptViewModel();
        }

        private bool CanClear()
        {
            return true;
        }

        // ---------------- Add Afdeling ---------------- //
        private void AddProduct()
        {
            var product = new ProductViewModel();

            product.ProductNaam = SelectedProduct.ProductNaam;
            var addProduct = product.ConvertToProduct(product);

            product.ProductId = addProduct.ProductId;

            foreach (ReceptViewModel rvm in Recepten)
            {

                if (rvm.ReceptId == ReceptId)
                {

                    rvm.Producten.Add(addProduct);
                    break;

                }

            }
        }

        private bool CanAddProduct()
        {
            return false;

        }

        // ---------------- Delete Afdeling ---------------- //
        private void DeleteProduct()
        {

            var removeProduct = SelectedProduct.ConvertToProduct(SelectedProduct);


            foreach (ReceptViewModel rvm in Recepten)
            {

                if (rvm.ReceptId == ReceptId)
                {

                    rvm.Producten.Remove(removeProduct);
                    break;

                }

            }

        }

        private bool CanDeleteProduct()
        {
            return true;
        }

        public void GekozenRecept(int id, List<ProductViewModel> products)
        {
            ProductenGekozenRecept.Clear();

            foreach (ProductViewModel pvm in products)
            {

                foreach (ReceptViewModel rvm in Recepten)
                {

                    if (rvm.ReceptId == id)
                    {

                        ProductenGekozenRecept.Add(pvm);

                    }
                }
            }
        }

    }
}
