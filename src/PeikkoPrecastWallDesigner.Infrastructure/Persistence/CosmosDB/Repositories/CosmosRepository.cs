using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using PeikkoPrecastWallDesigner.Domain.Entities;
using PeikkoPrecastWallDesigner.Domain.Infrastructure.Persistence.Repositories;
using System.Linq.Expressions;

namespace PeikkoPrecastWallDesigner.Infrastructure.Persistence.CosmosDB.Repositories
{
	public abstract class CosmosRepository<TEntity, Tid> : ICosmosRepository<TEntity, Tid>
		where TEntity : Entity<Tid>
	{
		protected readonly Container _container;
		protected readonly string _partitionKey;

		public CosmosRepository(Container container, string partitionKey)
		{
			_container = container;
			_partitionKey = partitionKey;
		}

		public async Task AddAsync(TEntity entity)
		{
			var idProperty = typeof(TEntity).GetProperty("Id")
				?? throw new InvalidOperationException("The entity does not contain a property named 'Id'.");
			var idValue = idProperty.GetValue(entity);
			if (idValue == null || string.IsNullOrEmpty(idValue.ToString()))
				throw new InvalidOperationException("The 'Id' property must have a valid value.");
			await _container.CreateItemAsync(entity, new PartitionKey(idValue.ToString()));
		}

		public async Task UpdateAsync(TEntity entity)
		{
			var idProperty = typeof(TEntity).GetProperty("Id")
				?? throw new InvalidOperationException("The entity does not contain a property named 'Id'.");
			var idValue = idProperty.GetValue(entity);
			if (idValue == null || string.IsNullOrEmpty(idValue.ToString()))
				throw new InvalidOperationException("The 'Id' property must have a valid value.");
			await _container.UpsertItemAsync(entity, new PartitionKey(idValue.ToString()));
		}

		public async Task PatchAsync(Tid id, Dictionary<string, object> patchOperations)
		{
			if (patchOperations == null || !patchOperations.Any())
			{
				throw new ArgumentException("Patch operations cannot be null or empty.", nameof(patchOperations));
			}
			var patchList = patchOperations.Select(pair => PatchOperation.Replace(pair.Key, pair.Value)).ToList();
			await _container.PatchItemAsync<TEntity>(id.ToString(), new PartitionKey(id.ToString()), patchList);
		}


		public async Task DeleteAsync(Tid id)
		{

			await _container.DeleteItemAsync<TEntity>(id.ToString(), new PartitionKey(id.ToString()));
		}

		public async Task<TEntity?> GetByIdAsync(Tid id)
		{
			try
			{
				var response = await _container.ReadItemAsync<TEntity>(id.ToString(), new PartitionKey(id.ToString()));
				return response.Resource;
			}
			catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
			{
				return null;
			}
		}

		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			var query = _container.GetItemLinqQueryable<TEntity>().Where(predicate).ToFeedIterator();

			var results = new List<TEntity>();
			while (query.HasMoreResults)
			{
				var response = await query.ReadNextAsync();
				results.AddRange(response.Resource);
			}
			return results;
		}
	}
}


