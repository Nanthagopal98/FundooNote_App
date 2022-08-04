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
    }
}
