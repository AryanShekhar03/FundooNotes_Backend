using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        public bool AddLabel(LabelModel labelModel);
        public IEnumerable<Label> GetlabelByNotesId(long NotesId);
        public bool DeleteLabel(long labelId);
    }
}
