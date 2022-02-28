using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RepositoryLayer.Entities
{
    public class Notes
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

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
        public User User { get; set; }

    }
}
