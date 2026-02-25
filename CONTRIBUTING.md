# Contributing to SharpIppNext

First off, thank you for considering contributing to `SharpIppNext`! It's people like you who make the open-source community such an amazing place to learn, inspire, and create.

## Getting Started

To contribute to this project, you will need:

- [.NET SDK 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) or later.
- A C# IDE (e.g., Visual Studio 2026 or VS Code).

### Clone the Repository

It is recommended to create a fork of the repository (fork button in GitHub) and clone your fork. Alternatively, you can clone the main repository directly:

```bash
git clone https://github.com/danielklecha/SharpIppNext.git
cd SharpIppNext
```

## How to Contribute

### Building the Project

You can build the project using the .NET CLI:

```bash
dotnet build --configuration Release
```

### Running Tests

We use MSTest V2 for testing. To run all tests and collect coverage information, use:

```bash
dotnet test --configuration Release /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat="opencover,lcov"
```

Please ensure all tests pass before submitting a pull request.

### Code Coverage

When running tests with the coverage flags as shown above, coverage reports are generated in the `TestResults/` directory.

- **Local Review**: You can use tools like [ReportGenerator](https://github.com/danielpalme/ReportGenerator) to convert the `opencover` or `lcov` reports into a human-readable HTML format for local review.
- **CI/CD Integration**: Our CI pipeline automatically publishes coverage reports to [Coveralls](https://coveralls.io/) for every push and pull request.

### Coding Standards

- Follow standard C# naming conventions and best practices.
- Adhere to the rules defined in the [.editorconfig](.editorconfig) file.
- Keep methods focused and classes loosely coupled.

### Pull Request Process

1. **Create a Branch**: Use a descriptive name for your branch (e.g., `feature/add-cups-operation` or `bugfix/fix-serialization-error`).
2. **Commit Changes**: Make atomic commits with clear, descriptive messages.
3. **Draft Pull Request**: If your work is still in progress, open a Draft Pull Request to get early feedback.
4. **Final Pull Request**: Ensure your branch is up-to-date with `master` and all CI checks pass.
5. **Review**: At least one maintainer will review your PR before merging.

## Reporting Issues

If you find a bug or have a feature request, please search the [existing issues](https://github.com/danielklecha/SharpIppNext/issues) before opening a new one.

---

By contributing to this project, you agree that your contributions will be licensed under the project's [MIT License](LICENSE.txt).
