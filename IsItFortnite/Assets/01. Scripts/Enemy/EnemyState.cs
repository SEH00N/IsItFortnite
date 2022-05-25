using System;

public class EnemyState
{
    public static EnemyState Instance = null;

    [Flags]
    public enum State
    {
        Idle = 0,
        Move = 1 << 0,
        Fire = 1 << 1,
        Damaged = 1 << 2,
    }

    public State state = State.Idle;
}
