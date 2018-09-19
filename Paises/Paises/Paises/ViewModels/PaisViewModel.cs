
namespace Paises.ViewModels
{
    using Paises.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PaisViewModel
    {
        private PaisItemViewModel paisItemViewModel;

        #region Properties
        public Pais Pais
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public PaisViewModel(Pais pais)
        {
            this.Pais = pais;
        } 
        #endregion
    }
}
