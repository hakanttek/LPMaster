using Bogus;
using LPMaster.Domain.Entities;
using LPMaster.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using LPMaster.Application;
using Microsoft.EntityFrameworkCore;
using LPMaster.Application.Contracts.Repositories;
using Moq;
using LPMaster.Application.Contracts.Repositories.Base;

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

    #region Dependency Injection
    private static readonly Lazy<IServiceProvider> _lazyServiceProvider = new(() =>
    {
        static Mock<IModelRepository> CreateMockModelRepo(DbContext context) => new Mock<IModelRepository>().SetupMockRepo<IModelRepository, Model>(context);
        static Mock<IEquationRepository> CreateMockEquationRepo(DbContext context) => new Mock<IEquationRepository>().SetupMockRepo<IEquationRepository, Equation>(context);
        static Mock<IExpressionRepository> CreateMockExpressionRepo(DbContext context) => new Mock<IExpressionRepository>().SetupMockRepo<IExpressionRepository, Expression>(context);
        static Mock<IDVarRepository> CreateMockDvarRepo(DbContext context) => new Mock<IDVarRepository>().SetupMockRepo<IDVarRepository, DVar>(context);

        var services = new ServiceCollection()
            .AddLPMasterApplication()
            .AddDbContext<DbContext>(opt => opt.UseInMemoryDatabase("DefaultFakeDB"))
            .AddScoped(sp => CreateMockModelRepo(sp.GetRequiredService<DbContext>()).Object)
            .AddScoped(sp => CreateMockEquationRepo(sp.GetRequiredService<DbContext>()).Object)
            .AddScoped(sp => CreateMockExpressionRepo(sp.GetRequiredService<DbContext>()).Object)
            .AddScoped(sp => CreateMockDvarRepo(sp.GetRequiredService<DbContext>()).Object);

        return services.BuildServiceProvider();
    });

    public static IServiceProvider Provider => _lazyServiceProvider.Value;
    #endregion
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

public static class MockRepositoryExtensions
{
    public static Mock<TRepository> SetupMockRepo<TRepository, TEntity>(this Mock<TRepository> mock, DbContext context) where TRepository : class, IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> entities = context.Set<TEntity>();
        
        #region Create
        mock.Setup(repo => repo.CreateAsync(It.IsAny<TEntity>())).Returns<TEntity>(async entity =>
        {
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        });
        mock.Setup(repo => repo.CreateAsync(It.IsAny<IEnumerable<TEntity>>())).Returns<IEnumerable<TEntity>>(async entityList =>
        {
            await entities.AddRangeAsync(entityList);
            await context.SaveChangesAsync();
        });
        #endregion
        #region Read
        mock.Setup(repo => repo.ReadAllAsync()).Returns(async () => await entities.AsNoTracking().ToListAsync());
        #endregion
        #region Update
        mock.Setup(repo => repo.UpdateAsync(It.IsAny<TEntity>())).Returns<TEntity>(async entity =>
        {
            entities.Update(entity);
            await context.SaveChangesAsync();
        });
        mock.Setup(repo => repo.UpdateAsync(It.IsAny<IEnumerable<TEntity>>())).Returns<IEnumerable<TEntity>>(async entityList =>
        {
            entities.UpdateRange(entityList);
            await context.SaveChangesAsync();
        });
        #endregion
        #region Delete
        mock.Setup(repo => repo.DeleteAsync(It.IsAny<TEntity>())).Returns<TEntity>(async entity =>
        {
            entities.Remove(entity);
            await context.SaveChangesAsync();
        });
        mock.Setup(repo => repo.DeleteAsync(It.IsAny<IEnumerable<TEntity>>())).Returns<IEnumerable<TEntity>>(async entityList =>
        {
            entities.RemoveRange(entityList);
            await context.SaveChangesAsync();
        });
        #endregion
        return mock;
    }
}