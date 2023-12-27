using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/activity/new")]
    public class NewActivityController : Controller {

        public struct BodyActivity {
            public string Activity { get; set; }
        }

        /// <summary>
        /// Adds a new activity to the database
        /// </summary>
        /// <param name="bodyActivity">The activity to add</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task NewActivity([FromBody] BodyActivity bodyActivity) {

            Database db = new();

            ActivityData activity = new();
            activity.Activity = bodyActivity.Activity;

            await db.Activities.AddAsync(activity);
            await db.SaveChangesAsync();

        }

    }
}
