using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/user/activity/add")]
    public class UserFavouriteActivityController {

        public struct ActivityEdit {
            public string Token { get; set; }
            public int ActivityID { get; set; }
        }

        /// <summary>
        /// Adds a favourite activity to your account
        /// </summary>
        /// <param name="activityAdd">Your auth token and activity ID.</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<string> FavouriteActivity([FromBody] ActivityEdit activityAdd) {

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(activityAdd.Token);

            if (userID == -1)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid token."));

            Database db = new();

            User? dbUser = db.Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (dbUser == null)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid user."));

            ActivityData? dbActivity = db.Activities.Where(activity => activity.ActivityID == activityAdd.ActivityID).FirstOrDefault();

            if (dbActivity == null)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid activity."));

            UserActivity join = new UserActivity();
            join.ActivityID = activityAdd.ActivityID;
            join.UserID = userID;

            await db.UserFavouriteActivity.AddAsync(join);
            await db.SaveChangesAsync();

            return JsonSerializer.Serialize(new GenericResponse(true, "Favourite activity added."));

        }

    }
}
