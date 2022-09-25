﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using BusinessLogicalLayer.Interfaces;
using Entities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared;

namespace BusinessLogicalLayer.ClassValidator
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

            SingleResponse<SyntaxTree> syntaxTree = ParseSyntaxTree(codeToCompile);

            string assemblyName = Path.GetRandomFileName();
            string[] refPaths = new[]
            {
                typeof(System.Object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll"),
            };
            MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

            Write("Adding the following references");
            foreach (var r in refPaths)
                Write(r);

            SingleResponse<CSharpCompilation> compilation = CompileCode(assemblyName, syntaxTree.Item, references);

            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Item.Emit(ms);

                if (!result.Success)
                {
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
                    ms.Seek(0, SeekOrigin.Begin);

                    Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    //2 exceções, e pega somente os tipos públicos em um assembly
                    Type type = assembly.ExportedTypes.First();
                    SingleResponse<MethodInfo[]> metodos = ValidatorMethods(type);
                    SingleResponse<List<ConstructorInfo[]>> construtores = ValidatorContructors(type);

                    PropertyInfo[] propriedades = type.GetProperties();
                    Response r = ValidatorProperty(propriedades);
                }
            }


        }
        public static SingleResponse<SyntaxTree> ParseSyntaxTree(string codeToCompile)
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
        public static SingleResponse<CSharpCompilation> CompileCode(string assemblyName, SyntaxTree syntaxTree, MetadataReference[] references)
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

        public static SingleResponse<MethodInfo[]> ValidatorMethods(Type type)
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
        public static SingleResponse<List<ConstructorInfo[]>> ValidatorContructors(Type type)
        {
            List<string> erros = new();
            try
            {
                ConstructorInfo[] construtores = type.GetConstructors();
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
                    //Add mais Ifs
                }
                return SingleResponseFactory<List<ConstructorInfo[]>>.CreateInstance().CreateFailureSingleResponse(erros);
            }
            catch (Exception ex)
            {
                return SingleResponseFactory<List<ConstructorInfo[]>>.CreateInstance().CreateFailureSingleResponse(ex);
            }
        }
        public static Response ValidatorProperty(PropertyInfo[] propriedades)
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
        //Aqui fica as funções de validações
        public static Response VerifyPascalCase(string name)
        {
            if (name[0] == char.ToLower(name[0]))
                return ResponseFactory.CreateInstance().CreateSuccessResponse("A propriedade está começando com letra maíuscula.");

            return ResponseFactory.CreateInstance().CreateFailureResponse("A propriedade deve começar com letra maíuscula!");
        }
    }
}
