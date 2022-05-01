using StageController;
using UnityEngine.SceneManagement;

namespace Menus
{
    internal class HardDifficultyButton : Button
    {
        public override void OnButtonPressed()
        {
            _difficultySelector.SetDifficulty(Difficulty.Hard);
            SceneManager.LoadScene("Scenes/Fabric");
        }
    }
}