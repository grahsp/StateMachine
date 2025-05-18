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

    [TestMethod]
    public void Transition_Occurs_WhileConditionIsTrue()
    {
         var context = new DummyContext();
         var from = new DummyState();
         var to = new DummyState();
 
         var fsm = new StateMachineBuilder<DummyContext>()
             .AddState(from)
             .AddState(to)
             .AddTransition(from, to, ctx => true)
             .AddTransition(to, from, ctx => true)
             .SetInitialState(from)
             .Build(context);

         const int updateCount = 5;
         for (var i = 0; i < updateCount; i++)
             fsm.Update(context);
 
         // IMPORTANT! subtract one because initial state is called when fsm is instantiated.
         Assert.AreEqual(updateCount, to.EnterCallCount + to.ExitCallCount);
         Assert.AreEqual(updateCount, from.EnterCallCount + from.ExitCallCount - 1);
    }

    [TestMethod]
    public void Transition_DoesNotOccur_WhenNoValidTransition()
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
 
         // Second update should have no effect since 'to' has no valid transitions
         fsm.Update(context);
         fsm.Update(context);
 
         Assert.AreSame(to, fsm.CurrentState);
         Assert.AreEqual(1, to.EnterCallCount);
         Assert.AreEqual(1, from.ExitCallCount);       
    }
    
    [TestMethod]
    public void TransitionToFirst_WhenMultipleValidTransitionsAvailable()
    {
         var context = new DummyContext();
         var from = new DummyState();
         var to1 = new DummyState();
         var to2 = new DummyState();
 
         var fsm = new StateMachineBuilder<DummyContext>()
             .AddState(from)
             .AddState(to1)
             .AddState(to2)
             .AddTransition(from, to1, ctx => true)
             .AddTransition(from, to2, ctx => true)
             .SetInitialState(from)
             .Build(context);
 
         fsm.Update(context);
 
         Assert.AreSame(to1, fsm.CurrentState);
    }
}