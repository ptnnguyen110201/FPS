using Cysharp.Threading.Tasks;

public interface IEnemyManager 
{
    IEnemyPrefabLoader EnemyPrefabLoad { get; }
    IEnemySpawner EnemySpawner { get; }


    
}