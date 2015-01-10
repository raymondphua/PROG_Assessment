using DomainModel.Model;
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

        public int MerkId { 
            get { return _merk.MerkId; }
            set { _merk.MerkId = value; OnPropertyChanged(); }
        }

        public string Merknaam 
        { 
            get { return _merk.MerkNaam; } 
            set { _merk.MerkNaam = value; OnPropertyChanged(); } 
        }

        public List<Product> Producten
        {
            get { return _merk.Producten.ToList(); }
        }

        public Merk ConvertToMerk(MerkViewModel convert)
        {
            var merk = new Merk();

            merk.MerkId = convert.MerkId;
            merk.MerkNaam = convert.Merknaam;
            merk.Producten = convert.Producten;

            return merk;
        }

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
