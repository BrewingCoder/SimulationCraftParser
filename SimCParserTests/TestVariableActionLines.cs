using Antlr4.Runtime;
using SimCParser;
using SimCParser.antlr;
using SimCParser.structure;

namespace SimCParserTests
{
    [TestClass]
    public class TestVariableActionLines
    {
        private static SimcParser Setup(string text)
        {
            SimcLexer simcLexer = new(new AntlrInputStream(text));
            ITokenStream tokens = new CommonTokenStream(simcLexer);
            var simcParser = new SimcParser(tokens)
            {
                BuildParseTree = true
            };
            return simcParser;
        }

        [TestMethod]
        public void TestVariableActionSimpleFromVisitor()
        {
            var parser = Setup("actions+=/variable,name=hold_xuen,op=set,value=!talent.invoke_xuen_the_white_tiger");
            var tree = parser.variableAction();
            var visitor = new BasicSimcVisitor();
            var results = visitor.VisitVariableAction(tree) as VariableAction;

            Assert.IsNotNull(results);
            Assert.AreEqual("actions", results.ActionClause);
            Assert.AreEqual("hold_xuen", results.VariableName);
            Assert.AreEqual("set", results.VariableOperator);
            Assert.AreEqual("!talent.invoke_xuen_the_white_tiger", results.VariableValue);
            Assert.IsNull(results.SubName);
        }

        [TestMethod]
        public void TestVariableActionSimpleFromProfile()
        {
            var parser = Setup("actions+=/variable,name=hold_xuen,op=set,value=!talent.invoke_xuen_the_white_tiger");
            var tree = parser.profile();
            var visitor = new BasicSimcVisitor();
            var results = visitor.VisitProfile(tree) as List<IAction>;

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count);
            
            Assert.IsInstanceOfType<VariableAction>(results[0]);

            var thisAction = results[0] as VariableAction;
            Assert.IsNotNull(thisAction);
            Assert.AreEqual(1,thisAction.Sequence);
            Assert.AreEqual("actions", thisAction.ActionClause);
            Assert.AreEqual("hold_xuen", thisAction.VariableName);
            Assert.AreEqual("set", thisAction.VariableOperator);
            Assert.AreEqual("!talent.invoke_xuen_the_white_tiger", thisAction.VariableValue);
        }

        [TestMethod]
        public void TestComplexVariableActionSimpleFromVisitor()
        {
            var parser = Setup("actions+=/variable,name=hold_tp_rsk,op=set,value=!debuff.skyreach_exhaustion.remains<1&cooldown.rising_sun_kick.remains<1&(set_bonus.tier30_2pc|active_enemies<5)");
            var tree = parser.variableAction();
            var visitor = new BasicSimcVisitor();
            var results = visitor.VisitVariableAction(tree) as VariableAction;

            Assert.IsNotNull(results);
            Assert.AreEqual("actions", results.ActionClause);
            Assert.AreEqual("hold_tp_rsk", results.VariableName);
            Assert.AreEqual("set", results.VariableOperator);
            Assert.AreEqual("!debuff.skyreach_exhaustion.remains<1&cooldown.rising_sun_kick.remains<1&(set_bonus.tier30_2pc|active_enemies<5)", results.VariableValue);
            Assert.IsNull(results.SubName);
        }


    }
}
