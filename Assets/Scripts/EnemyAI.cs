using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    CharacterController controller;
    CharacterMovement_Basic player;
    [SerializeField]
    private float m_currentSpeed = 5;
    [SerializeField]
    private float m_maxDistanceToPlayer = 20;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMovement_Basic>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManager.Instance.gameRunning)
        {
            if (player != null)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance > m_maxDistanceToPlayer)
                {
                    Vector3 newPosition = SegmentManager.Instance.m_currentSegment.GetRandomAdjacentSegment();
                    newPosition.y = 1;
                    transform.position = newPosition;
                }
                else
                {
                    MoveToPlayer();
                }




            }
        }
    }

    void MoveToPlayer()
    {
        transform.LookAt(player.transform);
        controller.Move(transform.forward * Time.deltaTime * m_currentSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GameplayManager.Instance.GameComplete(false);
        }
    }
}

