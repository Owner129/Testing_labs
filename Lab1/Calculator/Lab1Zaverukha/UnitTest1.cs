namespace Lab1Zaverukha
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    [TestClass]
    public class SeparateTests
    {
        // Припустимо, що ці поля є в класі з методом Separate
        private static readonly HashSet<char> _operators = new HashSet<char> { '+', '-', '*', '/' };
        private static readonly HashSet<char> _brackets = new HashSet<char> { '(', ')' };
        private static readonly HashSet<char> _unary_operators = new HashSet<char> { '-' };

        // Метод, що тестує Separate, отримуючи дані з бази даних
        [TestMethod]
        public void Separate_FromDatabaseData()
        {
            using (SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-B6V0R6SV\SQLEXPRESS;Initial Catalog=DBLibCalcZaverukha;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT Input, Expected FROM SeparateTestData", connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string input = reader.GetString(0);
                    string expectedString = reader.GetString(1);
                    List<string> expected = expectedString.Split(',').ToList();

                    // Act
                    var result = Separate(input).ToList();

                    // Assert
                    CollectionAssert.AreEqual(expected, result, $"Failed for input: {input}");
                }
                reader.Close();
            }
        }

        private static IEnumerable<string> Separate(string input)
        {
            int pos = 0;
            while (pos < input.Length)
            {
                string s = string.Empty + input[pos];
                if (!_operators.Union(_brackets).Union(_unary_operators).Contains(input[pos]))
                {
                    if (Char.IsDigit(input[pos]))
                        for (int i = pos + 1; i < input.Length && Char.IsDigit(input[i]); i++)
                            s += input[i];
                    else if (Char.IsLetter(input[pos]))
                        for (int i = pos + 1; i < input.Length && (Char.IsLetter(input[i]) || Char.IsDigit(input[i])); i++)
                            s += input[i];
                }
                yield return s;
                pos += s.Length;
            }
        }
    }
}
