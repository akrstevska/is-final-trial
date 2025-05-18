using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project.Data.Entities
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }

        public int Floor { get; set; }

        public string Type { get; set; }

        public virtual IEnumerable<Guest> Guests { get; set; }

    }
}