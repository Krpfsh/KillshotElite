using UnityEngine;

public class SearchState : BaseState
{
    private float _searchTimer;
    private float _moveTimer;

    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnowPos);
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
        if(enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            _searchTimer += Time.deltaTime;
            _moveTimer += Time.deltaTime;
            if (_moveTimer > Random.Range(3, 5))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                _moveTimer = 0;

            }
            if (_searchTimer > 10)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
}
