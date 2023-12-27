using COMP2001.data;
using Microsoft.AspNetCore.Mvc;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/user/new")]
    public class NewProfileController : Controller {

        public struct BodyUser {
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task NewUser([FromBody] BodyUser bodyUser) {

            Database db = new();

            User? existingEmail = db.Users.Where(user => user.Email == bodyUser.Email).FirstOrDefault();

            if (existingEmail != null)
                return;

            User user = new User();

            // Parameters from request body
            user.Username = bodyUser.Username;
            user.Email = bodyUser.Email;
            user.Password = bodyUser.Password;

            // Other default user details
            user.Units = "Metric";
            user.ActivityTimePreference = "Pace";
            user.Language = "English";
            user.Admin = "false";



            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();

        }

    }
}
