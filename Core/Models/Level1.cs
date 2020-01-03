using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Core.Extensions;
using Core.Interfaces;
using Newtonsoft.Json;

namespace Core.Models
{
    public class Level1 : IEquatable<Level1>, IEntityUpdatable<Level1>
    {
        /*public Guid DummyModelRefId { get; set; }
        
        [JsonIgnore]
        public DummyModel DummyModelRef { get; set; }*/
        
        [Key]
        public Guid Id { get; set; }
        
        public string L1P1 { get; set; }
        
        public decimal L1P2 { get; set; }

        public List<Level2> L1P3 { get; set; }

        public bool Equals(Level1 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) &&
                   L1P1 == other.L1P1 &&
                   L1P2 == other.L1P2 &&
                   L1P3.OrderBy(x => x.Id).SequenceEqual(other.L1P3.OrderBy(x => x.Id));
        }

        public Level1 CustomUpdate(Level1 dto)
        {
            L1P1 = dto.L1P1;
            L1P2 = dto.L1P2;
            L1P3 = L1P3.IdAwareUpdate(dto.L1P3, x => x.Id);

            return this;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Level1) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, L1P1, L1P2, L1P3);
        }

        public void PrepareForJson()
        {
            L1P3 = L1P3.OrderBy(x => x.Id).ToList();
            
            L1P3.ForEach(x => x.PrepareForJson());
        }
    }
}