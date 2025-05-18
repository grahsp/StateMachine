namespace StateMachine.Tests;

public class DummyContext
{
    public bool Flag { get; set; }
}

public class DummyState : State<DummyContext>
{
    public int EnterCallCount { get; private set; }
    public bool EnterCalled => EnterCallCount > 0;
    
    public int ExitCallCount { get; private set; }
    public bool ExitCalled => ExitCallCount > 0;

    public override void OnEnter(DummyContext context)
    {
        EnterCallCount++;
    }

    public override void OnExit(DummyContext context)
    {
        ExitCallCount++;
    }
}