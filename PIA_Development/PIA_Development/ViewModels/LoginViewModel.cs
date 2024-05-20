using Firebase.Auth;
using PIA_Development.Conections;
using PIA_Development.Model;
using PIA_Development.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PIA_Development.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private string email;
        private string Password;
        #endregion

        #region Propiedades
        public string TxtUsuario
        {
            get { return email; }
            set { SetValue(ref email, value); }
        }
        public string TxtPassword
        {
            get { return Password; }
            set { SetValue(ref Password, value); }
        }

        #endregion

        #region Command
        public Command LoginCommand { get; }
        #endregion
        public bool IsValidEmail(string email)
        {
            // Expresión regular para validar el formato de un correo electrónico
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailPattern);

            return regex.IsMatch(email);
        }

        #region Metodo
        public async Task LoginUsuario()
        {
            
            UserModel.Instancia.EmailField = email;
            UserModel.Instancia.PasswordField = Password;

            string emailUsuario = UserModel.Instancia.EmailField;
            string passwordUsuario = UserModel.Instancia.PasswordField;
            if (string.IsNullOrEmpty(this.email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar un correo almenos.",
                    "Accept");
                return;
            }

            if (!IsValidEmail(this.email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar un formato de correo valido.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar una contraseña.",
                    "Accept");
                return;
            }

            try
            {

                var autenticacion = new FirebaseAuthProvider(new FirebaseConfig(DBConn.webApiAuthentication));
                var authuser = await autenticacion.SignInWithEmailAndPasswordAsync(emailUsuario.ToString(), passwordUsuario.ToString());
                string obternertoken = authuser.FirebaseToken;
                await App.Current.MainPage.DisplayAlert("Login", "Inicio de Sesion Exitoso.", "Aceptar");
                var Propiedades_NavigationPage = new NavigationPage(new MenuPrincipal(emailUsuario.ToString(), passwordUsuario.ToString()));
                Propiedades_NavigationPage.BarBackgroundColor = Color.RoyalBlue;
                App.Current.MainPage = Propiedades_NavigationPage;


            }
            catch (Exception)
            {

                await App.Current.MainPage.DisplayAlert("Advertencia", "Los datos introducidos son incorrectos o el usuario se encuentra inactivo.", "Aceptar");
                //await App.Current.MainPage.DisplayAlert("Advertencia", ex.Message, "Aceptar");
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel(INavigation navegar)
        {
            Navigation = navegar;
            LoginCommand = new Command(async () => await LoginUsuario());

        }
        #endregion
    }
}
