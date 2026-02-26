using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slahsAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;

    private void Awake()
    {
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        playerController = GetComponentInParent<PlayerController>();
        playerControls = new PlayerControls();
        myAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Attack()
    {
        myAnimator.SetTrigger("Attack");

        slashAnim = Instantiate(slahsAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }

    public void SwingUpFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        
        if(playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void SwingDownFlipAnim()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler( 0, 0, 0);

        if (playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = playerController.transform.position;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerPos);
        

        float angle = Mathf.Atan2(mousePos.y - playerPos.y, Mathf.Abs(mousePos.x - playerPos.x)) * Mathf.Rad2Deg;

        if(mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
