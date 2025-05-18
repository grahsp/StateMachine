namespace StateMachine;

/// <summary>
/// Represents a finite state machine (FSM) that manages state transitions based on conditions.
/// </summary>
/// <typeparam name="TContext">The type of the shared context object used by the states.</typeparam>
public class StateMachine<TContext> where TContext : class
{
    /// <summary>
    /// Gets the current active state of the state machine.
    /// </summary>
    public State<TContext> CurrentState { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="StateMachine{TContext}"/> class
    /// with the specified initial state.
    /// </summary>
    /// <param name="initialState">The initial state of the FSM.</param>
    internal StateMachine(State<TContext> initialState)
    {
        CurrentState = initialState;
    }

    /// <summary>
    /// Updates the state machine using the provided context.
    /// This method performs the following:
    /// <list type="number">
    /// <item><description>Calls <see cref="IState{TContext}.OnUpdate"/> on the current state.</description></item>
    /// <item><description>Evaluates all transitions from the current state.</description></item>
    /// <item><description>If a valid transition is found, it exits the current state, enters the target state, and updates <see cref="CurrentState"/>.</description></item>
    /// </list>
    /// </summary>
    /// <param name="context">The context object used by the FSM and passed to all state lifecycle methods.</param>
    public void Update(TContext context)
    {
        CurrentState.OnUpdate(context);
        
        var transition = CurrentState.Transitions
            .FirstOrDefault(t => t.Condition(context));

        if (transition is null)
            return;
        
        CurrentState.OnExit(context);
        CurrentState = transition.Target;
        CurrentState.OnEnter(context);
    }
}
