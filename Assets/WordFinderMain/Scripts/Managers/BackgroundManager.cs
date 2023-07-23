using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer lightTheme;
    [SerializeField] private SpriteRenderer darkTheme;

    [SerializeField] private Image lightIcon;
    [SerializeField] private Image darkIcon;

    private bool isLightTheme = true;

    private float hue = 179;

    private void Start()
    {
        darkTheme.enabled = false;
        lightIcon.enabled = false;

        lightTheme.color = Color.HSVToRGB(hue / 360, 1f, 1f);
    }

    public void ChangeTheme()
    {
        if (isLightTheme)
        {
            lightTheme.enabled = false;
            darkTheme.enabled = true;

            lightIcon.enabled = true;
            darkIcon.enabled = false;
        }
        else
        {
            lightTheme.enabled = true;
            darkTheme.enabled = false;

            lightIcon.enabled = false;
            darkIcon.enabled = true;

            SetColor();
            
        }

        isLightTheme = !isLightTheme;
    }

    void SetColor()
    {
        hue = Random.Range(0, 255);
        lightTheme.color = Color.HSVToRGB(hue / 360f, 1f, 1f);
    }
}
