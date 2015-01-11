using DomainModel.Model;
using DomainModel.Repository;
using PROG6_Assessment.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.ViewModel
{
    public class MerkViewModel : INotifyPropertyChanged
    {

        private IMerkRepository _imr;

        public MerkViewModel(IMerkRepository imr)
        {
            _imr = imr;
        }

        public string GetMerkNaamMock()
        {

            return _imr.GetAllMerken().First().MerkNaam;

        }


        public int MerkId { 
            get { return _merk.MerkId; }
            set { _merk.MerkId = value; OnPropertyChanged(); }
        }

        public string Merknaam 
        { 
            get { return _merk.MerkNaam; } 
            set { _merk.MerkNaam = value; OnPropertyChanged(); } 
        }

        public double Multiplier
        {
            get { return _merk.Multiplier; }
            set { _merk.Multiplier = value; OnPropertyChanged(); }
        }

        public Product Product
        {
            get { return _merk.Product; }
            set { _merk.Product = value; OnPropertyChanged(); }
        }

        public Merk ConvertToMerk(MerkViewModel convert)
        {
            var merk = new Merk();

            merk.MerkId = convert.MerkId;
            merk.MerkNaam = convert.Merknaam;
            merk.Product = convert.Product;
            merk.Multiplier = convert.Multiplier;

            return merk;
        }

        public object SelectedProduct { get; set; }

        private Merk _merk;

        public MerkViewModel()
        {
            _merk = new Merk();
        }

        public MerkViewModel(Merk merk)
        {
            _merk = merk;
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
