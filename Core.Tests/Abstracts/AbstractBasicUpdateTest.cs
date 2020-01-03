using System;
using System.Linq;
using AutoFixture;
using Core.Models;
using Core.Tests.Extensions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Extensions.Logging;
using Xbehave;
using Xunit;

namespace Core.Tests.Abstracts
{
    public abstract class AbstractBasicUpdateTest : IDisposable
    {
        private readonly EntityDbContext _dbContext;

        private readonly IFixture _fixture;
        
        private readonly SqliteConnection _sqliteConnection;

        protected AbstractBasicUpdateTest()
        {
            _sqliteConnection =
                new SqliteConnection(new SqliteConnectionStringBuilder {DataSource = ":memory:"}.ConnectionString);

            _sqliteConnection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<EntityDbContext>()
                .UseSqlite(_sqliteConnection)
                .UseLoggerFactory(new SerilogLoggerFactory(new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger()));
            
            _dbContext = new EntityDbContext(optionsBuilder.Options);
            
            _fixture = new Fixture();

            /*
            _fixture.Customize<Level1>(x => 
                x.Without(y => y.DummyModelRef)
                    .Without(y => y.DummyModelRefId));
            
            _fixture.Customize<Level2>(x => 
                x.Without(y => y.Level1Ref)
                    .Without(y => y.Level1RefId));
            
            _fixture.Customize<Level3>(x => 
                x.Without(y => y.Level2Ref)
                    .Without(y => y.Level2RefId));*/
        }
        
        [Scenario]
        public virtual void Test__Update(DummyModel fixtureDummyModel, DummyModel dto, DummyModel entity)
        {
            "Create fixture dummy model"
                .x(() => fixtureDummyModel = _fixture.Create<DummyModel>());

            /*"Assign relationships"
                .x(() =>
                {
                    fixtureDummyModel.Level1s.ForEach(x =>
                    {
                        x.DummyModelRef = fixtureDummyModel;
                        x.DummyModelRefId = fixtureDummyModel.Id;

                        x.L1P3.ForEach(y =>
                        {
                            y.Level1Ref = x;
                            y.Level1RefId = x.Id;

                            y.L2P3.ForEach(z =>
                            {
                                z.Level2Ref = y;
                                z.Level2RefId = y.Id;
                            });
                        });
                    });
                });*/
            
            "Add fixture dummy model to DbContext"
                .x(() => _dbContext.Add(fixtureDummyModel));

            "SaveChanges of DbContext"
                .x(() => _dbContext.SaveChanges());

            "Get entity from DbContext"
                .x(() => entity = _dbContext.DummyModels
                    .Include(x => x.Level1s)
                        .ThenInclude(x => x.L1P3)
                            .ThenInclude(x => x.L2P3)
                    .First());
            
            "Get entity and convert it to DTO"
                .x(() => dto = entity.DeepClone());

            "Update DTO".x(() =>
            {
                dto.Level1s.Last().L1P3.First().L2P1 = "Hello world!";
                dto.Level1s = dto.Level1s.Shuffle();
            });

            "Update entity"
                .x(() => UpdateDummyModel(entity, dto, _dbContext));

            "SaveChanges of DbContext"
                .x(() => _dbContext.SaveChanges());
            
            "Get entity from DbContext again"
                .x(() => entity = _dbContext.DummyModels
                    .Include(x => x.Level1s)
                        .ThenInclude(x => x.L1P3)
                            .ThenInclude(x => x.L2P3)
                    .First());
            
            "Validate changes are applies to entity"
                .x(() =>
                {
                    entity.PrepareForJson();
                    dto.PrepareForJson();
                    
                    var e = entity.ToJson();
                    var d = dto.ToJson();

                    Assert.Equal(entity, dto);
                });
        }

        protected abstract void UpdateDummyModel(DummyModel entity, DummyModel dto, EntityDbContext entityDbContext);

        public void Dispose()
        {
            _dbContext.Dispose();

            _sqliteConnection.Dispose();
        }
    }
}