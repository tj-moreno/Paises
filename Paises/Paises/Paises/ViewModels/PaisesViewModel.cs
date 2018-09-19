

namespace Paises.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Paises.Models;
    using Paises.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class PaisesViewModel : BaseViewModel
    {

        #region Services
        private ApiService apiService;        
        #endregion

        #region Attributes
        private ObservableCollection<PaisItemViewModel> paises;
        private bool isRefreshing;
        private string search;
        private List<Pais> list;
        #endregion

        #region Properties
        public ObservableCollection<PaisItemViewModel> Paises {
            get { return this.paises; }
            set { SetValue(ref this.paises, value); }
        }

        public bool IsRefreshing {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Search {
            get { return this.search; }
            set { SetValue(ref this.search, value);
                this.BuscarPaises();
            }
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
            this.IsRefreshing = true;

            var connection = await this.apiService.ChekConnection();

            if (!connection.IsSurccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Ok");
                
                //Con esto le digo a la app que regrese a la pagina anterior
                await Application.Current.MainPage.Navigation.PopAsync();
                this.IsRefreshing = false;
                return;
            }

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

                await Application.Current.MainPage.Navigation.PopAsync();
                this.IsRefreshing = false;
                return;
            }

            this.list = (List<Pais>)response.Result;
            this.IsRefreshing = false;
            this.Paises = new ObservableCollection<PaisItemViewModel>(
                this.ToPaisItemViewModel());

        }

        #region Methods
        private IEnumerable<PaisItemViewModel> ToPaisItemViewModel()
        {
            return this.list.Select(l => new PaisItemViewModel {
            Alpha2Code=l.Alpha2Code,
            Alpha3Code=l.Alpha3Code,
            AltSpellings=l.AltSpellings,
            Area=l.Area,
            Borders=l.Borders,
            CallingCodes=l.CallingCodes,
            Capital=l.Capital,
            Cioc=l.Cioc,
            Currencies=l.Currencies,
            demonym=l.demonym,
            Flag=l.Flag,
            Gini=l.Gini,
            Languages=l.Languages,
            Latlng=l.Latlng,
            Name=l.Name,
            NativeName=l.NativeName,
            NumericCode=l.NumericCode,
            Population=l.Population,
            Region=l.Region,
            RegionalBlocs=l.RegionalBlocs,
            Subregion=l.Subregion,
            Timezones=l.Timezones,
            TopLevelDomain=l.TopLevelDomain,
            Translations=l.Translations
            }).ToList();
        } 
        #endregion

        private void BuscarPaises()
        {
            if (string.IsNullOrEmpty(this.Search))
            {
                this.Paises = new ObservableCollection<PaisItemViewModel>(
                    this.ToPaisItemViewModel());
            }
            else
            {                
                this.Paises= new ObservableCollection<PaisItemViewModel>(this.ToPaisItemViewModel().Where(
                    p=>p.Name.ToLower().Contains(this.Search.ToLower()) || 
                    p.Capital.ToLower().Contains(this.Search.ToLower())));
            }
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand {
            get {
                return new RelayCommand(LoadPaises);
            }
        } 

        public ICommand SearchCommand
        {
            get {
                return new RelayCommand(BuscarPaises);
            }
        }
        #endregion

    }
}
