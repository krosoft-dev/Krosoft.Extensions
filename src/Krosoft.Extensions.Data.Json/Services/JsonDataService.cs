using FluentValidation;
using FluentValidation.Results;
using JsonFlatFileDataStore;
using Krosoft.Extensions.Core.Legacy.Models;
using Krosoft.Extensions.Core.Models.Exceptions;
using Krosoft.Extensions.Data.Json.Interfaces;
using Microsoft.Extensions.Options;

namespace Krosoft.Extensions.Data.Json.Services;

internal class JsonDataService<T> : IJsonDataService<T> where T : class
{
    private readonly JsonDataSettings _jsonDataSettings;
    private readonly IEnumerable<IValidator<T>> _validators;

    public JsonDataService(IOptions<JsonDataSettings> options,
                           IEnumerable<IValidator<T>> validators)
    {
        _validators = validators;
        _jsonDataSettings = options.Value;
    }

    public IEnumerable<T> Query()
    {
        var collection = GetCollection();
        return collection.AsQueryable();
    }

    public async Task InsertAsync(T item, CancellationToken cancellationToken)
    {
        var collection = GetCollection();

        if (_validators.Any())
        {
            var failures = new List<ValidationFailure>();
            foreach (var validator in _validators)
            {
                var validationResult = await validator.ValidateAsync(item, cancellationToken);
                if (validationResult != null)
                {
                    failures.AddRange(validationResult.Errors);
                }
            }

            if (failures.Any())
            {
                throw new KrosoftMetierException(failures.Select(x => x.ErrorMessage).ToHashSet());
            }
        }

        await collection.InsertOneAsync(item);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var collection = GetCollection();
        await collection.DeleteOneAsync(id);
    }

    public async Task UpdateAsync(int id, T item, CancellationToken cancellationToken)
    {
        var collection = GetCollection();
        await collection.UpdateOneAsync(id, item);
    }

    public IDocumentCollection<T> GetCollection()
    {
        var store = new DataStore(_jsonDataSettings.DataFileName);
        var collection = store.GetCollection<T>();
        return collection;
    }
}