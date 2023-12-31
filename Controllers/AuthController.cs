using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace COMP2001.Controllers {

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
                return await GenericResponse<string>.UnauthorizedResponse.Serialize();  
            

            User? dbUser = db.Users.Where(user => user.Email == bodyUser.Email).FirstOrDefault();

            if (dbUser == null)
                return await GenericResponse<string>.InvalidUserResponse.Serialize();

            string token = AuthManager.instance.AuthorizeUser(dbUser.UserID);

            return await new GenericResponse<string>(true, token).Serialize(); 

        }
    }
}
