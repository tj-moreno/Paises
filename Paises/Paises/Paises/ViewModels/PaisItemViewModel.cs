
namespace Paises.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Paises.Models;
    using Paises.View;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class PaisItemViewModel:Pais
    {
        #region Commadns
        public ICommand SelectPaisComand {
            get
            {
                return new RelayCommand(SelectPais);
            }
        }

        private async void SelectPais()
        {
            MainViewModel.GetInstance().Pais = new PaisViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new PaisPage());
        }
        #endregion
    }
}
