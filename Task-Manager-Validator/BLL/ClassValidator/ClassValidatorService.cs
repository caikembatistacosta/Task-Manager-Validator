using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Entities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BLL.ClassValidator
{
    public class ClassValidatorService
    {

        //Checar nomenclatura métodos
        //Checar nomenclatura propriedades
        //Se checkbox de Entity for marcado, checar pra ver se existe um campo id inteiro
        //Se checkbox de Entity for marcado, checar pra ver se a classe tem um construtor sem parâmetro


        static Action<string> Write = Console.WriteLine;

        public static void Validator(string codeToCompile)
        {
            Write("Let's compile!");
            Write("Parsing the code into the SyntaxTree");
            SyntaxTree syntaxTree = ParseSyntaxTree(codeToCompile);

            string assemblyName = Path.GetRandomFileName();
            var refPaths = new[] {
                typeof(System.Object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll")
            };
            MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

            Write("Adding the following references");
            foreach (var r in refPaths)
                Write(r);

            Write("Compiling ...");
            CSharpCompilation compilation = CompileCode(assemblyName, syntaxTree, references);

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    Write("Compilation failed!");
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("\t{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    Write("Compilation successful! Now instantiating and executing the code ...");
                    ms.Seek(0, SeekOrigin.Begin);

                    Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    Type type = assembly.ExportedTypes.First();
                    MethodInfo[] metodos = ValidatorMethods(type);
                    ConstructorInfo[] construtores = ValidatorContructors(type);

                    PropertyInfo[] propriedades = type.GetProperties();
                    ValidatorProperty(propriedades);

                    
                }
            }


        }
        public static SyntaxTree ParseSyntaxTree(string codeToCompile)
        {
            try
            {
                return CSharpSyntaxTree.ParseText(codeToCompile);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro no parse!");
            }
        }
        public static CSharpCompilation CompileCode(string assemblyName, SyntaxTree syntaxTree, MetadataReference[] references)
        {
            try
            {
                return CSharpCompilation.Create(
                    assemblyName,
                    syntaxTrees: new[] { syntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro no compile!");
            }
        }

        public static MethodInfo[] ValidatorMethods(Type type)
        {
            try
            {
                return type.GetMethods();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro no methods!");
            }
        }
        public static ConstructorInfo[] ValidatorContructors(Type type)
        {
            List<string> erros = new List<string>();
            try
            {
                var construtores = type.GetConstructors();
                //Verifica se o arquivo que foi feito upload é de uma classe que herda de Entity
                if (type.BaseType == typeof(Entity))
                {
                    bool hasParameterelessConstructor = false;
                    foreach (var item in construtores)
                    {
                        //Checa pra ver se a classe enviada que É uma entidade tem um construtor sem parâmetro, obrigatório quando se usa EF.
                        if (item.GetParameters().Length == 0)
                        {
                            hasParameterelessConstructor = true;
                        }
                    }
                    if (!hasParameterelessConstructor)
                    {
                        erros.Add("Toda entidade deve possuir um construtor sem parâmetro para o EF.");
                    }

                }
                
                
                foreach (var item in construtores)
                {
                    if (item.GetParameters().Length == 0)
                    {

                    }
                }
                return type.GetConstructors();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro no contructors!");
            }
        }
        public static bool ValidatorProperty(PropertyInfo[] propriedades)
        {
            //Retornando falso existe error na propriedade
            bool error = false;
            foreach (PropertyInfo p in propriedades)
            {
                if (!VerifyPascalCase(p.Name))
                {
                    error = true;
                }
            }
            return error ? false : true;
        }
        //Aqui fica as funções de validações
        public static bool VerifyPascalCase(string name)
        {
            if (name[0] == char.ToLower(name[0]))
                return false;
            else
            {
                return true;
            }
        }
    }
}
