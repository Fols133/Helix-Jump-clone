using UnityEngine;

public class Circle : MonoBehaviour
{
    public float secDestroy = 3;
    public GameObject[] childCircle;

    private Transform player;
    private float radius = 1.4f;
    private float forceDecay = 400;
    private PlayerController playerController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.gameObject.GetComponent<PlayerController>();
    }


    void Update()
    {
        // перевірка що висота гравця менше ніж кільця 
        if (transform.position.y > player.position.y)
        {
            DestroyCircle();
            playerController.AddSpan();
        }
    }

    public void DestroyCircle()
    {
        foreach (GameObject floor in childCircle)
        {
            Rigidbody rb = floor.GetComponent<Rigidbody>();

            floor.GetComponent<BoxCollider>().isTrigger = true;

            rb.isKinematic = false;
            rb.AddExplosionForce(forceDecay, transform.position, radius, 3.0F);

        }
        this.enabled = false;
        Destroy(gameObject, secDestroy);
    }
}
