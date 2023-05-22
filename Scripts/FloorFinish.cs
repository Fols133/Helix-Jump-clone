using UnityEngine;

public class FloorFinish : MonoBehaviour
{
    private LevelController levelController;

    private void Start()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // якщо в колізію підлоги фінішу заходить гравець то робимо що гравець виграв та вимикаємо цей скрипт
        if (collision.gameObject.tag == "Player")
        {
            levelController.PlayerWin();
            GetComponent<FloorFinish>().enabled = false;
        }
    }
}
