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
        public bool Archive(long notesId);
        public bool Pin(long notesId);
        public bool Trash(long notesId);
        public NotesEntity Color(long notesId, string color);
        public string UploadImage(string filePath, long notesId);
    }
}
