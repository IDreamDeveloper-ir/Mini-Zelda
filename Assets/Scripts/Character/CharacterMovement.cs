using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private const string animParamiterMovement = "IsWalk";
    private const string animParamiterSprint = "Sprint";
    private const string animParamiterJump = "Jump";
    private const string animParamiterLoss = "IsDead";

    [SerializeField][Range(0, 10)] private float playerWalkSpeed;
    [SerializeField][Range(0, 10)] private float playerSprintSpeed;
    [SerializeField][Range(0, 10)] private float playerJump;
    [SerializeField] private LayerMask GroundLayerMask;

    private Animator _animator;
    private Rigidbody _rigidbody;
    private Vector2 _movmentAxis;
    private bool _OnGround;
    private bool _Sprint;

    private Dictionary<Vector2, float> CharacterRotationList;
    private float _characterRotation;


    // Start is called before the first frame update
    void Start()
    {
        _animator = transform.GetChild(0).GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();

        CharacterRotationList = new Dictionary<Vector2, float>
        {
            { Vector2.up, 0 },
            { Vector2.down, 180 },
            { Vector2.left, 270 },
            { Vector2.right, 90 },
            { Vector2.one, 45 },
            { Vector2.one * -1, 225 },
            { new Vector2(1, -1), 135 },
            { new Vector2(-1, 1), 315 }
        };
    }

    private void movement(Vector2 direction)
    {
        _rigidbody.velocity = ((Vector3.right * direction.x)
            + (Vector3.forward * direction.y)) 
            * (_Sprint ? playerSprintSpeed : playerWalkSpeed)
            + (Vector3.up * _rigidbody.velocity.y);

        if (direction != Vector2.zero)
        {
            CharacterRotationList.TryGetValue(
                AdvancedNormalized(direction),
                out _characterRotation);
            transform.GetChild(0).eulerAngles = _characterRotation * Vector3.up;
        }

        _animator.SetBool(animParamiterMovement,
            direction != Vector2.zero);

        _animator.SetBool(animParamiterSprint,
            _Sprint);

    }

    private void Jump()
    {
        _rigidbody.velocity += (Vector3.up * playerJump);
        _animator.SetTrigger(animParamiterJump);
    }

    private void Update()
    {
        _movmentAxis = Vector2.zero;
        _OnGround = Physics.Raycast(transform.position, Vector3.down,.5f,GroundLayerMask);

        if (Input.GetKeyDown(KeyCode.Space) && _OnGround)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _Sprint = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _Sprint = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<InteractionManager>().Interact();
        }


        _movmentAxis.x = Input.GetAxis("Horizontal");
        _movmentAxis.y = Input.GetAxis("Vertical");

        _movmentAxis = _movmentAxis.normalized;

        movement(_movmentAxis);
    }

    private Vector2 AdvancedNormalized(Vector2 direction)
    {
        direction.x = Mathf.Sign(direction.x) * (direction.x != 0 ? 1 : 0);
        direction.y = Mathf.Sign(direction.y) * (direction.y != 0 ? 1 : 0);
        return direction;
    }
}
