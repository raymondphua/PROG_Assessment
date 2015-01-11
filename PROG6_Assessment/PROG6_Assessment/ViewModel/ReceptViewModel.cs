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
    public class ReceptViewModel : INotifyPropertyChanged
    {
        public int ReceptId
        {
            get { return _recept.ReceptId; }
            set { _recept.ReceptId = value; OnPropertyChanged(); }
        }

        public string ReceptNaam
        {
            get { return _recept.ReceptNaam; }
            set { _recept.ReceptNaam = value; OnPropertyChanged(); }
        }

        public List<Product> Producten
        {
            get { return _recept.Producten.ToList(); }
            set { _recept.Producten = value; OnPropertyChanged(); }
        }

        public Recept ConvertToRecept(ReceptViewModel convert)
        {
            Recept recept = new Recept();

            recept.ReceptId = convert.ReceptId;
            recept.ReceptNaam = convert.ReceptNaam;
            recept.Producten = convert.Producten;

            return recept;
        }


        private Recept _recept;

        public ReceptViewModel()
        {
            _recept = new Recept();
        }

        public ReceptViewModel(Recept recept)
        {
            _recept = recept;
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
