// ============ WARNING ============ 
// Sources under "CC BY-SA 4.0"
// DO NOT MODIFY THESE SOURCES
// Otherwise publish your changes when distributing.
// ============ WARNING ============ 

namespace ParseCommandLineArguments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Original implementation by Mikescher located at https://gist.github.com/Mikescher/a1450d13980f4363b47cdab5430b411a revision 2.
    /// </summary>
    public static class ParseCommandLineArguments
    {
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine,
            out int pNumArgs);
        
        public static string[] AtifAziz(string commandLine)
        {
            int argc;
            var argv = CommandLineToArgvW(commandLine, out argc);
            if (argv == IntPtr.Zero)
                throw new System.ComponentModel.Win32Exception();
            try
            {
                var args = new string[argc];
                for (var i = 0; i < args.Length; i++)
                {
                    var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
                    args[i] = Marshal.PtrToStringUni(p);
                }

                return args;
            }
            finally
            {
                Marshal.FreeHGlobal(argv);
            }
        }

        public static string[] JeffreyLWhitledge(string commandLine)
        {
            char[] parmChars = commandLine.ToCharArray();
            bool inQuote = false;
            for (int index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"')
                    inQuote = !inQuote;
                if (!inQuote && parmChars[index] == ' ')
                    parmChars[index] = '\n';
            }

            return (new string(parmChars)).Split('\n');
        }

        public static IEnumerable<string> DanielEarwicker(string commandLine)
        {
            bool inQuotes = false;

            return DanielEarwicker_Split(commandLine, c =>
                {
                    if (c == '\"')
                        inQuotes = !inQuotes;

                    return !inQuotes && c == ' ';
                })
                .Select(arg => DanielEarwicker_TrimMatchingQuotes(arg.Trim(), '\"'))
                .Where(arg => !string.IsNullOrEmpty(arg));
        }

        public static IEnumerable<string> DanielEarwicker_Split(string str,
            Func<char, bool> controller)
        {
            int nextPiece = 0;

            for (int c = 0; c < str.Length; c++)
            {
                if (controller(str[c]))
                {
                    yield return str.Substring(nextPiece, c - nextPiece);
                    nextPiece = c + 1;
                }
            }

            yield return str.Substring(nextPiece);
        }

        public static string DanielEarwicker_TrimMatchingQuotes(string input, char quote)
        {
            if ((input.Length >= 2) &&
                (input[0] == quote) && (input[input.Length - 1] == quote))
                return input.Substring(1, input.Length - 2);

            return input;
        }

        public static String[] Anton(String argumentString)
        {
            StringBuilder translatedArguments = new StringBuilder(argumentString);
            bool escaped = false;
            for (int i = 0; i < translatedArguments.Length; i++)
            {
                if (translatedArguments[i] == '"')
                {
                    escaped = !escaped;
                }

                if (translatedArguments[i] == ' ' && !escaped)
                {
                    translatedArguments[i] = '\n';
                }
            }

            string[] toReturn = translatedArguments.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = Anton_RemoveMatchingQuotes(toReturn[i]);
            }

            return toReturn;
        }

        public static string Anton_RemoveMatchingQuotes(string stringToTrim)
        {
            int firstQuoteIndex = stringToTrim.IndexOf('"');
            int lastQuoteIndex = stringToTrim.LastIndexOf('"');
            while (firstQuoteIndex != lastQuoteIndex)
            {
                stringToTrim = stringToTrim.Remove(firstQuoteIndex, 1);
                stringToTrim = stringToTrim.Remove(lastQuoteIndex - 1, 1); //-1 because we've shifted the indicies left by one
                firstQuoteIndex = stringToTrim.IndexOf('"');
                lastQuoteIndex = stringToTrim.LastIndexOf('"');
            }

            return stringToTrim;
        }

        public static string[] CS(String argumentString)
        {
            StringBuilder translatedArguments = new StringBuilder(argumentString).Replace("\\\"", "\r");
            bool InsideQuote = false;
            for (int i = 0; i < translatedArguments.Length; i++)
            {
                if (translatedArguments[i] == '"')
                {
                    InsideQuote = !InsideQuote;
                }

                if (translatedArguments[i] == ' ' && !InsideQuote)
                {
                    translatedArguments[i] = '\n';
                }
            }

            string[] toReturn = translatedArguments.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = CS_RemoveMatchingQuotes(toReturn[i]);
                toReturn[i] = toReturn[i].Replace("\r", "\"");
            }

            return toReturn;
        }

        public static string CS_RemoveMatchingQuotes(string stringToTrim)
        {
            int firstQuoteIndex = stringToTrim.IndexOf('"');
            int lastQuoteIndex = stringToTrim.LastIndexOf('"');
            while (firstQuoteIndex != lastQuoteIndex)
            {
                stringToTrim = stringToTrim.Remove(firstQuoteIndex, 1);
                stringToTrim = stringToTrim.Remove(lastQuoteIndex - 1, 1); //-1 because we've shifted the indicies left by one
                firstQuoteIndex = stringToTrim.IndexOf('"');
                lastQuoteIndex = stringToTrim.LastIndexOf('"');
            }

            return stringToTrim;
        }

        public static string[] VapourintheAlley(string commandLine)
        {
            var parmChars = commandLine.ToCharArray();
            var inSingleQuote = false;
            var inDoubleQuote = false;
            for (var index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"' && !inSingleQuote)
                {
                    inDoubleQuote = !inDoubleQuote;
                    parmChars[index] = '\n';
                }

                if (parmChars[index] == '\'' && !inDoubleQuote)
                {
                    inSingleQuote = !inSingleQuote;
                    parmChars[index] = '\n';
                }

                if (!inSingleQuote && !inDoubleQuote && parmChars[index] == ' ')
                    parmChars[index] = '\n';
            }

            return (new string(parmChars)).Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> Monoman(string commandLine)
        {
            if (string.IsNullOrWhiteSpace(commandLine))
                yield break;

            var sb = new StringBuilder();
            bool inQuote = false;
            foreach (char c in commandLine)
            {
                if (c == '"' && !inQuote)
                {
                    inQuote = true;
                    continue;
                }

                if (c != '"' && !(char.IsWhiteSpace(c) && !inQuote))
                {
                    sb.Append(c);
                    continue;
                }

                if (sb.Length > 0)
                {
                    var result = sb.ToString();
                    sb.Clear();
                    inQuote = false;
                    yield return result;
                }
            }

            if (sb.Length > 0)
                yield return sb.ToString();
        }

        public static IEnumerable<string> ThomasPetersson(string CmdLine)
        {
            var re = @"\G(""((""""|[^""])+)""|(\S+)) *";
            var ms = Regex.Matches(CmdLine, re);
            var list = ms.Cast<Match>()
                .Select(m => Regex.Replace(
                    m.Groups[2].Success
                        ? m.Groups[2].Value
                        : m.Groups[4].Value, @"""""", @"""")).ToArray();
            return list;
        }

        public static string[] FabioIotti(string args)
        {
            char[] parmChars = args.ToCharArray();
            bool inSingleQuote = false;
            bool inDoubleQuote = false;
            bool escaped = false;
            bool lastSplitted = false;
            bool justSplitted = false;
            bool lastQuoted = false;
            bool justQuoted = false;

            int i, j;

            for (i = 0, j = 0; i < parmChars.Length; i++, j++)
            {
                parmChars[j] = parmChars[i];

                if (!escaped)
                {
                    if (parmChars[i] == '^')
                    {
                        escaped = true;
                        j--;
                    }
                    else if (parmChars[i] == '"' && !inSingleQuote)
                    {
                        inDoubleQuote = !inDoubleQuote;
                        parmChars[j] = '\n';
                        justSplitted = true;
                        justQuoted = true;
                    }
                    else if (parmChars[i] == '\'' && !inDoubleQuote)
                    {
                        inSingleQuote = !inSingleQuote;
                        parmChars[j] = '\n';
                        justSplitted = true;
                        justQuoted = true;
                    }
                    else if (!inSingleQuote && !inDoubleQuote && parmChars[i] == ' ')
                    {
                        parmChars[j] = '\n';
                        justSplitted = true;
                    }

                    if (justSplitted && lastSplitted && (!lastQuoted || !justQuoted))
                        j--;

                    lastSplitted = justSplitted;
                    justSplitted = false;

                    lastQuoted = justQuoted;
                    justQuoted = false;
                }
                else
                {
                    escaped = false;
                }
            }

            if (lastQuoted)
                j--;

            return (new string(parmChars, 0, j)).Split(new[] { '\n' });
        }

        public static string[] ygoe(string argsString)
        {
            // Collects the split argument strings
            List<string> args = new List<string>();
            // Builds the current argument
            var currentArg = new StringBuilder();
            // Indicates whether the last character was a backslash escape character
            bool escape = false;
            // Indicates whether we're in a quoted range
            bool inQuote = false;
            // Indicates whether there were quotes in the current arguments
            bool hadQuote = false;
            // Remembers the previous character
            char prevCh = '\0';
            // Iterate all characters from the input string
            for (int i = 0; i < argsString.Length; i++)
            {
                char ch = argsString[i];
                if (ch == '\\' && !escape)
                {
                    // Beginning of a backslash-escape sequence
                    escape = true;
                }
                else if (ch == '\\' && escape)
                {
                    // Double backslash, keep one
                    currentArg.Append(ch);
                    escape = false;
                }
                else if (ch == '"' && !escape)
                {
                    // Toggle quoted range
                    inQuote = !inQuote;
                    hadQuote = true;
                    if (inQuote && prevCh == '"')
                    {
                        // Doubled quote within a quoted range is like escaping
                        currentArg.Append(ch);
                    }
                }
                else if (ch == '"' && escape)
                {
                    // Backslash-escaped quote, keep it
                    currentArg.Append(ch);
                    escape = false;
                }
                else if (char.IsWhiteSpace(ch) && !inQuote)
                {
                    if (escape)
                    {
                        // Add pending escape char
                        currentArg.Append('\\');
                        escape = false;
                    }

                    // Accept empty arguments only if they are quoted
                    if (currentArg.Length > 0 || hadQuote)
                    {
                        args.Add(currentArg.ToString());
                    }

                    // Reset for next argument
                    currentArg.Clear();
                    hadQuote = false;
                }
                else
                {
                    if (escape)
                    {
                        // Add pending escape char
                        currentArg.Append('\\');
                        escape = false;
                    }

                    // Copy character from input, no special meaning
                    currentArg.Append(ch);
                }

                prevCh = ch;
            }

            // Save last argument
            if (currentArg.Length > 0 || hadQuote)
            {
                args.Add(currentArg.ToString());
            }

            return args.ToArray();
        }

        public static IEnumerable<string> KevinThach(string commandLine)
        {
            bool inQuotes = false;
            bool isEscaping = false;

            return KevinThach_Split(commandLine, c =>
                {
                    if (c == '\\' && !isEscaping)
                    {
                        isEscaping = true;
                        return false;
                    }

                    if (c == '\"' && !isEscaping)
                        inQuotes = !inQuotes;

                    isEscaping = false;

                    return !inQuotes && Char.IsWhiteSpace(c) /*c == ' '*/;
                })
                .Select(arg => KevinThach_TrimMatchingQuotes(arg.Trim(), '\"').Replace("\\\"", "\""))
                .Where(arg => !string.IsNullOrEmpty(arg));
        }

        public static IEnumerable<string> KevinThach_Split(string str,
            Func<char, bool> controller)
        {
            int nextPiece = 0;

            for (int c = 0; c < str.Length; c++)
            {
                if (controller(str[c]))
                {
                    yield return str.Substring(nextPiece, c - nextPiece);
                    nextPiece = c + 1;
                }
            }

            yield return str.Substring(nextPiece);
        }

        public static string KevinThach_TrimMatchingQuotes(string input, char quote)
        {
            if ((input.Length >= 2) &&
                (input[0] == quote) && (input[input.Length - 1] == quote))
                return input.Substring(1, input.Length - 2);

            return input;
        }

        public static string[] LucasDeJesus(string str) => LucasDeJesus_(str, out _);

        public static string[] LucasDeJesus_(string str, out int argumentos)
        {
            string[] linhaComando = new string[32];
            bool entre_aspas = false;
            int posicao_ponteiro = 0;
            int argc = 0;
            int inicio = 0;
            int fim = 0;
            string sub;

            for (int i = 0; i < str.Length;)
            {
                if (entre_aspas)
                {
                    // Está entre aspas
                    sub = str.Substring(inicio + 1, fim - (inicio + 1));
                    linhaComando[argc - 1] = sub;

                    posicao_ponteiro += ((fim - posicao_ponteiro) + 1);
                    entre_aspas = false;
                    i = posicao_ponteiro;
                }
                else
                {
                    tratar_aspas:
                    if (str.ElementAt(i) == '\"')
                    {
                        inicio = i;
                        fim = str.IndexOf('\"', inicio + 1);
                        entre_aspas = true;
                        argc++;
                    }
                    else
                    {
                        // Se não for aspas, então ler até achar o primeiro espaço em branco
                        if (str.ElementAt(i) == ' ')
                        {
                            if (str.ElementAt(i + 1) == '\"')
                            {
                                i++;
                                goto tratar_aspas;
                            }

                            // Pular os espaços em branco adiconais
                            while (str.ElementAt(i) == ' ') i++;

                            argc++;
                            inicio = i;
                            fim = str.IndexOf(' ', inicio);
                            if (fim == -1) fim = str.Length;
                            sub = str.Substring(inicio, fim - inicio);
                            linhaComando[argc - 1] = sub;
                            posicao_ponteiro += (fim - posicao_ponteiro);

                            i = posicao_ponteiro;
                            if (posicao_ponteiro == str.Length) break;
                        }
                        else
                        {
                            argc++;
                            inicio = i;
                            fim = str.IndexOf(' ', inicio);
                            if (fim == -1) fim = str.Length;

                            sub = str.Substring(inicio, fim - inicio);
                            linhaComando[argc - 1] = sub;
                            posicao_ponteiro += fim - posicao_ponteiro;
                            i = posicao_ponteiro;
                            if (posicao_ponteiro == str.Length) break;
                        }
                    }
                }
            }

            argumentos = argc;

            return linhaComando;
        }

        public static IEnumerable<String> HarryP(string commandLine)
        {
            Char quoteChar = '"';
            Char escapeChar = '\\';
            Boolean insideQuote = false;
            Boolean insideEscape = false;

            StringBuilder currentArg = new StringBuilder();

            // needed to keep "" as argument but drop whitespaces between arguments
            Int32 currentArgCharCount = 0;

            for (Int32 i = 0; i < commandLine.Length; i++)
            {
                Char c = commandLine[i];
                if (c == quoteChar)
                {
                    currentArgCharCount++;

                    if (insideEscape)
                    {
                        currentArg.Append(c); // found \" -> add " to arg
                        insideEscape = false;
                    }
                    else if (insideQuote)
                    {
                        insideQuote = false; // quote ended
                    }
                    else
                    {
                        insideQuote = true; // quote started
                    }
                }
                else if (c == escapeChar)
                {
                    currentArgCharCount++;

                    if (insideEscape) // found \\ -> add \\ (only \" will be ")
                        currentArg.Append(escapeChar + escapeChar);

                    insideEscape = !insideEscape;
                }
                else if (Char.IsWhiteSpace(c))
                {
                    if (insideQuote)
                    {
                        currentArgCharCount++;
                        currentArg.Append(c); // append whitespace inside quote
                    }
                    else
                    {
                        if (currentArgCharCount > 0)
                            yield return currentArg.ToString();

                        currentArgCharCount = 0;
                        currentArg.Clear();
                    }
                }
                else
                {
                    currentArgCharCount++;
                    if (insideEscape)
                    {
                        // found non-escaping backslash -> add \ (only \" will be ")
                        currentArg.Append(escapeChar);
                        currentArgCharCount = 0;
                        insideEscape = false;
                    }

                    currentArg.Append(c);
                }
            }

            if (currentArgCharCount > 0)
                yield return currentArg.ToString();
        }

        private static readonly Regex RxWinArgs
            = new Regex("([^\\s\"]+\"|((?<=\\s|^)(?!\"\"(?!\"))\")+)(\"\"|.*?)*\"[^\\s\"]*|[^\\s]+",
                RegexOptions.Compiled
                | RegexOptions.Singleline
                | RegexOptions.ExplicitCapture
                | RegexOptions.CultureInvariant);

        public static IEnumerable<string> TylerY86(string args)
        {
            var match = RxWinArgs.Match(args);

            while (match.Success)
            {
                yield return match.Value;
                match = match.NextMatch();
            }
        }

        public static string[] LouisSomers(string commandLine)
        {
            List<string> args = new List<string>();
            List<char> currentArg = new List<char>();
            char? quoteSection = null; // Keeps track of a quoted section (and the type of quote that was used to open it)
            char[] quoteChars = new[] { '\'', '\"' };
            char previous = ' '; // Used for escaping double quotes

            for (var index = 0; index < commandLine.Length; index++)
            {
                char c = commandLine[index];
                if (quoteChars.Contains(c))
                {
                    if (previous == c) // Escape sequence detected
                    {
                        previous = ' '; // Prevent re-escaping
                        if (!quoteSection.HasValue)
                        {
                            quoteSection = c; // oops, we ended the quoted section prematurely
                            continue; // don't add the 2nd quote (un-escape)
                        }

                        if (quoteSection.Value == c)
                            quoteSection = null; // appears to be an empty string (not an escape sequence)
                    }
                    else if (quoteSection.HasValue)
                    {
                        if (quoteSection == c)
                            quoteSection = null; // End quoted section
                    }
                    else
                        quoteSection = c; // Start quoted section
                }
                else if (char.IsWhiteSpace(c))
                {
                    if (!quoteSection.HasValue)
                    {
                        args.Add(new string(currentArg.ToArray()));
                        currentArg.Clear();
                        previous = c;
                        continue;
                    }
                }

                currentArg.Add(c);
                previous = c;
            }

            if (currentArg.Count > 0)
                args.Add(new string(currentArg.ToArray()));

            return args.ToArray();
        }

        public static IList<string> user2126375(string commandLineArgsString)
        {
            List<string> args = new List<string>();

            commandLineArgsString = commandLineArgsString.Trim();
            if (commandLineArgsString.Length == 0)
                return args;

            int index = 0;
            while (index != commandLineArgsString.Length)
            {
                args.Add(user2126375_ReadOneArgFromCommandLineArgsString(commandLineArgsString, ref index));
            }

            return args;
        }

        private static string user2126375_ReadOneArgFromCommandLineArgsString(string line, ref int index)
        {
            if (index >= line.Length)
                return string.Empty;

            var sb = new StringBuilder(512);
            int state = 0;
            while (true)
            {
                char c = line[index];
                index++;
                switch (state)
                {
                    case 0: //string outside quotation marks
                        if (c == '\\') //possible escaping character for quotation mark otherwise normal character
                        {
                            state = 1;
                        }
                        else if (c == '"') //opening quotation mark for string between quotation marks
                        {
                            state = 2;
                        }
                        else if (c == ' ') //closing arg
                        {
                            return sb.ToString();
                        }
                        else
                        {
                            sb.Append(c);
                        }

                        break;
                    case 1: //possible escaping \ for quotation mark or normal character
                        if (c == '"') //If escaping quotation mark only quotation mark is added into result
                        {
                            state = 0;
                            sb.Append(c);
                        }
                        else // \ works as not-special character
                        {
                            state = 0;
                            sb.Append('\\');
                            index--;
                        }

                        break;
                    case 2: //string between quotation marks
                        if (
                            c == '"') //quotation mark in string between quotation marks can be escape mark for following quotation mark or can be ending quotation mark for string between quotation marks
                        {
                            state = 3;
                        }
                        else if (c == '\\') //escaping \ for possible following quotation mark otherwise normal character
                        {
                            state = 4;
                        }
                        else //text in quotation marks
                        {
                            sb.Append(c);
                        }

                        break;
                    case 3: //quotation mark in string between quotation marks
                        if (
                            c == '"') //Quotation mark after quotation mark - that means that this one is escaped and can added into result and we will stay in string between quotation marks state
                        {
                            state = 2;
                            sb.Append(c);
                        }
                        else //we had two consecutive quotation marks - this means empty string but the following chars (until space) will be part of same arg result as well
                        {
                            state = 0;
                            index--;
                        }

                        break;
                    case 4: //possible escaping \ for quotation mark or normal character in string between quotation marks
                        if (c == '"') //If escaping quotation mark only quotation mark added into result
                        {
                            state = 2;
                            sb.Append(c);
                        }
                        else
                        {
                            state = 2;
                            sb.Append('\\');
                            index--;
                        }

                        break;
                }

                if (index == line.Length)
                    return sb.ToString();
            }
        }

        public static string[] DilipNannaware(string commandLine)
        {
            var isLastCharSpace = false;
            char[] parmChars = commandLine.ToCharArray();
            bool inQuote = false;
            for (int index = 0; index < parmChars.Length; index++)
            {
                if (parmChars[index] == '"')
                    inQuote = !inQuote;
                if (!inQuote && parmChars[index] == ' ' && !isLastCharSpace)
                    parmChars[index] = '\n';

                isLastCharSpace = parmChars[index] == '\n' || parmChars[index] == ' ';
            }

            return (new string(parmChars)).Split('\n');
        }

        public static IEnumerable<string> Mikescher(string commandLine)
        {
            var result = new StringBuilder();

            var quoted = false;
            var escaped = false;
            var started = false;
            var allowcaret = false;
            for (int i = 0; i < commandLine.Length; i++)
            {
                var chr = commandLine[i];

                if (chr == '^' && !quoted)
                {
                    if (allowcaret)
                    {
                        result.Append(chr);
                        started = true;
                        escaped = false;
                        allowcaret = false;
                    }
                    else if (i + 1 < commandLine.Length && commandLine[i + 1] == '^')
                    {
                        allowcaret = true;
                    }
                    else if (i + 1 == commandLine.Length)
                    {
                        result.Append(chr);
                        started = true;
                        escaped = false;
                    }
                }
                else if (escaped)
                {
                    result.Append(chr);
                    started = true;
                    escaped = false;
                }
                else if (chr == '"')
                {
                    quoted = !quoted;
                    started = true;
                }
                else if (chr == '\\' && i + 1 < commandLine.Length && commandLine[i + 1] == '"')
                {
                    escaped = true;
                }
                else if (chr == ' ' && !quoted)
                {
                    if (started) yield return result.ToString();
                    result.Clear();
                    started = false;
                }
                else
                {
                    result.Append(chr);
                    started = true;
                }
            }

            if (started) yield return result.ToString();
        }

        /// <summary>
        /// <para>Mikescher implementation modified by SandRock to return the index of each token.</para>
        /// </summary>
        public static IEnumerable<string> Mikescher_PlusIndex(string commandLine)
        {
            return Mikescher_PlusIndexImpl(commandLine).Select(x => x.Value);
        }

        /// <summary>
        /// <para>Mikescher implementation modified by SandRock to return the index of each token.</para>
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns>the index as key and the value</returns>
        private static IEnumerable<KeyValuePair<int, string>> Mikescher_PlusIndexImpl(string commandLine)
        {
            var result = new StringBuilder();

            var quoted = false;
            var escaped = false;
            var started = false;
            var allowcaret = false;
            for (int i = 0; i < commandLine.Length; i++)
            {
                var chr = commandLine[i];

                if (chr == '^' && !quoted)
                {
                    if (allowcaret)
                    {
                        result.Append(chr);
                        started = true;
                        escaped = false;
                        allowcaret = false;
                    }
                    else if (i + 1 < commandLine.Length && commandLine[i + 1] == '^')
                    {
                        allowcaret = true;
                    }
                    else if (i + 1 == commandLine.Length)
                    {
                        result.Append(chr);
                        started = true;
                        escaped = false;
                    }
                }
                else if (escaped)
                {
                    result.Append(chr);
                    started = true;
                    escaped = false;
                }
                else if (chr == '"')
                {
                    quoted = !quoted;
                    started = true;
                }
                else if (chr == '\\' && i + 1 < commandLine.Length && commandLine[i + 1] == '"')
                {
                    escaped = true;
                }
                else if (chr == ' ' && !quoted)
                {
                    if (started)
                    {
                        yield return new KeyValuePair<int, string>(i, result.ToString());
                    }
                    
                    result.Clear();
                    started = false;
                }
                else
                {
                    result.Append(chr);
                    started = true;
                }
            }

            if (started)
            {
                yield return new KeyValuePair<int, string>(0, result.ToString());
            }
        }
    }
}
