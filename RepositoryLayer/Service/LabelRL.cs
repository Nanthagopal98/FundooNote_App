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
    public class LabelRL : ILabelRL
    {
        public FundooContext fundooContext;
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        public LabelEntity Create(LabelModel labelModel, long userId)
        {
            try
            {
                var findNotes = fundooContext.NotesTable.Where(e => e.NotesId == labelModel.NotesId);
                if(findNotes != null)
                {
                    LabelEntity labelEntity = new LabelEntity();
                    labelEntity.LabelName = labelModel.LabelName;
                    labelEntity.NotesId = labelModel.NotesId;
                    labelEntity.UserId = userId;
                    fundooContext.LabelTable.Add(labelEntity);
                    fundooContext.SaveChanges();
                    return labelEntity;
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