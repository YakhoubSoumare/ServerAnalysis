namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IDbService _db;
    public ImagesController(IDbService db) => _db = db;

    [HttpGet]
    public async Task<IResult> Get()
    {
        var Images = new object();
        try
        {
            Images = await _db.GetAsync<Image, ImageDto>();
            if (Images == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(Images);
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var Images = new object();
        try
        {
            Images = await _db.SingleAsync<Image, ImageDto>(e => e.Id == id);
            if (Images == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(Images);
    }
}
