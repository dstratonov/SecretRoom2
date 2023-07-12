using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss01Movement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    public GameObject player;
    private Vector3 _lastPosition;
    private Vector3 _currentPosition;
    private Vector3 _velocity;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentPosition = transform.position;
        _lastPosition = _currentPosition;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(player.transform.position);
        _velocity = (_currentPosition - _lastPosition) / Time.deltaTime;
        _lastPosition = _currentPosition;
        _currentPosition = transform.position;
        print(_velocity.x);
        print(_velocity.z);
        _animator.SetFloat("velocityX", _velocity.x);
        _animator.SetFloat("velocityY", _velocity.z);
    }
}
