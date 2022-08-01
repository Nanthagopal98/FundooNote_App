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

        public NotesEntity DisplayNotes(long notesId)
        {
            try
            {
                return iNoteRL.DisplayNotes(notesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
