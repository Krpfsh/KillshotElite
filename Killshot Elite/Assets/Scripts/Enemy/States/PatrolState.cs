using UnityEngine;
public class PatrolState : BaseState
{
    public int WaypointIndex;
    public float WaitTimer;
    public override void Enter()
    {

    }
    public override void Perform()
    {
        PatrolCycle();
    }
    public override void Exit()
    {

    }
    public void PatrolCycle()
    {
        if(enemy.Agent.remainingDistance < .2f)
        {
            WaitTimer += Time.deltaTime;
            if(WaitTimer > 3)
            {
                if (WaypointIndex < enemy.Path.waypoints.Count - 1)
                {
                    WaypointIndex++;
                }
                else
                {
                    WaypointIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.Path.waypoints[WaypointIndex].position);
                WaitTimer = 0;
            }
        }
    }
}
