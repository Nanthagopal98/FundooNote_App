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
        public bool DeleteNotes(long notesId, long userId);
        public NotesEntity UpdateNotes(NotesUpdateModel notesUpdateModel, long notesId, long userId);
        public bool Archive(long notesId, long userId);
        public bool Pin(long notesId, long userId);
        public bool Trash(long notesId, long userId);
        public NotesEntity Color(long notesId, string color, long userId);
        public string UploadImage(string filePath, long notesId, long userId);
    }
}
