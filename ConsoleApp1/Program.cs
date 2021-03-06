﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Ket noi database bang
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder["Server"] = @"DESKTOP-GIQCGVR\HVDUC";
            stringBuilder["Database"] = "xtLab";
            stringBuilder["User Id"] = "sa";
            stringBuilder["Password"] = "12345";
            String sqlConnectionString = stringBuilder.ToString();

            SqlConnection connection = new SqlConnection(sqlConnectionString);


            // kích hoạt chế độ thu thập thông tin thống kê khi truy vấn
            connection.StatisticsEnabled = true;

            Console.WriteLine($"{"ConnectionString ",17} : {stringBuilder}");
            Console.WriteLine($"{"DataSource ",17} : {connection.DataSource}");

            // Bắt sự kiện trạng thái kết nối thay đổi
            connection.StateChange += (object sender, StateChangeEventArgs e) => {
                Console.WriteLine($"Kết nối thay đổi: {e.CurrentState}, trạng thái trước: " + $"{e.OriginalState}");
            };

            // mở kết nối
            connection.Open();

            // Dùng SqlCommand thi hành SQL  - sẽ  tìm hiểu sau
            using (SqlCommand command = connection.CreateCommand())
            {

                // Câu truy vấn SQL
                command.CommandText = "select top(5) * from Sanpham";
                var reader = command.ExecuteReader();
                // Đọc kết quả truy vấn
                Console.WriteLine("\r\nCÁC SẢN PHẨM:");
                Console.WriteLine($"{"SanphamID ",10} {"TenSanpham "}");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["SanphamID"],10} {reader["TenSanpham"]}");
                }
            }



            // Lấy thống kê và in số liệu thống kê
            Console.WriteLine("Thông tin thống kê các tương tác đã thực hiện trên kết nôis");
            var dicStatics = connection.RetrieveStatistics();
            foreach (var key in dicStatics.Keys)
            {
                Console.WriteLine($"{key,17} : {dicStatics[key]}");
            }

            // Không dùng đến kết nối thì phải đóng lại (giải phóng)
            connection.Close();

        }
    }
}
