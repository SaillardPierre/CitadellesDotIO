using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CitadellesDotIO.Enums.TurnChoices
{
    public enum MandatoryTurnChoice
    {
        BaseIncome,
        PickDistricts
    }

    public enum UnorderedTurnChoice
    {
        BonusIncome,
        UseCharacterSpell,
        BuildDistrict,
        EndTurn
    }

    public static class Consts {
        public static List<UnorderedTurnChoice> UnorderedTurnChoices = Enum.GetValues(typeof(UnorderedTurnChoice)).OfType<UnorderedTurnChoice>().ToList();
    }

}
