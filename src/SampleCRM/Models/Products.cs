using OpenRiaServices.Client;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SampleCRM.Web.Models
{
    public partial class Products : Entity
    {
        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == nameof(CategoryID))
            {
                OnPropertyChanged("CategoryName");
            }
        }

        public bool IsNew => string.IsNullOrEmpty(ProductID);

        private IEnumerable<Categories> _categoriesCombo;
        public IEnumerable<Categories> CategoriesCombo
        {
            get { return _categoriesCombo; }
            set
            {
                if (_categoriesCombo != value)
                {
                    _categoriesCombo = value;
                    OnPropertyChanged("CategoriesCombo");
                    OnPropertyChanged("CategoryName");
                }
            }
        }

        public string CategoryName => CategoriesCombo.FirstOrDefault(x => x.CategoryID == CategoryID)?.Name;
    }
}