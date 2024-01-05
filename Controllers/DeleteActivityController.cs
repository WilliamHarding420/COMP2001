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

        /// <summary>
        /// This deletes an activity with the given ID
        /// </summary>
        /// <param name="bodyActivity">Your token and the Activity's ID</param>
        /// <returns></returns>
        [HttpDelete]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<string> DeleteActivity([FromBody] ActivityDelete bodyActivity) {

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(bodyActivity.Token);

            if (userID == -1)
                return await GenericResponse<string>.InvalidTokenResponse.Serialize();

            bool checkAdmin = authManager.CheckAdmin(userID);

            if (!checkAdmin)
                return await GenericResponse<string>.UnauthorizedResponse.Serialize();

            Database db = new();

            IQueryable<ActivityData> activityData = db.Activities.Where(activity => activity.ActivityID == bodyActivity.ActivityID);

            if (!activityData.Any())
                return await GenericResponse<string>.InvalidActivityResponse.Serialize();

            await activityData.ExecuteDeleteAsync();

            return await new GenericResponse<string>(true, "Activity Deleted.").Serialize();

        }

    }
}
