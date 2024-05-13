namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicSourcesController : ControllerBase
{
    private readonly IDbService _db;
    public TopicSourcesController(IDbService db)
    {
        _db = db;
    }

    [HttpGet("{topicId}/{sourceId}")]
    public async Task<IResult> Get(int topicId, int sourceId)
    {
        var topicSource = new object();
        try{
            topicSource = await _db.SingleJoinEntityAsync<TopicSource, TopicSourceDto>(ts => ts.TopicId == topicId && ts.SourceId == sourceId);
            if (topicSource == null) { return Results.NotFound(); }
        } catch { return Results.BadRequest(); }

        return Results.Ok(topicSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IResult> Add(int topicId, int sourceId)
    {
        var topicSource = new object();
        try{
            topicSource = await _db.AddJoinEntityAsync<TopicSource>(topicId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!success) { return Results.BadRequest(); }
        }catch { return Results.BadRequest(); }
        
        return Results.Created("", topicSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpDelete("{topicId}/{sourceId}")]
    public async Task<IResult> Delete(int topicId, int sourceId)
    {
        try{
            var result = await _db.DeleteJoinEntityAsync<TopicSource>(topicId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!result || !success) { return Results.NotFound(); }

        } catch { return Results.BadRequest(); }
        
        return Results.NoContent();
    }
}
