using Firebase.Database;
using Firebase.Database.Query;
using PIA_Development.Model;
using PIA_Development.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIA_Development.Services.Implementations
{
    public class NoteService : iNoteService
    {
        FirebaseClient firebase = new FirebaseClient(Setting.FireBaseDatabaseUrl, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(Setting.FireBaseSeceret)
        });

        public async Task<bool> AddOrUpdateNote(NoteModel noteModel)
        {
            if (!string.IsNullOrWhiteSpace(noteModel.Key))
            {
                try
                {
                    await firebase.Child(nameof(NoteModel)).Child(noteModel.Key).PutAsync(noteModel);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                var response = await firebase.Child(nameof(NoteModel)).PostAsync(noteModel);
                if (response.Key != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public async Task<bool> DeleteNote(string key)
        {
            try
            {
                await firebase.Child(nameof(NoteModel)).Child(key).DeleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<NoteModel>> GetAllNoteByEmail(string email)
        {
            var allNote = (await firebase.Child(nameof(NoteModel)).OnceAsync<NoteModel>()).Select(f => new NoteModel
            {
                EmailField = f.Object.EmailField,
                Note = f.Object.Note,
                Key = f.Key
            }).ToList();

            return allNote.Where(e => e.EmailField == email).ToList();
        }
    }
}
