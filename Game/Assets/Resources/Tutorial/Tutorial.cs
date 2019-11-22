using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour
{
    IList<Sprite> TutorialImages { get; set; }
    SpriteRenderer Renderer { get; set; }
    private int Index { get; set;} = 0;
    private Sprite CurrentImage { get => this.TutorialImages[this.Index]; }

    // Start is called before the first frame update
    void Start()
    {
        this.Renderer = this.gameObject.GetComponent<SpriteRenderer>();

        this.TutorialImages = new List<Sprite>();
        for (int i = 1; i < 6; i++)
        {
            Sprite Sprite = UnityEngine.Resources.Load<Sprite>("Tutorial/Tutorial_" + i);
            this.TutorialImages.Add(Sprite);
        }

        this.Renderer.sprite = this.TutorialImages[this.Index];
    }

    public void NextImage()
    {
        if (this.Index + 1 < this.TutorialImages.Count)
        {
            this.Index++;
        }
        this.Renderer.sprite = this.CurrentImage;
    }

    public void PreviousImage()
    {
        if (this.Index - 1 >= 0)
        {
            this.Index--;
        }
        this.Renderer.sprite = this.CurrentImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
