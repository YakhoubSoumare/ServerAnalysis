namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TopicsController : ControllerBase
{
    private readonly IDbService _db;
    public TopicsController(IDbService db)
    {
        _db = db;
    }

	[HttpGet]
    public async Task<IResult> Get()
    {
        var topics = new object();
        try
        {
            topics = await _db.GetAsync<Topic, TopicDto>();
            if (topics == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(topics);
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var topic = new object();
        try
        {
            topic = await _db.SingleAsync<Topic, TopicDto>(e => e.Id == id);
            if (topic == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(topic);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IResult> Post([FromBody] TopicDto dto)
    {
        if (!ModelState.IsValid) { // model validation eg. [Required] etc.
                return Results.BadRequest(ModelState);
        }

        Topic topic;
        try
        {
            topic = await _db.AddAsync<Topic, TopicDto>(dto);
            var success = await _db.SaveChangesAsync();
            if (!success) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        string uri = _db.GetURI<Topic>(topic);
        return Results.Created(uri, topic);
    }

	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] TopicDto dto)
    {
        if (dto == null || dto.Id != id || dto.Id == 0){
            return Results.BadRequest("ID cannot be null or 0 and must match the ID.");
        }

        try
        {
            var exists = await _db.AnyAsync<Topic>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            _db.UpdateAsync<Topic, TopicDto>(id, dto);
            
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
            var exists = await _db.AnyAsync<Topic>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            var deletion = await _db.DeleteAsync<Topic>(id);
            var success = await _db.SaveChangesAsync();
            if (!success || !deletion) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        return Results.NoContent();
    }
}
