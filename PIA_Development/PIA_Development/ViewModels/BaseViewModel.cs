using PIA_Development.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;


namespace PIA_Development.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public INavigation Navigation;


        private string _emailField;
        public string EmailField
        {
            get { return _emailField; }
            set
            {
                _emailField = value;
                OnPropertyChanged(nameof(EmailField));
            }
        }

        public BaseViewModel()
        {
            // Asignar un valor predeterminado al campo de correo electrónico
            string correo = UserModel.Instancia.EmailField;
            EmailField = correo;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)

        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void SetValue<T>(ref T backingFieled, T value, [CallerMemberName] string propertyName = null)

        {

            if (EqualityComparer<T>.Default.Equals(backingFieled, value))

            {

                return;

            }

            backingFieled = value;

            OnPropertyChanged(propertyName);

        }

        protected virtual void OnPropertyChangeds([CallerMemberName] string propertyName = null)

        {

            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)

            {

                handler(this, new PropertyChangedEventArgs(propertyName));

            }

        }

        #region Properties
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        #endregion

        protected bool SetProperty<T>(ref T backingStore, T value,
                                        [CallerMemberName] string propertyName = "",
                                        Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }        

    }

}
