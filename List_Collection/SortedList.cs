using System;
using System.Collections.Generic;
using System.Text;

namespace List_Collection
{
    public class SortedList
    {
        public static void test()
        {
            var products = new SortedList<string, string>();
            products.Add("Iphone 6", "P-IPHONE-6"); // Thêm vào phần tử mới (key, value)
            products.Add("Laptop Abc", "P-LAP");
            products["Điện thoại Z"] = "P-DIENTHOAI"; // Thêm vào phần tử bằng Indexer
            products["Tai nghe XXX"] = "P-TAI";       // Thêm vào phần tử bằng Indexer


            //duyetj phan tu
            Console.WriteLine("Danh sach duyet mang");
            foreach(KeyValuePair<string, string> p in products)
                Console.WriteLine($"{p.Key} - {p.Value}");

            // Đọc value khi biết key
            string productName = "Tai nghe XXX";
            Console.WriteLine($"{productName} có mã là {products[productName]}");
        }
    }
}
