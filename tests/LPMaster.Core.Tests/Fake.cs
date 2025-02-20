using Bogus;
using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;

namespace LPMaster.Core.Tests;

public static class Fake
{
    #region Model
    public static IEnumerable<Model> CreateModel(int count) => new Faker<Model>()
            .RuleFor(m => m.Id, f => f.IndexGlobal)
            .RuleFor(m => m.Name, f => "Model" + f.IndexGlobal)
            .RuleFor(m => m.Objective, f => Objective.Minimization)
            .RuleFor(m => m.ObjectiveFunction, (f, e) => e.CreateDVar(f.Random.Int(1, 10)).ToExpression())
            .RuleFor(m => m.Constraints, (f, e) => CreateEquation(f.Random.Int(1, 10), e))
            .Generate(count);

    public static Model Model => CreateModel(1).First();
    #endregion

    #region Expression
    public static IEnumerable<Expression> CreateExpression(Model model, IEnumerable<Multi>? multis = null, int count = 1)
        => new Faker<Expression>()
                    .RuleFor(e => e.Id, f => f.IndexGlobal)
                    .RuleFor(e => e.Multis, f => multis ?? [])
                    .RuleFor(e => e.Model, f => model)
                    .Generate(count);

    public static Expression CreateEmptyExpression(Model model) => CreateExpression(model).First();

    public static Expression CreateConstantExpression(Model model) => CreateExpression(model, CreateMulti(Random.Shared.Next(2, 10))).First();
    #endregion

    #region Equation
    public static IEnumerable<Equation> CreateEquation(int count, Model model) => new Faker<Equation>()
            .RuleFor(e => e.LeftExpression, (f, e) => model?.CreateDVar(f.Random.Int(1, 10)).ToExpression() ?? CreateEmptyExpression(model!))
            .RuleFor(e => e.LeftExpressionId, (f, e) => e.LeftExpression!.Id)
            .RuleFor(e => e.RightExpression, f => model?.CreateDVar(f.Random.Int(1, 10)).ToExpression() ?? CreateEmptyExpression(model!))
            .RuleFor(e => e.RightExpressionId, (f, e) => e.RightExpression!.Id)
            .RuleFor(e => e.ModelId, f => model.Id)
            .RuleFor(e => e.Model, f => model)
            .Generate(count);
    #endregion

    #region Multi
    public static IEnumerable<Multi> CreateMulti(int count, DVar? dvar = null) => new Faker<Multi>()
        .RuleFor(m => m.Coef, f => f.Random.Double(1, 100))
        .RuleFor(m => m.DVar, f => dvar)
        .RuleFor(m => m.ColIndex, f => dvar?.ColIndex)
        .Generate(count);

    public static Multi CreateMulti(DVar? dvar = null) => CreateMulti(1, dvar).First();
    #endregion

    #region DVar
    public static IEnumerable<DVar> CreateDVar(int count, Model model) => new Faker<DVar>()
        .RuleFor(d => d.Name, f => "dvar" + f.IndexGlobal)
        .RuleFor(d => d.Model, f => model)
        .RuleFor(d => d.ModelId, f => model.Id)
        .RuleFor(d => d.ColIndex, f => f.IndexGlobal)
        .Generate(count);

    public static DVar CreateDVar(Model model) => CreateDVar(1, model).First();
    #endregion
}

public static class FakeExtensions
{
    #region DVar
    public static IEnumerable<DVar> CreateDVar(this Model model, int count) => Fake.CreateDVar(count, model);

    public static DVar CreateDVar(this Model model) => Fake.CreateDVar(model);
    #endregion

    #region Multi
    public static Multi ToMulti(this DVar dvar) => Fake.CreateMulti(dvar);

    public static IEnumerable<Multi> CreateMulti(this Model model, int count) => Fake.CreateDVar(count, model).Select(Fake.CreateMulti);

    public static Multi CreateMulti(this Model model) => Fake.CreateMulti(Fake.CreateDVar(model));
    #endregion

    #region Expression
    public static Expression CreateExpression(this Model model) => Fake.CreateExpression(model, model.CreateMulti(10), 1).First();

    public static Expression ToExpression(this IEnumerable<DVar> dvars) => Fake.CreateExpression(dvars.First().Model, dvars.Select(Fake.CreateMulti), 1).First();

    public static Expression ToExpression(this IEnumerable<Multi> multis) => Fake.CreateExpression(multis.First(d => d.Model is not null).Model!, multis, 1).First();
    #endregion
}