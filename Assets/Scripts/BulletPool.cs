using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab; // 총알 프리팹
    public int poolSize = 20;
    List<GameObject> Pool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform);
            bullet.SetActive(false); // 초기에는 비활성화
            Pool.Add(bullet);
        }
    }

    public GameObject GetBullet(Vector3 pos, Quaternion rot)
    {
        foreach(GameObject bullet in Pool) // pool list안을 순차적으로 탐색
        {
            if (bullet.activeInHierarchy == false) // 비활성화된 총알을 찾음
            {
                bullet.transform.position = pos; // 총알의 위치를 지정된 위치로 설정
                bullet.transform.rotation = rot; // 총알의 회전을 지정된 회전으로 설정
                bullet.SetActive(true); // 총알 활성화
                return bullet; // 활성화된 총알 반환
            }
        }

        GameObject newBullet = Instantiate(bulletPrefab, pos, rot, transform); // 비활성화된 총알이 없는 경우 새로 생성
        Pool.Add(newBullet); // 새 총알을 풀에 추가
        return newBullet;
    }
}
