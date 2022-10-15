using CitadellesDotIO.Model;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using Moq;
using System;
using System.Linq;

namespace CitadellesDotIO.Tests.Factories
{
    public static class PlayerMockFactory
    {
        public const int InitialGold = 99;
        public const int DistrictThreshold = 99;

        public static Mock<Player> WithSpecs(int initialGold = InitialGold, int districtThreshold = DistrictThreshold)
        {
            Mock<Player> player = new();
            player.Object.Gold = initialGold;
            player.Object.DistrictThreshold = districtThreshold;
            return player;
        }
        public static Mock<Player> WithCharacter(Type characterType, int initialGold = InitialGold, int districtThreshold = DistrictThreshold)
        {
            Mock<Player> player = WithSpecs(initialGold, districtThreshold);
            Character character = Activator.CreateInstance(characterType) as Character;
            player.Object.PickCharacter(character);
            return player;
        }
        public static Mock<Player> WithBuiltDistrict(Type districtType, int initialGold = 99, int districtThreshold = 99)
        {
            Mock<Player> player = WithSpecs(initialGold, districtThreshold);
            District toBuild = Activator.CreateInstance(districtType) as District;
            player.Object.PickDistrict(toBuild);
            player.Object.BuildDistrict(player.Object.BuildableDistricts.First());
            player.Object.Gold = initialGold;
            return player;
        }

        public static Mock<Player> WithCharacterAndBuiltDistrict(Type characterType, Type districtType, int initialGold = InitialGold, int districtThreshold = DistrictThreshold)
        {
            Mock<Player> player = WithCharacter(characterType, initialGold, districtThreshold);
            District toBuild = Activator.CreateInstance(districtType) as District;
            player.Object.PickDistrict(toBuild);
            player.Object.BuildDistrict(player.Object.BuildableDistricts.First());
            player.Object.Gold = initialGold;
            return player;
        }
    }
}
