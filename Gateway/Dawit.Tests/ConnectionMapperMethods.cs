using Dawit.Infrastructure.Service.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Dawit.API.Service.Neural;
using Dawit.API.Service.Signal;

namespace Dawit.Tests
{
    public class ConnectionMapperMethods
    {
        private readonly IConnectionMapping<Guid> _connectionMapper;

        public ConnectionMapperMethods()
        {
            _connectionMapper = TestContextUtils.ConnectionMapperContext();
        }

        [Theory(DisplayName = "Add new subscriber on event guid or create if not exists")]
        [InlineData("c906dff3-ef28-4a83-a96c-9598cc174978", 0, "123456")]
        [InlineData("c906dff3-ef28-4a83-a96c-9598cc174978", 0, "555655")]
        public async Task AddOrCreate(string key, int eventType, string connId)
        {
            var ex = await Record.ExceptionAsync(() => _connectionMapper.Add(Guid.Parse(key), eventType, connId));
            Assert.Null(ex);
        }

        [Fact(DisplayName = "GetAllByKey will return inserted events")]
        public async Task GetAllByKey()
        {
            await _connectionMapper.Add(Guid.Parse("c906dff3-ef28-4a83-a96c-9598cc174978"), 0, "666");
            await _connectionMapper.Add(Guid.Parse("c906dff3-ef28-4a83-a96c-9598cc174978"), 1, "666");

            var subscriptions = await _connectionMapper.GetAllByKey(Guid.Parse("c906dff3-ef28-4a83-a96c-9598cc174978"));
            Assert.Equal(2, subscriptions.Count);
        }

        [Fact(DisplayName = "RemoveAll will remove connections and event")]
        public async Task RemoveAll()
        {
            await _connectionMapper.Add(Guid.Parse("c906dff3-ef28-4a83-a96c-9598cc174978"), 0, "123456");
            await _connectionMapper.Add(Guid.Parse("c906dff3-ef28-4a83-a96c-9598cc174978"), 0, "555456");
            
            var subscriptions = await _connectionMapper.GetAllByKey(Guid.Parse("c906dff3-ef28-4a83-a96c-9598cc174978"));
            Assert.Equal(2, subscriptions.Count);

            await _connectionMapper.RemoveAll(Guid.Parse("c906dff3-ef28-4a83-a96c-9598cc174978"));
            Assert.Null(await _connectionMapper.GetAllByKey(Guid.Parse("c906dff3-ef28-4a83-a96c-9598cc174978")));
        }
    }
}
