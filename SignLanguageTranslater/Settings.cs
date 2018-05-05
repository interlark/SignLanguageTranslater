using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignLanguageTranslater
{
    public static class Settings
    {
        /// <summary>
        /// Получение ассоциативного массива транслитерованных папак и их русское отображение
        /// </summary>
        public static Dictionary<string, string> GetTranslatedFolders()
        {
            var script = new Script();
            script.Options.ScriptLoader = new FileSystemScriptLoader();
            script.DoFile("settings.lua");
            var func = script.Globals.Get("getTranslatedFolders");
            var table = func.Function.Call().Table;

            var dic = new Dictionary<string, string>();
            foreach (var kvp in table.Pairs)
            {
                var key = kvp.Key.String;
                var val = kvp.Value.String;

                dic.Add(key, val);
            }

            return dic;
        }
    }
}
