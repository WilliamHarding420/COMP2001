using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/activity/update/{id}")]
    public class UpdateActivityController {

        public struct ActivityUpdate {
            public string Token { get; set; }
            public string ActivityName { get; set; }
        }

        /// <summary>
        /// Endpoint for updating an activitie's name
        /// </summary>
        /// <param name="id">ID of the activity</param>
        /// <param name="authInfo">Your auth token and what to change the activity name to</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        public async Task<string> UpdateActivity([FromRoute] int id, [FromBody] ActivityUpdate authInfo) {

            AuthManager authManager = AuthManager.instance;

            int userID = authManager.GetIDFromToken(authInfo.Token);

            if (userID == -1)
                return await GenericResponse<string>.InvalidTokenResponse.Serialize();

            Database db = new();

            User? dbUser = db.Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (dbUser == null)
                return await GenericResponse<string>.InvalidUserResponse.Serialize();

            if (!authManager.CheckAdmin(dbUser))
                return await GenericResponse<string>.UnauthorizedResponse.Serialize();

            ActivityData? activity = db.Activities.Where(activity => activity.ActivityID == id).FirstOrDefault();

            if (activity == null)
                return await new GenericResponse<string>(false, "Invalid Activity.").Serialize();

            activity.Activity = authInfo.ActivityName;

            db.Activities.Update(activity);
            await db.SaveChangesAsync();

            return await new GenericResponse<string>(true, "Activity name updated.").Serialize();

        }

    }
}
