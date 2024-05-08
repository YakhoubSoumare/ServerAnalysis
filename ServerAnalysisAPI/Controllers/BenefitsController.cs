using System.Data;

namespace ServerAnalysisAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BenefitsController : ControllerBase
{
    private readonly IDbService _db;
    public BenefitsController(IDbService db) => _db = db;

    [HttpGet]
    public async Task<IResult> Get()
    {
        var benefits = new object();
        try
        {
            benefits = await _db.GetAsync<Benefit, BenefitDto>();
            if (benefits == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(benefits);
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var benefit = new object();
        try
        {
            benefit = await _db.SingleAsync<Benefit, BenefitDto>(e => e.Id == id);
            if (benefit == null) {return Results.NotFound();}  
        }
        catch { return Results.BadRequest(); }

        return Results.Ok(benefit);
    }

	[Authorize(Roles = "Admin")]
	[HttpPost]
    public async Task<IResult> Post([FromBody] BenefitDto dto)
    {
        if (!ModelState.IsValid || dto.TopicId == 0) { // model validation eg. [Required] etc.
                return Results.BadRequest("Title and TopicId cannot be null or 0");
        }

        Benefit benefit;
        try
        {
            benefit = await _db.AddAsync<Benefit, BenefitDto>(dto);
            var success = await _db.SaveChangesAsync();
            if (!success) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        string uri = _db.GetURI<Benefit>(benefit);
        return Results.Created(uri, benefit);
    }

	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
    public async Task<IResult> Put(int id, [FromBody] BenefitDto dto)
    {
        if (dto == null || dto.Id != id || dto.Id == 0){
            return Results.BadRequest("ID cannot be null or 0 and must match the ID.");
        }

        try
        {
            var exists = await _db.AnyAsync<Benefit>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            _db.UpdateAsync<Benefit, BenefitDto>(id, dto);
            
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
            var exists = await _db.AnyAsync<Benefit>(e => e.Id == id);
            if (!exists) {return Results.NotFound();}

            var deletion = await _db.DeleteAsync<Benefit>(id);
            var success = await _db.SaveChangesAsync();
            if (!success || !deletion) {return Results.BadRequest();}  
        }
        catch { return Results.BadRequest(); }

        return Results.NoContent();
    }
}
