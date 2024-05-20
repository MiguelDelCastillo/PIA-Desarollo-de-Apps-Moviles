using PIA_Development.Model;
using PIA_Development.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PIA_Development.Views.PaginaPrincipal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrUpdateNote : ContentPage
    {
        public AddOrUpdateNote()
        {
            InitializeComponent();
            string emailUsuario = UserModel.Instancia.EmailField;
            datosUsuario.Text = emailUsuario;
            //NoteDetail.EmailField = emailUsuario;
            
            this.BindingContext = new AddOrUpdateNoteViewModel();

        }

        public AddOrUpdateNote(NoteModel note)
        {
            InitializeComponent();
            string emailUsuario = UserModel.Instancia.EmailField;
            this.BindingContext = new AddOrUpdateNoteViewModel(emailUsuario, note);
        }
    }
}