using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    internal class HardDifficultyButton : Button
    {
        public override void OnButtonPressed()
        {
            SceneManager.LoadScene("Scenes/MainMenuScene");
        }
    }
}