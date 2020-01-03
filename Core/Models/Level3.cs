using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces;
using Newtonsoft.Json;

namespace Core.Models
{
    public class Level3 : IEquatable<Level3>, IEntityUpdatable<Level3>
    {
        /*public Guid Level2RefId { get; set; }
        
        [JsonIgnore]
        public Level2 Level2Ref { get; set; }*/

        [Key]
        public Guid Id { get; set; }
        
        public string L3P1 { get; set; }
        
        public decimal L3P2 { get; set; }
        
        public int L3P3 { get; set; }

        public bool Equals(Level3 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) &&
                   L3P1 == other.L3P1 &&
                   L3P2 == other.L3P2 &&
                   L3P3 == other.L3P3;
        }

        public Level3 CustomUpdate(Level3 dto)
        {
            L3P1 = dto.L3P1;
            L3P2 = dto.L3P2;
            L3P3 = dto.L3P3;

            return this;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Level3) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, L3P1, L3P2, L3P3);
        }
        
        public void PrepareForJson()
        {
           
        }
    }
}