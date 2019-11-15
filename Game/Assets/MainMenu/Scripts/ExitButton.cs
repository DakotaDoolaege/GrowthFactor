using UnityEngine;

namespace Assets.MainMenu.Scripts
{
    /// <summary>
    /// Class <c>ExitButton</c> takes care of exiting the application
    /// </summary>
    public class ExitButton : MonoBehaviour
    {
        /// <summary>
        /// Exits the application
        /// </summary>
        public void Exit()
        {
            Application.Quit(0);
        }
    }
}
