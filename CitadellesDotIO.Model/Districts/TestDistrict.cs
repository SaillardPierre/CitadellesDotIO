using CitadellesDotIO.Enums;
using CitadellesDotIO.Enums.TurnChoices;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CitadellesDotIO.Model.Districts
{
    public  class TestDistrict : District
    {
        private readonly int RandomTypeSeed = 0;
        public TestDistrict()
        {
            this.Name = Guid.NewGuid().ToString("n");
            this.BuildingCost = RandomNumberGenerator.GetInt32(1, 8);
            this.RandomTypeSeed = RandomNumberGenerator.GetInt32(0, Enum.GetNames(typeof(DistrictType)).Length);
        }
        public override DistrictType DistrictType => (DistrictType)RandomTypeSeed;    
    }
}
