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
    public class Level2 : IEquatable<Level2>, IEntityUpdatable<Level2>
    {
        /*public Guid Level1RefId { get; set; }

        [JsonIgnore]
        public Level1 Level1Ref { get; set; }*/

        [Key]
        public Guid Id { get; set; }
        
        public string L2P1 { get; set; }
        
        public decimal L2P2 { get; set; }

        public List<Level3> L2P3 { get; set; }

        public bool Equals(Level2 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) &&
                   L2P1 == other.L2P1 &&
                   L2P2 == other.L2P2 &&
                   L2P3.OrderBy(x => x.Id).SequenceEqual(other.L2P3.OrderBy(x => x.Id));
        }

        public Level2 CustomUpdate(Level2 dto)
        {
            L2P1 = dto.L2P1;
            L2P2 = dto.L2P2;
            L2P3 = L2P3.IdAwareUpdate(dto.L2P3, x => x.Id);

            return this;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Level2) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, L2P1, L2P2, L2P3);
        }

        public void PrepareForJson()
        {
            L2P3 = L2P3.OrderBy(x => x.Id).ToList();

            L2P3.ForEach(x => x.PrepareForJson());
        }
    }
}