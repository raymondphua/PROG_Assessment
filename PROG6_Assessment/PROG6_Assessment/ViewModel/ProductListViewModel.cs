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
        public ObservableCollection<ProductViewModel> AddProducten { get; set; }
        public ObservableCollection<ProductViewModel> ProductenGekozenRecept { get; set; }

        private ProductViewModel _selectedProduct;
        private IProductRepository productRepository;
        private IMerkRepository merkRepository;
        private IAfdelingRepository afdelingRepository;
        private IKortingRepository kortingRepository;
        private IReceptRepository receptRepository;

        private double totaalPrijs;

        public double TotaalPrijs
        {
            get { return totaalPrijs; }
            set { totaalPrijs = value; RaisePropertyChanged(); }
        }

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
        public ICommand BoodschappenCommand { get; set; }

        // Constructor
        public ProductListViewModel()
        {
            productRepository = new ProductRepository();
            merkRepository = new MerkRepository();
            afdelingRepository = new AfdelingRepository();
            kortingRepository = new KortingRepository();
            receptRepository = new ReceptRepository();

            var productList = productRepository.GetAll().Select(s => new ProductViewModel(s));

            AddProductCommand = new RelayCommand(AddNewProduct, CanAddProduct);
            EditProductCommand = new RelayCommand(EditProduct, CanEditProduct);
            DeleteProductCommand = new RelayCommand(DeleteProduct, CanDeleteProduct);
            ClearProductCommand = new RelayCommand(ClearProduct, CanClear);
            BoodschappenCommand = new RelayCommand(AddProductBoodschappen, CanAddBoodschappen);

            Products = new ObservableCollection<ProductViewModel>(productList);
            ProductenGekozenAfdeling = new ObservableCollection<ProductViewModel>();
            AddProducten = new ObservableCollection<ProductViewModel>();
            ProductenGekozenRecept = new ObservableCollection<ProductViewModel>();

            ////Lijstjes = new ObservableCollection<ProductViewModel>();
            //var item = productRepository.Find(1);
            //ProductViewModel swag = new ProductViewModel();
            //swag.ProductId = item.ProductId;
            //swag.ProductNaam = item.ProductNaam;
            //swag.Aantal = 1;
            //AddProducten.Add(swag);

            SelectedProduct = new ProductViewModel();
        }

        private void AddProductBoodschappen()
        {
            var product = AddProducten.FirstOrDefault(x => x.ProductId == SelectedProduct.ProductId);

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
                newProduct.Recept = SelectedProduct.Recept;
                newProduct.Aantal = 1;
                AddProducten.Add(newProduct);
            }

            BerekenPrijs();
        }

        public void AddReceptList(List<ProductViewModel> producten)
        {
            foreach (var item in producten)
            {
                var product = AddProducten.FirstOrDefault(x => x.ProductId == item.ProductId);

                if (product != null)
                {
                    product.Aantal++;
                }
                else
                {
                    var newProduct = new ProductViewModel();
                    newProduct.ProductId = item.ProductId;
                    newProduct.ProductNaam = item.ProductNaam;
                    newProduct.Prijs = item.Prijs;
                    newProduct.Merken = item.Merken;
                    newProduct.Kortingen = item.Kortingen;
                    newProduct.Recept = item.Recept;
                    newProduct.Aantal = 1;
                    AddProducten.Add(newProduct);
                }
            }

            BerekenPrijs();
        }
        private void BerekenPrijs()
        {
            // BEGIN MET REKENEN BITCH
            TotaalPrijs = 0;
            double x = 0;

            foreach (var item in AddProducten)
            {
                //foreach (var merk in item.Merken)
                //{
                //    if (merk.Product != null)
                //    {
                //        if (merk.Product.ProductId == item.ProductId)
                //        {
                //            // prijs van een product van een bepaalde merk
                //            x = item.Prijs * merk.Multiplier;
                //        }
                //    }
                //}
                x = item.Prijs;
                foreach (var korting in item.Kortingen)
                {
                    if (korting.Product != null)
                    {
                        if (korting.Product.ProductId == item.ProductId)
                        {
                            if (DateTime.Now >= korting.StartDatum && DateTime.Now <= korting.EindDatum)
                            {
                                x = (item.Prijs * 0.5);
                            }
                        }
                    }
                }

                TotaalPrijs = TotaalPrijs + (x * item.Aantal);
            }
        }

        private bool CanAddBoodschappen()
        {
            return true;
        }

        // ---------------- Add Product ---------------- //
        private void AddNewProduct()
        {
            var product = new ProductViewModel();

            // afdeling ophalen.
            AfdelingViewModel afdeling = (AfdelingViewModel)SelectedProduct.SelectedAfdeling;
            ReceptViewModel recept = (ReceptViewModel)SelectedProduct.SelectedRecept;

            product.ProductNaam = SelectedProduct.ProductNaam;
            product.Prijs = SelectedProduct.Prijs;
            product.Afdeling = afdelingRepository.Find(afdeling.AfdelingId);
            product.Recept = receptRepository.Find(recept.ReceptId);

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
            ReceptViewModel recept = (ReceptViewModel)SelectedProduct.SelectedRecept;

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

            if (recept == null)
            {
                product.Recept = SelectedProduct.Recept;
            }
            else
            {
                product.Recept = receptRepository.Find(recept.ReceptId);
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
                item.Recept = product.Recept;
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
                        foreignKeyFix2.Multiplier = item.Multiplier;
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

        public void GekozenReceptShow(int id)
        {
            ProductenGekozenRecept.Clear();

            foreach(ProductViewModel item in Products)
            {
                if (item.Recept.ReceptId == id)
                {
                    ProductViewModel p = new ProductViewModel();

                    p.ProductId = item.ProductId;
                    p.Prijs = item.Prijs;
                    p.ProductNaam = item.ProductNaam;
                    p.Afdeling = item.Afdeling;
                    p.Merken = item.Merken;
                    p.Kortingen = item.Kortingen;
                    p.Recept = item.Recept;

                    ProductenGekozenRecept.Add(p);
                }
            }
        }
    }
}
