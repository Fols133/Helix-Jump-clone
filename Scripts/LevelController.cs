using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public GameObject[] level;
    public Text levelNumText;
    public GameObject player;
    public int numberOfLaps = 3;
    public Menu menuController;

    private Vector3 playerStart;
    private CylinderRotator cylinderRotator;
    private GameObject currentLevel;
    private Rigidbody playerRB;
    public int currentLevelNum = 0;
    
    void Start()
    {
        cylinderRotator = GetComponent<CylinderRotator>();
        playerStart = player.GetComponent<Transform>().position;
        playerRB = player.GetComponent<Rigidbody>();
        playerRB.isKinematic = true;
    }

    public void StartGame(int levelNum)
    {
        ClearLevel();

        currentLevel = Instantiate(level[levelNum]);
        currentLevelNum = levelNum;
        levelNumText.text = (levelNum + 1).ToString();

        currentLevel.transform.parent = transform;

        transform.rotation = Quaternion.identity;
        currentLevel.transform.rotation = Quaternion.identity;

        player.transform.position = playerStart;
        playerRB.isKinematic = false;
        cylinderRotator.enabled = true;
        menuController.diePanel.SetActive(false);
    }

    public void ClearLevel()
    {
        if (currentLevel != null)
            Destroy(currentLevel);
        currentLevel = null;
        playerRB.isKinematic = true;
        cylinderRotator.enabled = false;
        player.GetComponent<PlayerController>().isGameOver = false;
    }

    public void RestartGame()
    {
        StartGame(currentLevelNum);
    }

    public void PlayerDie()
    {
        player.GetComponent<PlayerController>().isGameOver = true;
        cylinderRotator.enabled = false;
        menuController.EnableDiePanel();
    }

    public void PlayerWin()
    {
        cylinderRotator.enabled = false;
        menuController.EnableNextLevelPanel();
    }
}
