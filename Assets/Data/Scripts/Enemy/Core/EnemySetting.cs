using System;
using UnityEngine;

[Serializable]

public class EnemySetting
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;



    [Header("Animator Layer")]
    public string LayerAction = "Action Layer";


    [Header("Animation Name")]
    public string HitEffect = "BloodHit";
}