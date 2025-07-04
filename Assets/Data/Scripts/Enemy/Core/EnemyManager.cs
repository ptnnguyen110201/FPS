using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyManager : MonoBehaviour, IEnemyManager
{
    [Inject] public IEnemyPrefabLoader EnemyPrefabLoad { get; protected set; }
    [Inject] public IEnemySpawner EnemySpawner { get; protected set; }

    [SerializeField]
    protected List<Vector3> SpawnPos = new List<Vector3>()
{
    new Vector3(-4, 0, 0),
    new Vector3(-2, 0, 0),
    new Vector3(0, 0, 0),
    new Vector3(2, 0, 0),
    new Vector3(4, 0, 0),

    new Vector3(-4, 0, 2),
    new Vector3(-2, 0, 2),
    new Vector3(0, 0, 2),
    new Vector3(2, 0, 2),
    new Vector3(4, 0, 2),
};

    protected void OnEnable()
    {
        GameContext.Instance.InjectInto(this);
       
        foreach(Vector3 Pos in SpawnPos) 
        {
            this.EnemySpawner.Spawn("Ant_Blue", Pos, transform.rotation);
        }
    }


}