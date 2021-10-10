// Mix of sources from https://stackoverflow.com/questions/298830/split-string-containing-command-line-parameters-into-string-in-c-sharp
// Originally made by Mikescher at https://gist.github.com/Mikescher/a1450d13980f4363b47cdab5430b411a
// Assembled as a .net project by SandRock
// Licensed under "CC BY-SA 4.0" (specified by Stackoverflow's rules)

using System;
using Xunit;

namespace Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using ParseCommandLineArguments;

    /// <summary>
    /// <para>Runs test against each method.</para>
    /// <para>Using parameterized methods; inspired by https://andrewlock.net/creating-parameterised-tests-in-xunit-with-inlinedata-classdata-and-memberdata/</para>
    /// </summary>
    public class ParseCommandLineArgumentsTests
    {
        [Theory]
        [ClassData(typeof(TestInputs))]
        public void AtifAziz(TestInput input)
        {
            Test(input, nameof(AtifAziz), ParseCommandLineArguments.AtifAziz);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void JeffreyLWhitledge(TestInput input)
        {
            Test(input, nameof(JeffreyLWhitledge), ParseCommandLineArguments.JeffreyLWhitledge);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void DanielEarwicker(TestInput input)
        {
            Test(input, nameof(DanielEarwicker), ParseCommandLineArguments.DanielEarwicker);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void Anton(TestInput input)
        {
            Test(input, nameof(Anton), ParseCommandLineArguments.Anton);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void CS(TestInput input)
        {
            Test(input, nameof(CS), ParseCommandLineArguments.CS);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void VapourintheAlley(TestInput input)
        {
            Test(input, nameof(VapourintheAlley), ParseCommandLineArguments.VapourintheAlley);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void Monoman(TestInput input)
        {
            Test(input, nameof(Monoman), ParseCommandLineArguments.Monoman);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void ThomasPetersson(TestInput input)
        {
            Test(input, nameof(ThomasPetersson), ParseCommandLineArguments.ThomasPetersson);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void FabioIotti(TestInput input)
        {
            Test(input, nameof(FabioIotti), ParseCommandLineArguments.FabioIotti);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void ygoe(TestInput input)
        {
            Test(input, nameof(ygoe), ParseCommandLineArguments.ygoe);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void KevinThach(TestInput input)
        {
            Test(input, nameof(KevinThach), ParseCommandLineArguments.KevinThach);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void LucasDeJesus(TestInput input)
        {
            Test(input, nameof(LucasDeJesus), ParseCommandLineArguments.LucasDeJesus);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void HarryP(TestInput input)
        {
            Test(input, nameof(HarryP), ParseCommandLineArguments.HarryP);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void TylerY86(TestInput input)
        {
            Test(input, nameof(TylerY86), ParseCommandLineArguments.TylerY86);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void LouisSomers(TestInput input)
        {
            Test(input, nameof(LouisSomers), ParseCommandLineArguments.LouisSomers);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void user2126375(TestInput input)
        {
            Test(input, nameof(user2126375), ParseCommandLineArguments.user2126375);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void DilipNannaware(TestInput input)
        {
            Test(input, nameof(DilipNannaware), ParseCommandLineArguments.DilipNannaware);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void Mikescher(TestInput input)
        {
            Test(input, nameof(Mikescher), ParseCommandLineArguments.Mikescher);
        }

        [Theory]
        [ClassData(typeof(TestInputs))]
        public void Mikescher_PlusIndex(TestInput input)
        {
            Test(input, nameof(Mikescher_PlusIndex), ParseCommandLineArguments.Mikescher_PlusIndex);
        }

        private void Test(TestInput input, string name, Func<string, IEnumerable<string>> method)
        {
            try
            {
                var result = method(input.Input).ToArray();
                if (input.ExpectedResult.Length != result.Length)
                {
                    throw new InvalidOperationException($"Method ${name} failed for input ${input.Id}");
                    return;
                }

                for (int i = 0; i < input.ExpectedResult.Length; i++)
                {
                    if (input.ExpectedResult[i] != result[i])
                    {
                        throw new InvalidOperationException($"Method ${name} failed for input ${input.Id} at item ${i}");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Method ${name} crashed for input ${input.Id}", ex);
                return;
            }
        }
    }
}
