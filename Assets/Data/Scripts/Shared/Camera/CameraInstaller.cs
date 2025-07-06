using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

[Serializable]
public class CameraInstaller : GameSystemInstaller
{
    public override async UniTask Install(DIContainer container)
    {
        CameraManager CameraManager = GameObject.FindFirstObjectByType<CameraManager>();

        if (CameraManager == null)
        {
            ICameraPrefabsLoader cameraPrefabsLoader = new CameraPrefabsLoader();
            await cameraPrefabsLoader.LoadPrefabs();

            GameObject cameraPrefab = cameraPrefabsLoader.GetPrefab("CameraManager");
            GameObject cameraGO = GameObject.Instantiate(cameraPrefab);
            cameraGO.name = "CameraManager";

            CameraManager = cameraGO.GetComponent<CameraManager>();
            if (CameraManager == null)
            {
                CameraManager = cameraGO.AddComponent<CameraManager>();
            }
        }

        container.BindInstance<ICameraManager>(CameraManager);
        GameContext.Instance.InjectInto(CameraManager);
        Debug.Log("[DI] CameraManager installed.");

        await GameContext.Instance.Resolve<ICameraManager>().SetTarget();
    }
}
