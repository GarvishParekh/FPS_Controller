using UnityEngine;

[CreateAssetMenu (fileName = "Input data", menuName = "Scriptable/Input data")]
public class InputData : ScriptableObject
{
    public InputType inputType;

    [Header ("<b>Keyboard")]
    public float xInput;
    public float zInput;

    [Header("<b>Head movement")]
    public float xHead;
    public float yHead;

    [Header("<b>Touch inputs")]
    public float xTouchInputs;
    public float yTouchInputs;
}
