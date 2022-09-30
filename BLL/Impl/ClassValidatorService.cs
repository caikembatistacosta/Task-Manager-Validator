using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using BusinessLogicalLayer.Interfaces;
using Entities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Shared;

namespace BusinessLogicalLayer.Impl
{
    public class ClassValidatorService : IClassValidatorService
    {

        //Checar nomenclatura métodos
        //Checar nomenclatura propriedades
        //Se checkbox de Entity for marcado, checar pra ver se existe um campo id inteiro
        //Se checkbox de Entity for marcado, checar pra ver se a classe tem um construtor sem parâmetro



        public SingleResponse<ReflectionEntity> Validator(string codeToCompile)
        {
            SingleResponse<SyntaxTree> syntaxTree = ParseSyntaxTree(codeToCompile);

            string assemblyName = Path.GetRandomFileName();
            string[] refPaths = new[]
            {
                typeof(object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll"),
            };
            MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();
            SingleResponse<CSharpCompilation> compilation = CompileCode(assemblyName, syntaxTree.Item, references);

            using (MemoryStream ms = new())
            {
                EmitResult result = compilation.Item.Emit(ms);
                try
                {
                    if (!result.Success)
                    {
                        int a = 0;
                        int b = 0;
                        string c = string.Empty;
                        IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                            diagnostic.IsWarningAsError ||
                            diagnostic.Severity == DiagnosticSeverity.Error);
                        StringBuilder stringBuilder = new();

                        foreach (Diagnostic diagnostic in failures)
                        {
                            stringBuilder.AppendLine(diagnostic.Id);
                            stringBuilder.AppendLine(diagnostic.GetMessage());
                            if (diagnostic.GetMessage().Contains("Linq"))
                            {
                                a = diagnostic.Location.SourceSpan.Start;
                                b = diagnostic.Location.SourceSpan.End;
                                c = diagnostic.Location.SourceTree.ToString().Replace("using System.Linq;", "");
                            }
                        }
                        ReflectionEntity reflectionEntity = new()
                        {
                            NewCodeToCompile = c,
                        };
                        return SingleResponseFactory<ReflectionEntity>.CreateInstance().CreateFailureSingleResponse(reflectionEntity, c);
                    }
                    else
                    {
                        ms.Seek(0, SeekOrigin.Begin);
                        Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                        //2 exceções, e pega somente os tipos públicos em um assembly
                        Type type = assembly.ExportedTypes.First();
                        SingleResponse<MethodInfo[]> metodos = ValidatorMethods(type);
                        if (metodos.HasSuccess)
                        {
                            SingleResponse<ConstructorInfo[]> construtores = ValidatorContructors(type);
                            if (construtores.HasSuccess)
                            {
                                PropertyInfo[] propriedades = type.GetProperties();
                                Response r = ValidatorProperty(propriedades);


                                if (r.HasSuccess)
                                {
                                    ReflectionEntity reflectionEntity = new()
                                    {
                                        PropertyInfos = propriedades,
                                        MethodInfos = metodos.Item,
                                        ConstructorInfos = construtores.Item,
                                    };
                                    return SingleResponseFactory<ReflectionEntity>.CreateInstance().CreateSuccessSingleResponse(reflectionEntity, r.Message);
                                }
                                return SingleResponseFactory<ReflectionEntity>.CreateInstance().CreateFailureSingleResponse(r.Message);
                            }
                            return SingleResponseFactory<ReflectionEntity>.CreateInstance().CreateFailureSingleResponse(construtores.Message);
                        }
                        return SingleResponseFactory<ReflectionEntity>.CreateInstance().CreateFailureSingleResponse(metodos.Message);
                    }
                }

                catch (Exception ex)
                {
                    return SingleResponseFactory<ReflectionEntity>.CreateInstance().CreateFailureSingleResponse(ex);
                }


            }
        }
        public SingleResponse<SyntaxTree> ParseSyntaxTree(string codeToCompile)
        {
            try
            {
                return SingleResponseFactory<SyntaxTree>.CreateInstance().CreateSuccessSingleResponse(CSharpSyntaxTree.ParseText(codeToCompile));
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<SyntaxTree>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }
        public SingleResponse<CSharpCompilation> CompileCode(string assemblyName, SyntaxTree syntaxTree, MetadataReference[] references)
        {
            try
            {
                return SingleResponseFactory<CSharpCompilation>.CreateInstance().CreateSuccessSingleResponse(CSharpCompilation.Create(
                    assemblyName,
                    syntaxTrees: new[] { syntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)));
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<CSharpCompilation>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }

        public SingleResponse<MethodInfo[]> ValidatorMethods(Type type)
        {
            try
            {
                return SingleResponseFactory<MethodInfo[]>.CreateInstance().CreateSuccessSingleResponse(type.GetMethods());
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<MethodInfo[]>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }
        public SingleResponse<ConstructorInfo[]> ValidatorContructors(Type type)
        {
            List<string> erros = new();
            try
            {
                ConstructorInfo[] construtores = type.GetConstructors();
                //Verifica se o arquivo que foi feito upload é de uma classe que herda de Entity

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
                    return SingleResponseFactory<ConstructorInfo[]>.CreateInstance().CreateFailureSingleResponse(erros);
                }
                //Add mais Ifs
                return SingleResponseFactory<ConstructorInfo[]>.CreateInstance().CreateSuccessSingleResponse(construtores);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<ConstructorInfo[]>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }
        public Response ValidatorProperty(PropertyInfo[] propriedades)
        {
            //Retornando falso existe error na propriedade
            foreach (PropertyInfo p in propriedades)
            {
                if (VerifyPascalCase(p.Name).HasSuccess)
                {
                    return ResponseFactory.CreateInstance().CreateSuccessResponse("A propriedade está em PascalCase!");
                }
            }
            return ResponseFactory.CreateInstance().CreateFailureResponse("A propriedade não está em PascalCase!");
        }
        public Response VerifyPascalCase(string name)
        {
            if (name[0] == char.ToLower(name[0]))
                return ResponseFactory.CreateInstance().CreateFailureResponse("A propriedade deve começar com letra maíuscula!");

            return ResponseFactory.CreateInstance().CreateSuccessResponse("A propriedade está começando com letra maíuscula.");


        }
    }
}
