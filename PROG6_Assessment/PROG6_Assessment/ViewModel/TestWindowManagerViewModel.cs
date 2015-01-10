using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PROG6_Assessment.ViewModel
{
    public class TestWindowManagerViewModel
    {
        private MainWindow _mainWindow;
        private TestWindow _testWindow;
        private MerkWindow _merkWindow;
        private HoofdScherm _hoofdScherm;
        private OverzichtControl _overzichtControl;
        private AlleOverzichten alleOverzichten;
        private AlleAfdelingenWindow alleAfdelingenScherm;
        private AlleProductenWindow alleProductenScherm;
        private AlleMerkenWindow alleMerkenScherm;
        private TestAfdelingOverzicht afdelingOverzichtScherm;
        private TestManager beheerScherm;

        public ICommand ShowMainWindowCommand { get; set; }
        public ICommand ShowSecondWindowCommand { get; set; }
        public ICommand ShowMerkWindowCommand { get; set; }
        public ICommand ShowHoofdSchermCommand { get; set; }
        public ICommand ShowOverzichtControlCommand { get; set; }
        public ICommand ShowAlleOverzichtenCommand { get; set; }
        public ICommand AlleAfdelingenCommand { get; set; }
        public ICommand AlleProductenCommand { get; set; }
        public ICommand AlleMerkenCommand { get; set; }
        public ICommand AfdelingOverzichtCommand { get; set; }
        public ICommand BeheerschermCommand { get; set; }
        public TestWindowManagerViewModel()
        {
            _mainWindow = new MainWindow();
            _testWindow = new TestWindow();
            _merkWindow = new MerkWindow();

            ShowMainWindowCommand = new RelayCommand(showMainWindow, canShowMainWindow);
            ShowSecondWindowCommand = new RelayCommand(showSecondWindow, canShowSecondWindow);
            ShowMerkWindowCommand = new RelayCommand(ShowMerkWindow, canShowMerkWindow);
            ShowHoofdSchermCommand = new RelayCommand(ShowHoofdScherm, CanShowHoofdScherm);
            ShowOverzichtControlCommand = new RelayCommand(OverzichtControlScherm ,CanShowOverzichtControl);
            ShowAlleOverzichtenCommand = new RelayCommand(AlleOverzichtenScherm, CanShowAlleOverzichten);
            AlleAfdelingenCommand = new RelayCommand(AlleAfdelingenOverzicht, CanShowAlleAfdelingenOverzicht);
            AlleProductenCommand = new RelayCommand(AlleProductenOverzicht, CanShowAlleProducten);
            AlleMerkenCommand = new RelayCommand(AlleMerkenOverzicht, CanShowAlleMerken);
            AfdelingOverzichtCommand = new RelayCommand(AfdelingKlikScherm, CanShowAfdelingKlikScherm);
            BeheerschermCommand = new RelayCommand(BeheerSchermShow, CanShowBeheerScherm);
        }

        private void showMainWindow()
        {
            _mainWindow.Show();
        }

        private bool canShowMainWindow()
        {
            return _mainWindow.IsVisible == false;
        }

        private void showSecondWindow()
        {
            _testWindow.Show();
        }

        private bool canShowSecondWindow()
        {
            return _testWindow.IsVisible == false;
        }

        private void ShowMerkWindow()
        {
            _merkWindow.Show();
        }

        private bool canShowMerkWindow()
        {
            return _merkWindow.IsVisible == false;
        }

        private void ShowHoofdScherm()
        {
            _hoofdScherm.Show();
        }

        private bool CanShowHoofdScherm()
        {
            _hoofdScherm = new HoofdScherm();
            return _hoofdScherm.IsVisible == false;
        }

        private void OverzichtControlScherm()
        {
            _overzichtControl.Show();
        }

        private bool CanShowOverzichtControl()
        {
            _overzichtControl = new OverzichtControl();
            return _overzichtControl.IsVisible == false;
        }

        private void AlleOverzichtenScherm()
        {
            alleOverzichten.Show();
        }

        private bool CanShowAlleOverzichten()
        {
            alleOverzichten = new AlleOverzichten();
            return alleOverzichten.IsVisible == false;
        }

        private void AlleAfdelingenOverzicht()
        {
            alleAfdelingenScherm.Show();
        }

        private bool CanShowAlleAfdelingenOverzicht()
        {
            alleAfdelingenScherm = new AlleAfdelingenWindow();
            return alleAfdelingenScherm.IsVisible == false;
        }

        private void AlleProductenOverzicht()
        {
            alleProductenScherm.Show();
        }

        private bool CanShowAlleProducten()
        {
            alleProductenScherm = new AlleProductenWindow();
            return alleProductenScherm.IsVisible == false;
        }

        private void AlleMerkenOverzicht()
        {
            alleMerkenScherm.Show();
        }

        private bool CanShowAlleMerken()
        {
            alleMerkenScherm = new AlleMerkenWindow();
            return alleMerkenScherm.IsVisible == false;
        }

        private void AfdelingKlikScherm()
        {
            afdelingOverzichtScherm.Show();
        }

        private bool CanShowAfdelingKlikScherm()
        {
            afdelingOverzichtScherm = new TestAfdelingOverzicht();
            return afdelingOverzichtScherm.IsVisible == false;
        }

        private void BeheerSchermShow()
        {
            beheerScherm.Show();
        }

        private bool CanShowBeheerScherm()
        {
            beheerScherm = new TestManager();
            return beheerScherm.IsVisible == false;
        }
    }
}
