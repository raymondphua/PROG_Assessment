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
        private IProductRepository productRepository;

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
            productRepository = new ProductRepository();
            
            var afdelingList = afdelingRepository.GetAll().Select(s => new AfdelingViewModel(s));

            AddAfdelingCommand = new RelayCommand(AddAfdeling, CanAddAfdeling);
            EditAfdelingCommand = new RelayCommand(EditAfdeling, CanEditProduct);
            DeleteAfdelingCommand = new RelayCommand(DeleteAfdeling, CanDeleteAfdeling);
            ClearAfdelingCommand = new RelayCommand(ClearAfdeling, CanClear);
            //ShowProductsCommand = new RelayCommand(ShowProductOverzicht, canShowProductOverzicht);

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

            afdeling.AfdelingId = addAfdeling.AfdelingId;
            Afdelingen.Add(afdeling);
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
            var productList = productRepository.GetAll();

            // als ik een afdeling wil verwijderen moet ik eerst de afdeling verwijderen bij een product, anders krijg je een foreignkey constraint.
            var foreignKeyFix = new Product();

            foreach(var item in productList)
            {
                if (item.Afdeling.AfdelingId == SelectedAfdeling.AfdelingId)
                {
                    foreignKeyFix.ProductId = item.ProductId;
                    foreignKeyFix.ProductNaam = item.ProductNaam;
                    foreignKeyFix.Afdeling = null;
                    foreignKeyFix.Merk = item.Merk;
                    productRepository.Update(foreignKeyFix);
                }
            }
            var removeAfdeling = SelectedAfdeling.ConvertToAfdeling(SelectedAfdeling);

            afdelingRepository.Delete(removeAfdeling);
            Afdelingen.Remove(SelectedAfdeling);

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

        // ---------------- Product Overzicht ---------------- //
        //private void ShowProductOverzicht()
        //{
        //    plvm.GekozenAfdelingShow(SelectedAfdeling.AfdelingId);
        //    _productOverzicht.Show();
        //}
        //private bool canShowProductOverzicht()
        //{
        //    return _productOverzicht.IsVisible == false;
        //}
    }
}
