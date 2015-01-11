/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:PROG6_Assessment"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace PROG6_Assessment.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
                
            //SimpleIoc.Default.Register<MainViewModel>();
            //SimpleIoc.Default.Register<AfdelingListViewModel>();
            //SimpleIoc.Default.Register<ProductListViewModel>();
            //SimpleIoc.Default.Register<MerkListViewModel>();
            SimpleIoc.Default.Register<AllListViewModel>();

            //test
            SimpleIoc.Default.Register<TestWindowManagerViewModel>();
        }
        
        //public AfdelingListViewModel Afdeling
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<AfdelingListViewModel>();
        //    }
        //}

        //public ProductListViewModel Product
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<ProductListViewModel>();
        //    }
        //}

        //public MerkListViewModel Merk
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<MerkListViewModel>();
        //    }
        //}
        
        // test

        public TestWindowManagerViewModel Manager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TestWindowManagerViewModel>();
            }
        }

        public AllListViewModel All
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AllListViewModel>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}