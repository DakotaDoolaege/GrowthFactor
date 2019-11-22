using System.Collections;
using System.Collections.Generic;
using Assets.Resources.Classes;
using UnityEngine;
using UnityEngine.UI;

public enum ArrowButtonType {BACK, FORWARD}

public class BackButton : MonoBehaviour
{
    private Image Image { get; set; }
    public ArrowButtonType Type = ArrowButtonType.BACK;

    // Start is called before the first frame update
    void Start()
    {
        this.Image = this.gameObject.GetComponent<Image>();

        // this.Image.sprite = ApplicationTheme.CurrentTheme.GetBackIcon();
        this.Image.sprite = ApplicationTheme.CurrentTheme.GetArrowIcon(this.Type);
    }

    public void Refresh()
    {
        // this.Image.sprite = ApplicationTheme.CurrentTheme.GetBackIcon();
        this.Image.sprite = ApplicationTheme.CurrentTheme.GetArrowIcon(this.Type);
    }
}
