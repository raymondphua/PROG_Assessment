using DomainModel.Repository;
using GalaSoft.MvvmLight;
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
    public class AfdelingOverzichtViewModel : ViewModelBase
    {
        public ObservableCollection<AfdelingViewModel> Afdelingen { get; set; }

        private AfdelingViewModel _selectedAfdeling;
        private IAfdelingRepository afdelingRepository;
        private IProductRepository productRepository;
        private TestProductOverzicht _productOverzicht;

        public AfdelingViewModel SelectedAfdeling
        {
            get { return _selectedAfdeling; }
            set { _selectedAfdeling = value; RaisePropertyChanged(); }
        }

        // ICommands
        public ICommand ShowProductsCommand { get; set; }

        // Constructor
        public AfdelingOverzichtViewModel()
        {
            _productOverzicht = new TestProductOverzicht();
            afdelingRepository = new AfdelingRepository();
            productRepository = new ProductRepository();

            var afdelingList = afdelingRepository.GetAll().Select(s => new AfdelingViewModel(s));

            Afdelingen = new ObservableCollection<AfdelingViewModel>(afdelingList);
            SelectedAfdeling = new AfdelingViewModel();
        }

        // ---------------- Product Overzicht ---------------- //
        private void ShowProductOverzicht()
        {
            _productOverzicht.Show();
        }
        private bool canShowProductOverzicht()
        {
            return _productOverzicht.IsVisible == false;
        }
    }
}
