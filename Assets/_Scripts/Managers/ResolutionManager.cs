using System.Collections.Generic;
using _Scripts.Helpers;
using TMPro;
using UnityEngine;

namespace _Scripts.Managers
{
    public class ResolutionManager : MonoBehaviour
    {
        public TMP_Dropdown resolutionDropdown;
        Resolution[] _resolutions;

        void Start()
        {
            _resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);

                if (_resolutions[i].width == Screen.currentResolution.width &&
                    _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }
    
        public void SetResolution()
        {
            int resolutionIndex = resolutionDropdown.value;
            Resolution resolution = _resolutions[resolutionIndex];
            XLogger.Log(Category.Settings,$"Resolution set to {resolution.width} x {resolution.height}.");
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
