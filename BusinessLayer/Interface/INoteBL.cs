﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NotesEntity CreateNotes(NotesModel notesModel, long userId);
        public NotesEntity DisplayNotes(long notesId);
    }
}
