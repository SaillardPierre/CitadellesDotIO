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
        public TestDistrict()
        {
            this.Name = Guid.NewGuid().ToString("n");
            this.BuildingCost = RandomNumberGenerator.GetInt32(1, 8);
            this.DistrictType = (DistrictType)RandomNumberGenerator.GetInt32(0, Enum.GetNames(typeof(DistrictType)).Length);
        }
    }
}
