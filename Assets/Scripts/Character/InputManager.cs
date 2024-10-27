using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Joystick Joystick;
    private Vector2 _axis;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnSprintPreassed();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            OnSprintReleased();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }

#if(UNITY_EDITOR)

        _axis.x = Input.GetAxis("Horizontal");
        _axis.y = Input.GetAxis("Vertical");


#else

        _axis = Joystick.Direction;

#endif
        MyCharacterController.Instance.OnMove(_axis);
    }

    public void OnJump()
    {
        MyCharacterController.Instance.OnJump();
    }

    public void OnSprintPreassed()
    {
        MyCharacterController.Instance.OnSprint(true);
    }

    public void OnSprintReleased()
    {
        MyCharacterController.Instance.OnSprint(false);
    }

    public void OnInteract()
    {
        MyCharacterController.Instance.OnInteract();
    }
}
