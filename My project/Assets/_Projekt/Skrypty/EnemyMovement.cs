using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public float speed = 2f;

    
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Update()
    {
        
        if (currentWaypointIndex < waypoints.Length)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);

            
            if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            
            GameObject gameBase = GameObject.FindWithTag("Base");

            if (gameBase != null)
            {
                BaseHealth baseHealth = gameBase.GetComponent<BaseHealth>();
                if (baseHealth != null)
                {
                    baseHealth.TakeDamage(10f);
                }
            }

            Destroy(gameObject);
        }
    }

}
