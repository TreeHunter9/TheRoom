using TheRoom.InteractableObjects.MiniGames.SafeCode;
using UnityEngine;
using Zenject;

public class SafeDataHolderInstaller : MonoInstaller
{
    [SerializeField] private SafeDataHolder _safeDataHolder;
    public override void InstallBindings()
    {
        Container
            .Bind<SafeDataHolder>()
            .FromInstance(_safeDataHolder)
            .AsSingle()
            .NonLazy();
    }
}