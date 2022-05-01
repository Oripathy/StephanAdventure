using StageController;
using UnityEngine.SceneManagement;

namespace Menus
{
    internal class NormalDifficultyButton : Button
    {
        public override void OnButtonPressed()
        {
            _difficultySelector.SetDifficulty(Difficulty.Normal);
            SceneManager.LoadScene("Scenes/Fabric");
        }
    }
}