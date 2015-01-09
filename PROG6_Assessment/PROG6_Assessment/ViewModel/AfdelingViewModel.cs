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
    public class AfdelingViewModel :INotifyPropertyChanged
    {
        public int AfdelingId
        {
            get { return _afdeling.AfdelingId; }
        }

        public string AfdelingNaam
        {
            get { return _afdeling.AfdelingNaam; }
            set { _afdeling.AfdelingNaam = value; OnPropertyChanged(); }
        }

        public List<Product> Producten
        {
            get { return _afdeling.Producten.ToList(); }
        }


        public Afdeling ConvertToAfdeling(AfdelingViewModel convert)
        {
            Afdeling afdeling = new Afdeling();

            afdeling.AfdelingId = convert.AfdelingId;
            afdeling.AfdelingNaam = convert.AfdelingNaam;
            afdeling.Producten = convert.Producten;

            return afdeling;
        }


        private Afdeling _afdeling;

        public AfdelingViewModel()
        {
            _afdeling = new Afdeling();
        }

        public AfdelingViewModel(Afdeling afdeling)
        {
            _afdeling = afdeling;
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
