using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using static COMP2001.Controllers.UserFavouriteActivityController;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/user/activity/delete")]
    public class UserRemoveFavouriteActivityController {

        /// <summary>
        /// Endpoint to remove a favourite activity.
        /// </summary>
        /// <param name="activityDelete">Your auth token and activity to remove.</param>
        /// <returns></returns>
        [HttpDelete]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<string> RemoveFavouriteActivity([FromBody] ActivityEdit activityDelete) {

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(activityDelete.Token);

            if (userID == -1)
                return await GenericResponse<string>.InvalidTokenResponse.Serialize();

            Database db = new();

            User? dbUser = db.Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (dbUser == null)
                return await GenericResponse<string>.InvalidUserResponse.Serialize();

            IQueryable<UserActivity> activityFavourited = db.UserFavouriteActivity.Where(userActivity =>
                userActivity.UserID == userID &&
                userActivity.ActivityID == activityDelete.ActivityID);

            if (!activityFavourited.Any())
                return await new GenericResponse<string>(false, "Activity not favourited.").Serialize();

            await activityFavourited.ExecuteDeleteAsync();

            return await new GenericResponse<string>(true, "Favourite activity deleted.").Serialize();
            
        }

    }
}
