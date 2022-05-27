using TheRoom.Intro;
using UnityEngine;
using Zenject;

namespace TheRoom.Installers
{
    public class TutorialControllerInstaller : MonoInstaller
    {
        [SerializeField] private TutorialController _tutorialController;
        public override void InstallBindings()
        {
            Container
                .Bind<TutorialController>()
                .FromInstance(_tutorialController)
                .AsSingle()
                .NonLazy();
        }
    }
}