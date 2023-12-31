using System.Text.Json;

namespace COMP2001.data {
    public class GenericResponse<T> {

        public bool Success { get; set; }

        public T Message { get; set; }

        public GenericResponse(bool _Success, T _Message) {

            Success = _Success;
            Message = _Message;

        }

        public async Task<string> Serialize() {
            return JsonSerializer.Serialize(this);
        }

        public static GenericResponse<string> UnauthorizedResponse = new GenericResponse<string>(false, "Unauthorized.");
        public static GenericResponse<string> InvalidUserResponse = new GenericResponse<string>(false, "Invalid User.");
        public static GenericResponse<string> InvalidTokenResponse = new GenericResponse<string>(false, "Invalid Auth Token.");

    }
}
