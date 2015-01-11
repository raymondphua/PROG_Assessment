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
        public int ProductId 
        { 
            get { return _product.ProductId; }
            set { _product.ProductId = value; OnPropertyChanged(); }
        }

        public string ProductNaam
        {
            get { return _product.ProductNaam; }
            set { _product.ProductNaam = value; OnPropertyChanged(); }
        }
        
        public double Prijs
        {
            get { return _product.Prijs; }
            set { _product.Prijs = value; OnPropertyChanged(); }
        }
        public Afdeling Afdeling
        {
            get { return _product.Afdeling; }
            set { _product.Afdeling = value; OnPropertyChanged(); }
        }

        public List<Merk> Merken
        {
            get { return _product.Merken.ToList(); }
            set { _product.Merken = value; OnPropertyChanged(); }
        }

        public List<Korting> Kortingen
        {
            get { return _product.Kortingen.ToList(); }
            set { _product.Kortingen = value; OnPropertyChanged(); }
        }

        public Product ConvertToProduct(ProductViewModel convert)
        {
            Product product = new Product();

            product.ProductId = convert.ProductId;
            product.ProductNaam = convert.ProductNaam;
            product.Prijs = convert.Prijs;
            product.Afdeling = convert.Afdeling;
            product.Merken = convert.Merken;
            product.Kortingen = convert.Kortingen;
            return product;
        }

        //public object SelectedMerk { get; set; }

        public object SelectedAfdeling { get; set; }

        private Product _product;

        public ProductViewModel()
        {
            this._product = new Product();
        }

        public ProductViewModel(Product product)
        {
            this._product = product;
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
