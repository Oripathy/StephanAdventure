using UnityEngine;

namespace UI.MainMenu
{
    internal class QuitButton : Button
    {
        public override void OnButtonPressed()
        {
            Application.Quit();
            Debug.Log("Bye");
        }
    }
}