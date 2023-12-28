using System.Security.Cryptography;

namespace COMP2001 {

    public struct InfoAuth {
        public string Token { get; set; }
    }

    public class AuthManager {

        public static int MILLISECONDS_PER_MINUTE = 1000 * 60; 

        private static AuthManager _instance;

        public static AuthManager instance {
            get {
                if (_instance == null)
                    _instance = new AuthManager();

                return _instance;
            }
        }

        public struct TokenInfo {
            public int UserID { get; set; }
            public long ExpirationTime { get; set; }
        }

        private Dictionary<int, string> tokenFromID;
        private Dictionary<string, TokenInfo> authTokens;

        private AuthManager() {
            authTokens = new Dictionary<string, TokenInfo>();
            tokenFromID = new Dictionary<int, string>();
        }

        public string AuthorizeUser(int userID) {

            if (tokenFromID.ContainsKey(userID)) 
                return tokenFromID[userID];

            string token = GenerateNewToken();

            TokenInfo tokenInfo = new TokenInfo {
                UserID = userID,
                ExpirationTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds() + (5 * MILLISECONDS_PER_MINUTE)
            };

            authTokens.Add(token, tokenInfo);
            tokenFromID.Add(userID, token);

            return token;

        }

        public int GetIDFromToken(string token) {

            if (!authTokens.ContainsKey(token))
                return -1;

            return authTokens[token].UserID;

        }

        private string GenerateNewToken() {

            byte[] randomBytes = new byte[64];
            RandomNumberGenerator.Fill(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");

        }

    }
}
