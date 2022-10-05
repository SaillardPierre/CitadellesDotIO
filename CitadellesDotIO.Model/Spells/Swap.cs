using CitadellesDotIO.Model.Characters;
using CitadellesDotIO.Model.Districts;
using System;
using System.Collections.Generic;
using System.Text;

namespace CitadellesDotIO.Model.Spells
{
    public class Swap<T> : ISpell<T> where T : ITarget
    {
        private readonly Player Caster;
        public Swap(Player caster) {
            this.Caster = caster;
        }

        public void Cast(ref T target)
        {
            // Si la cible est le deck des districts, on mets toutes ses cartes sous la pioche et on en prend le même nombre
            if (target is Deck<District> districtsDeck)
            {
                int districtsInHand = this.Caster.DistrictsDeck.Count;
                Caster.DistrictsDeck.ForEach(d => districtsDeck.Enqueue(d));
                Caster.DistrictsDeck.Clear();
                for (int i = 0; i < districtsInHand; i++)
                {
                    Caster.DistrictsDeck.Add(districtsDeck.PickCard());
                }
            }

            // Si la cible est un autre joueur, on échange les deux decks 
            else if (target is Player targetPlayer)
            {
                (targetPlayer.DistrictsDeck, Caster.DistrictsDeck) = (Caster.DistrictsDeck, targetPlayer.DistrictsDeck);
            }
        }
    }
}
