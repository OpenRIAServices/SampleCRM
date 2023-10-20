using OpenRiaServices.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SampleCRM.Web.Models
{
    public partial class Customers : Entity
    {
        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == "FirstName" 
                || propertyName == "LastName")
            {
                RaisePropertyChanged("FullName");
                RaisePropertyChanged("Initials");
            } 
            else if (propertyName == "AddressLine1" 
                || propertyName == "AddressLine2"
                || propertyName == "City"
                || propertyName == "Region"
                || propertyName == "PostalCode"
                || propertyName == "CountryName")
            {
                RaisePropertyChanged("FullAddress");
            }

            base.OnPropertyChanged(propertyName);
        }

        private IEnumerable<Models.CountryCodes> _countryCodes;
        public IEnumerable<Models.CountryCodes> CountryCodes
        {
            get { return _countryCodes; }
            set
            {
                if (_countryCodes != value)
                {
                    _countryCodes = value;
                    OnPropertyChanged("CountryCodes");
                }
            }
        }

        public bool IsNew => CustomerID <= 0;

        private bool _isEditMode;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                if (_isEditMode != value)
                {
                    _isEditMode = value;
                    OnPropertyChanged("IsEditMode");
                }
            }
        }

        private string _countryName;
        public string CountryName
        {
            get { return _countryName; }
            set
            {
                if (_countryName != value)
                {
                    _countryName = value;
                    OnPropertyChanged("CountryName");
                }
            }
        }

        public string FullName => $"{FirstName} {LastName}";

        public string Initials => String.Format("{0}{1}", $"{FirstName} "[0], $"{LastName} "[0]).Trim().ToUpper();

        public string FullAddress => $"{AddressLine1} {AddressLine2}\n{City}, {Region} {PostalCode}\n{CountryName}";
    }
}