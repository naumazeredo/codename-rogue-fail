using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class FSM : MonoBehaviour
{
    [SerializeField] public List<State> states ;
    [SerializeField] private int current;
    private float timer;

    private void Awake()
    {
        states = new List<State>();
        timer = 0f;
    }

    private void Update()
    {
        if (current >= 0 && current < states.Count)
        {
            states[current].OnStateUpdate.Invoke();
            if (timer >= states[current].Duration)
            {
                timer = 0f;
                states[current].OnStateExit.Invoke();
                current = states[current].NextState;
                states[current].OnStateEnter.Invoke();
            }
            timer += Time.deltaTime;
        }
    }
}

namespace Player
{
    public class Walking : State{}
    public class LedgeIdle : State {}
    public class LedgeLifting : State {}
}

