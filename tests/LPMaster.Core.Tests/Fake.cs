using Bogus;
using LPMaster.Domain.Entities;

namespace LPMaster.Core.Tests;

public static class Fake
{
    #region Model
    public static IEnumerable<Model> CreateModel(int count) => new Faker<Model>()
            .RuleFor(m => m.Id, f => f.IndexGlobal)
            .RuleFor(m => m.Object, f => f.Random.Int(0, 1))
            .RuleFor(m => m.ObjectiveFunction, (f, e) => e.CreateDVar(f.Random.Int(1, 10)).ToExpression())
            .RuleFor(m => m.Constraints, f => CreateEquation(f.Random.Int(1, 10)))
            .Generate(count);

    public static Model Model => CreateModel(1).First();
    #endregion

    #region Expression
    public static IEnumerable<Expression> CreateExpression(IEnumerable<Multi>? multis = null, int count = 1)
        => new Faker<Expression>()
                    .RuleFor(e => e.Id, f => f.IndexGlobal)
                    .RuleFor(e => e.Multis, f => multis ?? [])
                    .Generate(count);

    public static Expression EmptyExpression => CreateExpression().First();

    public static Expression ConstantExpression => CreateExpression(CreateMulti(Random.Shared.Next(2, 10))).First();
    #endregion

    #region Equation
    public static IEnumerable<Equation> CreateEquation(int count, Model? model = null) => new Faker<Equation>()
            .RuleFor(e => e.Id, f => f.IndexGlobal)
            .RuleFor(e => e.LeftExpression, (f, e) => model?.CreateDVar(f.Random.Int(1, 10)).ToExpression() ?? EmptyExpression)
            .RuleFor(e => e.LeftExpressionId, (f, e) => e.LeftExpression.Id)
            .RuleFor(e => e.RightExpression, f => model?.CreateDVar(f.Random.Int(1, 10)).ToExpression() ?? EmptyExpression)
            .RuleFor(e => e.RightExpressionId, (f, e) => e.RightExpression.Id)
            .Generate(count);

    public static Equation EmptyEquation => CreateEquation(1).First();
    #endregion

    #region Multi
    public static IEnumerable<Multi> CreateMulti(int count, DVar? dvar = null) => new Faker<Multi>()
        .RuleFor(m => m.Coef, f => f.Random.Double(1, 100))
        .RuleFor(m => m.DVar, f => dvar)
        .RuleFor(m => m.DVarId, f => dvar?.Id)
        .Generate(count);

    public static Multi CreateMulti(DVar? dvar = null) => CreateMulti(1, dvar).First();
    #endregion

    #region DVar
    public static IEnumerable<DVar> CreateDVar(int count, Model model) => new Faker<DVar>()
        .RuleFor(d => d.Id, f => f.IndexGlobal)
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
    public static Expression CreateExpression(this Model model) => Fake.CreateExpression(model.CreateMulti(10), 1).First();

    public static Expression ToExpression(this IEnumerable<DVar> dvars) => Fake.CreateExpression(dvars.Select(Fake.CreateMulti), 1).First();

    public static Expression ToExpression(this IEnumerable<Multi> multis) => Fake.CreateExpression(multis, 1).First();
    #endregion
}