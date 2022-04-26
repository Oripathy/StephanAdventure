using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    internal class EasyDifficultyButton : Button
    {
        public override void OnButtonPressed()
        {
            SceneManager.LoadScene("Scenes/EasyDifficultyLevel");
        }
    }
}