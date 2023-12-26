using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2001.data {

    [Table("Users", Schema="CW2")]
    public class User {

        [Key]
        public int UserID { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string AboutMe { get; set; }

        public string Location { get; set; }

        public string Units { get; set; }

        public string ActivityTimePreference { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public string Birthday { get; set; }

        public string ProfilePictureLink { get; set; }

        public string Password { get; set; }

        public string Language { get; set; }

        public string Admin { get; set; }



    }
}
