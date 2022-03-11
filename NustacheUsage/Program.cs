using Newtonsoft.Json;
using Nustache.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace NustacheUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var currentAssembly = typeof(Program).Assembly;

            // Get the model from filed
            using var modelStream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.model.json");
            using StreamReader modelReader = new StreamReader(modelStream);
            string stringModel = modelReader.ReadToEnd();
                        
            // Deserialize the string model to object
            var model = JsonConvert.DeserializeObject(stringModel);

            // Get the template from file
            using var fileStream = currentAssembly.GetManifestResourceStream($"{currentAssembly.GetName().Name}.template.html");
            using StreamReader fileReader = new StreamReader(fileStream);
            string templateFile = fileReader.ReadToEnd();

            // Render the html
            var result = Render.StringToString(templateFile, model);
            
            // Write the html result to file
            var resultFile = "result.html";
            File.WriteAllText(resultFile, result);

            // Open the newly generated html file
            Process.Start(@"cmd.exe ", @"/c " + resultFile);
        }
    }
}
