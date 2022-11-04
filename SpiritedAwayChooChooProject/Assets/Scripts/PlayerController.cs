using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform head;
    public Camera Camera;

    [Header("Configurations")]
    public float walkSpeed;
    public float runSpeed;

    public float trainFloorY;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // The sit/stand "Solution"
        if(transform.position.y > (trainFloorY + 0.00001f)){
            transform.position = new Vector3(transform.position.x, trainFloorY,transform.position.z);
        }

        //Horizontal Rotation
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
    }

    void FixedUpdate() 
    {
        if (canMove)
        {
            Vector3 newVelocity = Vector3.up * rb.velocity.y;
            float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
            newVelocity.x = Input.GetAxis("Horizontal") * speed;
            newVelocity.z = Input.GetAxis("Vertical") * speed;
            rb.velocity = transform.TransformDirection(newVelocity);
        }
    }

    void LateUpdate() {
        //Vertical Rotation
        Vector3 e = head.eulerAngles;
        e.x -= Input.GetAxis("Mouse Y") * 2f;
        e.x = RestrictAngle(e.x, -85f, 85f);
        head.eulerAngles = e;
    }

    // Clamp the vertical head rotation (prevent breaking the players neck)
    public static float RestrictAngle(float angle, float angleMin, float angleMax) {
        if(angle > 180) {
            angle -= 360;
        }
        else if(angle < -180) {
            angle += 360;
        }

        if(angle > angleMax) {
            angle = angleMax;
        }
        if(angle < angleMin) {
            angle = angleMin;
        }

        return angle;
    }
}
