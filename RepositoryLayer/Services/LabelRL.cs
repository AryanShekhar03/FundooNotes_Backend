using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL :ILabelRL
    {
        public readonly FundooContext fundooContext; //context class is used to query or save data to the database.
        public LabelRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        public bool AddLabel(LabelModel labelModel)
        {
            try
            {
                var note = fundooContext.NotesTable.Where(x => x.NotesId == labelModel.NotesId).FirstOrDefault();
                if (note != null)
                {
                    Label label = new Label();
                    label.LabelName = labelModel.LabelName;
                    label.NoteId = note.NotesId;
                    label.UserId = note.UserId;

                    this.fundooContext.LabelsTable.Add(label);
                    int result = this.fundooContext.SaveChanges();
                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    
}
