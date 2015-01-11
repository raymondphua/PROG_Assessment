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
        public ObservableCollection<BoodschappenlijstjeViewModel> Lijstjes { get; set; }

        private BoodschappenlijstjeViewModel _selectedLijstje;
        private IBoodschappenlijstjeRepository lijstRepository;
        private IProductRepository productRepository;

        public BoodschappenlijstjeViewModel SelectedLijstje
        {
            get { return _selectedLijstje; }
            set { _selectedLijstje = value; RaisePropertyChanged(); }
        }

        // ICommands
        public ICommand AddLijstjeCommand { get; set; }
        public ICommand EditLijstjeCommand { get; set; }
        public ICommand DeleteLijstjeCommand { get; set; }
        public ICommand ClearLijstjeCommand { get; set; }

        // Constructor
        public BoodschappenlijstjeListViewModel()
        {
            lijstRepository = new BoodschappenlijstjeRepository();
            productRepository = new ProductRepository();

            var lijstList = lijstRepository.GetAll().Select(s => new BoodschappenlijstjeViewModel(s));

            AddLijstjeCommand = new RelayCommand(AddLijst, CanAddLijst);
            EditLijstjeCommand = new RelayCommand(EditLijst, CanEditLijst);
            DeleteLijstjeCommand = new RelayCommand(DeleteLijst, CanDeleteLijst);
            ClearLijstjeCommand = new RelayCommand(ClearLijst, CanClear);
            //ShowProductsCommand = new RelayCommand(ShowProductOverzicht, canShowProductOverzicht);

            Lijstjes = new ObservableCollection<BoodschappenlijstjeViewModel>(lijstList);
            SelectedLijstje = new BoodschappenlijstjeViewModel();
        }

        // ---------------- Add Afdeling ---------------- //
        private void AddLijst()
        {
            var lijst = new BoodschappenlijstjeViewModel();

            var addLijst = lijst.ConvertToLijst(lijst);

            lijstRepository.Create(addLijst);

            lijst.LijstId = addLijst.LijstId;
            Lijstjes.Add(lijst);
        }

        private bool CanAddLijst()
        {
            if (SelectedLijstje == null)
            {
                return false;
            }
            if (SelectedLijstje.Producten.Count == 0)
            {
                return false;
            }

            return true;
        }

        // ---------------- Edit Afdeling ---------------- //
        private void EditLijst()
        {
            Boodschappenlijstje updateLijst = SelectedLijstje.ConvertToLijst(SelectedLijstje);
            lijstRepository.Update(updateLijst);
        }

        private bool CanEditLijst()
        {
            if (SelectedLijstje == null)
            {
                return false;
            }
            if (SelectedLijstje.Producten.Count == 0)
            {
                return false;
            }
            return true;
        }

        // ---------------- Delete Afdeling ---------------- //
        private void DeleteLijst()
        {

            var removeLijst = SelectedLijstje.ConvertToLijst(SelectedLijstje);

            lijstRepository.Delete(removeLijst);
            Lijstjes.Remove(SelectedLijstje);

            SelectedLijstje = new BoodschappenlijstjeViewModel();
        }

        private bool CanDeleteLijst()
        {
            return SelectedLijstje != null;
        }

        // ---------------- Clear Afdeling ---------------- //
        private void ClearLijst()
        {
            SelectedLijstje = new BoodschappenlijstjeViewModel();
        }

        private bool CanClear()
        {
            return true;
        }
    }
}
