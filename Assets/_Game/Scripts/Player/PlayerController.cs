using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
	[SerializeField] private Animator animator;
	
	private Rigidbody2D _rb;
	private Vector2 _movement;
	
	private Vector2 _currentLookDirection;
	
	private readonly int _dirXHash = Animator.StringToHash("DirX");
	private readonly int _dirYHash = Animator.StringToHash("DirY");
	private readonly int _speedHash = Animator.StringToHash("Speed");
	
	private void Start()
	{
		_rb = GetComponent<Rigidbody2D>();
		_currentLookDirection = Vector2.right;
	}

	private void Update()
	{
		_movement.x = Input.GetAxisRaw("Horizontal");
		_movement.y = Input.GetAxisRaw("Vertical");
		
		
	}

	void FixedUpdate()
    {
		if (_movement != Vector2.zero)
		{
			SetLookDirectionFrom(_movement);
		}
		var movement = _movement * moveSpeed;
		var speed = movement.sqrMagnitude;
		
		animator.SetFloat(_dirXHash, _currentLookDirection.x);
		animator.SetFloat(_dirYHash, _currentLookDirection.y);
		animator.SetFloat(_speedHash, speed);
		
		_rb.MovePosition(_rb.position + _movement * moveSpeed * Time.fixedDeltaTime);
    }
	
	void SetLookDirectionFrom(Vector2 direction)
	{
		if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
		{
			_currentLookDirection = direction.x > 0 ? Vector2.right : Vector2.left;
		}
		else
		{
			_currentLookDirection = direction.y > 0 ? Vector2.up : Vector2.down;
		}
	}
}
