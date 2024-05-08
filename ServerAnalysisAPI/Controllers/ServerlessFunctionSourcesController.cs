namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerlessFunctionSourcesController : ControllerBase
{
    private readonly IDbService _db;
    public ServerlessFunctionSourcesController(IDbService db)
    {
        _db = db;
    }

    [HttpGet("{functionId}/{sourceId}")]
    public async Task<IResult> Get(int functionId, int sourceId)
    {
        var functionSource = new object();
        try{
            functionSource = await _db.SingleJoinEntityAsync<ServerlessFunctionSource, ServerlessFunctionSourceDto>(fs => fs.ServerlessFunctionId == functionId && fs.SourceId == sourceId);
            if (functionSource == null) { return Results.NotFound(); }
        } catch { return Results.BadRequest(); }
    
        return Results.Ok(functionSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IResult> Add(int functionId, int sourceId)
    {
        var functionSource = new object();
        try{
            functionSource = await _db.AddJoinEntityAsync<ServerlessFunctionSource>(functionId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!success) { return Results.BadRequest(); }
        }catch { return Results.BadRequest(); }
        
        return Results.Created("", functionSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpDelete("{functionId}/{sourceId}")]
    public async Task<IResult> Delete(int functionId, int sourceId)
    {
        try{
            var result = await _db.DeleteJoinEntityAsync<ServerlessFunctionSource>(functionId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!result || !success) { return Results.NotFound(); }
    
        } catch { return Results.BadRequest(); }
        
        return Results.NoContent();
    }
}
