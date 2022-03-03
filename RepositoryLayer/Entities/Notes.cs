using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RepositoryLayer.Entities
{
    public class Notes
    { 
        [Key] /// DataAnnotation for setting the primary Key value.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Generaters the values for the database Id's.

        public long NotesId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPinned { get; set; }
        public bool Delete { get; set; }
        public bool Archieve { get; set; }
        public DateTime? Reminder { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? ModifiedTime { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        //foreign key
        [ForeignKey("user")]
        public long UserId { get; set; }
        public virtual User user { get; set; }


    }
}
