using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/activity/delete")]
    public class DeleteActivityController {

        public struct ActivityDelete {
            public string Token { get; set; }
            public int ActivityID { get; set; }
        }

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<string> DeleteActivity([FromBody] ActivityDelete bodyActivity) {

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(bodyActivity.Token);

            if (userID == -1)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid token."));

            bool checkAdmin = authManager.CheckAdmin(userID);

            if (!checkAdmin)
                return JsonSerializer.Serialize(new GenericResponse(false, "Unauthorized."));

            Database db = new();

            IQueryable<ActivityData> activityData = db.Activities.Where(activity => activity.ActivityID == bodyActivity.ActivityID);

            if (!activityData.Any())
                return JsonSerializer.Serialize(new GenericResponse(false, "Activity doesn't exist."));

            await activityData.ExecuteDeleteAsync();

            return JsonSerializer.Serialize(new GenericResponse(true, "Activity deleted."));

        }

    }
}
