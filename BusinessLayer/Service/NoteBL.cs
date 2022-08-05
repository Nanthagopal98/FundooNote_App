using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
    {
        private readonly INoteRL iNoteRL;
        public NoteBL(INoteRL iNoteRL)
        {
            this.iNoteRL = iNoteRL;
        }

        public NotesEntity CreateNotes(NotesModel notesModel, long userId)
        {
            try
            {
                return iNoteRL.CreateNotes(notesModel, userId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public IEnumerable<NotesEntity> DisplayNotes(long userId)
        {
            try
            {
                return iNoteRL.DisplayNotes(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public bool DeleteNotes(long notesId, long userId)
        {
            try
            {
                return iNoteRL.DeleteNotes(notesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity UpdateNotes(NotesUpdateModel notesUpdateModel, long notesId, long userId)
        {
            try
            {
                return iNoteRL.UpdateNotes(notesUpdateModel, notesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Archive(long notesId, long userId)
        {
            try
            {
                return iNoteRL.Archive(notesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Pin(long notesId, long userId)
        {
            try
            {
                return iNoteRL.Pin(notesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Trash(long notesId, long userId)
        {
            try
            {
                return iNoteRL.Trash(notesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity Color(long notesId, string color, long userId)
        {
            try
            {
                return iNoteRL.Color(notesId, color, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UploadImage(string filePath, long notesId, long userId)
        {
            try
            {
                return iNoteRL.UploadImage(filePath, notesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
