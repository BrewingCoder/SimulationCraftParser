using Antlr4.Runtime;
using SimCParser;
using SimCParser.antlr;
using SimCParser.structure;

namespace SimCParserTests;

[TestClass]
public class TestSimpleActionLines
{

    private static SimcParser Setup(string text){

        SimcLexer simcLexer = new(new AntlrInputStream(text));
        ITokenStream tokens = new CommonTokenStream(simcLexer);
        var simcParser = new SimcParser(tokens)
        {
            BuildParseTree = true
        };
        return simcParser;
    }


    [TestMethod]
    public void TestSimpleActionLineFromVisitor()
    {
        var parser = Setup("actions=flask\n");
        var tree = parser.simpleAction();
        var visitor = new BasicSimcVisitor();
        var results = visitor.VisitSimpleAction(tree) as SimpleAction;

        Assert.IsNotNull(results);
        Assert.AreEqual("actions",results.ActionClause);
        Assert.AreEqual("flask",results.ActionName);
        Assert.AreEqual(ACTIONTYPE.SimpleAction,results.ActionType);

    }

    [TestMethod]
    public void TestSimpleActionLineFromProfile()
    {
        var parser = Setup("actions=flask\n");
        var tree = parser.profile();
        var visitor = new BasicSimcVisitor();
        var results = visitor.VisitProfile(tree) as List<IAction>;

        Assert.IsNotNull(results);
        Assert.AreEqual(1,results.Count);
        Assert.IsInstanceOfType<SimpleAction>(results[0]);
        Assert.AreEqual("actions",results[0].ActionClause);
        Assert.AreEqual("flask",results[0].ActionName);
        Assert.AreEqual(ACTIONTYPE.SimpleAction,results[0].ActionType);
    }

    [TestMethod]
    public void TestAdditiveActionLineFromVisitor() {
        var parser = Setup("actions+=/roll\n");
        var tree = parser.simpleAction();
        var visitor = new BasicSimcVisitor();
        var results = visitor.VisitSimpleAction(tree) as SimpleAction;

        Assert.IsNotNull(results);
        Assert.AreEqual("actions",results.ActionClause);
        Assert.AreEqual("roll",results.ActionName);
        Assert.AreEqual(ACTIONTYPE.SimpleAction,results.ActionType);        
    }

    [TestMethod]
    public void TestAdditiveActionLineFromProfile() {
        var parser = Setup("actions+=/roll\n");
        var tree = parser.profile();
        var visitor = new BasicSimcVisitor();
        var results = visitor.VisitProfile(tree) as List<IAction>;

        Assert.IsNotNull(results);
        Assert.AreEqual(1,results.Count);
        Assert.IsInstanceOfType<SimpleAction>(results[0]);
        Assert.AreEqual("actions",results[0].ActionClause);
        Assert.AreEqual("roll",results[0].ActionName);
        Assert.AreEqual(ACTIONTYPE.SimpleAction,results[0].ActionType);
    }



    [TestMethod]
    public void TestSubActionLineFromVisitor() {
        SimcParser parser = Setup("actions.precombat+=/snapshot_stats\n");
        var tree = parser.simpleAction();
        var visitor = new BasicSimcVisitor();
        var results = visitor.VisitSimpleAction(tree) as SimpleAction;

        Assert.IsNotNull(results);
        Assert.AreEqual("actions.precombat",results.ActionClause);
        Assert.AreEqual("precombat",results.SubName);
        Assert.AreEqual("snapshot_stats",results.ActionName);
        Assert.AreEqual(ACTIONTYPE.SimpleAction,results.ActionType);             
    }
    [TestMethod]
    public void TestSubActionLineFromProfile() {
        SimcParser parser = Setup("actions.precombat+=/snapshot_stats\n");
        var tree = parser.profile();
        var visitor = new BasicSimcVisitor();
        var results = visitor.VisitProfile(tree) as List<IAction>;

        Assert.IsNotNull(results);
        Assert.AreEqual(1,results.Count);
        Assert.IsInstanceOfType<SimpleAction>(results[0]);
        Assert.AreEqual("actions.precombat",results[0].ActionClause);
        Assert.AreEqual("precombat",results[0].SubName);
        Assert.AreEqual("snapshot_stats",results[0].ActionName);
        Assert.AreEqual(ACTIONTYPE.SimpleAction,results[0].ActionType);
       
    }


    /// <summary>
    /// Ensure that a conditional action line returns null when we try to
    /// Visit it with a simple Action visitor
    /// </summary>
    [TestMethod]
    public void TestSimpleActionShouldBeNull() {
        SimcParser parser = Setup("actions.precombat+=/expel_harm,if=chi<chi.max\n");
        var tree = parser.profile();
        var visitor = new BasicSimcVisitor();
        var results = visitor.VisitProfile(tree) as List<IAction>;
        
        Assert.IsNotNull(results);
        Assert.AreEqual(1,results.Count);
        Assert.IsNotInstanceOfType<SimpleAction>(results[0]);
        Assert.AreEqual("actions.precombat",results[0].ActionClause);
        Assert.AreEqual("precombat",results[0].SubName);
        Assert.AreEqual("expel_harm",results[0].ActionName);
        Assert.AreNotEqual(ACTIONTYPE.SimpleAction,results[0].ActionType);

    }
}