using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
public class CharacterInstaller : GameSystemInstaller
{
    public override async UniTask Install(DIContainer container)
    {
        container.Bind<ICharacterPrefabLoader, CharacterPrefabLoader>();
        container.Bind<ICharacterSpawner, CharacterSpawner>();
        container.Bind<ICharacterManager, CharacterManager>();

        await container.Resolve<ICharacterManager>().Initialize();




    }
}
