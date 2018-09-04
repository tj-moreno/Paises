
namespace Paises.ViewModels
{
    using System.Windows.Input;
    public class LoginViewModel
    {
        #region Propiedades
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRunning { get; set; }
        public bool IsRememberd { get; set; }
        #endregion

        #region Commnads
        public ICommand Entrat { get; set; }
        public ICommand Register { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.IsRememberd = true;
        }
        #endregion
    }
}
