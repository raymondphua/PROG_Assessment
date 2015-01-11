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
    public class KortingListViewModel : ViewModelBase
    {
        public ObservableCollection<KortingViewModel> Kortingen { get; set; }

        private KortingViewModel _selectedKorting;
        private IProductRepository productRepository;
        private IKortingRepository kortingRepository;

        public KortingViewModel SelectedKorting 
        {
            get { return _selectedKorting; }
            set { _selectedKorting = value; RaisePropertyChanged(); } 
        }

        // ICommands
        public ICommand AddKortingCommand { get; set; }
        public ICommand EditKortingCommand { get; set; }
        public ICommand DeleteKortingCommand { get; set; }
        public ICommand ClearKortingCommand { get; set; }

        //Constructor
        public KortingListViewModel()
        {
            productRepository = new ProductRepository();
            kortingRepository = new KortingRepository();

            var kortingList = kortingRepository.GetAll().Select(k => new KortingViewModel(k));

            AddKortingCommand = new RelayCommand(AddKorting, CanAddKorting);
            EditKortingCommand = new RelayCommand(EditKorting, CanEditKorting);
            DeleteKortingCommand = new RelayCommand(DeleteKorting, CanDeleteKorting);
            ClearKortingCommand = new RelayCommand(ClearKorting, CanClear);

            Kortingen = new ObservableCollection<KortingViewModel>(kortingList);
            SelectedKorting = new KortingViewModel();
        }

        // ---------------- Add Afdeling ---------------- //
        private void AddKorting()
        {
            var korting = new KortingViewModel();

            // product ophalen
            ProductViewModel product = (ProductViewModel)SelectedKorting.SelectedProduct;
           
            korting.Coupon = SelectedKorting.Coupon;
            korting.StartDatum = SelectedKorting.StartDatum;
            korting.EindDatum = SelectedKorting.EindDatum;
            korting.Product = productRepository.Find(product.ProductId);
            var addKorting = korting.ConvertToKorting(korting);

            kortingRepository.Create(addKorting);

            korting.KortingId = addKorting.KortingId;
            Kortingen.Add(korting);
        }

        private bool CanAddKorting()
        {
            if (SelectedKorting == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedKorting.Coupon))
            {
                return false;
            }

            return true;
        }

        // --------- Edit Korting --------- //
        private void EditKorting()
        {
            var korting = new KortingViewModel();

            // product ophalen
            ProductViewModel product = (ProductViewModel)SelectedKorting.SelectedProduct;

            korting.KortingId = SelectedKorting.KortingId;
            korting.Coupon = SelectedKorting.Coupon;
            korting.StartDatum = SelectedKorting.StartDatum;
            korting.EindDatum = SelectedKorting.EindDatum;
            korting.Product = productRepository.Find(product.ProductId);

            var editKorting = korting.ConvertToKorting(korting);
            kortingRepository.Update(editKorting);
        }

        private bool CanEditKorting()
        {
            if (SelectedKorting == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedKorting.Coupon))
            {
                return false;
            }

            return true;
        }

        // ---------- Delete Korting -------- //
        private void DeleteKorting()
        {
            var deleteKorting = SelectedKorting.ConvertToKorting(SelectedKorting);

            kortingRepository.Delete(deleteKorting);
            Kortingen.Remove(SelectedKorting);

            SelectedKorting = new KortingViewModel();
        }

        private bool CanDeleteKorting()
        {
            return SelectedKorting != null;
        }

        // ---------------- Clear Product ---------------- //
        private void ClearKorting()
        {
            SelectedKorting = new KortingViewModel();
        }

        private bool CanClear()
        {
            return true;
        }
    }
}
