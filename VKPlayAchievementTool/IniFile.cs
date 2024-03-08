using System.Collections.Generic;


namespace VKPlayAchievementTool
{
    internal class IniFile
    {
        public static Dictionary<string, Dictionary<string, string>> ParseFrom(IEnumerable<string> strings)
        {
            var ini = new Dictionary<string, Dictionary<string, string>>();
            var currentSection = new Dictionary<string, string>();

            var lastSection = "";
            foreach (string str in strings)
            {
                // ignore null or empty lines in an enumerable
                if (string.IsNullOrEmpty(str))
                    continue;

                // all interesting strings must be at least 3 characters long
                // a=b    <-- key value pair
                // OR
                // [a]    <-- section name
                // both are 3 chars...
                if (str.Length > 2)
                {
                    if (str[0] == '[' && str[str.Length - 1] == ']')
                    {
                        var newSectionName = str.Substring(1, str.Length - 2);
                        if (lastSection != "")
                        {
                            currentSection = new Dictionary<string, string>();
                        }

                        ini[newSectionName] = currentSection;
                        lastSection = newSectionName;
                    }
                    // handle ; comments and # comments
                    else if (str[0] != ';' && str[0] != '#')
                    {
                        var eqIndex = str.IndexOf('=');
                        // line cannot start with a =
                        if (eqIndex > 0)
                        {
                            var key = str.Substring(0, eqIndex);
                            var value = str.Substring(eqIndex + 1);
                            currentSection[key] = value;
                        }
                    }
                }
            }

            return ini;
        }
    }
}
