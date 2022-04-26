using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    internal class NormalDifficultyButton : Button
    {
        public override void OnButtonPressed()
        {
            SceneManager.LoadScene("Scenes/MainMenuScene");
        }
    }
}