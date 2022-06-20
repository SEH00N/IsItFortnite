using System;
using UnityEngine;

[Flags]
public enum State
{
    Idle = 0,
    Move = 1 << 0,
    Fire = 1 << 1,
    Damaged = 1 << 2,
    Ulti = 1 << 3,
    Stop = 1 << 4,
}

public class StateEnum : MonoBehaviour
{
    public State state = State.Idle;
}
