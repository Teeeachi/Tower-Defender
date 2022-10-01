using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody RB;
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private float cameraXTilt;
    public Image RedScreenImage;
    public Animator animator;
    private Quaternion rotation;
    public TextMeshProUGUI NightTxt;

    [SerializeField] private float moveSpeed;

    [SerializeField] private bool isOnTheGround;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private bool inputJump;
    private Vector3 velocity;
    private CharacterController controller;
    private Vector3 movement;
    private float rotationSpeed;

    public Button cameraButton;
    private Transform target;
    private float distance;
    public string enemyTag;
    private bool playerDidHitEnemy;

    private float hitStrength;

    public void GotHitByAnEnemy()
    {
        RedScreenImage.gameObject.SetActive(true);
        Color RedC = RedScreenImage.color;
        RedC.a = 0.8f;
        RedScreenImage.color = RedC;
    }

    private IEnumerator FadeOutCR()
    {
        Color TxtC = NightTxt.color;
        float duration = 3f; //0.5 secs
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float alpha;
            if (currentTime < duration / 2) {
                alpha = Mathf.Lerp(0f, 1f, currentTime / duration);
            }
            else
            {
                alpha = Mathf.Lerp(1f, 0f, currentTime / duration);
            }
            
            TxtC.a = alpha;
            NightTxt.color = TxtC;
            currentTime += Time.deltaTime;
            yield return null;
        }
        NightTxt.gameObject.SetActive(false);
        yield break;
    }

    void Start()
    {
        StartCoroutine("FadeOutCR");
        hitStrength = 20f;
        distance = 10f;
        playerDidHitEnemy = false;
        enemyTag = "Enemy";
        cameraXTilt = 0.3f;
        rotationSpeed = 10f;
        moveSpeed = 10f;
        groundCheckDistance = 0.2f;

        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();

        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
    }

    void Update()
    {
        if(Input.touchCount == 1)
        {
            Touch screenTouch = Input.GetTouch(0);
            if(screenTouch.phase == TouchPhase.Moved)
            {
                transform.Rotate(0f, screenTouch.deltaPosition.x, 0f);
            }
        }

        Move();
        if (RedScreenImage.color.a > 0)
        {
            Color c = RedScreenImage.color;
            c.a -= Time.deltaTime;
            RedScreenImage.color = c;
        }
        else
        {
            RedScreenImage.gameObject.SetActive(false);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= distance && playerDidHitEnemy == false)
        {
            target = nearestEnemy.transform;
            playerDidHitEnemy = true;
            StartCoroutine(hitAnEnemy(target.gameObject));
        }
    }

    private IEnumerator hitAnEnemy(GameObject hitEnemy)
    {
        DoTheAttack();
        playerDidHitEnemy = true;
        yield return new WaitForSeconds(0.5f);
        if (hitEnemy != null)
        {
            hitEnemy.GetComponent<Navmesh>().GotHit(hitStrength);
        }
        yield return new WaitForSeconds(1.5f);
        playerDidHitEnemy = false;
        //Navmesh NavMeshScript = hitEnemy.GetComponent<Navmesh>();
        //NavMeshScript.GotHit();
    }

    private void Move()
    {
        isOnTheGround = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        movement = new Vector3(joystick.Horizontal * moveSpeed * Time.deltaTime, 0, joystick.Vertical * moveSpeed * Time.deltaTime);
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            //Debug.Log("H " + joystick.Horizontal);
            //Debug.Log("V " + joystick.Vertical);
            if ((Mathf.Abs(joystick.Horizontal) >= 0.5f) || (Mathf.Abs(joystick.Vertical) >= 0.5f))
            {
                animator.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
            }
            else
            {
                animator.SetFloat("Speed", 0.5f, 0.2f, Time.deltaTime);
            }
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(movement), Time.deltaTime * rotationSpeed);
        }
        else
        {
            animator.SetFloat("Speed", 0, 0.2f, Time.deltaTime);
        }
        //transform.position += movement;
        transform.position += transform.forward * (joystick.Horizontal+joystick.Vertical) / 2;
        //Camera.main.transform.position = new Vector3(cameraXTilt * RB.position.x, RB.position.y + 12f, RB.position.z - 6f);
    }

    public void DoTheAttack()
    {
        if (animator.GetLayerWeight(animator.GetLayerIndex("Attack Layer")) != 1)
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 1);
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.9f);
        animator.SetLayerWeight(animator.GetLayerIndex("Attack Layer"), 0);
    }
}
