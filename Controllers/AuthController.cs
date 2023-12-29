using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace COMP2001.Controllers {

    public struct AuthResponse(bool authorized, string token) {
        public bool Authorized { get; set; } = authorized;
        public string Token { get; set; } = token;
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

            bool authorized = await AuthenticationAPI.AuthenticateUser(bodyUser.Email, bodyUser.Password);

            if (!authorized)
                return JsonSerializer.Serialize(new GenericResponse(false, "Unauthorized."));    
            

            User? dbUser = db.Users.Where(user => user.Email == bodyUser.Email).FirstOrDefault();

            if (dbUser == null)
                return JsonSerializer.Serialize(new GenericResponse(false, "Invalid user."));

            string token = AuthManager.instance.AuthorizeUser(dbUser.UserID);

            return JsonSerializer.Serialize(new AuthResponse(true, token)); 

        }
    }
}
