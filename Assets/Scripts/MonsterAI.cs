using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 15f;
    public float patrolRadius = 20f;
    public float moveSpeed = 8.0f;
    public float distanceToResetPatrol = 15f;
    public float distanceToStopChasing = 15f;

    private NavMeshAgent agent;
    private IMonsterState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;

        // �ʱ� ���¸� ������ ����
        currentState = new PatrolState(agent, transform, patrolRadius, distanceToResetPatrol);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�浹");
            // �浹�� ��ü�� �÷��̾��� ���, ���� ����
            EndGame();
        }   
    }

    private void EndGame()
    {
        // ���� ���� ������ ���⿡ ����
    }

    // �ٸ� �޼��嵵 �̰��� �߰� ����
}

public interface IMonsterState
{
    void UpdateState(MonsterAI monster);
}

public class PatrolState : IMonsterState
{
    private NavMeshAgent agent;
    private Transform monsterTransform;
    private float patrolRadius;
    private float distanceToResetPatrol;

    private Vector3 patrolDestination;

    public PatrolState(NavMeshAgent agent, Transform transform, float radius, float resetDistance)
    {
        this.agent = agent;
        this.monsterTransform = transform;
        this.patrolRadius = radius;
        this.distanceToResetPatrol = resetDistance;
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
            // ���� ������ �缳��
            SetRandomPatrolDestination(monster);
        }
    }

    private void SetRandomPatrolDestination(MonsterAI monster)
    {
        // ������ ���� ������ ����
        float randomX = Random.Range(-patrolRadius, patrolRadius);
        float randomZ = Random.Range(-patrolRadius, patrolRadius);
        patrolDestination = new Vector3(monsterTransform.position.x + randomX, monsterTransform.position.y, monsterTransform.position.z + randomZ);

        agent.SetDestination(patrolDestination);
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
            monster.ChangeState(new PatrolState(agent, monster.transform, monster.patrolRadius, monster.distanceToResetPatrol));
        }

        agent.SetDestination(playerTransform.position);
    }
}

// �߰� ���µ��� �ʿ信 ���� ���� ����