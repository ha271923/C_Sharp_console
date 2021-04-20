using System;
using System.Reflection;
using ConsoleApp.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyUnitTest
{
    [TestClass]
    public class UT_reflection
    {


        public class People
        {
            public People()
            {
                Console.WriteLine("{0}", this.GetType().FullName);
            }
            public int Id { get; set; }//Property

            public string Name { get; set; }//Property

            public string Description;//Field
        }

        [TestMethod] // -----------------------------------------------------------------------
        public void ReflectionField_test()
        {
            Type type = typeof(People);
            object oPeople = Activator.CreateInstance(type);
            foreach (var item in type.GetProperties())
            {
                Console.WriteLine(type.Name);
                Console.WriteLine(item.Name);
                Console.WriteLine(item.GetValue(oPeople));
                if (item.Name.Equals("Id"))
                {
                    item.SetValue(oPeople, 234);
                }
                else if (item.Name.Equals("Name"))
                {
                    item.SetValue(oPeople, "風蕭蕭");
                }

            }
        }


        [TestMethod] // -----------------------------------------------------------------------
        public void ReflectionMethod_test()
        {
            Assembly assembly = Assembly.Load("DB.SqlServer");
            Type type = assembly.GetType("DB.SqlServer.ReflectionTest");
            object oReflectionTest = Activator.CreateInstance(type);

            //不用轉換類型調用裡面方法
            MethodInfo method;
            method = type.GetMethod("Show1");//透過反射找到方法
            method.Invoke(oReflectionTest, null);//("實體","參數")　null=>無參數

            method = type.GetMethod("Show2");//透過反射找到方法
            method.Invoke(oReflectionTest, new object[] { 123 });//("實體","參數")　
                                                                 //靜態方法
            method = type.GetMethod("Show5");//透過反射找到方法
            method.Invoke(oReflectionTest, new object[] { "麥田的" });//("實體","參數")　
            method.Invoke(null, new object[] { "果然" });//因為是靜態方法所以實體也可以是null
                                                       //多載
            method = type.GetMethod("Show3", new Type[] { });//如果找函數時有多載可以傳遞參數列表進去
            method.Invoke(oReflectionTest, new object[] { });//沒參數的

            method = type.GetMethod("Show3", new Type[] { typeof(int) });//找int的
            method.Invoke(oReflectionTest, new object[] { 123 });

            method = type.GetMethod("Show3", new Type[] { typeof(string) });//找string的
            method.Invoke(oReflectionTest, new object[] { "ant" });
            // call private method
            method = type.GetMethod("Show4", BindingFlags.Instance | BindingFlags.NonPublic);//找私有的方法
            method.Invoke(oReflectionTest, new object[] { "天空之上" });
        }

        public class ReflectionTest
        {
            //無參數
            public void Show1()
            {
                Console.WriteLine("這裡是{0}的Show1", this.GetType());
            }
            //有參數
            public void Show2(int id)
            {
                Console.WriteLine("這裡是{0}的Show2", this.GetType());
            }
            //重載方法之一
            public void Show3(int id, string name)
            {
                Console.WriteLine("這裡是{0}的Show3_1", this.GetType());
            }
            //重載方法之二
            public void Show3(string name, int id)
            {
                Console.WriteLine("這裡是{0}的Show3_2", this.GetType());
            }
            //重載方法之三
            public void Show3(int id)
            {
                Console.WriteLine("這裡是{0}的Show3_3", this.GetType());
            }
            //重載方法之四
            public void Show3(string name)
            {
                Console.WriteLine("這裡是{0}的Show3_4", this.GetType());
            }
            //重載方法之五
            public void Show3()
            {
                Console.WriteLine("這裡是{0}的Show3_5", this.GetType());
            }
            //私有方法
            public void Show4(string name)
            {
                Console.WriteLine("這裡是{0}的Show4", this.GetType());
            }
            //靜態方法
            public void Show5(string name)
            {
                Console.WriteLine("這裡是{0}的Show５", typeof(ReflectionTest));
            }
        }

        /*
        [TestMethod] // -----------------------------------------------------------------------
        public void ReflectionDLL_test()
        {
            // System.Reflection.Net框架提共幫助類庫,可以讀取並使用metadata
            // A. 加載dll
            Assembly assembly = Assembly.Load("DB.Mysql");//dll名稱 從當前目錄加載，平時比較多用，性能高一些
            Assembly assembly1 = Assembly.LoadFile(@"C:\\DB.Mysql.dll");//帶完整路徑,需要加上副檔名
                                                                        //加載dll需要加入參考，使用的時候不會出錯
            Assembly assembly2 = Assembly.LoadFrom(@"DB.Mysql.dll");//需添加附檔名或者完整路徑
            // B. 獲取dll訊息

            foreach (var item in assembly.GetModules()) {
                Console.WriteLine(item.FullyQualiftedName);
            }

            foreach (var item in assembly.GetTypes()) {
                Console.WriteLine(item.FullName);
            }

            // C. 使用dll 1. 載入dll（Assembly.Load(“DB.Mysql”)） -> 2. 創建實體（Activator.CreateInstance(type)）-> 3. 轉型
            Type type = assembly.GetType("DB.Mysql.MySqlHelper");//需要堤共完整的類型名稱：命名空間＋類別名稱
            object oHelper = Activator.CreateInstance(type);//創建一個object類型的實體, 這邊寫true,可以透過反射調用private constructor
            object oReflectionTest1 = Activator.CreateInstance(type);//無參數建構子
            object oReflectionTest2 = Activator.CreateInstance(type, new object[] { 123 });//int的建構子
            object oReflectionTest3 = Activator.CreateInstance(type, new object[] { "123" });//string的建構子
            oHelper.Query();//無法執行，因為他是object類型，但實際上方法是有的　　（編譯器不認可）
            IDBHelper iDBHelper = (IDBHelper)oDBHelper;//先做類型轉換
            iDBHelper.Query();
        }
        */
    }
}
