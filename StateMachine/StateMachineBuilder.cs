namespace StateMachine;

public class StateMachineBuilder<TContext> where TContext : class
{
	private State<TContext>? _initialState;
	private readonly HashSet<State<TContext>> _states = [];

	public StateMachineBuilder<TContext> AddState(State<TContext> state)
	{
		_states.Add(state);
		return this;
	}

	public StateMachineBuilder<TContext> AddTransition(State<TContext> from, State<TContext> to,
		Func<TContext, bool> condition)
	{
		if (!_states.TryGetValue(from, out var state))
			throw new InvalidOperationException("State not found.");

		state.AddTransition(to, condition);
		return this;
	}

	public StateMachineBuilder<TContext> SetInitialState(State<TContext> state)
	{
		_initialState = state;
		return this;
	}

	public StateMachine<TContext> Build(TContext context)
	{
		var state = _initialState ?? _states.FirstOrDefault();
		if (state == null)
			throw new InvalidOperationException("Cannot build without states!");
		
		var fsm = new StateMachine<TContext>(state);
		fsm.CurrentState.OnEnter(context);
		
		return fsm;
	}
}