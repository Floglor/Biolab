using Infrastructure;
using Modern_UI_Pack.Scripts.Slider;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameConfigController : MonoBehaviour
{
    private SliderManager _carnivoresSlider;
    private SliderManager _herbivoresSlider;
    private SliderManager _mutationSlider;
    
    [Inject]
    public void Construct(
    [Inject (Id = Sliders.CarnivoresSlider)] SliderManager carnivoresSliderInjected,
    [Inject (Id = Sliders.HerbivoresSlider)] SliderManager herbivoresSliderInjected,
    [Inject (Id = Sliders.MutationSlider)] SliderManager mutationSliderInjected)
    {
        _carnivoresSlider = carnivoresSliderInjected;
        _herbivoresSlider = herbivoresSliderInjected;
        _mutationSlider = mutationSliderInjected;
    }
    public void BeginGame()
    {
        PlayerPrefs.SetFloat(Config.MutationModifier.ToString(), _mutationSlider.mainSlider.value);
        Debug.Log(_mutationSlider.mainSlider.value);
        
        PlayerPrefs.SetInt(Config.CarnivoresNumber.ToString(), (int) _carnivoresSlider.mainSlider.value);
        Debug.Log(_carnivoresSlider.mainSlider.value);
        
        PlayerPrefs.SetInt(Config.HerbivoresNumber.ToString(), (int) _herbivoresSlider.mainSlider.value);
        Debug.Log(_herbivoresSlider.mainSlider.value);

        SceneManager.LoadSceneAsync("SampleScene");

    }
}
