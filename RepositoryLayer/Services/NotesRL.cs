using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        private readonly FundooContext fundooContext; //context class is used to query or save data to the database.
        IConfiguration _Toolsettings;  //IConfiguration interface is used to read Settings and Connection Strings from AppSettings.
        public NotesRL(FundooContext fundooContext, IConfiguration Toolsettings)
        {
            this.fundooContext = fundooContext;
            _Toolsettings = Toolsettings;
        }




        public bool CreateNote(NotesModel notesModel, long userId)
        {
            try
            {
                Notes newNotes = new Notes();
                newNotes.UserId = userId;
                newNotes.Title = notesModel.Title;
                newNotes.Body = notesModel.Body;
                newNotes.Reminder = notesModel.Reminder;
                newNotes.Color = notesModel.Color;
                newNotes.Image = notesModel.Image;
                newNotes.Archieve = notesModel.Archieve;
                newNotes.IsPinned = notesModel.IsPinned;
                newNotes.Delete = notesModel.Delete;
                newNotes.CreatedTime = DateTime.Now;

                fundooContext.NotesTable.Add(newNotes); //connecting to database
                var result = this.fundooContext.SaveChanges();
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



        //public IEnumerable<Notes> GetAllNotes(long UserId)
        //{
        //    return FundooContext.NotesTable.Where(x => x.UserId == UserId).ToList();
        //}
    }

    
 }
