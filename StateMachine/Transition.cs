namespace StateMachine;

/// <summary>
/// Represents a transition between two states in a finite state machine (FSM).
/// </summary>
/// <typeparam name="TContext">The type of the context object shared across states.</typeparam>
/// <param name="source">The source state from which the transition originates.</param>
/// <param name="target">The target state to which the transition leads.</param>
/// <param name="condition">The condition that must be met for the transition to occur.</param>
public class Transition<TContext>(
	State<TContext> source,
	State<TContext> target,
	Func<TContext, bool> condition)
	where TContext : class
{
	/// <summary>
	/// Gets the source state of the transition.
	/// </summary>
	public State<TContext> Source { get; } = source;

	/// <summary>
	/// Gets the target state of the transition.
	/// </summary>
	public State<TContext> Target { get; } = target;

	/// <summary>
	/// Gets the condition function that determines whether the transition should occur.
	/// The condition is evaluated using the current FSM context.
	/// </summary>
	public Func<TContext, bool> Condition { get; } = condition;
}