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
    public class BoodschappenlijstjeViewModel : INotifyPropertyChanged
    {
        public int LijstId
        {
            get { return _lijstje.LijstId; }
            set { _lijstje.LijstId = value; OnPropertyChanged(); }
        }

        public List<Product> Producten
        {
            get { return _lijstje.Producten.ToList(); }
            set { _lijstje.Producten = value; OnPropertyChanged(); }
        }

        public List<Product> SortedProducten
        {

            get { return _lijstje.Producten.OrderBy(x => x.ProductId).Distinct().ToList(); }

        }

        public List<int> SortedAmmount
        {

            get { return (List<int>) _lijstje.Producten.OrderBy(x => x.ProductId).GroupBy(l => l.ProductId).Select(list => new  { Count = list.Select(l => l.ProductId).Distinct().Count()  }); }

        }

        public Boodschappenlijstje ConvertToLijst(BoodschappenlijstjeViewModel convert)
        {
            Boodschappenlijstje lijst = new Boodschappenlijstje();

            lijst.LijstId = convert.LijstId;
            lijst.Producten = convert.Producten;

            return lijst;
        }

        private Boodschappenlijstje _lijstje;

        public BoodschappenlijstjeViewModel()
        {
            _lijstje = new Boodschappenlijstje();
        }

        public BoodschappenlijstjeViewModel(Boodschappenlijstje lijstje)
        {
            _lijstje = lijstje;
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