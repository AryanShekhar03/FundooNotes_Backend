using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Interfaces
{
    public interface ICollabRL
    {
       public bool AddCollab(CollabModel collabModel, long UserId);

       public IEnumerable<Collaborator> GetCollabsByNoteId(long NotesId);


    }
}
