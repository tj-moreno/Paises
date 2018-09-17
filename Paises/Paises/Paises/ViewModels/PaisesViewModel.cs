

namespace Paises.ViewModels
{
    using Paises.Models;
    using Paises.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class PaisesViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;
        #endregion

        #region Attributes
        private ObservableCollection<Pais> paises;
        #endregion

        #region Properties
        public ObservableCollection<Pais> Paises {
            get { return this.paises; }
            set { SetValue(ref this.paises, value); }
        }
        #endregion

        #region Constructores
        public PaisesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadPaises();
        }
        #endregion

        #region Method
        private async void LoadPaises()
        {
            var response = await this.apiService.GetList<Pais>(
                "http://restcountries.eu", 
                "/rest", 
                "/v2/all");

            if (!response.IsSurccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Ok");

                return;
            }

            var list = (List<Pais>)response.Result;
            this.Paises = new ObservableCollection<Pais>(list);
        }
        #endregion

    }
}
