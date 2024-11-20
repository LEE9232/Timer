using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;
    private int menuMinutes;
    private int menuSeconds;

    public void Setup(string name, int minutes, int seconds)
    {
        buttonText.text = name;
        menuMinutes = minutes;
        menuSeconds = seconds;
        GetComponent<Button>().onClick.AddListener(() => SetTimer(menuMinutes, menuSeconds));
    }

    private void SetTimer(int minutes, int seconds)
    {
        ClickManager clickManager = FindObjectOfType<ClickManager>();
        if (clickManager != null)
        {
            clickManager.SetTimer(minutes, seconds);
        }
    }
}
