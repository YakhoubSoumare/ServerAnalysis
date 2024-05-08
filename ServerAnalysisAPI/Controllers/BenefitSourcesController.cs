namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BenefitSourcesController : ControllerBase
{
    private readonly IDbService _db;
    public BenefitSourcesController(IDbService db)
    {
        _db = db;
    }

    [HttpGet("{benefitId}/{sourceId}")]
    public async Task<IResult> Get(int benefitId, int sourceId)
    {
        var benefitSource = new object();
        try{
            benefitSource = await _db.SingleJoinEntityAsync<BenefitSource, BenefitSourceDto>(bs => bs.BenefitId == benefitId && bs.SourceId == sourceId);
            if (benefitSource == null) { return Results.NotFound(); }
        } catch { return Results.BadRequest(); }

        return Results.Ok(benefitSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IResult> Add(int benefitId, int sourceId)
    {
        var benefitSource = new object();
        try{
            benefitSource = await _db.AddJoinEntityAsync<BenefitSource>(benefitId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!success) { return Results.BadRequest(); }
        }catch { return Results.BadRequest(); }
        
        return Results.Created("", benefitSource);
    }

	[Authorize(Roles = "Admin")]
	[HttpDelete("{benefitId}/{sourceId}")]
    public async Task<IResult> Delete(int benefitId, int sourceId)
    {
        try{
            var result = await _db.DeleteJoinEntityAsync<BenefitSource>(benefitId, sourceId);
            var success = await _db.SaveChangesAsync();
            if (!result || !success) { return Results.NotFound(); }

        } catch { return Results.BadRequest(); }
        
        return Results.NoContent();
    }
}
