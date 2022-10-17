using CitadellesDotIO.Exceptions;
using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using CitadellesDotIO.Model.Targets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;

namespace CitadellesDotIO.Model.Spells
{
    public class Swap : Spell
    {
        public Swap(Player caster) {
            this.Caster = caster;
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
                    int districtsInHand = this.Caster.DistrictsDeck.Count;
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
                    this.Caster.DistrictsDeck.ForEach(d => targetPlayer.PickDistrict(d));
                    this.Caster.DistrictsDeck.Clear();
                    // Prise des cartes de la cible sur la table par le caster
                    table.ForEach(d => this.Caster.PickDistrict(d));
                }
            }
            else throw new SpellTargetException("La cible à échanger n'est ni la pioche ni le deck d'un autre joueur");
        }

        public override void GetAvailableTargets(List<ITarget> targets)
        {
            base.GetAvailableTargets(targets);
            this.Targets = targets;
        }
    }
}
