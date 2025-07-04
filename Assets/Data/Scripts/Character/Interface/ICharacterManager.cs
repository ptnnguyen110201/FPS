using Cysharp.Threading.Tasks;
using UnityEngine;

public interface ICharacterManager
{
    CharacterCtrl CharacterCtrl { get; }
    string CharacterName { get; }
    Vector3 SpawnPos {  get;  }
    Quaternion SpawnRot { get; }
    ICharacterSpawner CharacterSpawner { get; }
    ICharacterPrefabLoader PrefabsLoader { get; }
    UniTask Initialize();
}