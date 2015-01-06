using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PROG6_Assessment.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PROG6_Assessment.ViewModel
{
    public class ProductListViewModel : ViewModelBase
    {
        public ObservableCollection<ProductViewModel> Products { get; set; }

        private ProductViewModel _selectedProduct;
        private IProductRepository productRepository;

        public ProductViewModel SelectedProduct 
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; RaisePropertyChanged(); }
        }


        // ICommands
        public ICommand AddProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand ClearProductCommand { get; set; }

        // Constructor
        public ProductListViewModel()
        {
            productRepository = new DummyProductRepository();
            // productRepository = new ProductRepository();

            var productList = productRepository.ToList().Select(s => new ProductViewModel(s));

            AddProductCommand = new RelayCommand(AddNewProduct, CanAddProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
            ClearProductCommand = new RelayCommand(ClearProduct, CanClear);

            Products = new ObservableCollection<ProductViewModel>(productList);
            SelectedProduct = new ProductViewModel();
        }


        // ---------------- Add Product ---------------- //
        private void AddNewProduct()
        {
            var product = new ProductViewModel();

            product.ProductNaam = SelectedProduct.ProductNaam;

            Products.Add(product);
        }

        private bool CanAddProduct()
        {
            if (SelectedProduct == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedProduct.ProductNaam))
            {
                return false;
            }

            return true;
        }

        // ---------------- Delete Product ---------------- //
        private void DeleteProduct()
        {
            Products.Remove(SelectedProduct);
            SelectedProduct = new ProductViewModel();
        }

        private bool CanDeleteProduct()
        {
            return SelectedProduct != null;
        }

        // ---------------- Clear Product ---------------- //
        private void ClearProduct()
        {
            SelectedProduct = new ProductViewModel();
        }

        private bool CanClear()
        {
            return true;
        }
    }
}
