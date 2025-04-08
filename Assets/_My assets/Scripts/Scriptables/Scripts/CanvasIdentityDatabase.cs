using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CanvasIdentityDatabase", menuName = "Scriptable/Canvas identity database")]
public class CanvasIdentityDatabase : ScriptableObject
{
    public List<string> canvasIdentities = new List<string>();
}
