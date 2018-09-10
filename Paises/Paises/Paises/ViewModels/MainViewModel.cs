
namespace Paises.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels
        public LoginViewModel Login
        {
            get;
            set;
        }
        public PaisesViewModel Pais
        {
            get;
            set;
        }
        #endregion

        #region constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
