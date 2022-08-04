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
    }
}
