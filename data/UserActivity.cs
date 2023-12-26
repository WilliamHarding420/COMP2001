using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2001.data {

    [Table("UserFavouriteActivity", Schema="CW2")]
    [PrimaryKey(nameof(UserID), nameof(ActivityID))]
    public class UserActivity {

        public int UserID { get; set; }

        public int ActivityID { get; set; }

    }
}
