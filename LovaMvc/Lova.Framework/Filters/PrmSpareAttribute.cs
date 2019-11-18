using System;
using System.Collections.Generic;
using System.Text;

namespace Lova.Framework.Filters
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class PrmSpareAttribute : Attribute
    {
        public string Name { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public PrmSpareAttribute(string name)
        {
            this.Name = name;
        }
    }
}
