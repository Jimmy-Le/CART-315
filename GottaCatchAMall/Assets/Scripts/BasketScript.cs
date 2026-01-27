using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class BasketScript : MonoBehaviour
{
    // Objects
    public Transform basketTransform;
    
    public TextMeshProUGUI scoreText;
    
    // Input Action Stuff
    public InputActionAsset inputAction;
    private InputAction move_action;

    // Properties
    public float speed = 6f;
    public float score;
    private float limit = 9f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
    }

    void Awake()
    {
        move_action = inputAction.FindAction("Move");

    }

    // Update is called once per frame
    void Update()
    {
        if (move_action.IsPressed())
        {
            
            Vector2 direction = move_action.ReadValue<Vector2>();

            if (direction.x < 0 && basketTransform.position.x < -limit || direction.x > 0 && basketTransform.position.x > limit)
            {
                
            }
            else
            {
                basketTransform.Translate(direction.x * speed * Time.deltaTime, 0, 0, Space.World);
            }
        } 
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Mall")
        {
            score += 1;
        }
        else
        {
            score -= 1;
        }

        scoreText.text = $"Score: {score}";

        Destroy(other.gameObject);
    }
    
    
}
