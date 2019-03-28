using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace XamCross10.Models
{
    public class Experience
    {
        [AutoIncrement,PrimaryKey]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string VenueName { get; set; } // NEW

        public string VenueCategory { get; set; } // NEW

        public float VenueLat { get; set; } // NEW

        public float VenueLng { get; set; } // NEW


        public override string ToString()
        {
            return Title;
        }
    }
}
