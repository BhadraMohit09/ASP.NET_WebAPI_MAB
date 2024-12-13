using System;

namespace WebAPI_MAB.Models
{
    public class StateModel
    {
        public int StateID { get; set; } // Primary Key (Identity column)

        public int CountryID { get; set; } // Foreign Key, not null

        public string StateName { get; set; } // State name, not null

        public string StateCode { get; set; } // Optional state code (nullable in the database)

        public DateTime CreatedDate { get; set; } // Auto-populated with GETDATE()

        public DateTime? ModifiedDate { get; set; } // Nullable, to store last modification date
    }
}
