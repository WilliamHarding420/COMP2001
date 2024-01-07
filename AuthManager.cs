using COMP2001.data;
using System.Security.Cryptography;

namespace COMP2001 {

    public struct InfoAuth {
        public string Token { get; set; }
    }

    public struct TokenInfo {
        public int UserID { get; set; }
        public long ExpirationTime { get; set; }
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

        public Dictionary<int, string> tokenFromID;
        public Dictionary<string, TokenInfo> authTokens;

        private AuthManager() {
            authTokens = new Dictionary<string, TokenInfo>();
            tokenFromID = new Dictionary<int, string>();
        }

        public string AuthorizeUser(int userID) {

            if (tokenFromID.ContainsKey(userID))
            {
                if (authTokens[tokenFromID[userID]].ExpirationTime > CurrentUnixTimeStamp())
                    return tokenFromID[userID];

                // Remove and regenerate token if expired and user reauthorized
                RemoveToken(tokenFromID[userID]);
            }

            string token = GenerateNewToken();

            TokenInfo tokenInfo = new TokenInfo {
                UserID = userID,
                ExpirationTime = GetNewExpirationTime()
            };

            authTokens.Add(token, tokenInfo);
            tokenFromID.Add(userID, token);

            return token;

        }

        public void RemoveToken(string token) {
            tokenFromID.Remove(authTokens[token].UserID);
            authTokens.Remove(token);
        }

        public int GetIDFromToken(string token) {

            if (!authTokens.ContainsKey(token))
                return -1;

            TokenInfo tokenInfo = authTokens[token];

            if (CurrentUnixTimeStamp() > tokenInfo.ExpirationTime) {
                RemoveToken(token);
                return -1;
            }

            tokenInfo.ExpirationTime = GetNewExpirationTime();
            authTokens[token] = tokenInfo;

            return authTokens[token].UserID;

        }

        public long CurrentUnixTimeStamp() {
            return ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
        }

        public long GetNewExpirationTime() {
            return CurrentUnixTimeStamp() + (15 * MILLISECONDS_PER_MINUTE);
        }

        public bool CheckAdmin(User? user) {

            if (user == null)
                return false;

            return bool.Parse(user.Admin);

        }

        public bool CheckAdmin(int userID) {

            User? user = new Database().Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (user == null)
                return false;

            return bool.Parse(user.Admin);

        }

        public bool CheckAdmin(string token) {

            int userID = GetIDFromToken(token);

            if (userID == -1)
                return false;

            User? user = new Database().Users.Where(user => user.UserID == userID).FirstOrDefault();

            if (user == null)
                return false;

            return bool.Parse(user.Admin);
        }

        private string GenerateNewToken() {

            byte[] randomBytes = new byte[64];
            RandomNumberGenerator.Fill(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");

        }

    }
}
