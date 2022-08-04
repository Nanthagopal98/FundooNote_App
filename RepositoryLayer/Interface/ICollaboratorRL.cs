using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRL
    {
        public CollaboratorEntity Create(CollaboratorModel collaboratorModel, long userId);
        public bool Delete(long ColabId);
        public IEnumerable<CollaboratorEntity> Get(long notesId);
    }
}
