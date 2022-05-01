using StageController;
using UnityEngine.SceneManagement;

namespace Menus
{
    internal class EasyDifficultyButton : Button
    {
        public override void OnButtonPressed()
        {
            _difficultySelector.SetDifficulty(Difficulty.Easy);
            SceneManager.LoadScene("Scenes/Fabric");
        }
    }
}