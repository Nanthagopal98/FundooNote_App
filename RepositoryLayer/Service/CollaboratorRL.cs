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
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly FundooContext fUndooContext;
        public CollaboratorRL(FundooContext fUndooContext)
        {
            this.fUndooContext = fUndooContext;
        }
        public CollaboratorEntity Create(CollaboratorModel collaboratorModel, long userId)
        {
            try
            {
                var getNotes = fUndooContext.NotesTable.Where(e => e.NotesId == collaboratorModel.NotesId).FirstOrDefault();
                if(getNotes != null)
                {
                    CollaboratorEntity collaboratorEntity = new CollaboratorEntity();
                    collaboratorEntity.NotesId = collaboratorModel.NotesId;
                    collaboratorEntity.Email = collaboratorModel.Email;
                    collaboratorEntity.UserId = userId;
                    fUndooContext.CollaboratorTable.Add(collaboratorEntity);
                    fUndooContext.SaveChanges();
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
    }
}
