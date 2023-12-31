using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/activity/{id}")]
    public class GetActivityController {

        /// <summary>
        /// Gets an specific activity from its ID
        /// </summary>
        /// <param name="id">The activity's ID</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<string> GetActivity([FromRoute] int id) {

            Database db = new();
            IQueryable<ActivityData> activities = db.Activities.Where(activity => activity.ActivityID == id);

            if (!activities.Any())
                return JsonSerializer.Serialize(new GenericResponse(false, "No activity found."));

            return JsonSerializer.Serialize(activities.FirstOrDefault());

        }

    }
}
