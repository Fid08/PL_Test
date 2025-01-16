using UnityEngine;


[CreateAssetMenu(fileName = "CameraSettings", menuName = "Test/CameraSettings", order = 1)]
public class CameraSettings : ScriptableObject
{
    public float CameraBoundsR = 30f;
    public float CameraBoundsL = -30f;
    public float CameraScrollDelta = 0.05f;
    public float CameraBoardingScrollSpeed = 2;
    public float ScreenWeighOffset = 20;
}
