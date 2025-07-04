using System;
using UnityEngine;
[Serializable]    
public class CharacterSetting
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float aimingMoveSpeed = 3.8f;

    [Header("Jump & Gravity")]
    public float jumpForce = 16f;
    public float gravity = -9.81f;

    [Header("Mouse Look Settings")]
    public float mouseSensitivity = 25;

    [Header("Camera Clamp Limits")]
    public float pitchClampMin = -80f;
    public float pitchClampMax = 80f;


   
}