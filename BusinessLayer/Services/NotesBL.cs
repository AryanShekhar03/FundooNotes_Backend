using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using RepositoryLayer.Interfaces;


namespace BusinessLayer.Services
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL notesRL;
        

        public NotesBL(INotesRL notesRL)
        {

            this.notesRL = notesRL;

        }

        public string ArchieveNotes(long NotesID)
        {
            try
            {
                return this.notesRL.ArchieveNotes(NotesID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CreateNote(NotesModel notesModel, long userId)
        {
            try
            {
                return this.notesRL.CreateNote(notesModel, userId);
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
                return this.notesRL.DeleteNotes(NotesID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Notes> GetAllNotes(long userId)
        {
            try
            {
                return this.notesRL.GetAllNotes(userId);
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
                return this.notesRL.Notescolor(NotesID,color);
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
                return this.notesRL.Pinned(NotesID);
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
                return this.notesRL.TrashNotes(NotesID);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string UpdateNote(NotesModel notesUpdateModel, long NotesID)
        {
            try
            {
                return notesRL.UpdateNote(notesUpdateModel, NotesID);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
