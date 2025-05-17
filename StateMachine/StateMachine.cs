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

public class Transition<TContext>
	(State<TContext> source, State<TContext> target, Func<TContext, bool> condition)
	where TContext : class
{
	public State<TContext> Source { get; } = source;
	public State<TContext> Target { get; } = target;
	public Func<TContext, bool> Condition { get; } = condition;
}

public abstract class State<TContext> : IState<TContext> where TContext : class
{
	internal readonly List<Transition<TContext>> Transitions = [];

	internal void AddTransition(State<TContext> target, Func<TContext, bool> condition)
		=> Transitions.Add(new Transition<TContext>(this, target, condition));

	public virtual void OnEnter(TContext context) { }
	public virtual void OnExit(TContext context) { }
	public virtual void OnUpdate(TContext context) { }
}

public interface IState<in TContext> where TContext : class
{
	void OnEnter(TContext context);
	void OnExit(TContext context);
	void OnUpdate(TContext context);
}