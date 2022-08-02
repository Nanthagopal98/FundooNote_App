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
    }
}
