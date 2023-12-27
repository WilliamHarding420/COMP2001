using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace COMP2001.Controllers {

    public struct AuthResponse {
        public string Authorized { get; set; }
        public string Token { get; set; }
    }

    [ApiController]
    [Route("/user/auth")]
    public class AuthController : Controller {

        /// <summary>
        /// Authenticates the details sent to it.
        /// </summary>
        /// <param name="bodyUser">The user to authenticate</param>
        /// <returns>Whethere or not you were verified, as well as your auth token if you were</returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<string> Auth([FromBody] AuthenticationAPI.User bodyUser) {

            Database db = new();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            bool authorized = await AuthenticationAPI.AuthenticateUser(bodyUser.Email, bodyUser.Password);
            string token = "";

            if (authorized) {
                User? dbUser = db.Users.Where(user => user.Email == bodyUser.Email).FirstOrDefault();

                if (dbUser != null)
                    token = AuthManager.instance.AuthorizeUser(dbUser.UserID);
            }

            AuthResponse response = new AuthResponse {
                Authorized = authorized.ToString(),
                Token = token
            };

            return JsonSerializer.Serialize(response); 

        }
    }
}
