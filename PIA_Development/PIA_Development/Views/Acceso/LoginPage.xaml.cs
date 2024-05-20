using PIA_Development.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIA_Development.Views.Acceso
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation);
            
        }
        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        //private async void Ingresar_Clicked(object sender, EventArgs e)
        //{
        //    string username = TxtUsuario.Text;
        //    string password = TxtPassword.Text;

        //    if (IsValidData(username, password))
        //    {
        //        await DisplayAlert("Logrado", "Inicio Exitoso!", "Ok");
        //        await Navigation.PushAsync(new MenuPrincipal());
        //    }
        //    else
        //    {

        //        await DisplayAlert("Error", "Datos Incorrectos", "Ok");

        //    }
        //}

        //private bool IsValidData(string username, string password)
        //{
        //    return username == "1" && password == "1234";
        //}
    }
}