using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INotesBL
    {
        public bool CreateNote(NotesModel notesModel, long userId);

        //public IEnumerable<Notes> GetAllNotes(long UserId);
        //object MakesNotes(NotesModel notesmodel, long userid);
        // object AddNotes(NotesModel notesmodel);
    }
}
