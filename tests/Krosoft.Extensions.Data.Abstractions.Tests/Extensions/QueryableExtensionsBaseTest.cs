//using Krosoft.Extensions.Data.Abstractions.Extensions;
//using Krosoft.Extensions.Data.Abstractions.Interfaces;
//using Krosoft.Extensions.Samples.Library.Models;
//using Krosoft.Extensions.Testing;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Krosoft.Extensions.Data.Abstractions.Tests.Extensions;

//public abstract class QueryableExtensionsBaseTest : BaseTest
//{
//    private readonly Guid _id1 = new Guid("00000000-0000-0000-0000-000000000001");
//    private IReadRepository<Item> _repository = null!;

//    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddInfrastructure(configuration);
//        services.AddRepositories();
//        services.AddDbContextPostgreSql<KrosoftExtensionContext>(configuration);
//    }

//    protected IQueryable<Item> FilterCollectionEmptyQuery()
//    {
//        var query = _repository.Query()
//                               .Filter(new List<Guid>(), id => x => x.Id == id);
//        return query;
//    }

//    protected IQueryable<Item> FilterCollectionNullQuery()
//    {
//        var query = _repository.Query()
//                               .Filter(null as List<Guid>, id => x => x.Id == id)
//            ;
//        return query;
//    }

//    protected IQueryable<Item> FilterCollectionQuery()
//    {
//        var queryId = new List<Guid> { _id1 };
//        var query = _repository.Query()
//                               .Filter(queryId, id => x => x.Id == id)
//            ;
//        return query;
//    }

//    protected IQueryable<Item> FilterNullableNullQuery()
//    {
//        var query = _repository.Query()
//                               .Filter((DateTime?)null, date => x => x.Date <= date)
//            ;

//        return query;
//    }

//    protected IQueryable<Item> FilterNullableQuery()
//    {
//        DateTime? limite = new DateTime(2002, 1, 1);

//        var query = _repository.Query()
//                               .Filter(limite, date => x => x.Date >= date)
//            ;

//        return query;
//    }

//    protected IQueryable<Item> SearchAllQuery()
//    {
//        var searchTerm = "lorem omnis";
//        var query = _repository.Query().SearchAll(searchTerm, p => p.Description);

//        return query;
//    }

//    protected IQueryable<Item> SearchEmptyQuery()
//    {
//        var query = _repository.Query()
//                               .Search(string.Empty, x => x.Code)
//            ;

//        return query;
//    }

//    protected IQueryable<Item> SearchMultipleSelectorQuery()
//    {
//        var searchTerm = "consectetur";
//        var query = _repository.Query()
//                               .Search(searchTerm, p => p.Code, p => p.Libelle, p => p.Description);

//        return query;
//    }

//    protected IQueryable<Item> SearchNullQuery()
//    {
//        var query = _repository.Query()
//                               .Search(null, x => x.Code, x => x.Libelle)
//            ;

//        return query;
//    }

//    protected IQueryable<Item> SearchQuery()
//    {
//        var searchTerm = "lorem";
//        var query = _repository.Query()
//                               .Search(searchTerm, p => p.Description);

//        return query;
//    }

//    protected IQueryable<Item> SearchUpperQuery()
//    {
//        var searchTerm = "LOREM";
//        var query = _repository.Query();

//        query = query.Search(searchTerm, p => p.Description);

//        return query;
//    }

//    [TestInitialize]
//    public void SetUp()
//    {
//        var serviceProvider = CreateServiceCollection();
//        _repository = serviceProvider.GetRequiredService<IReadRepository<Item>>();
//    }
//}