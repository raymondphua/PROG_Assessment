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
    public class ProductListViewModel : ViewModelBase
    {
        public ObservableCollection<ProductViewModel> Products { get; set; }
        public ObservableCollection<ProductViewModel> ProductenGekozenAfdeling { get; set; }

        private ProductViewModel _selectedProduct;
        private IProductRepository productRepository;
        private IMerkRepository merkRepository;
        private IAfdelingRepository afdelingRepository;
        private IKortingRepository kortingRepository;

        public ProductViewModel SelectedProduct 
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; RaisePropertyChanged(); }
        }


        // ICommands
        public ICommand AddProductCommand { get; set; }
        public ICommand EditProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand ClearProductCommand { get; set; }

        // Constructor
        public ProductListViewModel()
        {
            productRepository = new ProductRepository();
            merkRepository = new MerkRepository();
            afdelingRepository = new AfdelingRepository();
            kortingRepository = new KortingRepository();

            var productList = productRepository.GetAll().Select(s => new ProductViewModel(s));

            AddProductCommand = new RelayCommand(AddNewProduct, CanAddProduct);
            EditProductCommand = new RelayCommand(EditProduct, CanEditProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
            ClearProductCommand = new RelayCommand(ClearProduct, CanClear);
            
            Products = new ObservableCollection<ProductViewModel>(productList);
            ProductenGekozenAfdeling = new ObservableCollection<ProductViewModel>();

            SelectedProduct = new ProductViewModel();
        }


        // ---------------- Add Product ---------------- //
        private void AddNewProduct()
        {
            var product = new ProductViewModel();

            // afdeling ophalen.
            AfdelingViewModel afdeling = (AfdelingViewModel)SelectedProduct.SelectedAfdeling;

            product.ProductNaam = SelectedProduct.ProductNaam;
            product.Prijs = SelectedProduct.Prijs;
            product.Afdeling = afdelingRepository.Find(afdeling.AfdelingId);

            var addProduct = product.ConvertToProduct(product);
            productRepository.Create(addProduct);

            product.ProductId = addProduct.ProductId;
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
            if (SelectedProduct.SelectedAfdeling == null)
            {
                return false;
            }

            return true;
        }

        // ---------------- Edit Product ---------------- //
        private void EditProduct()
        {
            var product = new ProductViewModel();

            // afdeling ophalen.
            AfdelingViewModel afdeling = (AfdelingViewModel)SelectedProduct.SelectedAfdeling;

            product.ProductId = SelectedProduct.ProductId;
            product.ProductNaam = SelectedProduct.ProductNaam;
            product.Prijs = SelectedProduct.Prijs;

            if (afdeling == null)
            {
                product.Afdeling = SelectedProduct.Afdeling;
            }
            else
            {
                product.Afdeling = afdelingRepository.Find(afdeling.AfdelingId);
            }
            product.Kortingen = SelectedProduct.Kortingen;
            Product updateProduct = product.ConvertToProduct(product);
            productRepository.Update(updateProduct);

            var item = Products.FirstOrDefault(i => i.ProductId == product.ProductId);
            if (item != null)
            {
                item.ProductId = product.ProductId;
                item.ProductNaam = product.ProductNaam;
                item.Prijs = product.Prijs;
                item.Afdeling = product.Afdeling;
                item.Merken = product.Merken;
            }
        }

        private bool CanEditProduct()
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
            var kortingList = kortingRepository.GetAll();
            var merkList = merkRepository.GetAll();

            var foreignKeyFix = new Korting();

            foreach(var item in kortingList)
            {
                if (item.Product != null)
                {
                    if (item.Product.ProductId == SelectedProduct.ProductId)
                    {
                        foreignKeyFix.KortingId = item.KortingId;
                        foreignKeyFix.Coupon = item.Coupon;
                        foreignKeyFix.StartDatum = item.StartDatum;
                        foreignKeyFix.EindDatum = item.EindDatum;
                        foreignKeyFix.Product = null;

                        kortingRepository.Update(foreignKeyFix);
                    }
                }
            }

            foreach(var item in merkList)
            {
                if (item.Product != null)
                {
                    if (item.Product.ProductId == SelectedProduct.ProductId)
                    {
                        var foreignKeyFix2 = new Merk();

                        foreignKeyFix2.MerkId = item.MerkId;
                        foreignKeyFix2.MerkNaam = item.MerkNaam;
                        foreignKeyFix2.Product = null;

                        merkRepository.Update(foreignKeyFix2);
                    }
                }
            }
            var deleteProduct = SelectedProduct.ConvertToProduct(SelectedProduct);

            productRepository.Delete(deleteProduct);
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

        // lijst van gekozen afdeling id 
        public void GekozenAfdelingShow(int id)
        {
            ProductenGekozenAfdeling.Clear();

            foreach (var item in Products)
            {
                if (item.Afdeling != null)
                {
                    if (item.Afdeling.AfdelingId == id)
                    {
                        ProductViewModel p = new ProductViewModel();

                        p.ProductId = item.ProductId;
                        p.Prijs = item.Prijs;
                        p.ProductNaam = item.ProductNaam;
                        p.Afdeling = item.Afdeling;
                        
                        ProductenGekozenAfdeling.Add(p);
                    }
                }
            }
        }
    }
}
