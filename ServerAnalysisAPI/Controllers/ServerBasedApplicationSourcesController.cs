namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerBasedApplicationSourcesController : ControllerBase
{
    private readonly IDbService _db;
    public ServerBasedApplicationSourcesController(IDbService db)
    {
        _db = db;
    }

    [HttpGet("{applicationId}/{sourceId}")]
    public async Task<IResult> Get(int applicationId, int sourceId)
    {
        var applicationSource = new object();
        try{
            applicationSource = await _db.SingleJoinEntityAsync<ServerBasedApplicationSource, ServerBasedApplicationSourceDto>(app => app.ServerBasedApplicationId == applicationId && app.SourceId == sourceId);
            if (applicationSource == null) { return Results.NotFound(); }
        } catch { return Results.BadRequest(); }
    
        return Results.Ok(applicationSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IResult> Add(int applicationId, int sourceId)
    {
        var applicationSource = new object();
        try{
            applicationSource = await _db.AddJoinEntityAsync<ServerBasedApplicationSource>(applicationId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!success) { return Results.BadRequest(); }
        }catch { return Results.BadRequest(); }
        
        return Results.Created("", applicationSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpDelete("{applicationId}/{sourceId}")]
    public async Task<IResult> Delete(int applicationId, int sourceId)
    {
        try{
            var result = await _db.DeleteJoinEntityAsync<ServerBasedApplicationSource>(applicationId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!result || !success) { return Results.NotFound(); }
    
        } catch { return Results.BadRequest(); }
        
        return Results.NoContent();
    }

}
