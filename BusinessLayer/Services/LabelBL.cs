using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {

        private readonly ILabelRL labelRL; // readonly can only be assigned a value from within the constructor(s) of a class.


        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public bool AddLabel(LabelModel labelModel)
        {
            try
            {
                return labelRL.AddLabel(labelModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteLabel(long labelId)
        {
            try
            {
                return labelRL.DeleteLabel(labelId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Label> GetlabelByNotesId(long NotesId)
        {
            try
            {
                return labelRL.GetlabelByNotesId(NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
