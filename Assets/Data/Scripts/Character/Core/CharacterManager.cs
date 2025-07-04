using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterManager : ICharacterManager
{
    public ICharacterSpawner CharacterSpawner {  get; protected set; }
    public ICharacterPrefabLoader PrefabsLoader { get; protected set; }
   
    public string CharacterName { get; protected set; } = "Player";
    public Vector3 SpawnPos {  get; protected set; } = Vector3.up;
    public Quaternion SpawnRot { get; protected set; } = Quaternion.LookRotation(Vector3.forward);

    public CharacterCtrl CharacterCtrl { get; protected set; }

    public CharacterManager(
        ICharacterSpawner CharacterSpawner,
        ICharacterPrefabLoader PrefabsLoader)
    {
        this.CharacterSpawner = CharacterSpawner;
        this.PrefabsLoader = PrefabsLoader;;
    }

    public async UniTask Initialize()
    {
        await this.PrefabsLoader.LoadPrefabs();
        this.CharacterCtrl =  await this.CharacterSpawner.SpawnerCharacter(this.CharacterName, this.SpawnPos, this.SpawnRot);
        GameContext.Instance.InjectInto(this.CharacterCtrl);
        Debug.Log("Character Manager Initialized");
    }
}