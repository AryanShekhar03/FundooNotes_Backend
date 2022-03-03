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

     public IEnumerable<Notes> GetAllNotes(long userId);

    public string UpdateNote(NotesModel notesUpdateModel , long NotesID);

    public bool DeleteNotes(long NotesID);

    public string ArchieveNotes(long NotesID);

    public string Pinned(long NotesID);

    public string TrashNotes(long NotesID);

    public string Notescolor(long NotesID, string color);


    }

}
