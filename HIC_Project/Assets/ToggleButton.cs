using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField]
    public Image background;
    [SerializeField]
    public Image checkmark;

    private bool isOn = false;
    void Start()
    {
        UpdateUI();
    }
    public void Toggle()
    {
        isOn = !isOn;
        UpdateUI();
    }

    private void UpdateUI()
    {
        checkmark.enabled = isOn;
        background.enabled = !isOn;
    }
}