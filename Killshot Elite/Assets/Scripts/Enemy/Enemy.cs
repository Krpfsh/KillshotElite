using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent { get => _agent;}


    private StateMachine _stateMachine;
    private NavMeshAgent _agent;

    [SerializeField] private string currentState;
    public Path Path;

    private void Start()
    {
        _stateMachine = GetComponent<StateMachine>();
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine.Initialize();

    }
}
