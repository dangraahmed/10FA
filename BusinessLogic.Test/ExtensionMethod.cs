using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Test
{
    public static class ExtensionMethod
    {
        public static string ToJson(this ISedolValidationResult SedolValidationResult)
        {
            return JsonConvert.SerializeObject(SedolValidationResult);
        }
    }
}
