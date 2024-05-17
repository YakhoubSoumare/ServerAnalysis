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
    public async Task<IActionResult> Get(int topicId, int sourceId)
    {
        var topicSource = new object();
        try{
            topicSource = await _db.SingleJoinEntityAsync<TopicSource, TopicSourceDto>(ts => ts.TopicId == topicId && ts.SourceId == sourceId);
            if (topicSource == null) { return NotFound(); }
        } catch { return BadRequest(); }

        return Ok(topicSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IActionResult> Add(int topicId, int sourceId)
    {
        var topicSource = new object();
        try{
            topicSource = await _db.AddJoinEntityAsync<TopicSource>(topicId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!success) { return BadRequest(); }
        }catch { return BadRequest(); }
        
        return Created("", topicSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpDelete("{topicId}/{sourceId}")]
    public async Task<IActionResult> Delete(int topicId, int sourceId)
    {
        try{
            var result = await _db.DeleteJoinEntityAsync<TopicSource>(topicId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!result || !success) { return NotFound(); }

        } catch { return BadRequest(); }
        
        return NoContent();
    }
}
