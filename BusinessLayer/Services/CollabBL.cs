using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Services;
using CommonLayer.Models;


namespace BusinessLayer.Services
{
    public class CollabBL : ICollabBL
    {
        private readonly ICollabRL collabRL;

        public CollabBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }

        public bool AddCollab(CollabModel collabModel, long UserId)
        {
            try
            {
                return collabRL.AddCollab(collabModel, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Collaborator> GetCollabsByNoteId(long NotesId)
        {
            try
            {
                return collabRL.GetCollabsByNoteId(NotesId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
