using UnityEngine;
public class AttackState : BaseState
{

    private float _moveTimer;
    private float _losePlayerTimer;
    private float _shotTimer;
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            _losePlayerTimer = 0;
            _moveTimer += Time.deltaTime;
            _shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if(_shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            if(_moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                _moveTimer = 0;

            }
            enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else
        {
            _losePlayerTimer += Time.deltaTime;
            if(_losePlayerTimer > 8)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
    }
    public void Shoot()
    {
        Transform gunBarrel = enemy.gunBarrel;
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunBarrel.position, enemy.transform.rotation);

        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.transform.position).normalized;

        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3, 3), Vector3.up) * shootDirection * 40;
        _shotTimer = 0;
    }
}
