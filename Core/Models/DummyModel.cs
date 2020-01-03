using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Core.Extensions;
using Core.Interfaces;

namespace Core.Models
{
    public class DummyModel : IEquatable<DummyModel>, IEntityUpdatable<DummyModel>
    {
        [Key]
        public Guid Id { get; set; }

        public List<Level1> Level1s { get; set; }

        public bool Equals(DummyModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) &&
                   Level1s.OrderBy(x => x.Id).SequenceEqual(other.Level1s.OrderBy(x => x.Id));
        }

        public DummyModel CustomUpdate(DummyModel dto)
        {
            Level1s = Level1s.IdAwareUpdate(dto.Level1s, x => x.Id);

            return this;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((DummyModel) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Level1s);
        }

        public void PrepareForJson()
        {
            Level1s = Level1s.OrderBy(x => x.Id).ToList();
            Level1s.ForEach(x => x.PrepareForJson());
        }
    }
}