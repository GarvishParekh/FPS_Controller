using UnityEngine;

public class GenericAnimationController : MonoBehaviour, ICanvasAnimation
{
    public CanvasGroup canvasGroup;
    [SerializeField] private AnimationType animationType;
    [SerializeField] private float animationDuration = 0.25f;
    [Range(0.5f, 2f)]
    [SerializeField] private float initialCanvasScale = 1.25f;

    private void Awake()
    {
        GameObject fChild = transform.GetChild(0).gameObject;
        canvasGroup = fChild.GetComponent<CanvasGroup>();
    }

    public void AnimateCanvas()
    {
        ResetCanvas();
        switch (animationType)
        {
            case AnimationType.FADE:
                LeanTween.alphaCanvas(canvasGroup, 1, animationDuration).setEaseInOutSine();
                break;
            case AnimationType.SCALE_AND_FADE:
                LeanTween.scale(canvasGroup.gameObject, Vector3.one, animationDuration).setEaseInOutSine();
                LeanTween.alphaCanvas(canvasGroup, 1, animationDuration).setEaseInOutSine();
                break;
        }
    }

    public void ResetCanvas()
    {
        canvasGroup.alpha = 0;
        switch (animationType)
        {
            case AnimationType.SCALE_AND_FADE:
                canvasGroup.transform.localScale = Vector3.one * initialCanvasScale; 
                break;
        }
    }
}


public enum AnimationType
{
    FADE,
    SCALE_AND_FADE
}