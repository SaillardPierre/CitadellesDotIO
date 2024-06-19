using CitadellesDotIO.Enums;

namespace CitadellesDotIO.Engine.DTOs;

public class CharacterDto
{
    public int Order { get; set; }
    public string Name { get; set; }
    public DistrictType? AssociatedDistrictType { get; set; }
    public SpellDto? Spell { get; set; }
    public PassiveDto? Passive { get; set; }   
    public CharacterDto(int order, string name, DistrictType? associatedDistrictType, SpellDto spell = null, PassiveDto passive = null)
    {
        Order = order;
        Name = name;
        AssociatedDistrictType = associatedDistrictType;
        Spell = spell;
        Passive = passive;
    }
}