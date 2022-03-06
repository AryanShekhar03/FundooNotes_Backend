using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
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
        public readonly FundooContext fundooContext;//context class is used to query or save data to the database.
        IConfiguration _Toolsettings;  //IConfiguration interface is used to read Settings and Connection Strings from AppSettings.
        public LabelRL(FundooContext fundooContext, IConfiguration Toolsettings)
        {
            this.fundooContext = fundooContext;
            _Toolsettings = Toolsettings;
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
                    label.NotesId = note.NotesId;
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

        public bool DeleteLabel(long labelID)
        {
            try
            {
                var check = this.fundooContext.LabelsTable.Where(x => x.LabelID == labelID).FirstOrDefault();
                this.fundooContext.LabelsTable.Remove(check);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return true;
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

        public IEnumerable<Label> GetlabelByNotesId(long NotesId)
        {
            try
            {
                var response = this.fundooContext.LabelsTable.Where(x => x.NotesId == NotesId).ToList();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string UpdateLabel(LabelModel labelModel, long labelID)
        {
            try
            {
                var update = fundooContext.LabelsTable.Where(X => X.LabelID == labelID).FirstOrDefault();
                if (update != null && update.LabelID == labelID)
                {
                    update.LabelName = labelModel.LabelName;
                    update.NotesId = labelModel.NotesId;

                    this.fundooContext.SaveChanges();
                    return "Label is modified";
                }
                else
                {
                    return "Label is not modified";
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
    
}
