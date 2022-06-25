using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderBrightnessSettings : MonoBehaviour
{
    public Slider slider;
    public Text txtSlider;
    [Tooltip("The Actual maximum value for your settings. The user CANT see this.")]
    public float maxSettingsValue;
    [Tooltip("The Actual minimum value for your settings. The user CANT see this.")]
    public float minSettingsValue;
}
