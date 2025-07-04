using Cysharp.Threading.Tasks;

using UnityEngine;


public class CharacterSpawner : Spawner<CharacterCtrl>, ICharacterSpawner
{
    public CharacterSpawner(ICharacterPrefabLoader prefabLoader) : base(prefabLoader)
    {
    }

    public UniTask<CharacterCtrl> SpawnerCharacter(string prefabName, Vector3 Pos, Quaternion Rot)
    {
        CharacterCtrl characterCtrl = this.Spawn(prefabName, Pos, Rot);
        return UniTask.FromResult(characterCtrl);
    }
}