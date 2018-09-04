
namespace Paises.Infraestructure
{
    using ViewModels;

    class InstasnsLocator
    {
        #region Properties
        public MainViewModel Main
        {
            get;
            set;
        }
        #endregion

        #region Constructor        
        public InstasnsLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
