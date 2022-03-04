using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entities;

namespace BusinessLayer.Interfaces
{
    public interface ICollabBL
    {
      public  bool AddCollab(CollabModel collabModel, long UserId);

        public IEnumerable<Collaborator> GetCollabsByNoteId(long NotesId);

        public string RemoveCollab(long CollabId);
    }
}
