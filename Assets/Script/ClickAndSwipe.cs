using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{

    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;

    private bool swiping;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping)
            {
                UpdateMousePosition();
            }


        }
        
    }

    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        transform.position = mousePos;
    }
    
    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }

    private void OnTriggerEnter(Collider other )
    {
        if (other.gameObject.GetComponent<Target>())
        {
            if (other.gameObject.CompareTag("Bad"))
            {
                gameManager.AddLives(-1);
            }
            other.gameObject.GetComponent<Target>().DestroyTarget();

            
        }
    }
}
