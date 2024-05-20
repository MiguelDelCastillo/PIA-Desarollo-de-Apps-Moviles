
using PIA_Development.Model;
using PIA_Development.Services.Interface;
using PIA_Development.Views.PaginaPrincipal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIA_Development.ViewModels
{
    class NoteListViewModel : BaseViewModel
    {
        #region Properties
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        private readonly iNoteService _NoteService;

        public ObservableCollection<NoteModel> Note { get; set; } = new ObservableCollection<NoteModel>();
        #endregion

        #region Constructor
        public NoteListViewModel()
        {
            _NoteService = DependencyService.Resolve<iNoteService>();
            GetAllNote();
        }
        #endregion

        #region Methods
        private void GetAllNote()
        {
            IsBusy = true;
            string emailUsuario = UserModel.Instancia.EmailField;
            Task.Run(async () =>
            {
                var noteList = await _NoteService.GetAllNoteByEmail(emailUsuario);

                Device.BeginInvokeOnMainThread(() =>
                {

                    Note.Clear();
                    if (noteList?.Count > 0)
                    {
                        foreach (var NOTE in noteList)
                        {
                            Note.Add(NOTE);
                        }
                    }
                    IsBusy = IsRefreshing = false;
                });

            });
        }
        #endregion

        #region Commands

        public ICommand RefreshCommand => new Command(() =>
        {
            IsRefreshing = true;
            GetAllNote();
        });

        public ICommand SelectedNoteCommand => new Command<NoteModel>(async (notes) =>
        {
            if (notes != null)
            {
                var response = await App.Current.MainPage.DisplayActionSheet("Opciones!", "Cancel", null, "Actualizar Nota", "Eliminar Nota");

                if (response == "Actualizar Nota")
                {
                    await App.Current.MainPage.Navigation.PushAsync(new AddOrUpdateNote(notes));
                }
                else if (response == "Eliminar Nota")
                {
                    IsBusy = true;
                    bool deleteResponse = await _NoteService.DeleteNote(notes.Key);
                    if (deleteResponse)
                    {
                        GetAllNote();
                    }
                }
            }
        });
        #endregion
    }
}
