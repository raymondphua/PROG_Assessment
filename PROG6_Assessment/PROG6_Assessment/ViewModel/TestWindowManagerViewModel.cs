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

        public ICommand ShowMainWindowCommand { get; set; }
        public ICommand ShowSecondWindowCommand { get; set; }

        public TestWindowManagerViewModel()
        {
            _mainWindow = new MainWindow();
            _testWindow = new TestWindow();

            ShowMainWindowCommand = new RelayCommand(showMainWindow, canShowMainWindow);
            ShowSecondWindowCommand = new RelayCommand(showSecondWindow, canShowSecondWindow);
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
    }
}
