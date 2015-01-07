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

        private MerkViewModel _selectedMerk;
        private IMerkRepository merkRepository;

        public MerkViewModel SelectedMerk 
        { 
            get { return _selectedMerk; }
            set { _selectedMerk = value; RaisePropertyChanged(); }
        }


        // ICommands
        public ICommand AddMerkCommand { get; set; }
        public ICommand DeleteMerkCommand { get; set; }
        public ICommand ClearMerkCommand { get; set; }

        // Constructor
        public MerkListViewModel()
        {
            this.merkRepository = new DummyMerkRepository();
            var merkList = merkRepository.GetAll().Select(s => new MerkViewModel(s));

            AddMerkCommand = new RelayCommand(AddNewMerk, CanAddMerk);
            DeleteMerkCommand = new RelayCommand(DeleteMerk, CanDeleteMerk);
            ClearMerkCommand = new RelayCommand(ClearMerk, CanClear);

            Merken = new ObservableCollection<MerkViewModel>(merkList);
            SelectedMerk = new MerkViewModel();
        }


        // ---------------- Add Merk ---------------- //
        private void AddNewMerk()
        {
            var merk = new MerkViewModel();

            merk.Merknaam = SelectedMerk.Merknaam;

            Merken.Add(merk);
        }

        private bool CanAddMerk()
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
    }
}
