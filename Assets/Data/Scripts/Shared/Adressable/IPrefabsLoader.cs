using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IPrefabsLoader 
{
    PrefabType PrefabType();
    UniTask LoadPrefabs();
    GameObject GetPrefab(string prefabName);
}
