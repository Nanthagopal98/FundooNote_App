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
                var validateUser = fundooContext.UserTable.FirstOrDefault(e => e.UserId == userId);
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
        public NotesEntity DisplayNotes(long notesId)
        {
            try
            {
                var result = fundooContext.NotesTable.Where(e => e.NotesId == notesId).FirstOrDefault();
                if(result != null)
                {
                    NotesEntity notesEntity = new NotesEntity();
                    notesEntity.NotesId = result.NotesId;
                    notesEntity.Title = result.Title;
                    notesEntity.Description = result.Description;
                    notesEntity.Reminder = result.Reminder;
                    notesEntity.Color = result.Color;
                    notesEntity.Image = result.Image;
                    notesEntity.Archive = result.Archive;
                    notesEntity.PinNotes = result.PinNotes;
                    notesEntity.Trash = result.Trash;
                    notesEntity.Created = result.Created;
                    notesEntity.Modified = result.Modified;
                    notesEntity.UserId= result.UserId;
                    //fundooContext.NotesTable.Update(notesModel);
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
    }
}
