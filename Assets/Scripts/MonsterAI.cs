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
            // �÷��̾ �߰��ϸ� ���� ���·� ��ȯ
            Debug.Log("����");
            monster.ChangeState(new ChaseState(agent, monster.GetPlayerTransform()));
        }

        if (!agent.pathPending && agent.remainingDistance < 0.1f)
        {
            // ���� ���� �������� �̵�
            currentPatrolPointIndex += patrolDirection;

            // ���� ���� �ε����� ������ ����� ������ �ٲ��ְ� �ε����� ����
            if (currentPatrolPointIndex >= patrolPoints.Count || currentPatrolPointIndex < 0)
            {
                patrolDirection *= -1; // ���� ����
                currentPatrolPointIndex += patrolDirection * 2; // �� ��° �������� ����
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
            Debug.Log("�ʹ� �־�����");
            // ���� �ߴ� �� ������ ����
            monster.ChangeState(new PatrolState(agent, monster.patrolPoints, monster.distanceToStopChasing));
        }

        agent.SetDestination(playerTransform.position);
    }
}


public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public List<Transform> patrolPoints; // List�� ���� ������ �߰��մϴ�.
    public float detectionRadius = 15f;
    public float moveSpeed = 8.0f;
    public float distanceToStopChasing = 15f;

    private NavMeshAgent agent;
    private IMonsterState currentState;
    private int currentPatrolPointIndex = 0;
    private int patrolDirection = 1; // ���� ����: 1�� ������, -1�� ������

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        // �ʱ� ���¸� ������ ����
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

    // �ٸ� �޼��嵵 �̰��� �߰� ����
}

public interface IMonsterState
{
    void UpdateState(MonsterAI monster);
}