using System.Net.Http.Headers;
using System.Text.Json;

namespace COMP2001 {
    public class AuthenticationAPI {

        public struct User {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public static async Task<bool> AuthenticateUser(string _email, string _password) {

            using (HttpClient client = new HttpClient()) {

                User user = new User {
                    Email = _email,
                    Password = _password
                };

                string url = "https://web.socem.plymouth.ac.uk/COMP2001/auth/api/users";

                HttpRequestMessage request = new HttpRequestMessage();
                request.Content = new StringContent(JsonSerializer.Serialize<User>(user), new MediaTypeHeaderValue("application/json"));
                request.RequestUri = new Uri(url);
                request.Method = HttpMethod.Post;

                HttpResponseMessage response = await client.SendAsync(request);
                string responseText = await response.Content.ReadAsStringAsync();

                string[]? jsonArray = JsonSerializer.Deserialize<string[]>(responseText);

                if (jsonArray == null)
                    return false;

                return bool.Parse(jsonArray[1].ToLower());

            }

        }

    }
}
