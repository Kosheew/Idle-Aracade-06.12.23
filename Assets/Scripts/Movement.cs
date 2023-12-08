using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private int numberOfRays = 60;
    [SerializeField] private float visionRange = 2f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] GameObject[] weapon;
    [SerializeField] Collider[] checkPlatform;
    public LayerMask platformLayer;

    private Rigidbody rb;
    private Animator animator;
    private float speed = 2.5f;
    private WeaponController controller;
    private int prewIndexWeapon = 0;

    void Start()
    {
        controller = FindObjectOfType<WeaponController>();
        controller.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
    }
    void Update()
    {
        MoveCharacter();
        RotateCharacter();
        RayCast();
    }

    private void RayCast()
    {
        for (int i = 0; i < numberOfRays; i++)
        {
            Ray ray = new Ray();
            ray.origin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            Vector3 forwardDirection = transform.forward;

            Quaternion rotation = Quaternion.AngleAxis((i * (60f / numberOfRays)) - 30, Vector3.up);
            ray.direction = rotation * forwardDirection;
          //  Debug.DrawRay(ray.origin, ray.direction * visionRange, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, visionRange))
            {
                SearchResurce(hit);
            }
        }
    }

    private void MoveCharacter()
    {
         float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        
        if (inputDirection.magnitude >= 0.1f)
        {
            Vector3 moveDirection = Quaternion.Euler(0f, Camera.main.transform.eulerAngles.y, 0f) * inputDirection;
            rb.MovePosition(rb.position + moveDirection * speed * Time.deltaTime);
            animator.SetBool("Run", true);
         
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void RotateCharacter()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;

        if (horizontalInput != 0f || verticalInput != 0f)
        {
            float targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
            float angle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
    private void SearchResurce(RaycastHit hit)
    {
        if (hit.collider.GetComponent<ResourceController>())
        {
            animator.SetBool("Attack", true);
            controller.gameObject.SetActive(true);
        }
        else
        {

            animator.SetBool("Attack", false);
            controller.gameObject.SetActive(false);
            weapon[prewIndexWeapon].SetActive(false);
        }
        switch (hit.collider.tag)
        {
            case "Wood":
                prewIndexWeapon = 0;
                weapon[prewIndexWeapon].SetActive(true);
                break;
            case "Stone":
                prewIndexWeapon = 1;
                weapon[prewIndexWeapon].SetActive(true);
                break;
            case "Crystal":
                prewIndexWeapon = 2;
                weapon[prewIndexWeapon].SetActive(true);
                break;
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Portal") 
        {
            FindObjectOfType<GameManager>().Restart();
            SceneManager.LoadScene(0);
        }
    }
}

