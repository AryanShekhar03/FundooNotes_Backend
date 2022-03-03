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

        //public bool CreateNotes(NotesModel notesModel ,long UserId)
        //{
        //    try
        //    {
        //        return this.notesBL.CreateNote(notesModel , UserId);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public IEnumerable<Notes> GetAllNotes(long userId)
        //{
        //    try
        //    {
        //        return this.notesBL.GetAllNotes(userId);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}


    }
}
