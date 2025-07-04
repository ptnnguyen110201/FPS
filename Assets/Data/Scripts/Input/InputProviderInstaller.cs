using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
public class InputProviderInstaller : GameSystemInstaller
{
    public override async UniTask Install(DIContainer container)
    {
        InputProvider existInputProvider = GameObject.FindFirstObjectByType<InputProvider>();

        if (existInputProvider == null)
        {
            GameObject newObj = new GameObject("InputProvider");
            existInputProvider = newObj.AddComponent<InputProvider>();
        }

        container.BindInstance<IInputProvider>(existInputProvider);
        Debug.Log("[DI] InputProvider installed.");

        await UniTask.Yield();
    }
}
