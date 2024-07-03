using Modern_UI_Pack.Scripts.Dropdown;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class SettingsController : MonoBehaviour
{
    private CustomDropdown _customDropdown;
    private Resolution[] _resolutions;
    private Toggle _resolutionToggle;

    [Inject]
    private void Construct(CustomDropdown settingsDropdown, Toggle resolutionToggleExternal)
    {
        _customDropdown = settingsDropdown;
        _resolutionToggle = resolutionToggleExternal;
    }

    public void SetResolution()
    {
        Resolution resolution = _resolutions[_customDropdown.selectedItemIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool toggle)
    {
        Screen.fullScreen = toggle;
    }
    
    private void Start()
    {
        SetResolutionToggle();
        SetDropdownMenu();
    }

    private void SetResolutionToggle()
    {
        _resolutionToggle.isOn = Screen.fullScreen;
        _resolutionToggle.onValueChanged.AddListener(SetFullScreen);
    }

    private void SetDropdownMenu()
    {
        _resolutions = Screen.resolutions;
        _customDropdown.dropdownItems.Clear();
        
        int i = 0;
        foreach (Resolution resolution in _resolutions)
        {
            _customDropdown.CreateNewItem($"{resolution.width}x{resolution.height} @ {resolution.refreshRate}Hz");

            UnityEvent currentEvent = _customDropdown.dropdownItems[i].OnItemSelection = new UnityEvent();
            currentEvent.AddListener(SetResolution);

            if (resolution.width == Screen.currentResolution.width &&
                resolution.height == Screen.currentResolution.height)
            {
                _customDropdown.ChangeDropdownInfo(i);
            }

            i++;
        }
    }
}

