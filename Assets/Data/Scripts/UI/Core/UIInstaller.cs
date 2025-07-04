using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

[Serializable]
public class UIInstaller : GameSystemInstaller
{
    public override async UniTask Install(DIContainer container)
    {
        UIManager UIManager = GameObject.FindFirstObjectByType<UIManager>();

        if (UIManager == null)
        {
            IUIPrefabLoader UIPrefabLoader = new UIPrefabLoader();
            await UIPrefabLoader.LoadPrefabs();

            GameObject cameraPrefab = UIPrefabLoader.GetPrefab("UIManager");
            GameObject cameraGO = GameObject.Instantiate(cameraPrefab);
            cameraGO.name = "UIManager";

            UIManager = cameraGO.GetComponent<UIManager>();
            if (UIManager == null)
            {
                UIManager = cameraGO.AddComponent<UIManager>();
            }
        }

        container.BindInstance<IUIManager>(UIManager);
        Debug.Log("[DI] UIManager installed.");

        await UniTask.Yield();
    }
}
