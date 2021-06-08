using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Camera MyCamera;
    public float Speed = 5f;
    public float RotationSpeed = 15f;
    CharacterController MyController;

    float mDesiredRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        MyController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(x, 0, z);

        Vector3 rotatedMovement = Quaternion.Euler(0, MyCamera.transform.rotation.eulerAngles.y, 0) * movement;

        MyController.Move(rotatedMovement * Speed * Time.deltaTime);

        if (rotatedMovement.magnitude > 0)
        {
            mDesiredRotation= Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg; 
        }

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, mDesiredRotation, 0);
        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, RotationSpeed * Time.deltaTime);
    }
}
