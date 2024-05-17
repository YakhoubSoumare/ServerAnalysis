namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IDbService _db;
    public ImagesController(IDbService db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var Images = new object();
        try
        {
            Images = await _db.GetAsync<Image, ImageDto>();
            if (Images == null) {return NotFound();}  
        }
        catch { return BadRequest(); }

        return Ok(Images);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var Images = new object();
        try
        {
            Images = await _db.SingleAsync<Image, ImageDto>(e => e.Id == id);
            if (Images == null) {return NotFound();}  
        }
        catch { return BadRequest(); }

        return Ok(Images);
    }
}
