using Cysharp.Threading.Tasks;
using UnityEngine;

public interface ICharacterSpawner
{
    UniTask<CharacterCtrl> SpawnerCharacter(string prefabName, Vector3 Pos, Quaternion Rot);
}
