using System.ComponentModel;

using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CEo.Pokemon.HomeBalls.Data;

public interface IDbContext :
    IDisposable,
    IAsyncDisposable
{
    DbContextId ContextId { get; }

    IModel Model { get; }

    ChangeTracker ChangeTracker { get; }

    DatabaseFacade Database { get; }

    event EventHandler<SavingChangesEventArgs>? SavingChanges;

    event EventHandler<SavedChangesEventArgs>? SavedChanges;

    event EventHandler<SaveChangesFailedEventArgs>? SaveChangesFailed;

    EntityEntry Add(Object entity);

    EntityEntry<TEntity> Add<TEntity>(TEntity entity) where TEntity : class;

    ValueTask<EntityEntry> AddAsync(Object entity, CancellationToken cancellationToken = default);

    ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

    void AddRange(IEnumerable<Object> entities);

    void AddRange(params Object[] entities);

    Task AddRangeAsync(IEnumerable<Object> entities, CancellationToken cancellationToken = default);

    Task AddRangeAsync(params Object[] entities);

    EntityEntry<TEntity> Attach<TEntity>(TEntity entity) where TEntity : class;

    EntityEntry Attach(Object entity);

    void AttachRange(params Object[] entities);

    void AttachRange(IEnumerable<Object> entities);

    EntityEntry Entry(Object entity);

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

    [EditorBrowsable(EditorBrowsableState.Never)]
    TEntity? Find<TEntity>(params Object?[]? keyValues) where TEntity : class;

    Object? Find(Type entityType, params Object?[]? keyValues);

    ValueTask<TEntity?> FindAsync<TEntity>(Object?[]? keyValues, CancellationToken cancellationToken) where TEntity : class;

    ValueTask<Object?> FindAsync(Type entityType, params Object?[]? keyValues);

    ValueTask<Object?> FindAsync(Type entityType, Object?[]? keyValues, CancellationToken cancellationToken);

    ValueTask<TEntity?> FindAsync<TEntity>(params Object?[]? keyValues) where TEntity : class;

    IQueryable<TResult> FromExpression<TResult>(Expression<Func<IQueryable<TResult>>> expression);

    EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class;

    EntityEntry Remove(Object entity);

    void RemoveRange(params Object[] entities);

    void RemoveRange(IEnumerable<Object> entities);

    Int32 SaveChanges();

    Int32 SaveChanges(Boolean acceptAllChangesOnSuccess);

    Task<Int32> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<Int32> SaveChangesAsync(Boolean acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    DbSet<TEntity> Set<TEntity>(String name) where TEntity : class;

    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    EntityEntry Update(Object entity);

    EntityEntry<TEntity> Update<TEntity>(TEntity entity) where TEntity : class;

    void UpdateRange(params Object[] entities);

    void UpdateRange(IEnumerable<Object> entities);
}