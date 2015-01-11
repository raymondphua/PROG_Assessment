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
    public class MerkListViewModel : ViewModelBase
    {
        public ObservableCollection<MerkViewModel> Merken { get; set; }
        public ObservableCollection<MerkViewModel> MerkGekozenProduct { get; set; }

        private MerkViewModel _selectedMerk;
        private IMerkRepository merkRepository;
        private IProductRepository productRepository;

        public MerkViewModel SelectedMerk 
        { 
            get { return _selectedMerk; }
            set { _selectedMerk = value; RaisePropertyChanged(); }
        }

        // ICommands
        public ICommand AddMerkCommand { get; set; }
        public ICommand EditMerkCommand { get; set; }
        public ICommand DeleteMerkCommand { get; set; }
        public ICommand ClearMerkCommand { get; set; }

        // Constructor
        public MerkListViewModel()
        {
            productRepository = new ProductRepository();
            merkRepository = new MerkRepository();
            var merkList = merkRepository.GetAll().Select(s => new MerkViewModel(s));

            AddMerkCommand = new RelayCommand(AddNewMerk, CanAddMerk);
            EditMerkCommand = new RelayCommand(EditMerk, CanEditMerk);
            DeleteMerkCommand = new RelayCommand(DeleteMerk, CanDeleteMerk);
            ClearMerkCommand = new RelayCommand(ClearMerk, CanClear);

            Merken = new ObservableCollection<MerkViewModel>(merkList);
            MerkGekozenProduct = new ObservableCollection<MerkViewModel>();
            SelectedMerk = new MerkViewModel();
        }


        // ---------------- Add Merk ---------------- //
        private void AddNewMerk()
        {
            var merk = new MerkViewModel();

            // haal product op
            ProductViewModel p = (ProductViewModel)SelectedMerk.SelectedProduct;

            merk.Merknaam = SelectedMerk.Merknaam;
            merk.Product = productRepository.Find(p.ProductId);
            merk.Multiplier = SelectedMerk.Multiplier;
            var addMerk = merk.ConvertToMerk(merk);

            merkRepository.Create(addMerk);

            merk.MerkId = addMerk.MerkId;
            Merken.Add(merk);
        }

        private bool CanAddMerk()
        {
            if (SelectedMerk == null)
            {
                return false;
            }
            if (SelectedMerk.SelectedProduct == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedMerk.Merknaam))
            {
                return false;
            }

            return true;
        }

        // ---------------- Edit Merk ---------------- //
        private void EditMerk()
        {
            var merk = new MerkViewModel();

            // product ophalen
            ProductViewModel p = (ProductViewModel)SelectedMerk.SelectedProduct;

            merk.MerkId = SelectedMerk.MerkId;
            merk.Merknaam = SelectedMerk.Merknaam;
            merk.Multiplier = SelectedMerk.Multiplier;

            if (p == null)
            {
                merk.Product = SelectedMerk.Product;
            }
            else
            {
                merk.Product = productRepository.Find(p.ProductId);
            }

            Merk updateMerk = merk.ConvertToMerk(merk);
            merkRepository.Update(updateMerk);

            var item = Merken.FirstOrDefault(i => i.MerkId == merk.MerkId);
            if (item != null)
            {
                item.MerkId = merk.MerkId;
                item.Merknaam = merk.Merknaam;
                item.Product = merk.Product;
                item.Multiplier = merk.Multiplier;
            }
        }

        private bool CanEditMerk()
        {
            if (SelectedMerk == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedMerk.Merknaam))
            {
                return false;
            }

            return true;
        }

        // ---------------- Delete Merk ---------------- //
        private void DeleteMerk()
        {
            //var productList = productRepository.GetAll();
            //var foreignKeyFix = new Product();

            //foreach (var item in productList)
            //{
            //    if (item.Merk.MerkId == SelectedMerk.MerkId)
            //    {
            //        foreignKeyFix.ProductId = item.ProductId;
            //        foreignKeyFix.ProductNaam = item.ProductNaam;
            //        foreignKeyFix.Afdeling = item.Afdeling;
            //        foreignKeyFix.Merk = null;
            //        productRepository.Update(foreignKeyFix);
            //    }
            //}
            var deleteMerk = SelectedMerk.ConvertToMerk(SelectedMerk);

            merkRepository.Delete(deleteMerk);
            Merken.Remove(SelectedMerk);

            SelectedMerk = new MerkViewModel();
        }

        private bool CanDeleteMerk()
        {
            return SelectedMerk != null;
        }

        // ---------------- Clear Merk ---------------- //
        private void ClearMerk()
        {
            SelectedMerk = new MerkViewModel();
        }

        private bool CanClear()
        {
            return true;
        }

        public void GekozenMerkShow(int id)
        {
            MerkGekozenProduct.Clear();
            foreach (var item in Merken)
            {
                if (item.Product != null)
                {
                    if (item.Product.ProductId == id)
                    {
                        MerkViewModel m = new MerkViewModel();

                        m.MerkId = item.MerkId;
                        m.Merknaam = item.Merknaam;
                        m.Product = item.Product;

                        MerkGekozenProduct.Add(m);
                    }
                }
            }
        }
    }
}
