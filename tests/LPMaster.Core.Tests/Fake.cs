using Bogus;
using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using LPMaster.Application;
using Microsoft.EntityFrameworkCore;
using LPMaster.Application.Contracts.Repositories;
using Moq;

namespace LPMaster.Core.Tests;

public static class Fake
{
    #region Model
    public static IEnumerable<Model> CreateModel(int count) => new Faker<Model>()
            .RuleFor(m => m.Id, f => f.IndexGlobal)
            .RuleFor(m => m.Name, f => "Model" + f.IndexGlobal)
            .RuleFor(m => m.Objective, f => Objective.Minimization)
            .RuleFor(m => m.ObjectiveFunction, (f, e) => Fake.CreateExpression(e).First())
            .RuleFor(m => m.Constraints, (f, e) => CreateEquation(f.Random.Int(1, 10), e))
            .Generate(count);

    public static Model Model => CreateModel(1).First();
    #endregion

    #region Expression
    public static IEnumerable<Expression> CreateExpression(Model model, IEnumerable<Multi>? multis = null, int numberOfMulti = 10, int count = 1)
        => new Faker<Expression>()
                    .RuleFor(e => e.Id, f => f.IndexGlobal)
                    .RuleFor(e => e.Model, f => model)
                    .RuleFor(e => e.ModelId, f => model.Id)
                    .RuleFor(e => e.Multis, (f, e) =>
                    {
                        List<Multi> allMultis = multis?.ToList() ?? new();
                        for (int i = 0; i < numberOfMulti; i++)
                            allMultis.Add(CreateMulti(e));
                        return multis;
                    })
                    .Generate(count);

    public static Expression CreateEmptyExpression(Model model) => CreateExpression(model).First();

    public static Expression CreateConstantExpression(Model model) => CreateExpression(model).First();
    #endregion

    #region Equation
    public static IEnumerable<Equation> CreateEquation(int count, Model model) => new Faker<Equation>()
            .RuleFor(e => e.LeftExpression, (f, e) => model.CreateExpression())
            .RuleFor(e => e.LeftExpressionId, (f, e) => e.LeftExpression!.Id)
            .RuleFor(e => e.RightExpression, f => model.CreateExpression())
            .RuleFor(e => e.RightExpressionId, (f, e) => e.RightExpression!.Id)
            .RuleFor(e => e.ModelId, f => model.Id)
            .RuleFor(e => e.Model, f => model)
            .Generate(count);
    #endregion

    #region Multi
    public static IEnumerable<Multi> CreateMulti(int count, Expression expression, DVar? dVar = null) => new Faker<Multi>()
        .RuleFor(m => m.Coef, f => f.Random.Double(1, 100))
        .RuleFor(m => m.ColIndex, f => dVar?.ColIndex ?? f.Random.Int(1, 10))
        .RuleFor(m => m.Expression, f => expression)
        .RuleFor(m => m.ExpressionId, f => expression.Id)
        .RuleFor(m => m.DVar, f => dVar)
        .Generate(count);

    public static Multi CreateMulti(Expression expression, DVar? dVar = null) => CreateMulti(1, expression, dVar).First();
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

    private static readonly Lazy<IServiceProvider> _lazyServiceProvider = new(() =>
    {
        var mockModelRepo = new Mock<IModelRepository>();
        var mockEquationsRepo = new Mock<IEquationRepository>();
        var mockExpressionRepo = new Mock<IExpressionRepository>();
        var mockDVarsRepo = new Mock<IDVarRepository>();

        var services = new ServiceCollection()
            .AddLPMasterApplication()
            .AddScoped(sp => mockModelRepo.Object)
            .AddScoped(sp => mockEquationsRepo.Object)
            .AddScoped(sp => mockExpressionRepo.Object)
            .AddScoped(sp => mockDVarsRepo.Object);

        return services.BuildServiceProvider();
    });

    public static IServiceProvider Provider => _lazyServiceProvider.Value;
}

class FakeDbContext(DbContextOptions<FakeDbContext> options) : DbContext(options)
{
    DbSet<Model> Models { get; set; }

    DbSet<Equation> Equations { get; set; }

    DbSet<Expression> Expressions { get; set; }

    DbSet<Multi> Multis { get; set; }
}

public static class FakeExtensions
{
    #region DVar
    public static IEnumerable<DVar> CreateDVar(this Model model, int count) => Fake.CreateDVar(count, model);

    public static DVar CreateDVar(this Model model) => Fake.CreateDVar(model);
    #endregion

    #region Multi
    public static Multi ToMulti(this DVar dvar, Expression expression) => Fake.CreateMulti(expression, dvar);

    public static IEnumerable<Multi> CreateMulti(this Model model, int count, Expression expression)
        => Fake.CreateDVar(count, model).Select(dvar => Fake.CreateMulti(expression, dvar));

    public static Multi CreateMulti(this Expression expression) => Fake.CreateMulti(expression);
    #endregion

    #region Expression
    public static Expression CreateExpression(this Model model)
        => Fake.CreateExpression(model, multis: null, numberOfMulti: Random.Shared.Next(3, 10), 1).First();

    public static Expression ToExpression(this IEnumerable<DVar> dvars, Expression expression)
        => Fake.CreateExpression(dvars.First().Model, dvars.Select(dvar => Fake.CreateMulti(expression, dvar)), 1).First();

    public static Expression ToExpression(this IEnumerable<Multi> multis) => Fake.CreateExpression(multis.First(d => d.Model is not null).Model!, multis, 1).First();
    #endregion
}

