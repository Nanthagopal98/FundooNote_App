using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;



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
        public bool DeleteNotes(long notesId, long userId)
        {
            try
            {
                var filterUser = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findNotes = filterUser.Where(e => e.NotesId == notesId).FirstOrDefault();
                    if (findNotes != null)
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
        public NotesEntity UpdateNotes(NotesUpdateModel notesUpdateModel, long notesId, long userId)
        {
            try
            {
                var filterUser = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findNotes = filterUser.FirstOrDefault(e => e.NotesId == notesId);
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
                    return null;
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
        public bool Archive(long notesId, long userId)
        {
            try
            {
                var filterUser = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findNotes = filterUser.FirstOrDefault(e => e.NotesId == notesId);
                    if (findNotes.Archive == false)
                    {
                        findNotes.Archive = true;
                        fundooContext.SaveChanges();
                        return findNotes.Archive;
                    }
                    else
                    {
                        findNotes.Archive = false;
                        fundooContext.SaveChanges();
                        return findNotes.Archive;
                    }
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
        public bool Pin(long notesId, long userId)
        {
            try
            {
                var filterUser = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findNote = filterUser.First(e => e.NotesId == notesId);
                    if (findNote.PinNotes == false)
                    {
                        findNote.PinNotes = true;
                        fundooContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        findNote.PinNotes = false;
                        fundooContext.SaveChanges();
                        return false;
                    }
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
        public bool Trash(long notesId, long userId)
        {
            try
            {
                var filterUser = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findNote = filterUser.First(e => e.NotesId == notesId);
                    if (findNote.Trash == false)
                    {
                        findNote.Trash = true;
                        fundooContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        findNote.Trash = false;
                        fundooContext.SaveChanges();
                        return false;
                    }
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
        public NotesEntity Color(long notesId, string color, long userId)
        {
            try
            {
                var filterUser = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findNotes = filterUser.FirstOrDefault(e => e.NotesId == notesId);
                    if (findNotes != null)
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
        public string UploadImage(string filePath, long notesId, long userId) // path format - D:\Bridgelabz\Bridgelabz\Web API\Imgaes\test1.png
        {
            try
            {
                var filterUser = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findNotes = filterUser.FirstOrDefault(e => e.NotesId == notesId);
                    if (findNotes != null)
                    {
                        Account account = new Account("dyod5szeo", "912552913142265", "WNhgZn-_MEOijEd4l3vOlfSWSLc");
                        Cloudinary cloudinary = new Cloudinary(account);
                        ImageUploadParams uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(filePath),
                            PublicId = findNotes.Title
                        };

                        ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

                        findNotes.Modified = DateTime.Now;
                        findNotes.Image = uploadResult.Url.ToString();
                        fundooContext.SaveChanges();
                        return "Upload Successfull";
                    }
                        return null;
                }
                else { return null; }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
