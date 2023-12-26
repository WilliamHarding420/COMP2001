using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2001.data {

    public class UserActivityJoin {

        public int UserID { get; set; }

        public string Username { get; set; }

        public int ActivityID { get; set; }

        public string Activity { get; set; }

    }
}
