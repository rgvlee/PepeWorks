using API.Features.Locations;
using AutoFixture;
using FluentValidation.TestHelper;

namespace API.Tests.Features.Locations;

public class AddLocationTests
{
    public class AddLocationValidatorTests
    {
        private static AddLocation.AddLocationCommand CreateAddLocationCommand(Action<CreateAddLocationCommandOptions> createOptions)
        {
            var resolvedCreateOptions = new CreateAddLocationCommandOptions();
            createOptions.Invoke(resolvedCreateOptions);
            return CreateAddLocationCommand(resolvedCreateOptions);
        }

        private static AddLocation.AddLocationCommand CreateAddLocationCommand(CreateAddLocationCommandOptions createOptions)
        {
            var addLocationCommand = new AddLocation.AddLocationCommand
            {
                Code = createOptions.Code,
                Name = createOptions.Name
            };
            return addLocationCommand;
        }

        private static string CreateString(short? stringLength)
        {
            var fixture = new Fixture();
            return stringLength is null ? fixture.Create<string>() : string.Join(string.Empty, fixture.CreateMany<char>(stringLength.Value));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void When_CodeIsEmpty_Expect_Error(string code)
        {
            var addLocationCommand = CreateAddLocationCommand(x => x.Code = code);
            var validator = new AddLocation.Validator();

            var validationResult = validator.TestValidate(addLocationCommand);

            validationResult.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [Test]
        public void When_CodeIsGreaterThanMaximumLength_Expect_Error()
        {
            var addLocationCommand = CreateAddLocationCommand(x => x.Code = CreateString(11));
            var validator = new AddLocation.Validator();

            var validationResult = validator.TestValidate(addLocationCommand);

            validationResult.ShouldHaveValidationErrorFor(x => x.Code);
        }

        [TestCase(1)]
        [TestCase(9)]
        [TestCase(10)]
        public void When_CodeIsValid_Expect_Success(short codeLength)
        {
            var addLocationCommand = CreateAddLocationCommand(x => x.Code = CreateString(codeLength));
            var validator = new AddLocation.Validator();

            var validationResult = validator.TestValidate(addLocationCommand);

            validationResult.ShouldNotHaveValidationErrorFor(x => x.Code);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void When_NameIsEmpty_Expect_Error(string name)
        {
            var addLocationCommand = CreateAddLocationCommand(x => x.Name = name);
            var validator = new AddLocation.Validator();

            var validationResult = validator.TestValidate(addLocationCommand);

            validationResult.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void When_NameIsGreaterThanMaximumLength_Expect_Error()
        {
            var addLocationCommand = CreateAddLocationCommand(x => x.Name = CreateString(51));
            var validator = new AddLocation.Validator();

            var validationResult = validator.TestValidate(addLocationCommand);

            validationResult.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [TestCase(1)]
        [TestCase(49)]
        [TestCase(50)]
        public void When_NameIsValid_Expect_Success(short nameLength)
        {
            var addLocationCommand = CreateAddLocationCommand(x => x.Name = CreateString(nameLength));
            var validator = new AddLocation.Validator();

            var validationResult = validator.TestValidate(addLocationCommand);

            validationResult.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        private class CreateAddLocationCommandOptions
        {
            public CreateAddLocationCommandOptions()
            {
                Code = CreateString(10);
                Name = CreateString(50);
            }

            public string Code { get; set; }

            public string Name { get; set; }
        }
    }

    public class AddLocationHandlerTests
    {
        // public void Foo()
        // {
        //     var handler = new AddLocation.Handler();
        // }
    }
}