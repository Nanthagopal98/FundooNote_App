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
                var getNotes = fundooContext.NotesTable.Where(e => e.NotesId == collaboratorModel.NotesId).FirstOrDefault();
                if(getNotes != null)
                {
                    CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                    collaboratorEntity.NotesId = collaboratorModel.NotesId;
                    collaboratorEntity.Email = collaboratorModel.Email;
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
            catch (Exception)
            {

                throw;
            }
        }
        public bool Delete(long ColabId)
        {
            try
            {
                var findCollaborator = fundooContext.CollaboratorTable.Where(e => e.ColabId == ColabId).First();
                if (findCollaborator != null)
                {
                    fundooContext.CollaboratorTable.Remove(findCollaborator);
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
        public IEnumerable<CollaboratorEntity> Get(long notesId)
        {
            try
            {
                var getCollab = fundooContext.CollaboratorTable.Where(e => e.NotesId == notesId);
                if (getCollab != null)
                {
                    return getCollab;
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
