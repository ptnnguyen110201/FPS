using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

public interface ICameraManager {

    CinemachineCamera GetCinemachineCamera();
    UniTask SetTarget();   
}