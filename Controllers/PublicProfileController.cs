using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/user/public/{id}")]
    public class PublicProfileController : Controller {

        public struct PublicUser(string username, string? aboutMe, string? profilePictureLink) {

            public string Username { get; set; } = username;
            public string? AboutMe { get; set; } = aboutMe;
            public string? ProfilePictureLink { get; set; } = profilePictureLink;

        }

        /// <summary>
        /// Gets a user's public information
        /// </summary>
        /// <param name="id">The ID of the user to retrieve</param>
        /// <returns>The user's public information</returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<string> Get([FromRoute] int id) {

            Database db = new();

            User? dbUser = db.Users.FirstOrDefault(user => user.UserID == id);

            if (dbUser == null)
                return await GenericResponse<string>.InvalidUserResponse.Serialize(); ;

            return await new GenericResponse<PublicUser>(true, new PublicUser(dbUser.Username, dbUser.AboutMe, dbUser.ProfilePictureLink)).Serialize();

        }

    }
}
