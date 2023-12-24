namespace COMP2001 {
    public class AuthManager {

        private static AuthManager _instance;

        public static AuthManager instance {
            get {
                if (_instance == null)
                    _instance = new AuthManager();

                return _instance;
            }
        }

        private AuthManager() {
            authTokens = new Dictionary<string, TokenInfo>();
        }

    }
}
