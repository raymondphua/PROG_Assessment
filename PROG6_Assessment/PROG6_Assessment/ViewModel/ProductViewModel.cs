using DomainModel.Model;
using DomainModel.Repository;
using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace PROG6_Assessment.ViewModel
{
    public class ProductViewModel : INotifyPropertyChanged
    {

        public string ProductNaam
        {
            get { return _product.ProductNaam; }
            set { _product.ProductNaam = value; OnPropertyChanged(); }
        }

        private Product _product;
        private IMerkRepository merken;

        public ProductViewModel()
        {
            this._product = new Product();
            this.merken = new DummyMerkRepository();
        }

        public ProductViewModel(Product product)
        {
            this._product = product;
            this.merken = new DummyMerkRepository();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
