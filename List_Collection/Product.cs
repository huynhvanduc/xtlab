using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace List_Collection
{
    public class Product : IComparable<Product>, IFormattable
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public double Price { set; get; }
        public string Origin { set; get; }



        public Product(int id, string name, double price, string origin)
        {
            ID = id; Name = name; Price = price; Origin = origin;
        }



        public int CompareTo(Product other)
        {
            //sap xep ve gia
            double delta = this.Price - other.Price;
            if (delta > 0)
                return -1;
            else if (delta < 0)
                return 1;
            return 0;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null) format = "O";
            switch (format.ToUpper())
            {
                case "O": // Xuất xứ trước
                    return $"Xuất xứ: {Origin} - Tên: {Name} - Giá: {Price} - ID: {ID}";
                case "N": // Tên xứ trước
                    return $"Tên: {Name} - Xuất xứ: {Origin} - Giá: {Price} - ID: {ID}";
                default: // Quăng lỗi nếu format sai
                    throw new FormatException("Không hỗ trợ format này");
            }
        }

        // Nạp chồng ToString
        override public string ToString() => $"{Name} - {Price}";

        // Quá tải thêm ToString - lấy chỗi thông tin sản phẩm theo định dạng
        public string ToString(string format) => this.ToString(format, null);
    }
}
