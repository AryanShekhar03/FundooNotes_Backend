using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class NotesModel
    {
        
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
        
    }
}
