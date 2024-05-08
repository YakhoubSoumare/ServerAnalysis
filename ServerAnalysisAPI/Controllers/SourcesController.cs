namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SourcesController : ControllerBase
{
    private readonly IDbService _db;
    public SourcesController(IDbService db) => _db = db;

    [HttpGet]
    public async Task<IResult> Get()
    {
        var sources = new object();
        try
        {
            sources = await _db.GetAsync<Source, SourceDto>();
            if (sources == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(sources);
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var source = new object();
        try
        {
            source = await _db.SingleAsync<Source, SourceDto>(e => e.Id == id);
            if (source == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(source);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IResult> Post([FromBody] SourceDto dto)
    {
        if (!ModelState.IsValid) { // model validation eg. [Required] etc.
                return Results.BadRequest(ModelState);
        }

        Source source;
        try
        {
            source = await _db.AddAsync<Source, SourceDto>(dto);
            var success = await _db.SaveChangesAsync();
            if (!success) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        string uri = _db.GetURI<Source>(source);
        return Results.Created(uri, source);
    }

	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] SourceDto dto)
    {
        if (dto == null || dto.Id != id || dto.Id == 0){
            return Results.BadRequest("ID cannot be null or 0 and must match the ID.");
        }

        try
        {
            var exists = await _db.AnyAsync<Source>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            _db.UpdateAsync<Source, SourceDto>(id, dto);
            
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
            var exists = await _db.AnyAsync<Source>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            var deletion = await _db.DeleteAsync<Source>(id);
            var success = await _db.SaveChangesAsync();
            if (!success || !deletion) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        return Results.NoContent();
    }
}
