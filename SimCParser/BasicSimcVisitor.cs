using System.Runtime.InteropServices.ComTypes;
using Antlr4.Runtime.Misc;
using SimCParser.antlr;
using SimCParser.structure;

namespace SimCParser;

public class BasicSimcVisitor : SimcParserBaseVisitor<object>
{

    public override object VisitProfile([NotNull] SimcParser.ProfileContext ctx)
    {
        var actions = new List<IAction>();

        for (var i=0; i<ctx.ChildCount; i++)
        {
            var child = ctx.GetChild(i);
            
            switch (child)
            {
                case SimcParser.SimpleActionContext actionContext:
                {
                    if (VisitSimpleAction(actionContext) is SimpleAction simpleAction)
                    {
                        simpleAction.Sequence = actions.Count + 1;
                        actions.Add(simpleAction);
                    }
                    break;
                }
                case SimcParser.ConditionalActionContext conditionalActionContext:
                {
                    if (VisitConditionalAction(conditionalActionContext) is ConditionalAction conditionalAction)
                    {
                        conditionalAction.Sequence = actions.Count + 1;
                        actions.Add(conditionalAction);
                    }
                    break;
                }
                case SimcParser.VariableActionContext variableActionContext:
                {
                    if (VisitVariableAction(variableActionContext) is VariableAction variableAction)
                    {
                        variableAction.Sequence = actions.Count + 1;
                        actions.Add(variableAction);
                    }
                    break;
                }
            }
        }
        return actions;
    }

    public override object VisitSimpleAction([NotNull] SimcParser.SimpleActionContext ctx)
    {
        var simpleAction = new SimpleAction
        {
            ActionType = ACTIONTYPE.SimpleAction,
            ActionClause = ctx.actionpart().GetText(),
            ActionName = ctx.actionName.Text,
            SubName = ctx.actionpart().subName?.Text
        };
        return simpleAction;
    }

    public override object VisitConditionalAction([NotNull] SimcParser.ConditionalActionContext ctx)
    {
        var conditionalAction = new ConditionalAction
        {
            ActionType = ACTIONTYPE.ConditionalAction,
            ActionClause = ctx.actionpart().GetText(),
            ActionName = VisitDotted_name(ctx.actionName) as string,
            SubName = ctx.actionpart().subName?.Text
        };
        return conditionalAction;
    }

    public override object VisitVariableAction(SimcParser.VariableActionContext ctx)
    {
        var variableAction = new VariableAction
        {
            ActionType = ACTIONTYPE.VariableAction,
            ActionClause = ctx.actionpart().GetText(),
            ActionName = ctx.VARIABLEASSIGN().GetText(),
            VariableName = VisitDotted_name(ctx.variableName) as string,
            VariableOperator = ctx.variableOperator.Text,
            Qualifier = VisitQualifier(ctx.qualifier()) as string,
            VariableValue = VisitExp(ctx.variableValue) as string,
            VariableCondition =VisitExp(ctx.exp_condition) as string,
            ActIf = VisitExp(ctx.exp_if) as string
        };
        return variableAction;
    }

    public override object VisitDotted_name(SimcParser.Dotted_nameContext ctx)
    {
        return ctx==null ? string.Empty : ctx.GetText();
    }

    public override object VisitQualifier(SimcParser.QualifierContext ctx)
    {
        return ctx==null ? string.Empty : ctx.GetText();
    }

    public override object VisitExp(SimcParser.ExpContext ctx)
    {
        return ctx==null ? string.Empty : ctx.GetText();
    }

}