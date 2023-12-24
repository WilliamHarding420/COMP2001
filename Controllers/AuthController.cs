using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    public struct AuthResponse {
        public string Authorized { get; set; }
        public string Token { get; set; }
    }

    [ApiController]
    [Route("/user/auth")]
    public class AuthController : Controller {

        [HttpPost]
        public async Task<string> Auth() {

            StreamReader reader = new StreamReader(Request.Body);
            string bodyString = await reader.ReadToEndAsync();

            AuthenticationAPI.User bodyUser = JsonSerializer.Deserialize<AuthenticationAPI.User>(bodyString);

            bool authorized = await AuthenticationAPI.AuthenticateUser(bodyUser.Email, bodyUser.Password);
            string token = "";

            if (authorized)
                token = AuthManager.instance.AuthorizeUser(0);

            AuthResponse response = new AuthResponse {
                Authorized = authorized.ToString(),
                Token = token
            };

            return JsonSerializer.Serialize(response); 

        }
    }
}
