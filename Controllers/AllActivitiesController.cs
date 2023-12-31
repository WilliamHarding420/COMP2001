using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/activities")]
    public class AllActivitiesController {

        /// <summary>
        /// Returns all of the activities in JSON format
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<string> Activities() {
            return await new GenericResponse<ActivityData[]>(true, new Database().Activities.ToArray()).Serialize();
        }

    }
}
