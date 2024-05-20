using PIA_Development.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PIA_Development.Services.Interface
{
    public interface iNoteService
    {
        Task<bool> AddOrUpdateNote(NoteModel notelModel);
        Task<bool> DeleteNote(string key);
        Task<List<NoteModel>> GetAllNoteByEmail(string email);
    }
}
