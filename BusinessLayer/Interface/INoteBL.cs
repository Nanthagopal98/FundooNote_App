using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NotesEntity CreateNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> DisplayNotes(long userId);
        public bool DeleteNotes(long notesId);
        public NotesEntity UpdateNotes(NotesUpdateModel notesUpdateModel, long notesId);
    }
}
