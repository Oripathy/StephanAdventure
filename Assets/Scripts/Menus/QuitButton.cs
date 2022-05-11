using UnityEngine;

namespace Menus
{
    internal class QuitButton : Button
    {
        public override void OnButtonPressed()
        {
            Application.Quit();
            //Debug.Log("Bye");
        }
    }
}