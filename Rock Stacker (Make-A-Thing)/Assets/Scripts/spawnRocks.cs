using UnityEngine;
using UnityEngine.InputSystem;

public class spawnRocks : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputActionAsset inputActions;
    private InputAction spawn_action;
    
    // Camera Control
    public cameraMovement cameraControl;
    
   
    // Rock Prefabs
    public GameObject[] rocks;
    public GameObject baseRock;
    private GameObject chosenRock;
    
    // Sprites
    public SpriteRenderer currentSprite;
    private Color previewColor = new Color(1, 1, 1, 0.4f);
    private Color cooldownColor = new Color(0.8f, 0.8f, 1, 0.15f);

    // Rock Data
    private int randomRock = 0;
    private int rockCounter = 0;
    private float spawnCooldown = 1f;
    private float spawnTimer;
    
    private bool gameOver = false;
    
    void Start()
    {
        
    }

    void Awake()
    {
        spawn_action = inputActions.FindAction("Spawn");
        chosenRock = baseRock;
        ChangeSprite();
        transform.rotation = Quaternion.Euler(0, 0, 0);
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Lower Cooldown
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
            
            return;
        }
        else
        {
            currentSprite.color =  previewColor;
        }
        
        // Spawn if spawn is available and click
        if (!gameOver && spawn_action.IsPressed() && spawnTimer <= 0)
        {
            if (rockCounter == 0)
            {
                Instantiate(chosenRock, transform.position, Quaternion.Euler(0,0,0));
            }
            else
            {
                Instantiate(chosenRock, transform.position, Quaternion.Euler(0,0,90));
            }
            // Set the next random rock
            randomRock = Random.Range(0, rocks.Length);
            chosenRock = rocks[randomRock];
            //Change the sprite based on the next rock
            ChangeSprite();
            
            spawnTimer = spawnCooldown;
            currentSprite.color =  cooldownColor;

            cameraControl.IncreaseCameraHeight();
            rockCounter++;
        }
        
    }

    void ChangeSprite()
    {
        currentSprite.sprite = chosenRock.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = chosenRock.transform.localScale;
        transform.rotation = Quaternion.Euler(0, 0, 90);
        
    }
    
    public int GetRockCounter()
    {
        return rockCounter;
    }

    public void SetGameOver()
    {
        gameOver = true;
    }
}
