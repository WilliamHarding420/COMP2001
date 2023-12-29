using COMP2001.data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace COMP2001.Controllers {

    public struct PrivateInfoUpdate {

        public string Token { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? AboutMe { get; set; }
        public string? Location { get; set; }
        public string? Units { get; set; }
        public string? ActivityTimePreference { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? Birthday { get; set; }
        public string? Language { get; set; }
    }
    [ApiController]
    [Route("/user/update")]
    public class UpdateDetailsController {

        /// <summary>
        /// Endpoint to update your details.
        /// </summary>
        /// <param name="info">Your info to update</param>
        /// <returns>Success / failure</returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<string> UpdateDetails([FromBody] PrivateInfoUpdate info) {

            AuthManager authManager = AuthManager.instance;
            int userID = authManager.GetIDFromToken(info.Token);

            if (userID == -1)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid auth token."));

            Database db = new();

            User? dbUser = db.Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (dbUser == null)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid user."));

            // Due to a trigger, Units must be updated before height and weight
            dbUser.Units = (info.Units == null) ? dbUser.Units : info.Units;
            db.Users.Update(dbUser);
            await db.SaveChangesAsync();
            

            // Now the rest of the details can be updated
            dbUser.Username = (info.Username == null) ? dbUser.Username : info.Username;
            dbUser.Email = (info.Email == null) ? dbUser.Email : info.Email;
            dbUser.AboutMe = (info.AboutMe == null) ? dbUser.AboutMe : info.AboutMe;
            dbUser.Location = (info.Location == null) ? dbUser.Location : info.Location;
            dbUser.ActivityTimePreference = (info.ActivityTimePreference == null) ? dbUser.ActivityTimePreference : info.ActivityTimePreference;
            dbUser.Height = (info.Weight == null) ? dbUser.Height : info.Height;
            dbUser.Weight = (info.Height == null) ? dbUser.Weight : info.Weight;
            dbUser.Birthday = (info.Birthday == null) ? dbUser.Birthday : info.Birthday;
            dbUser.Language = (info.Language == null) ? dbUser.Language : info.Language;
            db.Users.Update(dbUser);
            await db.SaveChangesAsync();

            return JsonSerializer.Serialize(new GenericResponse(true, "Details updated."));

        }

    }
}
