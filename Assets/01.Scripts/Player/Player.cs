using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField]
    private UnityEvent _onDieTrigger;
    [SerializeField]
    private InputReader _inputReader;
    [SerializeField]
    private float _moveSpeed;
    private CharacterController _characterController;
    UnityEvent IDamageable.OnDieTrigger => _onDieTrigger;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Movement()
    {
        _characterController.Move(_inputReader.movementDirection * _moveSpeed * Time.deltaTime);
    }
}
