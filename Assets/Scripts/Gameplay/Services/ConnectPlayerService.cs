using Unity.Netcode;
using VContainer;

namespace BattleGame.Gameplay.Service
{
    public class ConnectPlayerService : IService
    {
        [Inject] public NetworkManager net;

        public void StartHost()
        {
            net.StartHost();
        }

        public void StartClient()
        {
            net.StartClient();
        }
    }
}
