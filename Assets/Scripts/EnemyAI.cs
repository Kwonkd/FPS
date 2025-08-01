using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,  // 대기
    Chase, // 추격
    Attack, // 공격
    Die // 사망
}

public class EnemyAI : MonoBehaviour
{
    public EnemyState state = EnemyState.Idle; // 현재 상태, 초기값은 Idle
    public Transform player; // 추격 대상
    public float speed = 3.0f; // 이동 속도
    public float chaseRange = 10.0f; // 적의 인식 범위, 추격 시작 범위
    public float attackRange = 2f; // 공격 범위
    public int attackDamage = 10; // 적의 공격력
    public int attackCooldown = 1; // 공격 간격
    public int hp = 5; // 적의 체력
    private float lastAttackTime = 0f; // 마지막 공격 시간

    public NavMeshAgent agent; // NavMeshAgent 컴포넌트

    public Animator animator; // 애니메이터 컴포넌트

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position); // 플레이어와의 거리 계산

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
                        state = EnemyState.Idle; // Idle 상태로 변경
                    }
                    else if (dist < attackRange) // 공격 범위 내에 플레이어가 있다면
                    {
                        state = EnemyState.Attack; // Attack 상태로 변경
                    }
                    else
                    {
                        //두 객채 사이의 방향을 계산
                        //Vector3 dir = (player.position - transform.position).normalized;
                        //transform.position += dir * speed * Time.deltaTime; // 플레이어를 향해 이동
                        agent.SetDestination(player.position);
                    }
                }
                break;
                
                case EnemyState.Attack:
                {
                    if (dist > attackRange) // 공격 범위 밖으로 나가면
                    {
                        animator.SetFloat("Attack", 0);
                        state = EnemyState.Chase; // Chase 상태로 변경
                    }
                    else if (Time.time - lastAttackTime > attackCooldown) // 공격 쿨타임이 지났다면
                    {
                        PlayerHealth ph = player.GetComponent<PlayerHealth>();
                        if(ph != null)
                        {
                            if (animator != null)
                            {
                                animator.SetFloat("Attack", Time.time - lastAttackTime); // Attack 애니메이션 트리거
                            }
                            ph.TakeDamage(attackDamage);
                            Debug.Log("Enemy attacked player for " + attackDamage + " damage.");
                        }
                        lastAttackTime = Time.time; // 마지막 공격 시간 갱신
                        animator.SetFloat("Attack", lastAttackTime = Time.time);
                    }
                }
                break;
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        
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
        //ProcessDead();
        Destroy(gameObject,3);
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
}
