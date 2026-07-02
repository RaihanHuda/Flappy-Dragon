using UnityEngine;
public class GroundLoop : MonoBehaviour
{
    public float speed = 3f;  
    public float tileWidth = 20f; 
    public float leftLimit = -20f; 

    void Update()
    {
        if (GameManager.Instance.CurrentState == GameManager.State.GameOver) return;

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= leftLimit)
            transform.position += new Vector3(tileWidth * 2f, 0f, 0f);
    }
}
