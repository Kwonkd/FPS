using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; //이동속도
    public float mouseSensitivity = 2f; //마우스 감도.

    private CharacterController controller; //캐릭터 이동 처리를 하기 위한 컨트롤러 컴포넌트.
    private Transform cameraTransform; // 메인 카메라 참조.
    private float verticalTrotation = 0f; //상하 카메라 회전 값 누적 저장.
    private float verticalRotation = 0f;

    public Transform Muzzle;
    public GameObject Bullet;

    Transform BulletRotation;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        Muzzle.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        float moveX = Input.GetAxis("Horizontal"); //키보드의 A, D 입력 시
        float moveZ = Input.GetAxis("Vertical"); // 키보드의 W, S 입력 시

        Vector3 move = transform.right * moveX + transform.forward * moveZ; // 실제 이동 처리.
        controller.Move(move * speed * Time.deltaTime);

        LoadBullet();
    }

    void LoadBullet() // 총알이 총구에서 생성
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽버튼 클릭시 총알이 총구의 위치와 방향에 맞게 생성됨
        {
            //BulletRotation.transform.Rotate(Muzzle.rotation.x, Muzzle.rotation.y, Bullet.transform.rotation.z);
            //BulletRotation.rotation = Quaternion.Euler(Muzzle.rotation.x, Muzzle.rotation.y, Bullet.transform.rotation.z);
            //Instantiate(Bullet, Muzzle.transform.position,BulletRotation.rotation);
            //Instantiate(Bullet, Muzzle.transform.position, Bullet.transform.rotation);
            Instantiate(Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
        }
    }
}
