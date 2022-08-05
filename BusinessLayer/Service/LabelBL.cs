using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        private readonly ILabelRL iLabelRL;
        public LabelBL(ILabelRL iLabelRL)
        {
            this.iLabelRL = iLabelRL;
        }
        public LabelEntity Create(LabelModel labelModel, long userId)
        {
            try
            {
                return iLabelRL.Create(labelModel, userId);
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
                return iLabelRL.Update(userId, labelId, labelModel);
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
                return iLabelRL.Get(userId);
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
                return iLabelRL.Delete(userId, labelId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
