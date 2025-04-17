using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    private float deltaTime;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        gameData.FpsCount = fps;
    }
}
