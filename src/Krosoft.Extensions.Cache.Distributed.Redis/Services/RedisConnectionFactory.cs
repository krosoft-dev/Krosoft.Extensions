using Krosoft.Extensions.Cache.Distributed.Redis.Interfaces;
using Krosoft.Extensions.Core.Tools;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Krosoft.Extensions.Cache.Distributed.Redis.Services;

internal class RedisConnectionFactory : IRedisConnectionFactory
{
    private readonly Lazy<IConnectionMultiplexer> _connection;

    public RedisConnectionFactory(IConfiguration configuration)
    {
        Guard.IsNotNull(nameof(configuration), configuration);
        var connectionString = configuration["ConnectionStrings:Redis"];
        if (connectionString != null)
        {
            _connection = new Lazy<IConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(connectionString));
        }
    }

    public IConnectionMultiplexer Connection => _connection.Value;
}