using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoresTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private Database DB;

    private void Awake()
    {
        this.entryContainer = transform.Find("EntryContainer");
        this.entryTemplate = this.entryContainer.Find("EntryTemplate");

        // THis is the database script, of which there should only be ONE instance
        this.DB = GameObject.Find("DBScript").GetComponent<Database>();

        IList scores = (ArrayList) this.DB.TopSingle();

        this.entryTemplate.gameObject.SetActive(false);

        float templateHeight = 20f;
        for (int i = 0; i < scores.Count; i++)
        {
            // Create and place entries
            Transform entryTransform = Instantiate(this.entryTemplate, this.entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i - 80);
            entryTransform.gameObject.SetActive(true);

            // Set entry text values to DB query results
            IList record = (ArrayList) scores[i];
            entryTransform.Find("entryName").GetComponent<Text>().text = (string) record[2];
            entryTransform.Find("entryScore").GetComponent<Text>().text = (string) record[0];
            entryTransform.Find("entryLevel").GetComponent<Text>().text = (string) record[1];
        }
    }

    // Used by "Back" button to go back to main menu
    public void MainMenu()
    {
        SceneManager.LoadScene(0); 
    }
}
