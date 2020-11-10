using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CDCImplementation.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

		//using System;
		//using System.Reflection;
		//using System.Security.Cryptography;
		//using System.Linq.Expressions;

		//public static void ComputeTime(Action body)
		//{
		//	var watch = System.Diagnostics.Stopwatch.StartNew();

		//	for (int i = 0; i < 1000000; i++)
		//	{
		//		body();
		//	}

		//	watch.Stop();

		//	Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
		//}

		//public static void Main()
		//{
		//	PropertyInfo pi = typeof(Test).GetProperty("A");
		//	var test = new Test() { A = 1 };

		//	var param = Expression.Parameter(typeof(Test));
		//	var getter = Expression.Lambda<Func<Test, object>>(
		//		Expression.Convert(
		//			Expression.Property(param, "A"),
		//			typeof(object)
		//		),
		//		param
		//	).Compile();

		//	HashAlgorithm sha = SHA256.Create();

		//	Console.WriteLine("propertyinfo");
		//	ComputeTime(() =>
		//	{
		//		var a = pi.GetValue(test);
		//		test.A++;
		//	});
		//	Console.WriteLine();

		//	Console.WriteLine("direct access");
		//	ComputeTime(() =>
		//	{
		//		var a = test.A;
		//		test.A++;
		//	});
		//	Console.WriteLine();

		//	Console.WriteLine("expression tree");
		//	ComputeTime(() =>
		//	{
		//		var a = getter(test);
		//		test.A++;
		//	});
		//	Console.WriteLine();

		//	Console.WriteLine("hashing");
		//	ComputeTime(() =>
		//	{
		//		var hash = sha.ComputeHash(BitConverter.GetBytes(test.A));
		//		test.A++;
		//	});
		//	Console.WriteLine();
		//}
	}
}
