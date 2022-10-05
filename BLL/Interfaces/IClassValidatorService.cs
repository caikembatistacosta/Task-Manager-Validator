using Entities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicalLayer.Interfaces
{
    public interface IClassValidatorService
    {
        SingleResponse<ReflectionEntity> Validator(string compileCode);
        SingleResponse<PropertyInfo[]> ValidatorProperty(Type type);
        Response VerifyPascalCase(string name);
        SingleResponse<SyntaxTree> ParseSyntaxTree(string compileCode);
        SingleResponse<CSharpCompilation> CompileCode(string assemblyName, SyntaxTree syntaxTree, MetadataReference[] references);
        SingleResponse<MethodInfo[]> ValidatorMethods(Type type);
        SingleResponse<ConstructorInfo[]> ValidatorContructors(Type type);
    }
}
