using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
public abstract class PrefabsLoader : IPrefabsLoader
{

    protected readonly Dictionary<string, GameObject> Prefabs = new Dictionary<string, GameObject>();
    public abstract PrefabType PrefabType();

    public GameObject GetPrefab(string prefabName)
    {
        if(!this.Prefabs.TryGetValue(prefabName, out GameObject prefab)) 
        {
            Debug.LogError($"Prefab not found, { prefabName}");
        }
        return prefab;
    }

    public async UniTask LoadPrefabs()
    {
        string label = this.PrefabType().ToString();
        await this.LoadPrefabsByLabel(label);
    }
    public async UniTask LoadPrefabsByLabel(string label)
    {
        AsyncOperationHandle<IList<IResourceLocation>> handle = Addressables.LoadResourceLocationsAsync(label, typeof(GameObject));

        await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogError($"Failed to load label: {label}");
            return;
        }

        foreach (var location in handle.Result)
        {
            AsyncOperationHandle<GameObject> loadHandle = Addressables.LoadAssetAsync<GameObject>(location);
            await loadHandle.Task;

            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject prefab = loadHandle.Result;
                if (this.Prefabs.ContainsKey(prefab.name)) continue;
                this.Prefabs[prefab.name] = prefab;
                
            }
           
        }
    }

   
}