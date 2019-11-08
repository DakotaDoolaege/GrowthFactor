using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Classes;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Image Image { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        this.Image = this.gameObject.GetComponent<Image>();

        this.Image.sprite = ApplicationTheme.CurrentTheme.GetBackIcon();
    }

    public void Refresh()
    {
        this.Image.sprite = ApplicationTheme.CurrentTheme.GetBackIcon();
    }
}
