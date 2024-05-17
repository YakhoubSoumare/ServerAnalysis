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
    public async Task<IActionResult> Get()
    {
        var topics = new object();
        try
        {
            topics = await _db.GetAsync<Topic, TopicDto>();
            if (topics == null) {return NotFound();}  
        }
        catch { return BadRequest(); }

        return Ok(topics);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var topic = new object();
        try
        {
            topic = await _db.SingleAsync<Topic, TopicDto>(e => e.Id == id);
            if (topic == null) {return NotFound();}  
        }
        catch { return BadRequest(); }

        return Ok(topic);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IActionResult> Post([FromBody] TopicDto dto)
    {
        if (!ModelState.IsValid) { // model validation eg. [Required] etc.
                return BadRequest(ModelState);
        }

        Topic topic;
        try
        {
            topic = await _db.AddAsync<Topic, TopicDto>(dto);
            var success = await _db.SaveChangesAsync();
            if (!success) {return BadRequest();}  
        }
        catch { return BadRequest(); }

        string uri = _db.GetURI<Topic>(topic);
        return Created(uri, topic);
    }

	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] TopicDto dto)
    {
        if (dto == null || dto.Id != id || dto.Id == 0){
            return BadRequest("ID cannot be null or 0 and must match the ID.");
        }

        try
        {
            var exists = await _db.AnyAsync<Topic>(e => e.Id == id);
            if (!exists) {return NotFound();}

            _db.UpdateAsync<Topic, TopicDto>(id, dto);
            
            var success = await _db.SaveChangesAsync();
            if (!success) {return BadRequest();}  
        }
        catch { return BadRequest(); }

        return NoContent();
    }

	[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var exists = await _db.AnyAsync<Topic>(e => e.Id == id);
            if (!exists) {return NotFound();}

            var deletion = await _db.DeleteAsync<Topic>(id);
            var success = await _db.SaveChangesAsync();
            if (!success || !deletion) {return BadRequest();}  
        }
        catch { return BadRequest(); }

        return NoContent();
    }
}
