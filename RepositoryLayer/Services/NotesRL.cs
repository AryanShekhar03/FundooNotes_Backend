using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Data;

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



        public IEnumerable<Notes> GetAllNotes(long UserId)
        {
            return fundooContext.NotesTable.Where(x => x.UserId == UserId).ToList();
        }


        public string UpdateNote(NotesModel notesUpdateModel, long NotesID)
        {
            try
            {
                var update = fundooContext.NotesTable.Where(X => X.NotesId == NotesID).FirstOrDefault();
                if (update != null && update.NotesId == NotesID)
                {
                    update.Title = notesUpdateModel.Title;
                    update.Body = notesUpdateModel.Body;
                    update.ModifiedTime = DateTime.Now;
                    update.Color = notesUpdateModel.Color;
                    update.Image = notesUpdateModel.Image;

                    this.fundooContext.SaveChanges();
                    return "Note is Modified";
                }
                else
                {
                    return " Not Modified";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ArchieveNotes(long NotesID)
        {
            try
            {
                var archive = this.fundooContext.NotesTable.Where(Y => Y.NotesId == NotesID).FirstOrDefault();
                if (archive.Archieve == true)
                {
                    archive.Archieve = false;
                    this.fundooContext.SaveChanges();
                    return "note is archieved";
                }


                else
                {
                    archive.Archieve = true;
                    this.fundooContext.SaveChanges();
                    return "note is now unarchieved";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteNotes(long NotesID)
        {
            try
            {
                var notecheck = this.fundooContext.NotesTable.Where(x => x.NotesId == NotesID).FirstOrDefault();
                this.fundooContext.NotesTable.Remove(notecheck);
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



        public string Pinned(long NotesID)
        {
            try
            {
                var pinned = this.fundooContext.NotesTable.Where(s => s.NotesId == NotesID).FirstOrDefault();
                if (pinned.IsPinned == true)
                {
                    pinned.IsPinned = false;
                    this.fundooContext.SaveChanges();
                    return "note pinned";
                }


                else
                {
                    pinned.IsPinned = true;
                    this.fundooContext.SaveChanges();
                    return "note is now unpinned";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string TrashNotes(long NotesID)
        {
            try
            {
                var trashed = this.fundooContext.NotesTable.Where(s => s.NotesId == NotesID).FirstOrDefault();
                if (trashed.Delete == true)
                {
                    trashed.Delete = false;
                    this.fundooContext.SaveChanges();
                    return "notes recoverd";
                }


                else
                {
                    trashed.Delete = true;
                    this.fundooContext.SaveChanges();
                    return "note is in trashed";
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string Notescolor(long NotesID, string color)
        {
            try
            {
                var addcolor = this.fundooContext.NotesTable.Where(x => x.NotesId == NotesID).FirstOrDefault();
                if (addcolor.Color == null)
                {
                    addcolor.Color = color;
                    this.fundooContext.SaveChanges();
                    return "color is added";
                }
                else
                {
                    return "color is already added";
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }

    
 }
