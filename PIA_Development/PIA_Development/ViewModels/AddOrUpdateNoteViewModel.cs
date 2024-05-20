using PIA_Development.Model;
using PIA_Development.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIA_Development.ViewModels
{
    public class AddOrUpdateNoteViewModel : BaseViewModel
    {
        #region Properties

        
        private readonly iNoteService _noteService;

        private NoteModel _noteDetail = new NoteModel();
        public NoteModel NoteDetail
        {
            get => _noteDetail;
            set => SetProperty(ref _noteDetail, value);
        }
        #endregion

        #region Constructor
        public AddOrUpdateNoteViewModel()
        {
            _noteService = DependencyService.Resolve<iNoteService>();
        }

        public AddOrUpdateNoteViewModel(string email, NoteModel noteResponse)
        {
            _noteService = DependencyService.Resolve<iNoteService>();

            NoteDetail = new NoteModel
            {
                Key = noteResponse.Key,
                EmailField = noteResponse.EmailField,
                Note = noteResponse.Note
            };
            
            
        }
        #endregion

        #region Commands
        public ICommand SaveNoteCommand => new Command(async () =>
        {
            if (IsBusy) return;
            IsBusy = true;
            bool res = await _noteService.AddOrUpdateNote(NoteDetail);
            if (res)
            {
                if (!string.IsNullOrWhiteSpace(NoteDetail.Key))
                {
                    await App.Current.MainPage.DisplayAlert("Success!", "Record Updated successfully.", "Ok");
                }
                else
                {
                    NoteDetail = new NoteModel() { };
                    await App.Current.MainPage.DisplayAlert("Success!", "Record added successfully.", "Ok");
                }
            }
            IsBusy = false;
        });
        #endregion
    }
}
