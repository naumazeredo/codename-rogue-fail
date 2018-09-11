using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class State
{

    [SerializeField] private string _name;
    [SerializeField] private float _stateDuration;
    [SerializeField] private float _randomness;
    [SerializeField] private int _nextState;

    public int NextState
    {
        set { this._nextState = value; }
        get { return this._nextState;  }
    }
    public float Duration
    {
        get { return _stateDuration; }
    }
    public UnityEvent OnStateEnter;
    public UnityEvent OnStateUpdate;
    public UnityEvent OnStateExit;
}
