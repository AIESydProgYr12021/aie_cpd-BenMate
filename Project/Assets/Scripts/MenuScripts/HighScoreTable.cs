using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{

    [SerializeField] Transform entryContainer;
    [SerializeField] Transform entryTemplate;
    [SerializeField] float templateHeight = 50.0f;

    List<Transform> highscoreEntryTransformList;
    private void Awake()
    {
        entryContainer = GameObject.Find("HighscoreContainer").transform;
        entryTemplate = GameObject.Find("HighscoreTemplate").transform;

        entryTemplate.gameObject.SetActive(false);

        //attempt to add a new entry

        AddHighscoreEntry(100, "TEST");

       string jsonString = PlayerPrefs.GetString("highscoreTable");
       Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores.highscoreEntryList.Count > 0)
        {
            //sorts entry list by score            
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
                {
                    if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                    {
                        //swap
                        HighscoreEntry tmp = highscores.highscoreEntryList[i];
                        highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                        highscores.highscoreEntryList[j] = tmp;
                    }
                }
            }

            //updates the List onto the 
            highscoreEntryTransformList = new List<Transform>();
            foreach (HighscoreEntry item in highscores.highscoreEntryList)
            {
                CreateHighscoreEntryTransform(item, entryContainer, highscoreEntryTransformList);
            }
        }      
    }

    void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);

        entryTransform.gameObject.SetActive(true);   
        
        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;

        }

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        int score = highscoreEntry.score;

        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
        string name = highscoreEntry.name;

        entryTransform.Find("NameText").GetComponent<Text>().text = name;
        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name)
    {
        //create highscoreEntry
        HighscoreEntry highScoreEntry = new HighscoreEntry { score = score, name = name };

        //load saved highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        bool scoreAdded = false;

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            if (highScoreEntry.score > highscores.highscoreEntryList[i].score)
            {
                highscores.highscoreEntryList.Insert(i, highScoreEntry);
                scoreAdded = true;
                break;
            }
        }

        if (!scoreAdded)
        {
            //add new entry to highscores
            highscores.highscoreEntryList.Add(highScoreEntry);
        }

        //update list
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    //Represents a single high score entry
    //[System.Serializable]
    public class HighscoreEntry
    {
        public int score;
        public string name;
    }

}
