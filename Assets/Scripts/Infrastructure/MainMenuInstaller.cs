using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Infrastructure
{
    public enum Canvases
    {
        MainMenu,
        Settings,
        GameConfig
    }
    
    public enum Sliders {
        CarnivoresSlider,
        HerbivoresSlider,
        MutationSlider
    }

    public class MainMenuInstaller : MonoInstaller
    {
        public CustomDropdown resolutionsDropdown;
        public Toggle resolutionToggle;
        public Canvas mainMenuCanvas;
        public Canvas settingsCanvas;
        public Canvas gameConfigCanvas;

        public SliderManager carnivoresSlider;
        public SliderManager herbivoresSlider;
        public SliderManager mutationSlider;

        public override void InstallBindings()
        {
            Container
                .Bind<CustomDropdown>()
                .FromInstance(resolutionsDropdown);

            Container
                .Bind<Toggle>()
                .FromInstance(resolutionToggle);

            Container
                .Bind<Canvas>()
                .WithId(Canvases.MainMenu)
                .FromInstance(mainMenuCanvas);

            Container
                .Bind<Canvas>()
                .WithId(Canvases.Settings)
                .FromInstance(settingsCanvas);

            Container
                .Bind<Canvas>()
                .WithId(Canvases.GameConfig)
                .FromInstance(gameConfigCanvas);

            Container
                .Bind<SliderManager>()
                .WithId(Sliders.CarnivoresSlider)
                .FromInstance(carnivoresSlider);
            
            Container
                .Bind<SliderManager>()
                .WithId(Sliders.HerbivoresSlider)
                .FromInstance(herbivoresSlider);
            
            Container
                .Bind<SliderManager>()
                .WithId(Sliders.MutationSlider)
                .FromInstance(mutationSlider);

        }
    }
}