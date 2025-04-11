using UnityEngine;
using System.Collections.Generic;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    private List<CanvasIdentity> AllCanvas = new List<CanvasIdentity>();

    private void Awake()
    {
        instance = this;
    }

    public void OpenCanvas(string desireCanvas)
    {
        foreach (CanvasIdentity canvas in AllCanvas)
        {
            if (canvas.SelectedIdentity == desireCanvas)
            {
                canvas.OpenCanvas(); 
            }
            else canvas.CloseCanvas();
        }
    }

    public void CloseCanvas(string desireCanvas)
    {
        foreach (CanvasIdentity canvas in AllCanvas)
        {
            if (canvas.SelectedIdentity == desireCanvas)
            {
                canvas.CloseCanvas();
            }
        }
    }

    public void OpenPopup(string desireCanvas)
    {
        foreach (CanvasIdentity canvas in AllCanvas)
        {
            if (canvas.SelectedIdentity == desireCanvas)
            {
                canvas.OpenCanvas();
            }
        }
    }

    public void ClosePopup(string desireCanvas)
    {
        foreach (CanvasIdentity canvas in AllCanvas)
        {
            if (canvas.SelectedIdentity == desireCanvas)
            {
                canvas.CloseCanvas();
            }
        }
    }

    public void AddToList(CanvasIdentity canvasIdentity)
    {
        AllCanvas.Add(canvasIdentity);
    }
}
