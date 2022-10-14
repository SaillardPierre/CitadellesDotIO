using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        EndTurn,
        BonusIncome,
        CastCharacterSpell,
        CastDistrictSpell,
        BuildDistrict
    }

    public static class Consts {
        public static readonly ImmutableList<UnorderedTurnChoice> UnorderedTurnChoices = Enum.GetValues(typeof(UnorderedTurnChoice)).OfType<UnorderedTurnChoice>().ToImmutableList();
    }

}
