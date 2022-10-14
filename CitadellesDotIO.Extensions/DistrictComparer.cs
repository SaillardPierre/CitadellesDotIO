using CitadellesDotIO.Model.Districts;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CitadellesDotIO.Extensions
{
    public class DistrictComparer : IEqualityComparer<District>
    {
        public bool Equals(District x, District y)
            => x.Name == y.Name &&
            x.BuildingCost == y.BuildingCost &&
            x.ScoreValue == y.ScoreValue &&
            x.DestructionCost == y.DestructionCost &&
            x.CanBeDestroyed == y.CanBeDestroyed &&
            x.IsBuilt == y.IsBuilt &&
            x.DistrictType == y.DistrictType;        

        public int GetHashCode([DisallowNull] District obj)
        => obj.Name.GetHashCode();
    }
}
