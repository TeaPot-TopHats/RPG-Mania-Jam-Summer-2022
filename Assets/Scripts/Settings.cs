using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using System;

public class Settings : MonoBehaviour
{
    // Start is called before the first frame update
    public Dropdown resolutionDropdown;
    Resolution[] resolutions;
    //----------------------------------------
    [Header("Slider")]
    public AO_GameObject settings;
    public SliderBrightnessSettings brightnessSlider;
    public PostProcessProfile profile;
    //-----------------------------------------------
    void Start()
    {

        //------------------------------------------------
        brightnessSlider.slider.onValueChanged.AddListener(delegate { brightness(brightnessSlider.slider.value); });
        //------------------------------------------------
        /*
         * First we cleared our dropdown resolution
         * Create a list of string for our resolution options
         * A for loop that loop through each of our resolution array and for each option
         * Created a string formated in the following: "width" + "x" + "height" of the resolution
         * Lastly add the option to our options list
         * Then add them to our dropdown list
         * 
         * To set the resolution to the correct one right away we use an if statement to check resolution wide is equal
         * To screen current width resolution same for height
         */
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }// end of for loop
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }// end of start

    //------------------------------------------------
    void brightness(float currentValue)
    {
        float finalValue;
        finalValue = ConvertValue(brightnessSlider.slider.minValue, brightnessSlider.slider.maxValue,
            brightnessSlider.minSettingsValue, brightnessSlider.maxSettingsValue, currentValue);
        profile.GetSetting<ColorGrading>().postExposure.Override(finalValue);
        settings.bringhtnessValue = currentValue;
        brightnessSlider.txtSlider.text = Mathf.RoundToInt(currentValue).ToString();
        
    }

    private float ConvertValue(float virtualMin, float virtualMax, float actualMin, float actualMax, float currentValue)
    {
        float ration = (actualMax - actualMin) / (virtualMax - virtualMin);
        float returnValue = ((currentValue * ration) - (virtualMin * ration)) + actualMax;
        return returnValue;
    }
    //------------------------------------------------
    public void SetResolutions(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }// end of setResolutions
    /*
     * -----------------------------------------------------------------------------
     */
    public void setFullScreen(bool Fullscreen)
    {
        Screen.fullScreen = Fullscreen;
    }// end of Fullscreen

    /*
     * -----------------------------------------------------------------------------
     */

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }// end of Quality

    /*
     * -----------------------------------------------------------------------------
     */
    public void QuitGame()
    {
        Application.Quit();
    }// end of QuitGame

    /*
    * -----------------------------------------------------------------------------
    */

    public AudioMixer mixer;

    public void SetVolumeLevel(float sliderValue)
    {
        /*
         * represent the slider value as a log 10 and then multiply by 20. This will take the min and max value we set up in unity
         * and represent it is 0 to -80 on a logirthmic scale
         */
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }// end of SetVolumeLevel

    /*
    * -----------------------------------------------------------------------------
    */

    public GameObject Panel;
    public void OpenPanel()
    {
        Panel.SetActive(true);
    }// end of OpenPanel

    /*
    * -----------------------------------------------------------------------------
    */

}// end of Setting Class
