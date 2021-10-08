using ppedv.GMV.Logic;
using ppedv.GMV.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;

namespace ppedv.GMV.UI.Wpf.ViewModels
{
    public class MesswerteViewModel : INotifyPropertyChanged
    {


        public ObservableCollection<Messwert> MesswerteListe { get; set; }

        private Messwert selectedMesswert;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        public Messwert SelectedMesswert
        {
            get => selectedMesswert;
            set
            {
                selectedMesswert = value;
                Über1000 = selectedMesswert.Wert > 1000;
                OnPropertyChanged(nameof(SelectedMesswert));
                OnPropertyChanged(nameof(Über1000));
            }
        }

        public bool Über1000 { get; set; }

        public Core Core { get; }


        public MesswerteViewModel() : this(new Core(new Data.EfCore.EfRepository()))
        {
        }

        public MesswerteViewModel(Core core)
        {
            
            MesswerteListe = new ObservableCollection<Messwert>(core.Repository.GetAll<Messwert>());
        }
    }
}
