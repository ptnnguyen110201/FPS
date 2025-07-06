using UnityEngine;

public class CharacterAnimationSetting
{
    [Header("Animation Layer")]

    public readonly string LayerPose = "Layer Locomotion";
    public readonly string LayerLocomotion = "Layer Locomotion";
    public readonly string LayerOverLay = "Layer Overlay";
    public readonly string LayerWiggles = "Layer Wiggles";
    public readonly string LayerHolster = "Layer Holster";
    public readonly string LayerAction = "Layer Action";


    [Header("Animation Parameter")]
    public string AimPara = "Aim";
    public string AimingPara = "Aiming";
    public string RunningPara = "Running";
    public string MovementPara = "Movement";
    public string HolsterPara = "Holster";


    [Header("Animation State Name")]
    public string Fire = "Fire";
    public string Holster = "Holster";
    public string UnHolster = "UnHolster";
}