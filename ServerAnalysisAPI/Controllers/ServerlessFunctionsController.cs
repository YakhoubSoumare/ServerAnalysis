namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerlessFunctionsController : ControllerBase
{
    private readonly IDbService _db;
    public ServerlessFunctionsController(IDbService db) => _db = db;

    [HttpGet]
    public async Task<IResult> Get()
    {
        var serverlessFunctions = new object();
        try
        {
            serverlessFunctions = await _db.GetAsync<ServerlessFunction, ServerlessFunctionDto>();
            if (serverlessFunctions == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(serverlessFunctions);
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var serverlessFunction = new object();
        try
        {
            serverlessFunction = await _db.SingleAsync<ServerlessFunction, ServerlessFunctionDto>(e => e.Id == id);
            if (serverlessFunction == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(serverlessFunction);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IResult> Post([FromBody] ServerlessFunctionDto dto)
    {
        if (!ModelState.IsValid || dto.BenefitId == 0) { // model validation eg. [Required] etc.
                return Results.BadRequest("Title and BenefitId cannot be null or 0");
        }

        ServerlessFunction serverlessFunction;
        try
        {
            serverlessFunction = await _db.AddAsync<ServerlessFunction, ServerlessFunctionDto>(dto);
            var success = await _db.SaveChangesAsync();
            if (!success) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        string uri = _db.GetURI<ServerlessFunction>(serverlessFunction);
        return Results.Created(uri, serverlessFunction);
    }

	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] ServerlessFunctionDto dto)
    {
        if (dto == null || dto.Id != id || dto.Id == 0){
            return Results.BadRequest("ID cannot be null or 0 and must match the ID.");
        }

        try
        {
            var exists = await _db.AnyAsync<ServerlessFunction>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            _db.UpdateAsync<ServerlessFunction, ServerlessFunctionDto>(id, dto);
            
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
            var exists = await _db.AnyAsync<ServerlessFunction>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            var deletion = await _db.DeleteAsync<ServerlessFunction>(id);
            var success = await _db.SaveChangesAsync();
            if (!success || !deletion) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        return Results.NoContent();
    }
}