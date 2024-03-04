using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState ActiveState;
    public PatrolState PatrolState;

    public void Initialize()
    {
        PatrolState = new PatrolState();
        ChangeState(PatrolState);
    }
    private void Update()
    {
        if(ActiveState != null)
        {
            ActiveState.Perform();
        }
    }
    public void ChangeState(BaseState newState)
    {
        if (ActiveState != null)
        {
            ActiveState.Exit();
        }

        ActiveState = newState;
        if(ActiveState != null)
        {
            //setup new state
            ActiveState.StateMachine = this;
            ActiveState.enemy = GetComponent<Enemy>();
            ActiveState.Enter();
        }
    }
}
