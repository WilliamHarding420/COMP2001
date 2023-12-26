using COMP2001.data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace COMP2001.Controllers {

    [ApiController]
    [Route("/activity/new")]
    public class NewActivityController : Controller {

        public struct BodyActivity {
            public string Activity { get; set; }
        }

        [HttpPost]
        public async Task NewActivity() {

            StreamReader reader = new StreamReader(Request.Body);
            string bodyString = await reader.ReadToEndAsync();

            Database db = new();

            JsonSerializerOptions options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            BodyActivity bodyActivity = JsonSerializer.Deserialize<BodyActivity>(bodyString, options);

            ActivityData activity = new ActivityData();
            activity.Activity = bodyActivity.Activity;

            await db.Activities.AddAsync(activity);
            await db.SaveChangesAsync();

        }

    }
}
