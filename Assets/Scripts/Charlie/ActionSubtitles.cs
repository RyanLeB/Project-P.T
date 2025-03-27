using System.Collections;
using UnityEngine;
using TMPro;

public class ActionSubtitles : MonoBehaviour
{
    public bool settingIsOn = true;

    public TextMeshProUGUI subtitle;

    public float subtitleDuration = 1.5f;

    public bool isActive = false;


    public IEnumerator ShowSubtitle(string subtitleText)
    {
        if (settingIsOn)
        {

            if (isActive)
            {
                subtitle.text = subtitleText;
            }

            isActive = true;
            subtitle.gameObject.SetActive(true);
            subtitle.text = subtitleText;
            yield return new WaitForSecondsRealtime(subtitleDuration);
            subtitle.gameObject.SetActive(false);
            isActive = false;
        }
    }


    public void ToggleSetting()
    {
        settingIsOn = !settingIsOn;
    }
}
