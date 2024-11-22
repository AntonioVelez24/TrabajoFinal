using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
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
    private float patrolSpeed = 12;
    private float chaseSpeed = 20;
    private NavMeshAgent agent;
    private Vector3 positionToMove;
    private int currentPoint = 0;
    private bool isPlayerDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();        
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
        agent.speed = patrolSpeed; 
        Vector3 point = walkPoints[currentPoint].position; 
        agent.SetDestination(point); Vector3 direction = point - transform.position; 

        if (agent.remainingDistance == 0f && !agent.pathPending) 
        {
            currentPoint = currentPoint + 1;
            Debug.Log(currentPoint);
        } 
        if(currentPoint >= walkPoints.Length)
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
            agent.speed = chaseSpeed;
        }
        //else
        //{
            //isPlayerDetected = false;
        //}
    }
    private void ChasePlayer()
    {
        Vector3 direction = detectedPlayer.position - transform.position;

        agent.SetDestination(detectedPlayer.position);

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * enemySpeed);
        }

    }
}
