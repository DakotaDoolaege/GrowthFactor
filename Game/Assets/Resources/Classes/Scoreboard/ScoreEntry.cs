using System;
using Assets.Resources.Classes;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreEntry : MonoBehaviour
{
    /**
     * TODO:
     * - Make ScoreEntry prefab have a ScoreEntry class and a TextButton class
     * - The TextButton class will set the correct sprite to use
     * - The ScoreEntry will modify the size and add the appropriate text to the TextButton
     * - Follow the buttons on the main menu to find out how to do the TextButton
     * 
     * 
     * - Modify prefab so that it grows when a player touches it or hovers over it
     */

    public Image Icon { get; set; }
    private TextMeshProUGUI Text { get; set; }
    public int PlayerNumber;

    // Start is called before the first frame update
    public void Start()
    {
        this.Icon = this.gameObject.transform.Find("Image").GetComponent<Image>();
        this.Icon.sprite = ApplicationTheme.CurrentTheme.GetButtonLarge();
        RectTransform image = this.Icon.transform as RectTransform;
        image.sizeDelta = new Vector2(253.0f, 80.0f);

        this.Text = this.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>();

        try
        {
            this.SetText();
            this.Text.transform.position = image.position;
            this.gameObject.SetActive(true);
        }
        catch (ArgumentOutOfRangeException)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void SetText()
    {
        IList<string> entries = Scores.GetScore(PlayerNumber);
        string entryText = (this.PlayerNumber + 1) + ".\t" + entries[2] + "\n";
        entryText += "\tScore: " + entries[0] + "\n";
        entryText += "\tLevel: " + entries[1];

        this.Text.text = entryText;
    }
}
