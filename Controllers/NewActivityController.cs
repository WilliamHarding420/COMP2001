using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/activity/new")]
    public class NewActivityController : Controller {

        public struct BodyActivity {
            public string Token { get; set; }
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
        public async Task<string> NewActivity([FromBody] BodyActivity bodyActivity) {

            Database db = new();

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(bodyActivity.Token);

            if (userID == -1)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid auth token."));

            User? dbUser = db.Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (dbUser == null)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid user."));

            if (!authManager.CheckAdmin(dbUser))
                return JsonSerializer.Serialize(new GenericResponse(false, "Unauthorized."));

            ActivityData activity = new();
            activity.Activity = bodyActivity.Activity;

            await db.Activities.AddAsync(activity);
            await db.SaveChangesAsync();

            return JsonSerializer.Serialize(new GenericResponse(true, "Activity successfully added."));

        }

    }
}
