using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.Services
{
    public class CollabRL : ICollabRL
    {

        public readonly FundooContext fundooContext; //context class is used to query or save data to the database.
        public CollabRL(FundooContext fundooContext)  
        {
            this.fundooContext = fundooContext;
        }
        public bool AddCollab(CollabModel collabModel, long UserId)
        {
            try
            {
                var noteData = this.fundooContext.NotesTable.Where(x => x.NotesId == collabModel.NotesID).FirstOrDefault();
                var userData = this.fundooContext.UserTables.Where(x => x.Email == collabModel.CollabEmail).FirstOrDefault();
                if (noteData != null && userData != null)
                {
                    Collaborator collab = new Collaborator();
                    collab.CollabEmail = collabModel.CollabEmail;
                    collab.NotesId = collabModel.NotesID;
                    collab.UserId = userData.Id;
                    this.fundooContext.CollabTable.Add(collab);//Adding the data to database
                }

                //Save the changes in database
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

        public IEnumerable<Collaborator> GetCollabsByNoteId(long NotesId)
        {
            try
            {
                var response = this.fundooContext.CollabTable.Where(x => x.NotesId == NotesId).ToList();
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string RemoveCollab(long CollabId)
        {

            var collab = fundooContext.CollabTable.Where(X => X.CollabId == CollabId).SingleOrDefault();
            if (collab != null)
            {
                fundooContext.CollabTable.Remove(collab);
                this.fundooContext.SaveChanges();
                return "Member removed from collaboration ";
            }
            else
            {
                return null;
            }
        }
    }
}
