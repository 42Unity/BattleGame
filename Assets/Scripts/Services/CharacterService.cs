using System.Collections.Generic;
using BattleGame.Model;

namespace BattleGame
{
    public class CharacterService
    {
        private readonly List<Player> players = new();
        public List<Player> Players => players;
        private readonly List<Character> characters = new();
        public List<Character> Characters => characters;

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }
    }
}
