using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private MyCharacterController _characterController;
    [SerializeField] private LayerMask GroundLayerMask;

    private bool _OnGround;
    private Vector2 _axis;

    // Update is called once per frame
    void Update()
    {
        _OnGround = Physics.Raycast(transform.position, Vector3.down, .5f, GroundLayerMask);

        if (Input.GetKeyDown(KeyCode.Space) && _OnGround)
        {
            _characterController.OnJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _characterController.OnSprint();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _characterController.OnSprint();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _characterController.OnInteract();
        }

        _axis.x = Input.GetAxis("Horizontal");
        _axis.y = Input.GetAxis("Vertical");

        _characterController.OnMove(_axis);
    }
}
