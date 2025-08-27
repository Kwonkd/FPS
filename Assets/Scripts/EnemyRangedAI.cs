using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRangedAI : MonoBehaviour
{
    [Header("Target")]
    public Transform player;         // 비어 있으면 Tag로 자동 탐색
    public string playerTag = "Player";
    public Transform firePoint;

    [Header("Ranges")]
    public float detectRange = 15f;  // 이 거리 내에 있으면 쫓아감(여유값)
    public float attackRange = 5.0f; // 이 거리 안이면 공격 가능

    [Header("Bullet")]
    public GameObject bullet;
    public float shootDamage = 15f;
    public float shootSpeed = 5.0f;

    [Header("Cooldown")]
    public float attackCooldown = 3f; // 공격 후 쉬는 시간(초)
    private float nextAttackTime = 0f;  // 이 시간 이후에 다시 공격 가능

    [Header("Look & Move")]
    public float turnSpeed = 10f;  // 플레이어 바라볼 때 회전 속도

    [Header("Layer Mask")]
    public LayerMask playerMask;   // Player 레이어만 맞추도록 설정

    private NavMeshAgent agent;
    //public Animator animator;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        // 멈출 거리를 적당히 두면 서로 밀착해서 떨리는 일이 줄어듭니다.
        if (agent != null && agent.stoppingDistance < 0.1f)
            agent.stoppingDistance = 1.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag(playerTag);
            if (p != null) player = p.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null || agent == null) return;

        // 플레이어까지 거리
        float dist = Vector3.Distance(transform.position, player.position);

        // 1) 멀면 추적
        if (dist > attackRange && dist <= detectRange)
        {
            //animator.SetBool("Chase", true);
            agent.isStopped = false;
            agent.SetDestination(player.position);
            return;
        }

        // 2) 너무 멀어 추적도 안 할 정도면 멈춤(여기서는 간단히 멈추기만)
        if (dist > detectRange)
        {
            //animator.SetBool("Chase", false);
            agent.isStopped = true;
            return;
        }

        // 3) 공격 범위에 들어왔다면 멈추고 바라보기
        if (dist <= attackRange)
        {
            //animator.SetBool("Chase", false);
            agent.isStopped = true;

            // 수평으로만 플레이어를 바라보게(위아래 흔들림 방지)
            Vector3 dir = player.position - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion lookRot = Quaternion.LookRotation(dir.normalized, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * turnSpeed);
            }

            // 4) 쿨타임이 끝났다면 한 번 때리기
            if (Time.time >= nextAttackTime)
            {
                RangedAttack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    void RangedAttack()
    {
        GameObject prefabBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
    }
}
