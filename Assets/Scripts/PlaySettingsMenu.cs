using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySettingsMenu : MonoBehaviour
{
    public InputField inputField;
    public Toggle toggle;
    public Slider slider;
    public GameObject mainMenu;

    private bool animOn;
    private int animSpeed;
    private int ringCount;

    private readonly bool oAnimOn = true;
    private readonly int oAnimSpeed = 5;
    private readonly int oRingCount = 4;


    // Start is called before the first frame update
    void Start()
    {
        animOn = PlayerPrefs.HasKey("animOn") ? PlayerPrefs.GetInt("animOn") == 1 ? true : false : oAnimOn;
        animSpeed = PlayerPrefs.HasKey("animSpeed") ? PlayerPrefs.GetInt("animSpeed") : oAnimSpeed;
        ringCount = PlayerPrefs.HasKey("ringCount") ? PlayerPrefs.GetInt("ringCount") : oRingCount;

        toggle.isOn = animOn;
        slider.value = animSpeed;
        inputField.text = ringCount.ToString();
    }

    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }


    public void SaveSettings()
    {
        PlayerPrefs.SetInt("animOn", toggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("animSpeed", (int)slider.value);
        PlayerPrefs.SetInt("ringCount", int.Parse(inputField.text));
        PlayerPrefs.Save();
    }

    public void ResetSettings()
    {
        toggle.isOn = animOn;
        slider.value = animSpeed;
        inputField.text = ringCount.ToString();
    }
}
