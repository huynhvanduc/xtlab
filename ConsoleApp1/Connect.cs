using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADONET
{
    public class Connect
    {
        public static void ConnectDb()
        {
            SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder["Server"] = @"DESKTOP-GIQCGVR\HVDUC";
            stringBuilder["Database"] = "xtLab";
            stringBuilder["User Id"] = "sa";
            stringBuilder["Password"] = "12345";

            string sqlConnectStr = stringBuilder.ToString();

            using (SqlConnection connection = new SqlConnection(sqlConnectStr))
            {
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
            }
        }
    }
}
