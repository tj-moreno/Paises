
namespace Paises.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using System.ComponentModel;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel
    {
        
        #region Atributes
        private string email;
        private string password;
        private bool isrunning;
        private bool isrememberd;
        private bool isenabled;
        #endregion

        #region Propiedades
        public string Email {
            get { return email; }
            set { SetValue(ref this.email, value); }
        }
        public string Password {
            get { return password; }
            set { SetValue(ref this.password, value); }
        }
        public bool IsRunning {
            get { return isrunning; }
            set { SetValue(ref this.isrunning, value); }
        }
        public bool IsRememberd { get; set; }
        public bool IsEnabled {
            get { return isenabled; }
            set { SetValue(ref this.isenabled, value); }
        }
        #endregion

        #region Commnads
        public ICommand EntrarCommand
        {
            get
            {
                return new RelayCommand(Entrar);
            }
        }        

        private async void Entrar()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No se digito ningun E-Mail",
                    "Aceptar"
                    );

                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "No se digito ninguna Contraseña",
                    "Aceptar"
                    );

                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            if (this.Email!="alfonso125@gmail.com" || this.Password!="1234")
            {

                await Application.Current.MainPage.DisplayAlert(
                    "Login Error",
                    "Correo o contraseña invalida..",
                    "Ok"
                    );

                this.IsRunning = false;
                this.IsEnabled = true;

                this.Password = string.Empty;

                return;
            }


            this.IsRunning = false;
            this.IsEnabled = true;

            await Application.Current.MainPage.DisplayAlert(
                    "Login",
                    "Listo.....",
                    "Ok"
                    );
        }

        public ICommand RegisterCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.IsRememberd = true;
            this.Email = "alfonso125@gmail.com";
        }
        #endregion
    }
}
