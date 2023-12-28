using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/user/private")]
    public class GetPrivateInfoController : Controller {

        public struct PrivateInfo(User user) {
            public string Username { get; set; } = user.Username;
            public string Email { get; set; } = user.Email;
            public string? AboutMe { get; set; } = user.AboutMe;
            public string? Location { get; set; } = user.Location;
            public string Units { get; set; } = user.Units;
            public string ActivityTimePreference { get; set; } = user.ActivityTimePreference;
            public int? Height { get; set; } = user.Height;
            public int? Weight { get; set; } = user.Weight;
            public string? Birthday { get; set; } = user.Birthday;
            public string? ProfilePictureLink { get; set; } = user.ProfilePictureLink;
            public string Language { get; set; } = user.Language;
        }

        /// <summary>
        /// Endpoint to get a user's private information.
        /// </summary>
        /// <param name="authorization">Your authentication token.</param>
        /// <returns>Your private details.</returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<string> GetPrivateInfo([FromBody] InfoAuth authorization) {

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(authorization.Token);

            if (userID == -1)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid auth token."));

            Database db = new();

            User? dbUser = db.Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (dbUser == null)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid user."));

            return JsonSerializer.Serialize(new PrivateInfo(dbUser));

        }

    }
}
