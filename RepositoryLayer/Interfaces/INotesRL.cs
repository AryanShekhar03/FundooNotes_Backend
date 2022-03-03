using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL
    { 


    public bool CreateNote(NotesModel notesModel, long userId);

    //public IEnumerable<Notes> GetAllNotes(long userId);
    }
}
