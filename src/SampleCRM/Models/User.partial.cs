using System.ComponentModel;

namespace SampleCRM.Web
{
    /// <summary>
    /// Extensions to the <see cref="User"/> class.
    /// </summary>
    public partial class User
    {
        #region Make DisplayName Bindable

        /// <summary>
        /// Override of the <c>OnPropertyChanged</c> method that generates property change notifications when <see cref="User.DisplayName"/> changes.
        /// </summary>
        /// <param name="e">The property change event args.</param>
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == "Name" || propertyName == "FriendlyName")
            {
                this.RaisePropertyChanged("DisplayName");
            }
        }

        #endregion Make DisplayName Bindable
    }
}