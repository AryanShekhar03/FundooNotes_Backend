using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Label
    {

        [Key]  /// DataAnnotation for setting the primary Key value.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Generaters the values for the database Id's.
        public long LabelID { get; set; }
        public string LabelName { get; set; }

        [ForeignKey("user")]
        public long UserId { get; set; }
        public virtual User user { get; set; }

        [ForeignKey("note")]
        public long NoteId { get; set; }
        public virtual Notes notes { get; set; }
    }
}
