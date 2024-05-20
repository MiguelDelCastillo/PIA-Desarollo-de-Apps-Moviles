using PIA_Development.Views.PaginaPrincipal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PIA_Development.Views.Acceso;
using PIA_Development.Model;

namespace PIA_Development.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPrincipal : ContentPage
    {
        public string UsuarioSeleccionado { get; private set; }
        public MenuPrincipal(string TxtUsuario, string TxtPassword)
        {
            
            InitializeComponent();
            datosUsuario.ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical);

            // Aquí creas una lista de strings con el nombre de usuario
            var usuarioList = new List<string> { TxtUsuario };

            // Asignas la lista como el origen de datos del CollectionView
            datosUsuario.ItemsSource = usuarioList;
            Preferences.Set("Correo",RandomText.Text);
        }
        private async void showNote_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NoteUserList());
        }

        private async void addNote_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddOrUpdateNote());
        }

        private void datosUsuario_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Verifica que haya elementos seleccionados
            if (e.CurrentSelection != null && e.CurrentSelection.Count > 0)
            {
                // Obtiene el elemento seleccionado
                string usuario = e.CurrentSelection.FirstOrDefault() as string;

                // Asigna el usuario seleccionado a la propiedad
                UsuarioSeleccionado = usuario;
            }
        }

    }
}
