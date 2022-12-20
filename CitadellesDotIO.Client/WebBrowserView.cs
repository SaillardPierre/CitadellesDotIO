using CitadellesDotIO.Engine;
using CitadellesDotIO.Engine.Characters;
using CitadellesDotIO.Engine.Districts;
using CitadellesDotIO.Engine.Targets;
using CitadellesDotIO.Engine.View;
using CitadellesDotIO.Enums.TurnChoices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CitadellesDotIO.Client
{
    [JsonObject(IsReference = true)]
    public class WebBrowserView : IView
    {
        public Player Player { get; set; }
        public Task DisplayRanking(Player player, int rank)
        {
            throw new NotImplementedException();
        }

        public Task<Character> PickCharacter(List<Character> characters)
        {
            throw new NotImplementedException();
        }

        public Task<District> PickDistrict(List<District> districts)
        {
            throw new NotImplementedException();
        }

        public Task<List<District>> PickDistrictsFromPool(int pickCount, List<District> pool)
        {
            throw new NotImplementedException();
        }

        public Task<MandatoryTurnChoice> PickMandatoryTurnChoice()
        {
            throw new NotImplementedException();
        }

        public Task<ITarget> PickSpellTarget(List<ITarget> targets)
        {
            throw new NotImplementedException();
        }

        public Task<UnorderedTurnChoice> PickUnorderedTurnChoice(List<UnorderedTurnChoice> availableChoices)
        {
            throw new NotImplementedException();
        }

        public void SetPlayer(Player player)
        {
            this.Player = player;
        }
    }
}
