using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NoteRL : INoteRL
    {
        private readonly FundooContext fundooContext;
        public NoteRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public NotesEntity CreateNotes(NotesModel notesModel, long userId)
        {
            try
            {
                var validateUser = fundooContext.UserTable.Where(e => e.UserId == userId);
                if (validateUser != null)
                {
                    NotesEntity notesEntity = new NotesEntity();
                    notesEntity.Title = notesModel.Title;
                    notesEntity.Description = notesModel.Description;
                    notesEntity.Reminder = notesModel.Reminder;
                    notesEntity.Color = notesModel.Color;
                    notesEntity.Image = notesModel.Image;
                    notesEntity.Archive = notesModel.Archive;
                    notesEntity.PinNotes = notesModel.PinNotes;
                    notesEntity.Trash = notesModel.Trash;
                    notesEntity.Created = notesModel.Created;
                    notesEntity.Modified = notesModel.Modified;
                    notesEntity.UserId = userId;
                    fundooContext.NotesTable.Add(notesEntity);
                    fundooContext.SaveChanges();
                    return notesEntity;
                }
                else
                {
                    return null;
                }
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
                var result = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
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
                var findNotes = fundooContext.NotesTable.First(e => e.NotesId == notesId);
                if(findNotes != null)
                {
                    fundooContext.NotesTable.Remove(findNotes);
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var findNotes = fundooContext.NotesTable.First(e => e.NotesId == notesId);
                if (findNotes != null)
                {
                    findNotes.Title = notesUpdateModel.Title;
                    findNotes.Description = notesUpdateModel.Description;
                    findNotes.Reminder = notesUpdateModel.Reminder;
                    findNotes.Color = notesUpdateModel.Color;
                    findNotes.Image = notesUpdateModel.Image;
                    findNotes.Modified = DateTime.Now;
                    fundooContext.SaveChanges();
                    return findNotes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public NotesEntity Archive(long notesId)
        {
            try
            {
                var findNotes = fundooContext.NotesTable.First(e => e.NotesId == notesId);
                if (findNotes.Archive == false)
                {
                    findNotes.Archive = true;
                    fundooContext.SaveChanges();
                    return findNotes;
                }
                else if ((findNotes.Archive == true))
                {
                    findNotes.Archive = false;
                    fundooContext.SaveChanges();
                    return findNotes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity Pin(long notesId)
        {
            try
            {
                var findNote = fundooContext.NotesTable.First(e => e.NotesId == notesId);
                if (findNote.PinNotes == false)
                {
                    findNote.PinNotes = true;
                    fundooContext.SaveChanges();
                    return findNote;
                }
                else if (findNote.PinNotes == true)
                {
                    findNote.PinNotes = false;
                    fundooContext.SaveChanges();
                    return findNote;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public NotesEntity Trash(long notesId)
        {
            try
            {
                var findNote = fundooContext.NotesTable.First(e => e.NotesId == notesId);
                if (findNote.Trash == false)
                {
                    findNote.Trash = true;
                    fundooContext.SaveChanges();
                    return findNote;
                }
                else if (findNote.Trash == true)
                {
                    findNote.Trash = false;
                    fundooContext.SaveChanges();
                    return findNote;
                }
                else
                {
                    return null;
                }
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
                var findNotes = fundooContext.NotesTable.First(e => e.NotesId == notesId);
                if(findNotes != null)
                {
                    findNotes.Color = color;
                    fundooContext.SaveChanges();
                    return findNotes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
