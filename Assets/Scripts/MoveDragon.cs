using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MoveDragon : MonoBehaviour
{
    public AudioClip DragonObs;
    public AudioClip DragonJump;

    // user inputs
    private float wsInput;
    private float adInput;
    private float inputScale;

    // camera and character heading related
    public GameObject MainCamera;
    public Transform CameraTransform;
    private Vector3 userRotation;
    private Vector3 cameraRotation;
    private Vector3 heading;

    // tune sensitivity of controls
    // original mass, drag, angularDrag: 1, 2, 0.05
    private float moveScale = 0.6f; // original 0.5
    private float jumpScale = 10.0f; // original 4.0 (using AddForce)

    // jump limiter
    [SerializeField] private LayerMask PlatformLayerMask;
    public Collider DragonCollider;
    private bool userJumped;
    public float distanceToGround;

    // model components
    private Rigidbody DragonRigidbody;
    private Transform DragonTransform;

    // Colliders
    public Collider normalCollider;

    // animation related
    Animator Animator;
    bool movingForward;
    public bool isGrounded;
    bool jumping;
    bool sprinting;
    bool hasFallen;

    void Start() {
        DragonRigidbody = GetComponent<Rigidbody>();
        DragonTransform = GetComponent<Transform>();
        CameraTransform = MainCamera.GetComponent<Transform>();
        Animator = GetComponent<Animator>();
        DragonCollider = normalCollider;
        distanceToGround = normalCollider.bounds.extents.y;
        hasFallen = false;
        // StartCoroutine(printStates());
    }

    void Update() {
        // get keyboard inputs
        wsInput = Input.GetAxis("Vertical");
        adInput = Input.GetAxis("Horizontal");
        userJumped = Input.GetButton("Jump");

        // play animations according to keyboard inputs
        movingForward = Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d");
        isGrounded = IsGrounded();
        jumping = Input.GetKey("space");
        sprinting = Input.GetKey(KeyCode.LeftShift) && isGrounded;
    }

    private void FixedUpdate() {
        // decide which direction should the character go (according to camera heading)
        userRotation = DragonTransform.rotation.eulerAngles;
        cameraRotation = CameraTransform.rotation.eulerAngles;
        inputScale = 0;

        // perform animations
        Animator.SetBool("isWalking", movingForward && !hasFallen);
        // Animator.SetBool("isFlying", !isGrounded && !hasFallen);
        // Animator.SetBool("isGrounded", isGrounded && !hasFallen);
        Animator.SetBool("isRunning", sprinting && !hasFallen);
        Animator.SetBool("isIdle", !movingForward && isGrounded && !hasFallen);
        Animator.SetBool("fallen", hasFallen);

        // move 90 degrees right (press only "D" or "D" + "W" + "S")
        if (!hasFallen)
        {
          if ((Input.GetKey("d") && !Input.GetKey("w") && !Input.GetKey("s")) || (Input.GetKey("d") && Input.GetKey("w") && Input.GetKey("s"))) {
              userRotation[1] = cameraRotation[1];
              userRotation += new Vector3(0, 90, 0);
              heading = DragonTransform.forward;
              inputScale = Mathf.Abs(adInput);

          // move 90 degrees left (press only "A" or "A" + "W" + "S")
          } else if ((Input.GetKey("a") && !Input.GetKey("w") && !Input.GetKey("s")) || (Input.GetKey("a") && Input.GetKey("w") && Input.GetKey("s"))) {
              userRotation[1] = cameraRotation[1];
              userRotation += new Vector3(0, -90, 0);
              heading = DragonTransform.forward;
              inputScale = Mathf.Abs(adInput);

          // move 0 degree forward (press only "W" or "W" + "A" + "D")
          } else if ((Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("d")) || (Input.GetKey("w") && Input.GetKey("a") && Input.GetKey("d"))) {
              userRotation[1] = cameraRotation[1];
              heading = DragonTransform.forward;
              inputScale = Mathf.Abs(wsInput);

          // move 180 degrees backward (press only "S" or "S" + "A" + "D")
          } else if ((Input.GetKey("s") && !Input.GetKey("a") && !Input.GetKey("d")) || (Input.GetKey("s") && Input.GetKey("a") && Input.GetKey("d"))) {
              userRotation[1] = cameraRotation[1];
              userRotation += new Vector3(0, 180, 0);
              heading = DragonTransform.forward;
              inputScale = Mathf.Abs(wsInput);

          // move 45 degrees right (press "W" + "D")
          } else if (Input.GetKey("w") && Input.GetKey("d")) {
              userRotation[1] = cameraRotation[1];
              userRotation += new Vector3(0, 45, 0);
              heading = DragonTransform.forward;
              inputScale = (Mathf.Abs(wsInput) + Mathf.Abs(adInput)) / 2.0f;

          // move 45 degrees left (press "W" + "A")
          } else if (Input.GetKey("w") && Input.GetKey("a")) {
              userRotation[1] = cameraRotation[1];
              userRotation += new Vector3(0, -45, 0);
              heading = DragonTransform.forward;
              inputScale = (Mathf.Abs(wsInput) + Mathf.Abs(adInput)) / 2.0f;

          // move 135 degrees right (press "S" + "D")
          } else if (Input.GetKey("s") && Input.GetKey("d")) {
              userRotation[1] = cameraRotation[1];
              userRotation += new Vector3(0, 135, 0);
              heading = DragonTransform.forward;
              inputScale = (Mathf.Abs(wsInput) + Mathf.Abs(adInput)) / 2.0f;

          // move 135 degrees left (press "S" + "A")
          } else if (Input.GetKey("s") && Input.GetKey("a")) {
              userRotation[1] = cameraRotation[1];
              userRotation += new Vector3(0, -135, 0);
              heading = DragonTransform.forward;
              inputScale = (Mathf.Abs(wsInput) + Mathf.Abs(adInput)) / 2.0f;

          // stand still
          } else {
              inputScale = 0;
          }
        }

        // rotate character to the right direction
        DragonTransform.rotation = Quaternion.Lerp(DragonTransform.rotation, Quaternion.Euler(userRotation), 0.3f);

        // let the character go forward
        if (sprinting && !hasFallen) {
            DragonRigidbody.velocity += heading * inputScale * moveScale * 1.3f;
        } else {
            DragonRigidbody.velocity += heading * inputScale * moveScale;
        }


        // only able to jump if you are on the ground
        if (userJumped && !hasFallen) {
            //GetComponent<AudioSource>().clip = DragonJump;
            //GetComponent<AudioSource>().Play();
            DragonRigidbody.velocity = Vector3.up * jumpScale;
        }
    }


    /** Return the euclidean norm of x and y */
    private float euclideanNorm (float x, float y) {
        return Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2));
    }

    /** Send a raycast to check if player is grounded and returns true if
     the player is on some sort of ground */
    private bool IsGrounded() {
        // boxcast not working now
        // float extraHeight = 0.05f;
        // bool hitGround = Physics.BoxCast(DragonCollider.bounds.center, DragonTransform.lossyScale, DragonTransform.up * -1, Quaternion.Euler(Vector3.zero), DragonTransform.lossyScale.y + extraHeight);
        bool hitGround = Physics.Raycast(DragonTransform.position, Vector3.down, 0.2f);

        Color rayColor;
        if (hitGround) {
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }
        Debug.DrawRay(DragonTransform.position, Vector3.down * 0.2f, rayColor);

        return hitGround;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Obstacle")) {
            hasFallen = true;
            StartCoroutine(Slowed());
        }
    }

    public IEnumerator Slowed() {
        GetComponent<AudioSource>().clip = DragonObs;
        GetComponent<AudioSource>().Play();
        moveScale = 0.1f;
        yield return new WaitForSeconds(3);
        hasFallen = false;
        moveScale = 0.5f; // original 0.5
    }
}
