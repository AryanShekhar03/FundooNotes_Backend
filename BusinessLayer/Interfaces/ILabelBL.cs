using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        public bool AddLabel(LabelModel labelModel);
        public IEnumerable<Label> GetlabelByNotesId(long NotesId);
        public bool DeleteLabel(long labelID);

        public string UpdateLabel(LabelModel labelModel, long labelID);
    }
}
