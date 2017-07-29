using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.

    int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
    float camRayLength = 100f;          // The length of the ray from the camera into the scene.
    CharacterController controller;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

    Vector2 mouseLook;
    Vector2 smoothV;
    public float sensitivity = 5.0f;
    public float smoothing = 2.0f;

    void Awake()
    {
        // Create a layer mask for the floor layer.
        floorMask = LayerMask.GetMask("Floor");
        controller = GetComponent<CharacterController>();
        UnityEngine.Cursor.visible = false;

    }



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


        Turning();

    }



    void Turning()
    {
        //see http://answers.unity3d.com/questions/574457/limit-rotation-using-mathclamp.html second answer

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
        transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);

        Quaternion rot = Quaternion.AngleAxis(-1 * mouseLook.y, Vector3.right);
        float x = Mathf.Clamp(rot.x, -0.7f, 0.7f);
        Debug.Log(rot.x + "  " + Mathf.Clamp(rot.x, -0.7f, 0.7f) +" " +x);
        Camera.main.gameObject.transform.localRotation = new Quaternion(x, rot.y, rot.z, 1.0f);

        Debug.Log(Camera.main.gameObject.transform.localRotation);

    }

}