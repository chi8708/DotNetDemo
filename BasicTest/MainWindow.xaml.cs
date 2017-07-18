using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Concurrent;
using System.Threading;

namespace BasicTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ConcurrentQueue<Product> productsCon = new ConcurrentQueue<Product>();
        //ConcurrentStack 与 ConcurrentQueue 相似
        List<Product> products = new List<Product>();
        private static object o=new object();
        Stopwatch sw = new Stopwatch();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSub_Click(object sender, RoutedEventArgs e)
        {
              sw.Start();
              Task task1 = Task.Factory.StartNew(AddProducts);
              Task task2 = Task.Factory.StartNew(AddProducts);
              Task task3 = Task.Factory.StartNew(AddProducts);
              Task.WaitAll(task1,task2,task3);
              sw.Stop();
              txtMsg.Text = string.Format("执行完成,集合成员数{0}；耗时:{1}",products.Count,sw.Elapsed);
             
        }

        public void AddProducts() 
        {
            Parallel.For(0, 1000, (int i) =>
            {
                Product p = new Product() { Id = i, Name = "name" + i };
                //加锁解决并发使数量为3000
                lock (o)
                {
                    products.Add(p);
                }

            });
  
        }

        private void btnConcurrent_Click(object sender, RoutedEventArgs e)
        {
            sw.Start();
            Task task1 = Task.Factory.StartNew(AddConProducts);
            Task task2 = Task.Factory.StartNew(AddConProducts);
            Task task3 = Task.Factory.StartNew(AddConProducts);
            Task.WaitAll(task1, task2, task3);
            sw.Stop();
            txtMsg.Text += string.Format("执行完成,集合成员数{0}；耗时:{1}", productsCon.Count, sw.Elapsed);
            Parallel.Invoke(PeekConcurrenProducts, DelReturn);
            txtMsgCon.Text += string.Format("移除完成集合成员数{0}", productsCon.Count);

            foreach (var item in backConProducts)
            {
                txtMsgConPeek.Text += ","+item.Name;
            }
        }

        public void AddConProducts() 
        {
            Parallel.For(0, 1000, (i) =>
            {
                Product p = new Product() { Id = i, Name = "name" + i };
                productsCon.Enqueue(p);//ConcurrentQueue集合解决了并发问题 
            }
            );
            for (int i = 0; i < 1000; i++)
            {
                 Product p=new Product(){Id=i,Name="name"+i};
                productsCon.Enqueue(p);//ConcurrentQueue集合解决了并发问题 
            }
        }

        List<Product> backConProducts = new List<Product>();
        /*尝试返回 但不移除*/
        public void PeekConcurrenProducts()
        {
         
            Parallel.For(0, 2, (i) =>
            {
                Product product = null;
                bool excute = productsCon.TryPeek(out product);
                backConProducts.Add(product);
               // txtMsgConPeek.Text +=product.Name;
            });
        }
        /// <summary>
        /// 移除并返回对象
        /// </summary>
        public void DelReturn() 
        {
            Parallel.For(0, 2, i =>
            {
                Product p = null;
                productsCon.TryDequeue(out p);
                //txtMsgCon.Text += p.Name;
            });
        }
    }

    public class Product 
    {
       internal  int Id { get; set; }
       internal string Name { get; set; }
    }
}
