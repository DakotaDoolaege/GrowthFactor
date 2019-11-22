using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class <c>BackToMenu</c> is to be used in the Tutorial scene. It is added to
/// the back button to cycle through images and get back to the menu. This class
/// gets the currently displayed Tutorial object and, if the user presses the
/// back button that this class is attached to while the first tutorial image
/// is shown, this class will change the scene back to the main menu.
/// </summary>
public class BackToMenu : MonoBehaviour
{
    Tutorial Images { get; set; }
    MainMenu Menu { get; set; }

    /// <summary>
    /// Start is called before the first frame
    /// </summary>
    void Start()
    {
        this.Images = FindObjectOfType<Tutorial>().gameObject.GetComponent<Tutorial>(); 
        this.Menu = FindObjectOfType<MainMenu>().gameObject.GetComponent<MainMenu>();
    }

    /// <summary>
    /// Checks if the tutorial is at the beginning. If it is and the user presses
    /// the button this class is attached to, then this method will return the
    /// user to the main menu scene.
    /// </summary>
    public void ToMenu()
    {
        if (this.Images.AtBeginning)
        {
            this.Menu.ReturnToMenu();
        }
    }
}
