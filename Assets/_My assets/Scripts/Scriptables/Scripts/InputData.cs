using UnityEngine;

[CreateAssetMenu (fileName = "Input data", menuName = "Scriptable/Input data")]
public class InputData : ScriptableObject
{
    [Header ("<b>Keyboard")]
    public float xInput;
    public float zInput;

    [Header ("<b>Mouse")]
    public float xMouse;
    public float yMouse;
}
