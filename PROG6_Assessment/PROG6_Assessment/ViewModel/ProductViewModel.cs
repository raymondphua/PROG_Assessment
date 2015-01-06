﻿using PROG6_Assessment.Model;
using PROG6_Assessment.Repository;
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
            set { _product.Merken = merken.ToList(); OnPropertyChanged(); }
        }

        public List<Merk> Merken
        {
            get { return _product.Merken; }
            set { _product.Merken = value; OnPropertyChanged(); }
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
