using UnityEngine;

public class Spikes_Saw_Script : MonoBehaviour
{
    public float speed = 0.5f; // скорость

    public Transform point; // точка патруля
    public float distancePatrol; // дистанция
    public bool horizontal = false; 
    public bool vertical = false;
    private bool revers = false;

    private void Update()
    {
        if (horizontal)
        {
            if (transform.position.x > point.transform.position.x + distancePatrol)
            {
                revers = false;
            }
            else if (transform.position.x < point.transform.position.x - distancePatrol)
            {
                revers = true;
            }

            if (revers)
            {
                transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            }
        }

        if (vertical)
        {
            if (transform.position.y > point.transform.position.y + distancePatrol)
            {
                revers = false;
            }
            else if (transform.position.y < point.transform.position.y - distancePatrol)
            {
                revers = true;
            }

            if (revers)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
            }
        }
    }
}