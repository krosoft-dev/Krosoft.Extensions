using FluentValidation;
using FluentValidation.TestHelper;
using Krosoft.Extensions.Testing;
using Krosoft.Extensions.Validations.Extensions;
using Krosoft.Extensions.WebApi.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Krosoft.Extensions.Validations.Tests.Extensions;

[TestClass]
public class RuleBuilderExtensionsTests : BaseTest
{
    private IValidator<TestObject> _validator = null!;

    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddWebApi(configuration, typeof(TestValidator).Assembly);
    }

    [TestInitialize]
    public void SetUp()
    {
        var serviceProvider = CreateServiceCollection();
        _validator = serviceProvider.GetRequiredService<IValidator<TestObject>>();

        Check.That(_validator).IsNotNull();
    }

    [TestMethod]
    public void Should_Have_Error_When_InvoiceId_Is_Empty()
    {
        var query = new TestObject { InvoiceId = string.Empty, Status = StatusCode.Legal };

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.InvoiceId)
              .WithErrorMessage("'Invoice Id' ne doit pas être vide.");
    }

    [TestMethod]
    public void Should_Have_Error_When_InvoiceId_Is_Null()
    {
        var query = new TestObject { InvoiceId = null, Status = StatusCode.Legal };

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.InvoiceId);
    }

    [TestMethod]
    public void Should_Have_Error_When_InvoiceId_Is_Whitespace()
    {
        var query = new TestObject { InvoiceId = "   ", Status = StatusCode.Legal };

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.InvoiceId);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_InvoiceId_Is_Valid()
    {
        var query = new TestObject { InvoiceId = "INV-12345", Status = StatusCode.Legal };

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveValidationErrorFor(x => x.InvoiceId);
    }

    [TestMethod]
    public void Should_Have_Error_When_TypeCode_Is_Empty()
    {
        var query = new TestObject { InvoiceId = "INV-12345", Status = string.Empty };

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Status)
              .WithErrorMessage("'Status' ne doit pas être vide.");
    }

    [TestMethod]
    public void Should_Have_Error_When_TypeCode_Is_Null()
    {
        var query = new TestObject { InvoiceId = "INV-12345", Status = null };

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Status);
    }

    [TestMethod]
    public void Should_Have_Error_When_TypeCode_Is_Invalid()
    {
        var query = new TestObject { InvoiceId = "INV-12345", Status = "INVALID" };

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Status)
              .WithErrorMessage("'Status' must be one of these values: LEGAL or READABLE");
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_TypeCode_Is_Legal()
    {
        var query = new TestObject { InvoiceId = "INV-12345", Status = StatusCode.Legal };

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveValidationErrorFor(x => x.Status);
    }

    [TestMethod]
    public void Should_Not_Have_Error_When_TypeCode_Is_Lisible()
    {
        var query = new TestObject { InvoiceId = "INV-12345", Status = StatusCode.Lisible };

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveValidationErrorFor(x => x.Status);
    }

    [TestMethod]
    public void Should_Be_Valid_When_All_Properties_Are_Valid()
    {
        var query = new TestObject
        {
            InvoiceId = "INV-12345",
            Status = StatusCode.Lisible,
            Count = 2,
            Code = StatusCode.Legal
        };

        var result = _validator.Validate(query);

        Check.That(result.IsValid).IsTrue();
        Check.That(result.Errors).IsEmpty();
    }

    [TestMethod]
    public void Should_Have_Multiple_Errors_When_All_Properties_Are_Invalid()
    {
        var query = new TestObject
        {
            InvoiceId = string.Empty,
            Status = "INVALID"
        };

        var result = _validator.Validate(query);

        Check.That(result.IsValid).IsFalse();
        Check.That(result.Errors).HasSize(6);
        Check.That(result.Errors.Select(e => e.PropertyName))
             .ContainsExactly("InvoiceId", "Code", "Code", "Status", "Count", "Count");
    }

    [TestMethod]
    public void Should_Throw_ArgumentException_When_No_Valid_Options_Provided()
    {
        var validator = new TestValidator();
        Check.ThatCode(() => validator.RuleFor(x => x.Status).In())
             .Throws<ArgumentException>()
             .WithMessage("At least one valid option is required. (Parameter 'validOptions')");
    }

    [TestMethod]
    public void Should_Throw_ArgumentException_When_Null_Valid_Options_Provided()
    {
        var validator = new TestValidator();
        Check.ThatCode(() => validator.RuleFor(x => x.Status).In(null!))
             .Throws<ArgumentException>()
             .WithMessage("At least one valid option is required. (Parameter 'validOptions')");
    }

    [TestMethod]
    public void Should_Format_Single_Option_Correctly()
    {
        var validator = new TestValidator();
        validator.RuleFor(x => x.Code).In("Active");
        var testObject = new TestObject { Code = "Inactive", Status = StatusCode.Legal, Count = 1, InvoiceId = Guid.NewGuid().ToString() };

        var result = _validator.TestValidate(testObject);

        result.ShouldHaveValidationErrorFor(x => x.Code)
              .WithErrorMessage("'Code' must be one of these values: LEGAL");
    }

    private class TestValidator : AbstractValidator<TestObject>
    {
        public TestValidator()
        {
            RuleFor(m => m.InvoiceId).NotEmpty();
            RuleFor(m => m.Code).NotEmpty().In(StatusCode.Legal);
            RuleFor(m => m.Status).NotEmpty().In(StatusCode.Legal, StatusCode.Lisible);
            RuleFor(m => m.Count).NotEmpty().In(1, 2, 3);
        }
    }

    private class TestObject
    {
        public string? InvoiceId { get; set; }
        public string? Code { get; set; }
        public string? Status { get; set; }
        public int Count { get; set; }
    }

    private static class StatusCode
    {
        public const string Legal = "LEGAL";
        public const string Lisible = "READABLE";
    }
}