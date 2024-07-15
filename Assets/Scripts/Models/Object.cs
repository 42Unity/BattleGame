namespace BattleGame.Model
{
    public abstract class Object
    {
        public static int NextInstanceId = 0;
        public int InstanceId { get; set; } = NextInstanceId++;
    }
}
