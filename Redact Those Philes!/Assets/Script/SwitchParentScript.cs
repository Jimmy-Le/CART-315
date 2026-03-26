using UnityEngine;

public class SwitchParentScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hi");
        transform.SetParent(other.transform);
    }


}
