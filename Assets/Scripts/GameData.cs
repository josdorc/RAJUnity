using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot]
public class GameData
{
    [XmlElement]
    public PlayerData Player;
    [XmlElement]
    public string CheckPointLevel;    
    [XmlElement]
    public bool HasWarp;
    [XmlElement]
    public Vector3 WarpPos;
    [XmlElement]
    public bool WarpFlip;
    [XmlArray]
    public LevelData[] Levels;

    public GameData()
    {
        Player = new PlayerData() { Health = 4, Stars = 0, Coins = 0, Gems = 0};
        Levels = new LevelData[Constants.MAX_LEVELS];

        Levels[0] = new LevelData() { SceneName = "Home" };
        Levels[1] = new LevelData() { SceneName = "Forest1", IsVisible = true };
        Levels[2] = new LevelData() { SceneName = "Forest2" };
        Levels[3] = new LevelData() { SceneName = "Forest3", HasCheckPoint = true };
        Levels[4] = new LevelData() { SceneName = "Forest4" };
        Levels[5] = new LevelData() { SceneName = "Forest5" };
        Levels[6] = new LevelData() { SceneName = "Forest6", HasCheckPoint = true };
        Levels[7] = new LevelData() { SceneName = "Forest7" };
        Levels[8] = new LevelData() { SceneName = "Forest8" };
        Levels[9] = new LevelData() { SceneName = "Forest9", HasCheckPoint = true, IsDestiny = true };
        Levels[10] = new LevelData() { SceneName = "Forest10" };
        Levels[11] = new LevelData() { SceneName = "Sand1" };
    }

    public LevelData CurrentLevel
    {
        get
        {
            return GetLevel(Application.loadedLevelName);
        }
    }

    public LevelData GetLevel(string name)
    {
        for(int i=0; i < Constants.MAX_LEVELS; i++)
        {
            if (Levels[i] != null && Levels[i].SceneName == name)
                return Levels[i];            
        }

        return null;
    }

    public void SetLevelVisited(string name)
    {
        for (int i = 0; i < Constants.MAX_LEVELS; i++)
        {
            if (Levels[i] != null && Levels[i].SceneName == name)
            {
                Levels[i].IsVisited = true;
                Levels[i].IsVisible = true;
                Levels[i].IsDestiny = false;
                break;
            }
        }
    }

    public void SetZoneVisible(string zone)
    {
        for (int i = 0; i < Constants.MAX_LEVELS; i++)
        {
            if (Levels[i] != null && Levels[i].SceneName.StartsWith(zone))
                Levels[i].IsVisible = true;
        }
    }
}

[XmlType]
public class PlayerData
{
    [XmlElement]
    public int Health;
    [XmlElement]
    public int Stars;
    [XmlElement]
    public int Coins;
    [XmlElement]
    public int Gems;
    [XmlElement]
    public EAttackType CurrentAttack = EAttackType.Shuriken; 
    [XmlElement]
    public bool YellowKey = true;
    [XmlElement]
    public bool HasForestMap;
}

[XmlType]
public class LevelData
{
    [XmlElement]
    public string SceneName;
    [XmlElement]
    public bool IsVisited;
    [XmlElement]
    public bool IsVisible;
    [XmlElement]
    public bool IsDestiny;
    [XmlElement]
    public bool HasCheckPoint;
    [XmlElement]
    public bool GemFound = false;
}

public static class Data 
{
    public static int GameIndex = 0;
    private static GameData[] m_Games;

    public static GameData Game
    {
        get
        {
            if (m_Games == null)
                m_Games = new GameData[Constants.MAX_GAMES];

            if (m_Games[GameIndex] == null)
                m_Games[GameIndex] = LoadGameData(GameIndex);

            return m_Games[GameIndex];
        }
    }

    private static GameData LoadGameData(int i)
    {
        XmlSerializer xmls = new XmlSerializer(typeof(GameData));

        if (PlayerPrefs.HasKey(i.ToString()))
            return xmls.Deserialize(new StringReader(PlayerPrefs.GetString(i.ToString()))) as GameData;
        else
            return new GameData();
    }
    
    public static void ResetGame()
    {
        PlayerPrefs.DeleteKey(GameIndex.ToString());
    }

    public static void SaveGame()
    {
        XmlSerializer xmls = new XmlSerializer(typeof(GameData));
        StringWriter sw = new StringWriter();
        xmls.Serialize(sw, m_Games[GameIndex]);
        string xml = sw.ToString();
        Debug.Log(xml);
        PlayerPrefs.SetString(GameIndex.ToString(), xml);

        PlayerPrefs.Save();
    }
}
