using Core.Models;
using Core.Tests.Abstracts;
using Xbehave;

namespace Core.Tests.Attempts
{
    public class EfUpdateTestNative : AbstractBasicUpdateTest
    {
        [Scenario]
        public override void Test__Update(DummyModel fixtureDummyModel, DummyModel dto, DummyModel entity)
        {
            base.Test__Update(fixtureDummyModel, dto, entity);
        }

        protected override void UpdateDummyModel(DummyModel entity, DummyModel dto, EntityDbContext entityDbContext)
        {
             entityDbContext.Entry(entity).CurrentValues.SetValues(dto);
        }
    }
}