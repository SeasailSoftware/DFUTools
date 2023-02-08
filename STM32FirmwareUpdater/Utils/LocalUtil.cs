using Caliburn.Micro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace STM32FirmwareUpdater.Utils
{
    public static class LocalUtil
    {
        /// <summary>
        /// 所有语言文件的地址
        /// </summary>
        public static Dictionary<CultureInfo, List<Uri>> LocalUris;

        /// <summary>
        /// 获取支持的所有语言
        /// </summary>
        public static CultureInfo[] Languages
        {
            get { return LocalUris.Keys.ToArray(); }
        }

        /// <summary>
        /// 语言文件的存储位置
        /// </summary>
        public static string LocalPath
        {
            get
            {
                return Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "i18N");
            }
        }

        public static CultureInfo CurrentCulture { get; set; }

        public static CultureInfo DefaultLocal
        {
            get
            {
                //var neutralResourceLanguageAttributes = Assembly.GetExecutingAssembly()
                //    .GetCustomAttributes(typeof(NeutralResourcesLanguageAttribute)).ToArray();
                //var languageAttribute = (NeutralResourcesLanguageAttribute) neutralResourceLanguageAttributes[0];
                //return CultureInfo.GetCultureInfo(languageAttribute.CultureName);
                return CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(p => p.Name == "en-US");
            }
        }

        static Dictionary<CultureInfo, Uri> FindLangUrisFromAssembly(Assembly assembly)
        {
            Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".g.resources");
            if (stream == null)
                return null;

            Dictionary<CultureInfo, Uri> uris = new Dictionary<CultureInfo, Uri>();

            var i18n = "i18n/";
            using (ResourceReader reader = new ResourceReader(stream))
            {
                foreach (DictionaryEntry entry in reader)
                {
                    var s = ((string)entry.Key).ToLower();
                    if (!s.StartsWith(i18n))
                        continue;
                    var uri = new Uri(
                        string.Format("pack://application:,,,/{0};component/{1}",
                            assembly.GetName().Name,
                            s.Replace(".baml", ".xaml"), UriKind.Absolute));
                    var lang =
                        Path.GetFileNameWithoutExtension(
                            uri.OriginalString.Substring(i18n.Length).Replace('_', '-'));
                    try
                    {
                        CultureInfo culture = CultureInfo.GetCultureInfo(lang);
                        uris[culture] = uri;
                    }
                    catch
                    {
                        continue;
                    }
                }
            }

            return uris;
        }

        static LocalUtil()
        {
            LocalUris = new Dictionary<CultureInfo, List<Uri>>();

            // 遍历程序语言文件夹下的语言
            if (Directory.Exists(LocalPath))
            {
                foreach (var path in Directory.EnumerateFiles(LocalPath, "*.xaml", SearchOption.TopDirectoryOnly))
                {
                    var info = new FileInfo(path);
                    var items = info.Name.Split(new[] { '.' });
                    if (items.Length < 2 || items.Last().ToLower() != "xaml")
                        continue;

                    var lang = items[items.Length - 2].Replace('_', '-').ToLower();
                    try
                    {
                        CultureInfo culture = CultureInfo.GetCultureInfo(lang);
                        if (!LocalUris.ContainsKey(culture))
                        {
                            LocalUris[culture] = new List<Uri>();
                        }

                        LocalUris[culture].Add(new Uri(path));
                    }
                    catch
                    {
                        continue;
                    }
                }
            }


            // 遍历程序集中的语言文件


            List<Assembly> allAssemblies = new List<Assembly>(AssemblySource.Instance);

            foreach (var assembly in allAssemblies)
            {
                var uris = FindLangUrisFromAssembly(assembly);
                if (uris == null)
                    continue;
                foreach (var item in uris)
                {
                    if (!LocalUris.ContainsKey(item.Key))
                    {
                        LocalUris[item.Key] = new List<Uri>();
                    }

                    LocalUris[item.Key].Add(item.Value);
                }
            }

        }

        /// <summary>
        /// 切换语言
        /// </summary>
        /// <param name="culture">要切换的语言</param>
        public static void SwitchCulture(CultureInfo culture)
        {
            // 找不到相应语言
            var localFiles = FindLocalFile(culture);
            if (localFiles == null)
            {
                return;
            }


            foreach (var uri in localFiles)
            {
                var resources = new ResourceDictionary { Source = uri };
                foreach (DictionaryEntry entry in resources)
                {
                    if (Application.Current.Resources.Contains(entry.Key))
                    {
                        Application.Current.Resources[entry.Key] = entry.Value;
                    }
                    else
                    {
                        Application.Current.Resources.Add(entry.Key, entry.Value);
                    }
                }
            }

            // 如果不是默认语言，使用默认语言填充可能没翻译的
            if (!Equals(culture, DefaultLocal))
            {
                foreach (var uri in FindLocalFile(DefaultLocal))
                {
                    var resources = new ResourceDictionary { Source = uri };
                    foreach (DictionaryEntry entry in resources)
                    {
                        if (!Application.Current.Resources.Contains(entry.Key))

                        {
                            Application.Current.Resources.Add(entry.Key, entry.Value);
                        }
                    }
                }
            }

            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
            CurrentCulture = culture;
        }


        public static List<Uri> FindLocalFile(CultureInfo local)
        {
            // 如果包含指定区域直接返回
            if (LocalUris.ContainsKey(local))
            {
                return LocalUris[local];
            }

            // 查找父语言
            for (var parent = local.Parent; !string.IsNullOrEmpty(parent.Name); parent = parent.Parent)
            {
                if (LocalUris.ContainsKey(parent))
                    return LocalUris[parent];
            }


            // 否则查找语言相同但区域不同的语言
            var near = LocalUris.Keys.FirstOrDefault(
                           x => x.ThreeLetterISOLanguageName == local.ThreeLetterISOLanguageName) ??
                       LocalUris.Keys.FirstOrDefault(
                           x => x.TwoLetterISOLanguageName == local.TwoLetterISOLanguageName);
            return near != null ? LocalUris[near] : null;
        }

    }
}
