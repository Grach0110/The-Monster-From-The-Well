using UnityEngine;

public class Platform_Script : MonoBehaviour
{
    public Transform[] points;
    int randomPoint;
    public float speed; // Скорость платформы

    public float startTime;
    float waitTime;

    private void Start()
    {
        waitTime = startTime;
        randomPoint = Random.Range(0, points.Length);
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[randomPoint].transform.position, speed * Time.deltaTime);

        if (Vector2.Distance (transform.position, points[randomPoint].transform.position) <0.2f)
        {
            if (waitTime <= 0)
            {
                randomPoint = Random.Range(0, points.Length);
                waitTime = startTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}