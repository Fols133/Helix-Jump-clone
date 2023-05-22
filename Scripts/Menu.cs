using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public LevelController levelController;
    public GameObject mainMenu;
    public GameObject buttonMainMenu;
    public GameObject levelMenu;
    public GameObject nextLevelPanel;
    public GameObject nextLevelButton;
    public GameObject diePanel;

    private AudioSource audioSourceMainMenu;
    public List<Button> levelButtons = new List<Button>();
    public Sprite lockImageLevel;
    public Sprite defaultImagelevel;
    public Color colorTextLock;

    private void Start()
    {
        mainMenu.SetActive(true);
        LoadSelectionLevel(false);
        audioSourceMainMenu = GetComponent<AudioSource>();
        StartCoroutine(EnableSoundMusic());
        // якщо гравець перший раз зайшов в гру то записуємо перший рівень
        if (PlayerPrefs.HasKey("LevelNum") == false)
            PlayerPrefs.SetInt("LevelNum", 0);
        PlayerPrefs.Save();
    }

    public void StartGame()
    {
        StartLevel(PlayerPrefs.GetInt("LevelNum"));
    }

    public void LoadSelectionLevel(bool load)
    {
        // для перемикання між меню рівнями та головним 
        if (load)
        {
            // спочатку відчиняємо всі рівні, а далі робимо зачиненими рівні які вище ніж скільки відчинено у гравця
            LevelButtonActivity(0, true, defaultImagelevel, Color.white);
            int level = PlayerPrefs.GetInt("LevelNum") + 1;
            LevelButtonActivity(level, false, lockImageLevel, colorTextLock);
        }
        buttonMainMenu.SetActive(!load);
        levelMenu.SetActive(load);
    }

    // Метод зміни кнопок для відчинення/зачинення рівнів
    private void LevelButtonActivity(int level,bool active,Sprite imageSprite,Color color)
    {
        while (level < levelButtons.Count)
        {
            levelButtons[level].image.sprite = imageSprite;
            levelButtons[level].GetComponentInChildren<Text>().color = color;
            levelButtons[level].interactable = active;
            level++;
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void StartLevel(int lvlNum)
    {
        levelController.StartGame(lvlNum);
        mainMenu.SetActive(false);
       StartCoroutine(DisableSoundMusic());
    }

    public void ClearLevelCompleted()
    {
        PlayerPrefs.SetInt("LevelNum", 0);
        PlayerPrefs.Save();
    }

    public void OpenAllLevel()
    {
        PlayerPrefs.SetInt("LevelNum", 9);
        PlayerPrefs.Save();
    }

    public void NextLevel()
    {
        SaveNewLevel();

        nextLevelPanel.SetActive(false);
        StartLevel(PlayerPrefs.GetInt("LevelNum"));
    }

    public void RestartLevel()
    {
        nextLevelPanel.SetActive(false);
        StartGame();
    }

    public void GoMenu()
    {
        SaveNewLevel();
        mainMenu.SetActive(true);
        nextLevelPanel.SetActive(false);
        LoadSelectionLevel(false);
        levelController.ClearLevel();
        StartCoroutine(EnableSoundMusic());

    }

    public void EnableNextLevelPanel()
    {
        // якщо це останій рівень то вимикаємо кнопку перейти на наступний рівень
        if (levelController.currentLevelNum == levelButtons.Count - 1)
        {
            nextLevelButton.SetActive(false);
        }
        
        nextLevelPanel.SetActive(true);
    }

    public void EnableDiePanel()
    {
        diePanel.SetActive(true);
    }

    private void SaveNewLevel()
    {
        int lastOpenLevel = PlayerPrefs.GetInt("LevelNum");
        int nextLevel = levelController.currentLevelNum + 1;
        if (lastOpenLevel < nextLevel && nextLevel < levelButtons.Count)
        {
            PlayerPrefs.SetInt("LevelNum", nextLevel);
            PlayerPrefs.Save();
        }
    }

    
    private IEnumerator EnableSoundMusic()
    {
        // корутина для збільшення гучності музики
        audioSourceMainMenu.volume = 0f;
        audioSourceMainMenu.Play();
        while (audioSourceMainMenu.volume < 1f)
        {
            audioSourceMainMenu.volume += 0.05f;
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    private IEnumerator DisableSoundMusic()
    {
        // корутина для зменшення гучності музики
        while (audioSourceMainMenu.volume >= 0.1f)
        {
            audioSourceMainMenu.volume -= 0.1f;
            yield return new WaitForSecondsRealtime(0.5f);
        }
        audioSourceMainMenu.Stop();
    }
}
