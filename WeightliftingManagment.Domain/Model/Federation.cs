using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WeightliftingManagment.MvvmSupport.BindableBase;

namespace WeightliftingManagment.Domain.Model
{
    public class Federation : BindableObject
    {
        public Federation()
        {

        }

        public Federation(string? name, string? address, string? email, string? webSite)
        {
            Name = name;
            Address = address;
            Email = email;
            WebSite = webSite;
        }

        #region Fields
        private string? _name;
        private string? _address;
        private string? _email;
        private string? _webSite;
        #endregion

        #region Property

        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string? Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string? WebSite
        {
            get => _webSite;
            set => SetProperty(ref _webSite, value);
        }

        #endregion
    }
}
