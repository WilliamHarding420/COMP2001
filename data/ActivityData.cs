using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2001.data {

    [Table("Activities", Schema="CW2")]
    public class ActivityData {

        [Key]
        public int ActivityID { get; set; }

        public string Activity { get; set; }

    }
}
