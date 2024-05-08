namespace ServerAnalysisAPI.Interfaces;

public interface IDbService
{
    Task<List<TDto>> GetAsync<TEntity, TDto>() 
    where TEntity : class, IEntity
    where TDto : class;

    Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>>
    expression) 
    where TEntity : class, IEntity 
    where TDto : class;

    Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> expression)
    where TEntity : class, IEntity;

    Task<bool> SaveChangesAsync();

    Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) 
    where TEntity : class, IEntity
    where TDto : class;

    void UpdateAsync<TEntity, TDto>(int id, TDto dto) 
    where TEntity : class, IEntity
    where TDto : class;

    Task<bool> DeleteAsync<TEntity>(int id) 
    where TEntity : class, IEntity;

    Task<TJoinEntityDto> SingleJoinEntityAsync<TJoinEntity, TJoinEntityDto>(Expression<Func<TJoinEntity, bool>>
    expression) 
    where TJoinEntity : class, IReferenceEntity 
    where TJoinEntityDto : class;
    
    Task<TJoinEntity> AddJoinEntityAsync<TJoinEntity>(int entity1Id, int entity2Id) 
    where TJoinEntity : class, IReferenceEntity, new();
    
    Task<bool> DeleteJoinEntityAsync<TJoinEntity>(int entity1Id, int entity2Id) 
    where TJoinEntity : class, IReferenceEntity;

    string GetURI<TEntity>(TEntity entity) where TEntity : class, IEntity;
}