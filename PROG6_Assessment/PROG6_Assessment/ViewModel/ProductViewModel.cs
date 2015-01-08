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
        public int ProductId { get { return _product.ProductId; } }

        public string ProductNaam
        {
            get { return _product.ProductNaam; }
            set { _product.ProductNaam = value; OnPropertyChanged(); }
        }

        public Afdeling Afdeling
        {
            get { return _product.Afdeling; }
        }

        public Merk Merk
        {
            get { return _product.Merk; }
        }

        public List<Merk> Merken { get; set; }

        public Product ConvertToProduct(ProductViewModel convert)
        {
            Product product = new Product();

            product.ProductId = convert.ProductId;
            product.ProductNaam = convert.ProductNaam;
            product.Afdeling = convert.Afdeling;
            product.Merk = convert.Merk;
            return product;
        }


        private Product _product;

        public ProductViewModel()
        {
            this._product = new Product();
        }

        public ProductViewModel(Product product, List<Merk> merken)
        {
            this._product = product;
            Merken = merken;
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
