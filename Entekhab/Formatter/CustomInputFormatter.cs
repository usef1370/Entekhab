using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Services.Salary.Model;
using System.Globalization;
using System.Text;

namespace Entekhab.Formatter
{
    public class CustomInputFormatter : TextInputFormatter
    {
        public CustomInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/custom"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(
            InputFormatterContext context, Encoding encoding)
        {
            var request = context.HttpContext.Request;

            using (var reader = new StreamReader(request.Body, encoding))
            {
                var text = await reader.ReadToEndAsync();

                // Split the input text into lines
                var lines = text.Split(new[] { "\r\n", "\r", "\n" },
                    StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length != 3)
                {
                    // The input does not match the expected format
                    return InputFormatterResult.Failure();
                }

                // Get the field names from the first line
                var fieldNames = lines[0].Split(new[] { '/' });

                if (fieldNames.Length != 6 ||
                    !fieldNames.SequenceEqual(new[]
                        { "FirstName", "LastName", "BasicSalary",
                      "Allowance", "Transportation", "PayDate" }))
                {
                    // The field names do not match the expected format
                    return InputFormatterResult.Failure();
                }

                // Get the field values from the second line
                var fieldValues = lines[1].Split(new[] { '/' });

                if (fieldValues.Length != 6)
                {
                    // The field values do not match the expected format
                    return InputFormatterResult.Failure();
                }
                // Parse the overtimeCalculator field
                var overtimeCalculator = lines[2].Split(':')[1].Trim();

                // Parse the field values into the model
                var model = new AddSalaryRequset
                {
                    Data = new SalaryData
                    {
                        FirstName = fieldValues[0],
                        LastName = fieldValues[1],
                        BasicSalary = decimal.Parse(fieldValues[2], CultureInfo.InvariantCulture),
                        Allowance = decimal.Parse(fieldValues[3], CultureInfo.InvariantCulture),
                        Transportation = decimal.Parse(fieldValues[4], CultureInfo.InvariantCulture),
                        PayDate = DateTime.ParseExact(fieldValues[5], "yyyy-MM-dd", CultureInfo.InvariantCulture)
                    },

                    OverTimeCalculator = overtimeCalculator,
                };

                return InputFormatterResult.Success(model);
            }
        }
    }
}
