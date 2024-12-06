using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


BenchmarkRunner.Run<BenchmarkEmailValidator>();

public partial class EmailValidatorGenerated
{
    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex EmailRegex();

    public bool IsValidEmail(string email) => EmailRegex().IsMatch(email);
}

public class EmailValidatorStatic
{
    private static readonly Regex StaticRegex = new(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);

    public bool IsValidEmail(string email) => StaticRegex.IsMatch(email);
}

public class EmailValidatorNew
{
    public bool IsValidEmail(string email) =>
        new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").IsMatch(email);
}

public class BenchmarkEmailValidator
{
    private readonly EmailValidatorGenerated _generatedValidator = new();
    private readonly EmailValidatorStatic _staticValidator = new();
    private readonly EmailValidatorNew _newValidator = new();
    public BenchmarkEmailValidator()
    {
        emails = GenerateEmails(10000);
    }

    private readonly string[] emails;

    [Benchmark]
    public void GeneratedRegexBenchmark()
    {
        foreach (var email in emails)
            _generatedValidator.IsValidEmail(email);
    }

    [Benchmark]
    public void StaticRegexBenchmark()
    {
        foreach (var email in emails)
            _staticValidator.IsValidEmail(email);
    }

    [Benchmark]
    public void NewRegexBenchmark()
    {
        foreach (var email in emails)
            _newValidator.IsValidEmail(email);
    }

    static string[] GenerateEmails(int count)
    {
        var emailList = new string[count];
        var domains = new[] { "example.com", "domain.com", "test.org", "email.net" };

        for (int i = 0; i < count; i++)
        {
            var domain = domains[i % domains.Length];
            emailList[i] = $"user{i}@{domain}";
        }

        return emailList;
    }
}