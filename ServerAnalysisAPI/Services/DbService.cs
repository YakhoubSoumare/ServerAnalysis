
namespace ServerAnalysisAPI.Services;

public class DbService: IDbService
{
    private readonly DataContext _db;
    private readonly IMapper _mapper;

    public DbService(DataContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    // _db.Set<TEntity>() is used to interact with the database in the context of the current TEntity type.

    // returns an empty list if no entities are found

    #region IEntities
    public async Task<List<TDto>> GetAsync<TEntity, TDto>()
        where TEntity : class, IEntity
        where TDto : class
    {
        #nullable disable
        var entities = await _db.Set<TEntity>().ToListAsync();
        var mappedEntities = _mapper.Map<List<TDto>>(entities);
        return mappedEntities;
        #nullable enable
    }

    public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
        where TDto : class
    {
        #nullable disable
        var entity = await SingleAsyncHelper(expression);
        return _mapper.Map<TDto>(entity);
        #nullable enable
    }

    public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
        where TEntity : class, IEntity
    {
        bool entityExists = await _db.Set<TEntity>().AnyAsync(expression);
        return entityExists;
    }

    public async Task<bool> SaveChangesAsync()
    {
        int changesSaved = await _db.SaveChangesAsync();
        return changesSaved > 0;
    }

    public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto)
        where TEntity : class, IEntity
        where TDto : class
    {
        #nullable disable
        var entity = _mapper.Map<TEntity>(dto);
        await _db.Set<TEntity>().AddAsync(entity);
        return entity;
        #nullable enable
    }

    public void UpdateAsync<TEntity, TDto>(int id, TDto dto)
        where TEntity : class, IEntity
        where TDto : class
    {
        #nullable disable
        var entity = _mapper.Map<TEntity>(dto);
        entity.Id = id;
        _db.Set<TEntity>().Update(entity);
        #nullable enable
    }

    public async Task<bool> DeleteAsync<TEntity>(int id)
        where TEntity : class, IEntity
    {
        #nullable disable
        var entity = await SingleAsyncHelper<TEntity>(e => e.Id == id);
        if (entity == null) return false;
        _db.Set<TEntity>().Remove(entity);
        #nullable enable
        return true;
    }

    public string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity
    {
        string URI = string.Empty;
        var node = typeof(TEntity).Name.ToLower();
        if (node.EndsWith("y"))
        {
            URI = $"{node.Remove(node.Length - 1, 1) + "ie"}s/{entity.Id}";
        }
        else
        {
            URI = $"{node}s/{entity.Id}";
        }
        return URI;
    }
    #endregion


    #region IReferenceEntities
    public async Task<TJoinEntityDto> SingleJoinEntityAsync<TJoinEntity, TJoinEntityDto>(Expression<Func<TJoinEntity, bool>> expression) 
        where TJoinEntity : class, IReferenceEntity 
        where TJoinEntityDto : class
    {
        #nullable disable
        var entity = await SingleJoinEntityAsyncHelper(expression);
        var dto = _mapper.Map<TJoinEntityDto>(entity); // Assuming you have a mapper to convert Entity to DTO
        return dto;
        #nullable enable
    }
    
    public async Task<TJoinEntity> AddJoinEntityAsync<TJoinEntity>(int entity1Id, int entity2Id) 
        where TJoinEntity : class, IReferenceEntity, new()
    {
        #nullable disable
        var entity = new TJoinEntity();
        entity.GetType().GetProperty("Entity1Id").SetValue(entity, entity1Id);
        entity.GetType().GetProperty("Entity2Id").SetValue(entity, entity2Id);
        await _db.Set<TJoinEntity>().AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
        #nullable enable
    }
    
    public async Task<bool> DeleteJoinEntityAsync<TJoinEntity>(int entity1Id, int entity2Id) 
        where TJoinEntity : class, IReferenceEntity
    {
        #nullable disable
        var entity = await _db.Set<TJoinEntity>()
            .SingleOrDefaultAsync(e => (int)e.GetType().GetProperty("Entity1Id").GetValue(e) == entity1Id 
                                   && (int)e.GetType().GetProperty("Entity2Id").GetValue(e) == entity2Id);
        if (entity == null) return false;
        _db.Set<TJoinEntity>().Remove(entity);
        #nullable enable
        return true;
    }
    #endregion

    #region Helper Functions to avoid DRY
    private async Task<TEntity> SingleAsyncHelper<TEntity>(Expression<Func<TEntity, bool>> expression)
    where TEntity : class
    {
        #nullable disable
        return await _db.Set<TEntity>().SingleOrDefaultAsync(expression);
        #nullable enable
    }

    private async Task<TJoinEntity> SingleJoinEntityAsyncHelper<TJoinEntity>(Expression<Func<TJoinEntity, bool>> expression)
        where TJoinEntity : class, IReferenceEntity
    {
        #nullable disable
        return await _db.Set<TJoinEntity>().SingleOrDefaultAsync(expression);
        #nullable enable
    }
    #endregion
}