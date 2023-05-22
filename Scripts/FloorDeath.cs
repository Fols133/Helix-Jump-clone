using UnityEngine;

public class FloorDeath : MonoBehaviour
{
    private PlayerController playerController;
    private LevelController levelController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        levelController = playerController.levelController;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // якщо в колізію підлоги смерті зайшов гравець та кількість пролетівших кілець менже ніж потрібно для бонусу то вбивати гравця 
        if (collision.gameObject.tag == "Player" && playerController.flewLapsRow < levelController.numberOfLaps)
        {
            levelController.PlayerDie();
        }
    }
}
