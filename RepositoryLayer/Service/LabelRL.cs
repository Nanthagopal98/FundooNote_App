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
                var filterUser = fundooContext.NotesTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findNotes = filterUser.Where(e => e.NotesId == labelModel.NotesId).ToList();
                    if (findNotes.Count != 0)
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
                else { return null; }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public LabelEntity Update(long userId, long labelId, LabelModel labelModel)
        {
            try
            {
                var filterUser = fundooContext.LabelTable.Where(e => e.UserId == userId);
                if (filterUser != null)
                {
                    var findLabel = filterUser.Where(e => e.LabelId == labelId).FirstOrDefault();
                    if (findLabel != null)
                    {
                        findLabel.LabelName = labelModel.LabelName;
                        fundooContext.SaveChanges();
                        return findLabel;
                    }
                    else
                    {
                        return null;
                    }
                }
                else { return null; }
            }
            catch (Exception)
            {

                throw;
            }            
        }
        public IEnumerable<LabelEntity> Get(long userId)
        {
            try
            {
                var filterUser = fundooContext.LabelTable.Where(e => e.UserId == userId);
                if(filterUser != null)
                {
                    return filterUser;
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
        public bool Delete(long userId, long labelId)
        {
            try
            {
                var filterUser = fundooContext.LabelTable.Where(e => e.UserId == userId);
                if(filterUser != null)
                {
                    var findLabel = filterUser.Where(e => e.LabelId == labelId).FirstOrDefault();
                    if(findLabel != null)
                    {
                        fundooContext.LabelTable.Remove(findLabel);
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
    }
}