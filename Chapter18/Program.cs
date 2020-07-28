using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Chapter18
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //GetProperty();

            Generic();
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
                property.get
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
    }
}
