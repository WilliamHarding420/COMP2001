using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/user/new")]
    public class NewProfileController : Controller {

        public struct BodyUser {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        /// <summary>
        /// Adds a new user to the database
        /// </summary>
        /// <param name="bodyUser">The user to add to the database</param>
        /// <returns></returns>
        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<string> NewUser([FromBody] BodyUser bodyUser) {

            if (bodyUser.Username.Trim().Equals("") || bodyUser.Email.Trim().Equals("") || bodyUser.Password.Trim().Equals(""))
                return await new GenericResponse<string>(false, "No arguments can be empty.").Serialize();

                Database db = new();

            User? existingEmail = db.Users.Where(user => user.Email == bodyUser.Email).FirstOrDefault();

            if (existingEmail != null)
                return await new GenericResponse<string>(false, "Email Already Registered.").Serialize();

            User user = new User();

            // Parameters from request body
            user.Username = bodyUser.Username;
            user.Email = bodyUser.Email;
            user.Password = bodyUser.Password;

            // Other default user details
            user.Units = "Metric";
            user.ActivityTimePreference = "Pace";
            user.Language = "English (UK)";
            user.Admin = "false";

            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

            return await new GenericResponse<string>(true, "User successfully created.").Serialize();

        }

    }
}
