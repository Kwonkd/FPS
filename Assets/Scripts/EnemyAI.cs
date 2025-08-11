using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public enum EnemyState
{
    Idle,  // 대기
    Chase, // 추격
    AttackMelee, // 근거리 공격
    AttackRange, // 원거리 공격
    Return, // 복귀
    Die // 사망
}

public class EnemyAI : MonoBehaviour
{
    public EnemyState state = EnemyState.Idle; // 현재 상태, 초기값은 Idle
    public Transform player; // 추격 대상
    public float speed = 3.0f; // 이동 속도
    public float chaseRange = 10.0f; // 적의 인식 범위, 추격 시작 범위
    public float attackMelee = 2f; // 근거리 공격 범위
    public float attackRange = 5f; // 원거리 공격 범위
    public int attackDamage = 10; // 적의 공격력
    public int attackCooldown = 1; // 공격 간격
    public int hp = 5; // 적의 체력
    private float lastAttackTime = 0f; // 마지막 공격 시간
    private Vector3 returnPoint;

    public Transform firePos;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5;

    public NavMeshAgent agent; // NavMeshAgent 컴포넌트

    public Animator animator; // 애니메이터 컴포넌트

    public AudioSource fireSound;
    private void Start()
    {
        returnPoint = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position); // 플레이어와의 거리 계산
        animator.SetFloat("Attack", dist);
        float returndist = Vector3.Distance(transform.position, returnPoint);

        switch (state)
        {
            case EnemyState.Idle:
                {
                    if(dist < chaseRange) // 인식 범위 내에 플레이어가 있다면
                    {
                        if (animator != null)
                        {
                            animator.SetBool("Chase",true); // Chase 애니메이션 트리거
                        }
                        state = EnemyState.Chase; // 상태를 Chase로 변경
                    }
                }
                break;

            case EnemyState.Chase:
                {
                    if(dist > chaseRange) // 인식 범위 내에 플레이어가 없다면
                    {
                        animator.SetBool("Chase", false);
                        animator.SetBool("Return", true);
                        state = EnemyState.Return; // Return 상태로 변경
                    }
                    else if (dist < attackRange) // 공격 범위 내에 플레이어가 있다면
                    {
                        state = EnemyState.AttackRange; // Attack 상태로 변경
                    }
                    else
                    {
                        //두 객채 사이의 방향을 계산
                        //Vector3 dir = (player.position - transform.position).normalized;
                        //transform.position += dir * speed * Time.deltaTime; // 플레이어를 향해 이동
                        Direction();
                        agent.SetDestination(player.position);
                    }
                }
                break;
                
                case EnemyState.AttackRange:
                {
                    Direction();
                    if (dist > attackRange) // 공격 범위 밖으로 나가면
                    {
                        state = EnemyState.Chase; // Chase 상태로 변경
                    }
                    else if(dist < attackMelee)
                    {
                        state = EnemyState.AttackMelee; // 근접공격 상태로 변경
                    }
                    else if (Time.time - lastAttackTime > attackCooldown && dist < attackRange) // 공격 쿨타임이 지났다면, 플레이어와의 거리가 attackRange보다 짧다면
                    {
                        if (animator != null)
                        {
                            GameObject bullet = Instantiate(bulletPrefab, firePos.transform.position, firePos.transform.rotation);
                            StartCoroutine(DelayTime());
                            if (fireSound != null)
                            {
                                fireSound.Play();
                            }
                            lastAttackTime = Time.time; // 마지막 공격 시간 갱신
                        }
                    }
                }
                break;

            case EnemyState.AttackMelee:
                if(dist > attackMelee)
                {
                    state = EnemyState.AttackRange; //원거리 공격 상태로 변경
                }
                else if(Time.time - lastAttackTime > attackCooldown)
                {
                    PlayerHealth ph = player.GetComponent<PlayerHealth>();
                    if(ph != null)
                    {
                        lastAttackTime = Time.time;
                        ph.TakeDamage(attackDamage);
                        Debug.Log("Enemy attacked player for " + attackDamage + " damage.");
                    }
                }
                break;

            case EnemyState.Return:
                if(dist < chaseRange)
                {
                    animator.SetBool("Chase", true);
                    animator.SetBool("Return", false);
                    state = EnemyState.Chase;
                }
                if(returndist < 0.3f)
                {
                    animator.SetBool("Return", false);
                    state = EnemyState.Idle;
                }
                else
                {
                    agent.SetDestination(returnPoint);
                }
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        if (animator != null)
        {
            animator.SetBool("Hit", true);
            hp -= damage;
            StartCoroutine(DelayTime());
            animator.SetBool("Hit", false);
        }

        if (hp <= 0) // 체력이 0 이하가 되면
        { 
            state = EnemyState.Die; // Die 상태로 변경
            Die();
        }
    }
    void Die()
    {
        if (animator != null)
        {
            animator.SetBool("dead", true); // Die 애니메이션 트리거
        }
        Debug.Log("적이 죽었습니다.");
        ProcessDead();
        Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }

    public void ProcessDead()
    {
        WaveManager manager = GameObject.FindObjectOfType<WaveManager>();
        if (manager != null)
        {
            manager.OnEnemyDefeated();
        }
    }
    public IEnumerator DelayTime()
    {
        Debug.Log("딜레이 시작");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("딜레이 끝");
    }

    void Direction() // 플레이어와 게임 오브젝트 간의 방향 계산 함수
    {
        Vector3 dir = player.transform.position - transform.position;
        dir.y = 0f;

        Quaternion rot = Quaternion.LookRotation(dir.normalized);

        transform.rotation = rot;
    }
}
