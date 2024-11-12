using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private Transform[] walkPoints;
    [SerializeField] private float speed;
    public float energy;
    public float restingTime;
    public float timer;
    public Vector2 positionToMove;
    public float speedMove;
    private int currentPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = walkPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        Transform target = walkPoints[currentPoint];
        while (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }            
        if (currentPoint < walkPoints.Length - 1)
        {
            currentPoint = currentPoint + 1;
        }
        else
        {
            currentPoint = 0;
        }
    }
    public void SetNewPosition(Vector2 newPosition)
    {
        positionToMove = newPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }
}
