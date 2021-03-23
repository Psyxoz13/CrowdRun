using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public int CurrentLevelIndex;
    public List<Level> Levels = new List<Level>();

    public void SelectLevel(int levelIndex)
    {
        levelIndex = GetCorrectedIndex(levelIndex);

        var level = Levels[levelIndex];

        if (level.LevelObject)
        {
            SelLevelParams(level);

            CurrentLevelIndex = levelIndex;
        }
    }

    public void SelectLevel(Level level)
    {
        var levelIndex = Levels.IndexOf(level);
        SelectLevel(GetCorrectedIndex(levelIndex));
    }

    public void NextLevel()
    {
        SelectLevel(++CurrentLevelIndex);
    }

    public void PrevLevel()
    {
        SelectLevel(--CurrentLevelIndex);
    }

    private int GetCorrectedIndex(int levelIndex)
    {
        if (levelIndex > Levels.Count - 1)
        {
            levelIndex = Levels.Count - 1;
        }

        if (levelIndex < 0)
        {
            levelIndex = 0;
        }

        return levelIndex;
    }

    private void SelLevelParams(Level level)
    {
        if (level.LevelObject)
        {
            DeactivateLevels();
            level.LevelObject.SetActive(true);
        }

        if (level.SkyboxMaterial)
        {
            RenderSettings.skybox = level.SkyboxMaterial;
        }
    }

    private void DeactivateLevels()
    {
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].LevelObject.SetActive(false);     
        }
    }
}

[System.Serializable]
public class Level
{
    public GameObject LevelObject;
    public Material SkyboxMaterial; 
}
