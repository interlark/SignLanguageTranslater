using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignLanguageTrainer
{
    public static class Settings
    {
        /// <summary>
        /// Получение скрипта на лету (с изменениями)
        /// </summary>
        internal static Script GetScript()
        {
            var script = new Script();
            script.Options.ScriptLoader = new FileSystemScriptLoader();
            script.DoFile("settings.lua");
            return script;
        }

        /// <summary>
        /// Получение ассоциативного массива транслитерованных папак и их русское отображение
        /// </summary>
        public static IDictionary<string, string> GetTranslatedFolders()
        {
            var func = GetScript().Globals.Get("getTranslatedFolders");
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

        /// <summary>
        /// Получение названия папки для скрипта тренировки tensorflow
        /// </summary>
        public static string GetTrainFolderName()
        {
            var func = GetScript().Globals.Get("getTrainFolderName");
            var name = func.Function.Call().CastToString();

            return name;
        }

        /// <summary>
        /// Получение названия графа для скрипта тренировки tensorflow
        /// </summary>
        public static string GetOutputGraphName()
        {
            var func = GetScript().Globals.Get("getOutputGraph");
            var name = func.Function.Call().CastToString();

            return name;
        }

        /// <summary>
        /// Получение символов графа для скрипта тренировки tensorflow
        /// </summary>
        public static string GetOutputlabelsName()
        {
            var func = GetScript().Globals.Get("getOutputGraph");
            var name = func.Function.Call().CastToString();

            return name;
        }
    }
}

