using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IMonsterState
{
    private NavMeshAgent agent;
    private List<Transform> patrolPoints;
    private float distanceToStopChasing;
    private int currentPatrolPointIndex = 0;
    private int patrolDirection = 1;

    public PatrolState(NavMeshAgent agent, List<Transform> points, float stopChasingDistance)
    {
        this.agent = agent;
        this.patrolPoints = points;
        this.distanceToStopChasing = stopChasingDistance;
    }

    public void UpdateState(MonsterAI monster)
    {
        float distanceToPlayer = Vector3.Distance(monster.transform.position, monster.GetPlayerTransform().position);

        if (distanceToPlayer <= monster.detectionRadius && monster.CanSeePlayer())
        {
            // 플레이어를 발견하면 추적 상태로 전환
            Debug.Log("추적");
            monster.ChangeState(new ChaseState(agent, monster.GetPlayerTransform()));
        }

        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            // 다음 순찰 지점으로 이동
            currentPatrolPointIndex += patrolDirection;

            // 순찰 지점 인덱스가 범위를 벗어나면 방향을 바꿔주고 인덱스를 조정
            if (currentPatrolPointIndex >= patrolPoints.Count || currentPatrolPointIndex < 0)
            {
                patrolDirection *= -1; // 방향 변경
                currentPatrolPointIndex += patrolDirection * 2; // 두 번째 지점부터 시작
            }

            agent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
        }
    }
}

public class ChaseState : IMonsterState
{
    private NavMeshAgent agent;
    private Transform playerTransform;

    public ChaseState(NavMeshAgent agent, Transform playerTransform)
    {
        this.agent = agent;
        this.playerTransform = playerTransform;
    }

    public void UpdateState(MonsterAI monster)
    {
        float distanceToPlayer = Vector3.Distance(monster.transform.position, playerTransform.position);

        if (distanceToPlayer > monster.distanceToStopChasing)
        {
            Debug.Log("너무 멀어졌네");
            // 추적 중단 후 순찰로 복귀
            monster.ChangeState(new PatrolState(agent, monster.patrolPoints, monster.distanceToStopChasing));
        }

        agent.SetDestination(playerTransform.position);
    }
}


public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public List<Transform> patrolPoints; // List로 순찰 지점을 추가합니다.
    public float detectionRadius = 15f;
    public float moveSpeed = 8.0f;
    public float distanceToStopChasing = 15f;

    private NavMeshAgent agent;
    private IMonsterState currentState;
    private int currentPatrolPointIndex = 0;
    private int patrolDirection = 1; // 순찰 방향: 1은 정방향, -1은 역방향

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        // 초기 상태를 순찰로 설정
        currentState = new PatrolState(agent, patrolPoints, distanceToStopChasing);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }

    public bool CanSeePlayer()
    {
        RaycastHit hit;
        Vector3 directionToPlayer = player.position - transform.position;
        if (Physics.Raycast(transform.position, directionToPlayer, out hit, detectionRadius))
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }

    public void ChangeState(IMonsterState newState)
    {
        currentState = newState;
    }

    // 다른 메서드도 이곳에 추가 가능
}

public interface IMonsterState
{
    void UpdateState(MonsterAI monster);
}