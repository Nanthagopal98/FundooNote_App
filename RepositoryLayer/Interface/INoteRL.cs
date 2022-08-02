using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NotesEntity CreateNotes(NotesModel notesModel, long userId);
        public IEnumerable<NotesEntity> DisplayNotes(long userId);
        public bool DeleteNotes(long notesId);
        public NotesEntity UpdateNotes(NotesUpdateModel notesUpdateModel, long notesId);
        public NotesEntity Archive(long notesId);
        public NotesEntity Pin(long notesId);
        public NotesEntity Trash(long notesId);
        public NotesEntity Color(long notesId, string color);
    }
}
