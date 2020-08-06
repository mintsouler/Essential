using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Dynamic;
using System.Linq.Expressions;
using System.Linq;

namespace Chapter18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //GetProperty();

            Generic();

            DynamicBind();

            ArrayTest();
        }

        public static void GetProperty()
        {
            DateTime dateTime = new DateTime();
            Type type = dateTime.GetType();
            Console.WriteLine("------------属性-----------------");
            foreach (System.Reflection.PropertyInfo property in type.GetProperties())
            {
                Console.WriteLine(property.Name);
                //property.Attributes
            }

            Console.WriteLine("------------特性-----------------");
            foreach (var attr in type.GetCustomAttributesData())
            {
                Console.WriteLine(attr.ToString());
            }

            Console.WriteLine("------------方法-----------------");
            foreach (System.Reflection.MethodInfo method in type.GetMethods())
            {
                Console.WriteLine(method.Name);
            }

            //调用
            //Console.WriteLine(type.GetMethod("").Invoke());



        }

        public static void Generic()
        {
            Console.WriteLine("\n泛型参数的类型：");
            Dictionary<string, int> dic = new Dictionary<string, int>();

            Type t = dic.GetType();
            foreach (Type type in t.GetGenericArguments())
            {
                Console.WriteLine(type.FullName);
            }
        }

        public static void VarParam()
        {
            var v = "dsf";
            //v = 123; //var 不能隐式变换类型
            dynamic d = "sdfds";
            d = 1323; //dynamic 可以隐式变换类型

            Console.WriteLine(v);
        }

        
        //动态绑定
        public static void DynamicBind()
        {
            Console.WriteLine("\n------------动态绑定-----------------");
            dynamic person = DynamicXml.Parse(@"<Person>
            <FirstName>lee</FirstName>
            <LastName>xifoeng</LastName>
            </Person>");

            Console.WriteLine(person.FirstName);
        }

        public static void ArrayTest()
        {
            Console.WriteLine("---ArrayTest()---------------------");
            string str = "1,2,5,3,9,4,6,6,8,7";
            int[] ids = Array.ConvertAll(str.Split(','), int.Parse);
            Array.Sort(ids, (a, b) => { if (a > b) return 1; else if (a == b) return 0; else return -1; });

            int[] ids1 = Array.FindAll(ids, (a) => { return a <= 5; });
            int[] ids2 = Array.FindAll(ids, (a) => { return a > 5; });

            Array.ForEach(ids1, (a) => { Console.WriteLine(a); });
            Console.WriteLine("---------------------------");
            Array.ForEach(ids2, (a) => { Console.WriteLine(a); });

            Console.WriteLine(string.Join(',',ids1));
        }
    }
}
