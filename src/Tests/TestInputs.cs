namespace Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class TestInputs : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return Test( 0, "One",                    new[] { "One" });
            yield return Test( 1, "One ",                   new[] { "One" });
            yield return Test( 2, " One",                   new[] { "One" });
            yield return Test( 3, " One ",                  new[] { "One" });
            yield return Test( 4, "One Two",                new[] { "One", "Two" });
            yield return Test( 5, "One  Two",               new[] { "One", "Two" });
            yield return Test( 6, "One   Two",              new[] { "One", "Two" });
            yield return Test( 7, "\"One Two\"",            new[] { "One Two" });
            yield return Test( 8, "One \"Two Three\"",      new[] { "One", "Two Three" });
            yield return Test( 9, "One \"Two Three\" Four", new[] { "One", "Two Three", "Four" });
            yield return Test(10, "One=\"Two Three\" Four", new[] { "One=Two Three", "Four" });
            yield return Test(11, "One\"Two Three\" Four",  new[] { "OneTwo Three", "Four" });
            yield return Test(12, "One\"Two Three   Four",  new[] { "OneTwo Three   Four" });
            yield return Test(13, "\"One Two\"",            new[] { "One Two" });
            yield return Test(14, "One\" \"Two",            new[] { "One Two" });
            yield return Test(15, "\"One\"  \"Two\"",       new[] { "One", "Two" });
            yield return Test(16, "One\\\"  Two",           new[] { "One\"", "Two" });
            yield return Test(17, "\\\"One\\\"  Two",       new[] { "\"One\"", "Two" });
            yield return Test(18, "One\"",                  new[] { "One" });
            yield return Test(19, "\"One",                  new[] { "One" });
            yield return Test(20, "One \"\"",               new[] { "One", "" });
            yield return Test(21, "One \"",                 new[] { "One", "" });
            yield return Test(22, "1 A=\"B C\"=D 2",        new[] { "1", "A=B C=D", "2" });
            yield return Test(23, "1 A=\"B \\\" C\"=D 2",   new[] { "1", "A=B \" C=D", "2" });
            yield return Test(24, "1 \\A 2",                new[] { "1", "\\A", "2" });
            yield return Test(25, "1 \\\" 2",               new[] { "1", "\"", "2" });
            yield return Test(26, "1 \\\\\" 2",             new[] { "1", "\\\"", "2" });
            yield return Test(27, "\"",                     new[] { "" });
            yield return Test(28, "\\\"",                   new[] { "\"" });
            yield return Test(29, "'A B'",                  new[] { "'A", "B'" });
            yield return Test(30, "^",                      new[] { "^" });
            yield return Test(31, "^A",                     new[] { "A" });
            yield return Test(32, "^^",                     new[] { "^" });
            yield return Test(33, "\\^^",                   new[] { "\\^" });
            yield return Test(34, "^\\\\", new[] { "\\\\" });
            yield return Test(35, "^\"A B\"", new[] { "A B" });

            // Test cases Anton

            yield return Test(36, @"/src:""C:\tmp\Some Folder\Sub Folder"" /users:""abcdefg@hijkl.com"" tasks:""SomeTask,Some Other Task"" -someParam foo", new[] { @"/src:C:\tmp\Some Folder\Sub Folder", @"/users:abcdefg@hijkl.com", @"tasks:SomeTask,Some Other Task", @"-someParam", @"foo" });

            // Test cases Daniel Earwicker 

            yield return Test(37, "", new string[] { });
            yield return Test(38, "a", new[] { "a" });
            yield return Test(39, " abc ", new[] { "abc" });
            yield return Test(40, "a b ", new[] { "a", "b" });
            yield return Test(41, "a b \"c d\"", new[] { "a", "b", "c d" });

            // Test cases Fabio Iotti 

            yield return Test(42, "this is a test ", new[] { "this", "is", "a", "test" });
            yield return Test(43, "this \"is a\" test", new[] { "this", "is a", "test" });

            // Test cases Kevin Thach

            yield return Test(44, "\"C:\\Program Files\"", new[] { "C:\\Program Files" });
            yield return Test(45, "\"He whispered to her \\\"I love you\\\".\"", new[] { "He whispered to her \"I love you\"." });
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private object[] Test(int id, string input, string[] result)
        {
            return new object[]
            {
                new TestInput()
                {
                    Id = id,
                    Input = input,
                    ExpectedResult = result,
                },
            };
        }
    }

    public class TestInput
    {
        public int Id { get; set; }
        public string Input { get; set; }
        public string[] ExpectedResult { get; set; }
    }
}
