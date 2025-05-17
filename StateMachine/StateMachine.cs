namespace StateMachine;

public class StateMachine<TContext> where TContext : class
{
	public State<TContext> CurrentState { get; private set; }

	internal StateMachine(State<TContext> initialState)
	{
		CurrentState = initialState;
	}
}

public class Transition<TContext> where TContext : class
{
	public State<TContext> Source { get; }
	public State<TContext> Target { get; }
	public Func<TContext, bool> Condition { get; }
	

	public Transition(State<TContext> source, State<TContext> target, Func<TContext, bool> condition)
	{
		Source = source;
		Target = target;
		Condition = condition;
	}
}

public abstract class State<TContext> : IState where TContext : class
{
	internal readonly List<Transition<TContext>> Transitions = [];

	internal void AddTransition(State<TContext> target, Func<TContext, bool> condition)
		=> Transitions.Add(new Transition<TContext>(this, target, condition));

	public abstract void OnEnter();
	public abstract void OnExit();
}

public interface IState
{
	void OnEnter();
	void OnExit();
}