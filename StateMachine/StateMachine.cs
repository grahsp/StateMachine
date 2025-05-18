namespace StateMachine;

public class StateMachine<TContext> where TContext : class
{
	public State<TContext> CurrentState { get; private set; }

	internal StateMachine(State<TContext> initialState)
	{
		CurrentState = initialState;
	}

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