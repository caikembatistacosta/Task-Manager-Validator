using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ReflectionEntity
    {
        public ConstructorInfo[]? ConstructorInfos { get; set; }
        public MethodInfo[]? MethodInfos { get; set; }
        public PropertyInfo[]? PropertyInfos { get; set; }
        public string? NewCodeToCompile { get; set; }
    }
}
