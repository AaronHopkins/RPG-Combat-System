using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft;  } set { facingLeft = value; } }

    [SerializeField] private float moveSpeed = 1f;

    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private PlayerControls playerControls;

    private bool facingLeft = false;

    private void Awake() {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        PlayerInput();
    }

    void FixedUpdate()
    {
        AdjustPlatyerFacingDirection();
        Move();
    }
    private void OnEnable() {
        playerControls.Enable();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlatyerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        
        if(mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }
}
 