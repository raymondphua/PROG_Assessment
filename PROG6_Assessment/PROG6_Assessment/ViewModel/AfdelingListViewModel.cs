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
    public class AfdelingListViewModel : ViewModelBase
    {
        public ObservableCollection<AfdelingViewModel> Afdelingen { get; set; }

        private AfdelingViewModel _selectedAfdeling;
        private IAfdelingRepository afdelingRepository;

        public AfdelingViewModel SelectedAfdeling
        {
            get { return _selectedAfdeling; }
            set { _selectedAfdeling = value; RaisePropertyChanged(); }
        }

        // ICommands
        public ICommand AddAfdelingCommand { get; set; }
        public ICommand EditAfdelingCommand { get; set; }
        public ICommand DeleteAfdelingCommand { get; set; }
        public ICommand ClearAfdelingCommand { get; set; }

        // Constructor
        public AfdelingListViewModel()
        {
            afdelingRepository = new AfdelingRepository();

            var afdelingList = afdelingRepository.GetAll().Select(s => new AfdelingViewModel(s));

            AddAfdelingCommand = new RelayCommand(AddAfdeling, CanAddAfdeling);
            EditAfdelingCommand = new RelayCommand(EditAfdeling, CanEditProduct);
            DeleteAfdelingCommand = new RelayCommand(DeleteAfdeling, CanDeleteAfdeling);
            ClearAfdelingCommand = new RelayCommand(ClearAfdeling, CanClear);

            Afdelingen = new ObservableCollection<AfdelingViewModel>(afdelingList);
            SelectedAfdeling = new AfdelingViewModel();
        }

        // ---------------- Add Afdeling ---------------- //
        private void AddAfdeling()
        {
            var afdeling = new AfdelingViewModel();

            afdeling.AfdelingNaam = SelectedAfdeling.AfdelingNaam;

            var addAfdeling = afdeling.ConvertToAfdeling(afdeling);

            afdelingRepository.Create(addAfdeling);
        }

        private bool CanAddAfdeling()
        {
            if (SelectedAfdeling == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedAfdeling.AfdelingNaam))
            {
                return false;
            }

            return true;
        }

        // ---------------- Edit Afdeling ---------------- //
        private void EditAfdeling()
        {
            Afdeling updateAfdeling = SelectedAfdeling.ConvertToAfdeling(SelectedAfdeling);
            afdelingRepository.Update(updateAfdeling);
        }

        private bool CanEditProduct()
        {
            if (SelectedAfdeling == null)
            {
                return false;
            }
            if (String.IsNullOrEmpty(SelectedAfdeling.AfdelingNaam))
            {
                return false;
            }
            return true;
        }

        // ---------------- Delete Afdeling ---------------- //
        private void DeleteAfdeling()
        {
            Afdeling removeAfdeling = SelectedAfdeling.ConvertToAfdeling(SelectedAfdeling);

            afdelingRepository.Delete(removeAfdeling);

            SelectedAfdeling = new AfdelingViewModel();
        }

        private bool CanDeleteAfdeling()
        {
            return SelectedAfdeling != null;
        }

        // ---------------- Clear Afdeling ---------------- //
        private void ClearAfdeling()
        {
            SelectedAfdeling = new AfdelingViewModel();
        }

        private bool CanClear()
        {
            return true;
        }
    }
}
