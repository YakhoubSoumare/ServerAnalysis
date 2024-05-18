namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AboutsController : ControllerBase
{
    private readonly IDbService _db;
    public AboutsController(IDbService db)
    {
        _db = db;
    }

	[HttpGet]
    public async Task<IActionResult> Get()
    {
        var abouts = new object();
        try
        {
            abouts = await _db.GetAsync<About, AboutDto>();
            if (abouts == null) {return NotFound();}  
        }
        catch { return BadRequest(); }

        return Ok(abouts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var about = new object();
        try
        {
            about = await _db.SingleAsync<About, AboutDto>(e => e.Id == id);
            if (about == null) {return NotFound();}  
        }
        catch { return BadRequest(); }

        return Ok(about);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IActionResult> Post([FromBody] AboutDto dto)
    {
        if (!ModelState.IsValid) { // model validation eg. [Required] etc.
                return BadRequest(ModelState);
        }

        About about;
        try
        {
            about = await _db.AddAsync<About, AboutDto>(dto);
            var success = await _db.SaveChangesAsync();
            if (!success) {return BadRequest();}  
        }
        catch { return BadRequest(); }

        string uri = _db.GetURI<About>(about);
        return Created(uri, about);
    }

	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AboutDto dto)
    {
        if (dto == null || dto.Id != id || dto.Id == 0){
            return BadRequest("ID cannot be null or 0 and must match the ID.");
        }

        try
        {
            var exists = await _db.AnyAsync<About>(e => e.Id == id);
            if (!exists) {return NotFound();}

            _db.UpdateAsync<About, AboutDto>(id, dto);
            
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
            var exists = await _db.AnyAsync<About>(e => e.Id == id);
            if (!exists) {return NotFound();}

            var deletion = await _db.DeleteAsync<About>(id);
            var success = await _db.SaveChangesAsync();
            if (!success || !deletion) {return BadRequest();}  
        }
        catch { return BadRequest(); }

        return NoContent();
    }
}
