namespace StateMachine.Tests;

[TestClass]
public class StateMachineTests
{
    [TestMethod]
    public void InitialState_IsSetCorrectly()
    {
        var context = new DummyContext();
        var state = new DummyState();
            
        var fsm = new StateMachineBuilder<DummyContext>()
            .AddState(state)
            .SetInitialState(state)
            .Build(context);

        Assert.AreSame(state, fsm.CurrentState);
    }

    [TestMethod]
    public void OnEnter_IsCalledOnInitialState()
    {
        var context = new DummyContext();
        var from = new DummyState();
        var to = new DummyState();

        var fsm = new StateMachineBuilder<DummyContext>()
            .AddState(from)
            .AddState(to)
            .AddTransition(from, to, ctx => true)
            .SetInitialState(from)
            .Build(context);

        fsm.Update(context);
    
        Assert.AreSame(to, fsm.CurrentState);
        Assert.IsTrue(to.EnterCalled);
    }

    [TestMethod]
    public void Transition_Occurs_WhenConditionIsTrue()
    {
        var context = new DummyContext();
        var from = new DummyState();
        var to = new DummyState();

        var fsm = new StateMachineBuilder<DummyContext>()
            .AddState(from)
            .AddState(to)
            .AddTransition(from, to, ctx => true)
            .SetInitialState(from)
            .Build(context);

        fsm.Update(context);

        Assert.AreSame(to, fsm.CurrentState);
        Assert.IsTrue(from.ExitCalled);
        Assert.IsTrue(to.EnterCalled);
    }

    [TestMethod]
    public void Transition_DoesNotOccur_WhenConditionIsFalse()
    {
        var context = new DummyContext();
        var from = new DummyState();
        var to = new DummyState();

        var fsm = new StateMachineBuilder<DummyContext>()
            .AddState(from)
            .AddState(to)
            .AddTransition(from, to, ctx => false)
            .SetInitialState(from)
            .Build(context);

        fsm.Update(context);

        Assert.AreSame(from, fsm.CurrentState);
        Assert.IsFalse(from.ExitCalled);
        Assert.IsFalse(to.EnterCalled);
    }

    [TestMethod]
    public void Throws_WhenAddingTransitionFromNonexistentState()
    {
        var from = new DummyState();
        var to = new DummyState();

        var builder = new StateMachineBuilder<DummyContext>()
            .AddState(to);

        Assert.ThrowsException<InvalidOperationException>(() => builder.AddTransition(from, to, ctx => true));
    }

    [TestMethod]
    public void Throws_WhenBuildingWithoutStates()
    {
        var context = new DummyContext();
        var builder = new StateMachineBuilder<DummyContext>();

        Assert.ThrowsException<InvalidOperationException>(() => builder.Build(context));
    }
}

public class DummyContext
{
    public bool Flag { get; set; }
}

public class DummyState : State<DummyContext>
{
    public bool EnterCalled { get; private set; }
    public bool ExitCalled { get; private set; }

    public override void OnEnter(DummyContext context)
    {
        EnterCalled = true;
    }

    public override void OnExit(DummyContext context)
    {
        ExitCalled = true;
    }
}