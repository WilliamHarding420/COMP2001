using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/user/public/{id}")]
    public class PublicProfileController : Controller {

        public struct PublicUser {

            public string Username { get; set; }
            public string AboutMe { get; set; }
            public string ProfilePictureLink { get; set; }

        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<string> Get([FromRoute] int id) {

            Database db = new();

            User dbUser = db.Users.FirstOrDefault(user => user.UserID == id);

            if (dbUser == null)
                return JsonSerializer.Serialize(new Dictionary<string, string> {
                    { "error", "User ID doesn't exist." }
                });

            PublicUser user = new PublicUser();
            user.Username = dbUser.Username;
            user.AboutMe = dbUser.AboutMe;
            user.ProfilePictureLink = dbUser.ProfilePictureLink;

            return JsonSerializer.Serialize(user);

        }

    }
}
