using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/user/delete/{id}")]
    public class DeleteUserController {

        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<string> DeleteUser([FromRoute] int id, [FromBody] InfoAuth auth) {

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(auth.Token);

            if (userID == -1)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid auth token."));

            Database db = new();

            User? dbUser = db.Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (dbUser == null)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid user."));


            bool admin = authManager.CheckAdmin(dbUser);

            if (!admin)
                return JsonSerializer.Serialize(new GenericResponse(false, "Unauthorized."));

            /*
             * 
             * Here the user would be sent off to be archived by another microservice / program.
             * 
             */

            await db.Users.Where(user => user.UserID == id).ExecuteDeleteAsync();

            return JsonSerializer.Serialize(new GenericResponse(true, $"User ID {id} deleted successfully."));

        }

    }
}
