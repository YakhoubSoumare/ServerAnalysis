namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AboutSourcesController : ControllerBase
{
    private readonly IDbService _db;
    public AboutSourcesController(IDbService db)
    {
        _db = db;
    }

    [HttpGet("{aboutId}/{sourceId}")]
    public async Task<IActionResult> Get(int aboutId, int sourceId)
    {
        var aboutSource = new object();
        try{
            aboutSource = await _db.SingleJoinEntityAsync<AboutSource, AboutSourceDto>(ts => ts.AboutId == aboutId && ts.SourceId == sourceId);
            if (aboutSource == null) { return NotFound(); }
        } catch { return BadRequest(); }

        return Ok(aboutSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IActionResult> Add(int aboutId, int sourceId)
    {
        var aboutSource = new object();
        try{
            aboutSource = await _db.AddJoinEntityAsync<AboutSource>(aboutId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!success) { return BadRequest(); }
        }catch { return BadRequest(); }
        
        return Created("", aboutSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpDelete("{aboutId}/{sourceId}")]
    public async Task<IActionResult> Delete(int aboutId, int sourceId)
    {
        try{
            var result = await _db.DeleteJoinEntityAsync<AboutSource>(aboutId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!result || !success) { return NotFound(); }

        } catch { return BadRequest(); }
        
        return NoContent();
    }
}
