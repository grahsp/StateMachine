namespace StateMachine.Tests;

[TestClass]
public class StateMachineTests
{
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
}