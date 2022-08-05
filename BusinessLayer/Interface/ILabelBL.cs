﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity Create(LabelModel labelModel, long userId);
        public LabelEntity Update(long userId, long labelId, LabelModel labelModel);
        public IEnumerable<LabelEntity> Get(long userId);
        public bool Delete(long userId, long labelId);
    }
}
