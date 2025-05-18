namespace StateMachine;

/// <summary>
/// Represents a state in a finite state machine. Provides hooks for entering, exiting, and updating,
/// and maintains a list of transitions to other states.
/// </summary>
/// <typeparam name="TContext">
/// The type of the context object shared between states, typically used to make transition decisions.
/// </typeparam>
public abstract class State<TContext> : IState<TContext> where TContext : class
{
	/// <summary>
	/// A list of transitions originating from this state.
	/// </summary>
	internal readonly List<Transition<TContext>> Transitions = [];

	/// <summary>
	/// Adds a transition from this state to a target state with a specified condition.
	/// </summary>
	/// <param name="target">The target state to transition to if the condition is met.</param>
	/// <param name="condition">
	/// A function that evaluates the context and returns <c>true</c> if the transition should occur.
	/// </param>
	internal void AddTransition(State<TContext> target, Func<TContext, bool> condition)
		=> Transitions.Add(new Transition<TContext>(this, target, condition));

	/// <summary>
	/// Called when the state is entered.
	/// Override this method to perform custom logic on entering the state.
	/// </summary>
	/// <param name="context">The context object passed to the FSM.</param>
	public virtual void OnEnter(TContext context) { }
	
	/// <summary>
	/// Called when the state is exited.
	/// Override this method to perform custom logic on exiting the state.
	/// </summary>
	/// <param name="context">The context object passed to the FSM.</param>
	public virtual void OnExit(TContext context) { }
	
	/// <summary>
	/// Called on every update cycle while the state is active.
	/// Override this method to perform custom update logic.
	/// </summary>
	/// <param name="context">The context object passed to the FSM.</param>
	public virtual void OnUpdate(TContext context) { }
}