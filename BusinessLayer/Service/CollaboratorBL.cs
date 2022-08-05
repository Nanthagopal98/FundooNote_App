using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollaboratorBL : ICollaboratorBL
    {
        private readonly ICollaboratorRL iCollaboratorRL;
        public CollaboratorBL(ICollaboratorRL iCollaboratorRL)
        {
            this.iCollaboratorRL = iCollaboratorRL;
        }
        public CollaboratorEntity Create(CollaboratorModel collaboratorModel, long userId)
        {
            try
            {
                return iCollaboratorRL.Create(collaboratorModel, userId);
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
                return iCollaboratorRL.Delete(ColabId, userId);
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
                return iCollaboratorRL.Get(notesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
