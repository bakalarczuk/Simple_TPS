using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Vector3[] Positions;
    [SerializeField]
    private float DockDuration = 2f;
    [SerializeField]
    private float MoveSpeed = 0.01f;

    private void Start()
    {
        StartCoroutine(MovePlatform());
    }
    private IEnumerator MovePlatform()
    {
        transform.position = Positions[0];
        int positionIndex = 0;
        int lastPositionIndex;
        WaitForSeconds Wait = new WaitForSeconds(DockDuration);

        while (true)
        {
            lastPositionIndex = positionIndex;
            positionIndex++;
            if (positionIndex >= Positions.Length)
            {
                positionIndex = 0;
            }

            Vector3 platformMoveDirection = (Positions[positionIndex] - Positions[lastPositionIndex]).normalized;
            float distance = Vector3.Distance(transform.position, Positions[positionIndex]);
            float distanceTraveled = 0;
            while (distanceTraveled < distance)
            {
                transform.position += platformMoveDirection * MoveSpeed;
                distanceTraveled += platformMoveDirection.magnitude * MoveSpeed;
                yield return null;
            }

            yield return Wait;
        }
    }

	public void OnCollisionEnter(Collision collision)
	{
        collision.other.transform.SetParent(transform);
	}

	public void OnCollisionExit(Collision collision)
	{
        collision.other.transform.SetParent(null);
    }
}