using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    [SerializeField] private Transform[] walkPoints;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float detectionRange;
    [SerializeField] private float detectionAngle;
    [SerializeField] private Transform detectedPlayer;
    [SerializeField] private PlayerControl playerControl;
    //[SerializeField] private float enemyDamage;

    private AudioSource _audioSource;
    private float patrolSpeed = 6;
    private float chaseSpeed = 10;
    private NavMeshAgent agent;
    private Vector3 positionToMove;
    private int currentPoint = 0;
    private bool isPlayerDetected = false;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();        
    }

    // Update is called once per frame
    void Update()
    {        
        if (isPlayerDetected == true)
        {
            ChasePlayer();
        }
        else if (isPlayerDetected == false)
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
        } 
        if (currentPoint >= walkPoints.Length)
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
            if (playerControl.isHiding == false)
            {
                isPlayerDetected = true;                
                agent.speed = chaseSpeed;
                _audioSource.Play();
            }              
        }
        else if (playerControl.isHiding == true)
        {
            isPlayerDetected = false;
        }
    }
    private void ChasePlayer()
    {        
        Vector3 direction = detectedPlayer.position - transform.position;
        agent.SetDestination(detectedPlayer.position);       
        if (playerControl.isHiding == true)
        {
            isPlayerDetected = false;
            Patrol();
        }
    }
}
