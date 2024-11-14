using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private Transform[] walkPoints;
    [SerializeField] private float enemySpeed;
    //[SerializeField] private float enemyDamage;
    private Vector3 positionToMove;
    private int currentPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = walkPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameControl.Instance.GameOver();
        }
    }
    private void Patrol()
    {
        Transform target = walkPoints[currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);

        Vector3 direction = target.position - transform.position;
        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * enemySpeed);
        }

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentPoint = currentPoint + 1;
        }
        if (walkPoints.Length <= currentPoint)
        {
            currentPoint = 0;
        }
    }
}
