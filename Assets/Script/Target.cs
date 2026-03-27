using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody rb;
    public ParticleSystem explosionParticle;
   

    private float maxTorque = 3;
    public float minSpeed = 12;
    public float maxSpeed = 16;
    public float xSpawnRange = 5;
    public float ySpawnPos = -2;

    public int pointValue = 0;


    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(RandomForceDirection(), ForceMode.Impulse);
        rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        


    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!gameObject.CompareTag("Bad") && other.gameObject.CompareTag("Sensor"))
        {
            Destroy(gameObject);

            gameManager.AddLives(-1);
            
        }
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            gameManager.AddScore(pointValue);
            gameManager.DestroySound();
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            
        }
    }








    Vector3 RandomForceDirection()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return (Random.Range(-maxTorque, maxTorque));
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xSpawnRange, xSpawnRange), ySpawnPos);
    }
}
