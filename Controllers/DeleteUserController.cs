using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/user/delete/{id}")]
    public class DeleteUserController {

        [HttpPost]
        /// <summary>
        /// Deletes the user with the given ID
        /// </summary>
        /// <param name="id">The user's ID</param>
        /// <param name="auth">Your authorization</param>
        /// <returns></returns>
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<string> DeleteUser([FromRoute] int id, [FromBody] InfoAuth auth) {

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(auth.Token);

            if (userID == -1)
                return await GenericResponse<string>.InvalidTokenResponse.Serialize();

            Database db = new();

            User? dbUser = db.Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (dbUser == null)
                return await GenericResponse<string>.InvalidUserResponse.Serialize();

            bool admin = authManager.CheckAdmin(dbUser);

            if (!admin)
                return await GenericResponse<string>.UnauthorizedResponse.Serialize();

            /*
             * 
             * Here the user would be sent off to be archived by another microservice / program.
             * 
             */

            await db.Users.Where(user => user.UserID == id).ExecuteDeleteAsync();

            return await new GenericResponse<string>(true, $"User ID {id} deleted successfully.").Serialize();

        }

    }
}
