namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerBasedApplicationsController : ControllerBase
{
    private readonly IDbService _db;
    public ServerBasedApplicationsController(IDbService db) => _db = db;

    [HttpGet]
    public async Task<IResult> Get()
    {
        var serverBasedApplications = new object();
        try
        {
            serverBasedApplications = await _db.GetAsync<ServerBasedApplication, ServerBasedApplicationDto>();
            if (serverBasedApplications == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(serverBasedApplications);
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var serverBasedApplication = new object();
        try
        {
            serverBasedApplication = await _db.SingleAsync<ServerBasedApplication, ServerBasedApplicationDto>(e => e.Id == id);
            if (serverBasedApplication == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(serverBasedApplication);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IResult> Post([FromBody] ServerBasedApplicationDto dto)
    {
        if (!ModelState.IsValid || dto.BenefitId == 0) { // model validation eg. [Required] etc.
                return Results.BadRequest("Title and BenefitId cannot be null or 0");
        }

        ServerBasedApplication serverBasedApplication;
        try
        {
            serverBasedApplication = await _db.AddAsync<ServerBasedApplication, ServerBasedApplicationDto>(dto);
            var success = await _db.SaveChangesAsync();
            if (!success) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        string uri = _db.GetURI<ServerBasedApplication>(serverBasedApplication);
        return Results.Created(uri, serverBasedApplication);
    }

	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] ServerBasedApplicationDto dto)
    {
        if (dto == null || dto.Id != id || dto.Id == 0){
            return Results.BadRequest("ID cannot be null or 0 and must match the ID.");
        }

        try
        {
            var exists = await _db.AnyAsync<ServerBasedApplication>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            _db.UpdateAsync<ServerBasedApplication, ServerBasedApplicationDto>(id, dto);
            
            var success = await _db.SaveChangesAsync();
            if (!success) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        return Results.NoContent();
    }

	[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
    public async Task<IResult> Delete(int id)
    {
        try
        {
            var exists = await _db.AnyAsync<ServerBasedApplication>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            var deletion = await _db.DeleteAsync<ServerBasedApplication>(id);
            var success = await _db.SaveChangesAsync();
            if (!success || !deletion) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        return Results.NoContent();
    }
}