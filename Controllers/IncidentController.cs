namespace FeedbackSentimentAnalysis.Api.Controllers
{
    using FeedbackSentimentAnalysis.App.Mgr.IncidentManager;
    using FeedbackSentimentAnalysis.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;


    [Route("api/incident")]
    [ApiController]
    public class IncidentController : ControllerBase
    {

        private readonly IIncidentManager incidentManager;

        public IncidentController(IIncidentManager incidentManager)
        {
            this.incidentManager = incidentManager;
        }

        [Route("system/details")]
        [HttpGet]
        public async Task<Incident> GetIncidentSystemDetails(string  incidentNumber)
        {
           return await incidentManager.GetIncidentSysId(incidentNumber);
        }


        [Route("system/work-notes")]
        [HttpGet]
        public async Task<IncidentDetails> GetIncidentDetailsAsync(string incidentNumber)
        {
            return await incidentManager.GetIncidentComments(incidentNumber);
        }

    }
}
