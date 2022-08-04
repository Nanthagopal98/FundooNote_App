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
        public bool Delete(long ColabId)
        {
            try
            {
                return iCollaboratorRL.Delete(ColabId);
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
                return iCollaboratorRL.Get(notesId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
