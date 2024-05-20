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
    public partial class NoteUserList : ContentPage
    {
        public NoteUserList()
        {
            InitializeComponent();
            this.BindingContext = new NoteListViewModel();
        }
    }
}