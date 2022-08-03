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
       
        public bool DeleteNotes(long notesId)
        {
            try
            {
                return iNoteRL.DeleteNotes(notesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity UpdateNotes(NotesUpdateModel notesUpdateModel, long notesId)
        {
            try
            {
                return iNoteRL.UpdateNotes(notesUpdateModel, notesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Archive(long notesId)
        {
            try
            {
                return iNoteRL.Archive(notesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Pin(long notesId)
        {
            try
            {
                return iNoteRL.Pin(notesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Trash(long notesId)
        {
            try
            {
                return iNoteRL.Trash(notesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity Color(long notesId, string color)
        {
            try
            {
                return iNoteRL.Color(notesId, color);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UploadImage(string filePath, long notesId)
        {
            try
            {
                return iNoteRL.UploadImage(filePath, notesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
