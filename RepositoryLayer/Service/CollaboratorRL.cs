using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly FundooContext fundooContext;
        public CollaboratorRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public CollaboratorEntity Create(CollaboratorModel collaboratorModel, long userId)
        {
            try
            {
                var filterUser = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var getNotes = filterUser.Where(e => e.NotesId == collaboratorModel.NotesId).FirstOrDefault();
                    var getEmail = fundooContext.UserTable.Where(e => e.Email == collaboratorModel.Email).FirstOrDefault();
                    if (getNotes != null && getEmail != null)
                    {
                        CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                        collaboratorEntity.NotesId = getNotes.NotesId;
                        collaboratorEntity.Email = getEmail.Email;
                        collaboratorEntity.UserId = userId;
                        fundooContext.CollaboratorTable.Add(collaboratorEntity);
                        fundooContext.SaveChanges();
                        return collaboratorEntity;
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
        public bool Delete(long ColabId, long userId)
        {
            try
            {
                var filterUser = fundooContext.CollaboratorTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findCollaborator = filterUser.Where(e => e.ColabId == ColabId).FirstOrDefault();
                    if (findCollaborator != null)
                    {
                        fundooContext.CollaboratorTable.Remove(findCollaborator);
                        fundooContext.SaveChanges();
                        return true;
                    }
                        return false;
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
        public IEnumerable<CollaboratorEntity> Get(long notesId, long userId)
        {
            try
            {
                var filterUser = fundooContext.CollaboratorTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var getCollab = filterUser.Where(e => e.NotesId == notesId).ToList();
                    if (getCollab.Count != 0)
                    {
                        return getCollab;
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
    }
}
