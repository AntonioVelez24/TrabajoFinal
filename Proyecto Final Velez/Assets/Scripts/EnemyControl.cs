using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.AxisState;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private Transform[] walkPoints;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float detectionRange;
    [SerializeField] private float detectionAngle;
    [SerializeField] private Transform detectedPlayer;
    //[SerializeField] private float enemyDamage;
    private Vector3 positionToMove;
    private int currentPoint = 0;
    private bool isPlayerDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = walkPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {        
        if (isPlayerDetected)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
            DetectPlayer();
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
    private void DetectPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, detectedPlayer.position);

        Vector3 directionToPlayer = detectedPlayer.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (distanceToPlayer <= detectionRange && angleToPlayer <= detectionAngle)
        {
            isPlayerDetected = true;
        }
        //else
        //{
            //isPlayerDetected = false;
        //}
    }
    private void ChasePlayer()
    {
        Vector3 direction = detectedPlayer.position - transform.position;

        transform.position = Vector3.MoveTowards(transform.position, detectedPlayer.position, enemySpeed * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * enemySpeed);
        }

    }
}
