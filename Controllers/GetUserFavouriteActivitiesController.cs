using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    public struct ActivityDataResponse(int _ActivityID, string _Activity) {
        public int ActivityID { get; set; } = _ActivityID;
        public string Activity { get; set; } = _Activity;
    }

    [ApiController]
    [Route("/user/activities")]
    public class GetUserFavouriteActivitiesController : Controller {

        /// <summary>
        /// Gets your favourite activities.
        /// </summary>
        /// <param name="auth">Your auth token</param>
        /// <returns>A JSON list of your favourite activities</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<string> GetFavouriteActivities([FromBody] InfoAuth auth) {

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(auth.Token);

            if (userID == -1)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid auth token."));

            Database db = new();

            IQueryable<UserActivityJoin> userActivities = db.UserActivityJoinView.Where(join => join.UserID == userID);
            UserActivityJoin[] activityList = userActivities.ToArray();

            return JsonSerializer.Serialize(activityList);

        }

    }
}
