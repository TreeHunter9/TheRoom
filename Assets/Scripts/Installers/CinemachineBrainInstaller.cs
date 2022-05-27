using Cinemachine;
using UnityEngine;
using Zenject;

namespace TheRoom.Installers
{
    public class CinemachineBrainInstaller : MonoInstaller
    {
        [SerializeField] private CinemachineBrain _cinemachineBrain;
    
        public override void InstallBindings()
        {
            Container
                .Bind<CinemachineBrain>()
                .FromInstance(_cinemachineBrain)
                .AsCached()
                .NonLazy();
        }
    }
}