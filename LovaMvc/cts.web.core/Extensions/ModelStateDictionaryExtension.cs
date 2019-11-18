using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.AspNetCore.Mvc
{
    /// <summary>
    /// ModelState 扩展
    /// </summary>
    public static class ModelStateDictionaryExtension
    {
        /// <summary>
        /// 获取未验证通过的内容
        /// </summary>
        /// <param name="msDictionary"></param>
        /// <returns></returns>
        public static string GetErrMsg(this ModelStateDictionary msDictionary)
        {
            if (msDictionary.IsValid || !msDictionary.Any()) return "";
            foreach (string key in msDictionary.Keys)
            {
                ModelStateEntry tempModelState = msDictionary[key];
                if (tempModelState.Errors.Any())
                {
                    var firstOrDefault = tempModelState.Errors.FirstOrDefault();
                    if (firstOrDefault != null)
                        return firstOrDefault.ErrorMessage;
                }
            }
            return "";
        }
    }
}
