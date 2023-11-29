using UnityEngine;

public class EnemyFindPlayerState : EnemyBaseState
{
    private PolygonCollider2D baseCollider => stateManager.BaseCollider;
    private Vector2 _currentTarget;
    private double _startTime;
    
    public EnemyFindPlayerState(EnemyStateManager.EnemyState state, EnemyStateManager enemyStateManager) : base(state, enemyStateManager)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered Find Player State");

        stateManager.gotHit = false;

        _currentTarget = Perception.player.transform.position;
        Pathfinding.CalculateNewPath(_currentTarget);
        _startTime = Time.time;
    }

    public override void ExitState()
    {
        Pathfinding.StopCalculatingPath();
    }

    public override EnemyStateManager.EnemyState GetNextState()
    {
        // if (Time.time - _startTime > 5)
        //     return EnemyStateManager.EnemyState.Capturing;
        
        if (Perception.CanSeePlayer)
            return EnemyStateManager.EnemyState.Shooting;

        return stateKey;
    }

    public override void OnTriggerEnter(Collider2D other)
    {
    }

    public override void OnTriggerExit(Collider2D other)
    {
    }

    public override void OnTriggerStay(Collider2D other)
    {
    }

    public override void UpdateState()
    {
        if (!Perception.player.transform.position.Equals(_currentTarget))
        {
            _currentTarget = Perception.player.transform.position;
            Pathfinding.CalculateNewPath(_currentTarget);
        }
            
    }
}