using UnityEngine.SceneManagement;

namespace UI
{
    internal class RestartButton : Button
    {
        public override void OnButtonPressed()
        {
            SceneManager.LoadScene("Scenes/MainMenuScene");
        }
    }
}