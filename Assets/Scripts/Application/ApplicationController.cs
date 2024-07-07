using Unity.Netcode;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using BattleGame.Gameplay.Objects;
using UnityEngine.SceneManagement;

namespace BattleGame.Application
{
    public class ApplicationController : LifetimeScope
    {
        public static ApplicationController Instance { get; private set; }
        [SerializeField] NetworkManager netManager;
        [SerializeField] GameDataSource dataSource;

        protected override void Awake()
        {
            base.Awake();
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            SceneManager.LoadScene("MainMenu");
        }

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            builder.RegisterComponent(netManager);
            builder.RegisterComponent(dataSource);
            builder.Register<Entity>(Lifetime.Singleton);
        }
    }
}
