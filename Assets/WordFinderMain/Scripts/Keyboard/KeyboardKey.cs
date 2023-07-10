using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeyboardKey : MonoBehaviour
{
    public static Action<char> onKeyPressed;

    [SerializeField] private TextMeshProUGUI letterText;
    [SerializeField] private Image keyRenderer;

    private Validity validity;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);

        Initialize();
    }

    private void SendKeyPressedEvent()
    {
        onKeyPressed?.Invoke(letterText.text[0]);

        Debug.Log(letterText.text[0]);
    }

    public bool IsUntouched()
    {
        return validity == Validity.None;
    }

    public void Initialize()
    {
        keyRenderer.color = Color.white;
        validity = Validity.None;
    }

    public char GetLetter()
    {
        return letterText.text[0];
    }

    public void SetValid()
    {
        keyRenderer.color = Color.green;
        validity = Validity.Valid;
    }

    public void SetPotantial()
    {
        if (validity == Validity.Valid)
            return;

        keyRenderer.color = Color.yellow;
        validity = Validity.Potential;
    }

    public void SetInvalid()
    {
        if (validity == Validity.Valid || validity == Validity.Potential)
            return;

        keyRenderer.color = Color.red;
        validity = Validity.Invalid;
    }
}

