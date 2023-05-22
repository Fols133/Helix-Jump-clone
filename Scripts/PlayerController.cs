using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float forceJump;
    public bool isGameOver;
    public LevelController levelController;
    public int flewLapsRow = 0;

    private AudioSource playerJump;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerJump = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isGameOver != true)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, forceJump * Time.deltaTime, rigidbody.velocity.z);
            playerJump.Play();

            // перевірка кількісті кілець, що пролетіли
            if (flewLapsRow >= levelController.numberOfLaps)
            {
                collision.gameObject.GetComponentInParent<Circle>().DestroyCircle();
            }

            flewLapsRow = 0;
        }

    }

    // додавання пролетівших кілець
    public void AddSpan()
    {
        flewLapsRow++;
    }
}
