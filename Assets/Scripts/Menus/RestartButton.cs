using UnityEngine.SceneManagement;

namespace Menus
{
    internal class RestartButton : Button
    {
        public override void OnButtonPressed()
        {
            SceneManager.LoadScene("Scenes/MainMenuScene");
        }
    }
}