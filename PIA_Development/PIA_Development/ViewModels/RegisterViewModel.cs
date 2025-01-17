﻿using Firebase.Auth;
using GalaSoft.MvvmLight.Command;
using PIA_Development.Views.Acceso;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIA_Development.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {

        #region Attributes
        public string email;
        public string password;
        public string name;
        public string age;

        public bool isRunning;
        public bool isVisible;
        public bool isEnabled;
        #endregion

        #region Properties
        public string EmailTxt
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string PasswordTxt
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public string NameTxt
        {
            get { return this.name; }
            set { SetValue(ref this.name, value); }
        }

        public string AgeTxt
        {
            get { return this.age; }
            set { SetValue(ref this.age, value); }
        }

        public bool IsVisibleTxt
        {
            get { return this.isVisible; }
            set { SetValue(ref this.isVisible, value); }
        }

        public bool IsEnabledTxt
        {
            get { return this.isEnabled; }
            set { SetValue(ref this.isEnabled, value); }
        }

        public bool IsRunningTxt
        {
            get { return this.isRunning; }
            set { SetValue(ref this.isRunning, value); }
        }

        #endregion

        #region Commands
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(RegisterMethod);
            }
        }
        public bool IsValidEmail(string email)
        {
            // Expresión regular para validar el formato de un correo electrónico
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(emailPattern);

            return regex.IsMatch(email);
        }
        #endregion

        #region Methods
        private async void RegisterMethod()
        {
            if (string.IsNullOrEmpty(this.email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
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

            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");
                return;
            }

            string WebAPIkey = "AIzaSyA6-Jw6PIDmu4LALq0gOXA4OHOK8eFdhgw";

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebAPIkey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailTxt.ToString(), PasswordTxt.ToString());
                string gettoken = auth.FirebaseToken;
                await App.Current.MainPage.DisplayAlert("Login", "Usuario Registrado", "OK");
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }

            /*
            if (string.IsNullOrEmpty(this.name))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a name.",
                    "Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.age))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an age.",
                    "Accept");
                return;
            }

            this.IsVisibleTxt = true;
            this.IsRunningTxt = true;
            this.IsEnabledTxt = false;


            var user = new UserModel
            {
                EmailField = email,
                PasswordField = password,
                NamesField = name,
                AgeField = age,
                Creation_Date = DateTime.Now,
            };

            await App.Database.SaveUserModelAsync(user);

            await Application.Current.MainPage.DisplayAlert("Successfully", "Welcome " + name.ToString() + " to your app", "Ok");

            this.IsRunningTxt = false;
            this.IsVisibleTxt = false;
            this.IsEnabledTxt = true;

            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            */
        }
        #endregion

        #region Constructor
        public RegisterViewModel()
        {
            IsEnabledTxt = true;
        }
        #endregion

    }
}
