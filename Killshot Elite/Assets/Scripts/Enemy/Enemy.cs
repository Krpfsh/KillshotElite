using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent { get => _agent; }
    public GameObject Player { get => _player; }

    public Path Path;
    [Header("Sight Values")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float EyesHeight;
    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;

    [SerializeField] private string currentState;
    private StateMachine _stateMachine;
    private NavMeshAgent _agent;
    private GameObject _player;

    private void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine.Initialize();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        CanSeePlayer();
        currentState = _stateMachine.ActiveState.ToString();
    }
    public bool CanSeePlayer()
    {
        if (_player != null)
        {
            if (Vector3.Distance(transform.position, _player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = _player.transform.position - transform.position - (Vector3.up * EyesHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + Vector3.up * EyesHeight, targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        return true;
                    }

                }
            }
        }
        return false;
    }
}
