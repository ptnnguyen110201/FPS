using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : LoadComponents, ICameraManager
{
    [SerializeField] protected CinemachineCamera VirtualCamera;
    [Inject] protected ICharacterManager CharacterManager;
    public CinemachineCamera GetCinemachineCamera() => this.VirtualCamera;

    public async UniTask SetTarget()
    {
        await UniTask.WaitUntil(() => this.CharacterManager != null);

        CharacterCtrl CharacterCtrl = this.CharacterManager.CharacterCtrl;
        if (CharacterCtrl == null) return;
        Transform Follow = CharacterCtrl.transform.Find("Model");
        this.VirtualCamera.Follow = Follow;
    }

}