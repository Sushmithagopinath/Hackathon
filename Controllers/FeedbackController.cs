namespace FeedbackSentimentAnalysis.Controllers
{
    using FeedbackSentimentAnalysis.App.Mgr.FeedbackManager;
    using FeedbackSentimentAnalysis.App.Mgr.IncidentManager;
    using FeedbackSentimentAnalysis.Contracts;
    using Microsoft.AspNetCore.Mvc;


    [Route("api/feedback/")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager feedbackManager;
        private readonly IIncidentManager incidentManager;


        public FeedbackController(IFeedbackManager feedbackManager, IIncidentManager incidentManager)
        {
            this.feedbackManager = feedbackManager;
            this.incidentManager = incidentManager;
        }

        [Route("sentiment/ai-response")]
        [HttpPost]
        public async Task<IList<Feedbacksentiment>> GetFeedbackSetiment([FromBody] List<string> feedbackList, bool includeOpenionMining)
        {
            return await feedbackManager.GetSentimentResultsAsync(feedbackList, includeOpenionMining);
        }

        [Route("sentiment/ai-response/final")]
        [HttpPost]
        public async Task<string> GetAggregateSentiment([FromBody] List<string> feedbackList, bool includeOpenionMining)
        {
            return await feedbackManager.GetAggregateSentiment(feedbackList, includeOpenionMining);
        }

        [Route("sentiment/gen-ai-response")]
        [HttpPost]
        public async Task<SentimentResponse> GetFeedbackSentimentFromGenAI(string incidentNumber)
        {
            IncidentDetails incidentDetails = await incidentManager.GetIncidentComments(incidentNumber);
            if (incidentDetails is null)
            {
                return new SentimentResponse();
            }
            var sentimentResponse = await feedbackManager.GetSentimentFromGenAI(incidentDetails.Result.Comments);
            sentimentResponse.IncidentStatus = incidentDetails.Result.Incident_State;
            return sentimentResponse;
        }
    }
}
