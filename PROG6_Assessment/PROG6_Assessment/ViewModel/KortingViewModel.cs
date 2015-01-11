using DomainModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PROG6_Assessment.ViewModel
{
    public class KortingViewModel : INotifyPropertyChanged
    {
        private Korting _korting;

        public int KortingId 
        {
            get { return _korting.KortingId; } 
            set { _korting.KortingId = value; OnPropertyChanged(); }
        }

        public string Coupon
        {
            get { return _korting.Coupon; }
            set { _korting.Coupon = value; OnPropertyChanged(); }
        }

        public DateTime StartDatum
        {
            get { return _korting.StartDatum; }
            set { _korting.StartDatum = value; OnPropertyChanged(); }
        }

        public DateTime EindDatum
        {
            get { return _korting.EindDatum; }
            set { _korting.EindDatum = value; OnPropertyChanged(); }
        }

        public Product Product
        {
            get { return _korting.Product; }
            set { _korting.Product = value; OnPropertyChanged(); }
        }

        public object SelectedProduct { get; set; }
        public KortingViewModel()
        {
            _korting = new Korting();
        }

        public KortingViewModel(Korting k)
        {
            _korting = k;
        }

        public Korting ConvertToKorting(KortingViewModel convert)
        {
            var korting = new Korting();

            korting.KortingId = convert.KortingId;
            korting.Coupon = convert.Coupon;
            korting.StartDatum = convert.StartDatum;
            korting.EindDatum = convert.EindDatum;
            korting.Product = convert.Product;

            return korting;
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
