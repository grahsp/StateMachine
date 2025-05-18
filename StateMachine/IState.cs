namespace StateMachine;

/// <summary>
/// Represents a state in a finite state machine (FSM) that operates on a shared context.
/// </summary>
/// <typeparam name="TContext">The type of the context object used by the FSM.</typeparam>
public interface IState<in TContext> where TContext : class
{
	/// <summary>
	/// Called when the state is entered.
	/// Used to perform any setup or initialization logic.
	/// </summary>
	/// <param name="context">The context object passed to the FSM.</param>
	void OnEnter(TContext context);

	/// <summary>
	/// Called when the state is exited.
	/// Used to perform any cleanup logic before transitioning to another state.
	/// </summary>
	/// <param name="context">The context object passed to the FSM.</param>
	void OnExit(TContext context);

	/// <summary>
	/// Called during each update cycle while the state is active.
	/// Used to perform ongoing logic such as processing or checking conditions.
	/// </summary>
	/// <param name="context">The context object passed to the FSM.</param>
	void OnUpdate(TContext context);
}