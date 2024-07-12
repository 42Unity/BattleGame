namespace BattleGame.Model
{
    public class Player : Character
    {
        public Weapon Weapon { get; set; }
        public float CurrentExp { get; set; }
        public int Level { get; set; }
    }
}
