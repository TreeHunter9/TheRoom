using TheRoom.CameraMovement;
using UnityEngine;
using Zenject;

namespace TheRoom.Installers
{
    public class CameraSettingsInstaller : MonoInstaller
    {
        [SerializeField] private CameraSettings _cameraSettings;

        public override void InstallBindings()
        {
            BindChannel(_cameraSettings);
        }

        private void BindChannel<T>(T channel) where T : ScriptableObject
        {
            Container
                .Bind<T>()
                .FromInstance(channel)
                .AsCached()
                .NonLazy();
        }
    }
}