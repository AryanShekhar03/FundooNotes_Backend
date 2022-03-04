using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Collaborator
    {

            

            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long CollabId { get; set; }
            public string CollabEmail { get; set; }

            [ForeignKey("Notes")]
            public long NotesId { get; set; }
            public virtual Notes note { get; set; }

            [ForeignKey("user")]
            public long UserId { get; set; }
            public virtual User user { get; set; }
        
    }
}

