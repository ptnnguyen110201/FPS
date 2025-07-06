using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

public class SceneBootstrapper : MonoBehaviour
{
    protected IBootstrapper bootstrapper;
    protected void Start()
    {
        this.RunBootstrapAsync().Forget();
    }

    protected async UniTaskVoid RunBootstrapAsync()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
                this.bootstrapper = new MainMenuBootstrapper();
                break;
            case "GamePlay":
                this.bootstrapper = new GamePlayBootstrapper();
                break;
            default:
                return;
        }



        await this.bootstrapper.Initialize();
        Debug.Log("Scene Initialized");
       
    }
}
