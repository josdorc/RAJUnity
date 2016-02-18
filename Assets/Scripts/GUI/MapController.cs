using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapController : MonoBehaviour {

    public string SelectedLevel;
   
	// Use this for initialization
	void Start () {

    }
	
    public void SetupMap()
    {
       transform.FindChild("RadarCircle").GetComponent<Image>().enabled = false;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform levelNode = transform.GetChild(i);
            LevelData level = Data.Game.GetLevel(levelNode.name);
            
            if (levelNode.name == "RadarCircle")
                continue;

            levelNode.FindChild("Text").GetComponent<RectTransform>().rotation = Quaternion.identity;

            if (level.IsVisible)
            {
                levelNode.GetComponent<Image>().color = level.IsVisited ? new Color(0,1,0,0.75f) : Color.gray;
                levelNode.FindChild("Text").GetComponent<Text>().text = level.HasCheckPoint ? "S" : string.Empty;
            }
            else
            {
                levelNode.GetComponent<Image>().enabled = false;
                levelNode.FindChild("Text").GetComponent<Text>().text = string.Empty;
            }

            if (level.IsDestiny)
            {
                transform.FindChild("RadarCircle").transform.position = levelNode.transform.position;
                transform.FindChild("RadarCircle").GetComponent<Image>().enabled = true;
            }
        }

        Transform currentLevel = transform.FindChild(Application.loadedLevelName);
        currentLevel.GetComponent<Animator>().enabled = true;
    }

    public void OnLevelPressed(string levelName)
    {
        Transform levelNode = transform.FindChild(levelName);
        LevelData level = Data.Game.GetLevel(levelNode.name);

        if (level.IsVisited && level.HasCheckPoint)
        {
            SelectedLevel = levelNode.name;
            transform.parent.FindChild("Teleport").GetComponent<Image>().color = Color.white;
        }
        else
        {
            SelectedLevel = string.Empty;
            transform.parent.FindChild("Teleport").GetComponent<Image>().color = new Color(1,1,1,0.5f);
        }
    }
}
