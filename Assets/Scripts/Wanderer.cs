using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Wanderer : MonoBehaviour
{
    [Tooltip("Minimum time the character will walk in one direction")]
    public float minWalkTime = 1.5f;

    [Tooltip("Maximum time the character will walk in one direction")]
    public float maxWalkTime = 3f;

    [Tooltip("Chance to idle instead of moving")]
    [Range(0f, 1f)]
    public float idleChance = 0.3f;

    [Tooltip("Minimum idle time")]
    public float minIdleTime = 1f;

    [Tooltip("Maximum idle time")]
    public float maxIdleTime = 2.5f;

    [Tooltip("Detection distance for obstacles")]
    public float obstacleDetectionDistance = 2f;

    private CharacterController characterController;
    private float walkTimer;
    private float idleTimer;
    private bool isIdle;
    private Vector3 moveDirection;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        StartCoroutine(WanderRoutine());
    }

    private void Update()
    {
        if (isIdle)
        {
            characterController.ForwardInput = 0f;
            characterController.StrafeInput = 0f;
            characterController.TurnInput = 0f;
        }
        else
        {
            // Obstacle detection
            if (Physics.Raycast(transform.position, transform.forward, obstacleDetectionDistance))
            {
                // Rotate away from obstacle
                float randomTurn = Random.Range(-90f, 90f);
                transform.Rotate(0f, randomTurn, 0f);
            }

            characterController.ForwardInput = 1f;
            characterController.TurnInput = 0f;
        }
    }

    private IEnumerator WanderRoutine()
    {
        while (true)
        {
            // Decide whether to idle or move
            isIdle = Random.value < idleChance;

            if (isIdle)
            {
                idleTimer = Random.Range(minIdleTime, maxIdleTime);
                yield return new WaitForSeconds(idleTimer);
            }
            else
            {
                walkTimer = Random.Range(minWalkTime, maxWalkTime);

                // Choose a random direction to move in
                float randomAngle = Random.Range(-180f, 180f);
                transform.Rotate(0f, randomAngle, 0f);

                float timer = 0f;
                while (timer < walkTimer)
                {
                    timer += Time.deltaTime;
                    yield return null;
                }
            }
        }
    }
}
