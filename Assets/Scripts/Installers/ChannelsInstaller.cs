using TheRoom.InventorySystem.Core;
using UnityEngine;
using Zenject;

namespace TheRoom.Installers
{
    public class ChannelsInstaller : MonoInstaller
    {
        [SerializeField] private InventoryChannel _inventoryChannel;
        [SerializeField] private InventoryCursorChannel _inventoryCursorChannel;
    
        public override void InstallBindings()
        {
            BindChannel(_inventoryChannel);
            BindChannel(_inventoryCursorChannel);
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