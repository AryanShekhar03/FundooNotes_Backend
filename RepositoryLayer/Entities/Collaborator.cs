using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Collaborator
    {

            

            [Key]/// DataAnnotation for setting the primary Key value.
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]// Generaters the values for the database Id's
            public long CollabId { get; set; }
            public string CollabEmail { get; set; }

            [ForeignKey("Notes")]
            public long NotesId { get; set; }
            public virtual Notes notes { get; set; }

            [ForeignKey("user")]
            public long UserId { get; set; }
            public virtual User user { get; set; }
        
    }
}

