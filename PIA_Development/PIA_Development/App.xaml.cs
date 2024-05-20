using PIA_Development.Services.Implementations;
using PIA_Development.Services.Interface;
using PIA_Development.Views.Acceso;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIA_Development
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
            DependencyService.Register<iNoteService, NoteService>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
