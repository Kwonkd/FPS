using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RigidbodyPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 이동속도
    public float mouseSensitivity = 100f; // 마우스 감도
    public Transform cameraTransform; // 카메라 객체

    private Rigidbody rb;
    private float xRotation = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; // 게임 중 마우스 커서 안보이게
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 카마레 시야를 지정한 범위 값으로 고정하기 위한 처리.
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //카메라 회전 적용 //카메라가 Player의 자식으로 들어가 있기 때문에 localRotation
        transform.Rotate(Vector3.up * mouseX); // 좌우 회전
    }

    //물리 처리를 매 프레임마다 하기 위해, 고정된 프레임
    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal"); //키보드의 A, D 입력 시
        float v = Input.GetAxis("Vertical"); // 키보드의 W, S 입력 시
        Vector3 move = transform.right * h + transform.forward * v; // 실제 이동 처리.
        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.velocity.y; // 중력 유지 땅 밖으로 벗어나면 밑으로 떨어짐. //velocity.y = rb.velocity.y;
        rb.velocity = velocity; //최종 속도 적용. //rb.velocity = velocity;
    }
}
