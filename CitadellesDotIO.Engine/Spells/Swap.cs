using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Engine.Districts;
using System;
using System.Collections.Generic;

namespace CitadellesDotIO.Engine.Spells
{
    public class Swap : Spell
    {
        public Swap(Player caster)
        {
            Caster = caster;
        }

        public override Type TargetType => typeof(ISwappable);
        public override bool HasToPickTargets => true;

        public override void Cast(ITarget target)
        {
            base.Cast(target);
            if (target is ISwappable)
            {
                // Si la cible est le deck des districts, on mets toutes ses cartes sous la pioche et on en prend le même nombre
                if (target is Deck<District> districtsDeck)
                {
                    int districtsInHand = Caster.DistrictsDeck.Count;
                    Caster.DistrictsDeck.ForEach(d => districtsDeck.Enqueue(d));
                    Caster.DistrictsDeck.Clear();
                    for (int i = 0; i < districtsInHand; i++)
                    {
                        Caster.PickDistrict(districtsDeck.PickCard());
                    }
                }

                // Si la cible est un autre joueur, on échange les deux decks 
                else if (target is Player targetPlayer)
                {
                    // Copie (vidage) des cartes de la cible sur la table
                    List<District> table = new(targetPlayer.DistrictsDeck);
                    targetPlayer.DistrictsDeck.Clear();
                    // Prise des cartes du caster par la cible
                    Caster.DistrictsDeck.ForEach(d => targetPlayer.PickDistrict(d));
                    Caster.DistrictsDeck.Clear();
                    // Prise des cartes de la cible sur la table par le caster
                    table.ForEach(d => Caster.PickDistrict(d));
                }
            }
            else throw new SpellTargetException("La cible à échanger n'est ni la pioche ni le deck d'un autre joueur");
        }

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            Targets = targets;
        }
    }
}
