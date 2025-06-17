using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab03
{
    internal class MatHang
    {
        public int maMh;
        public string tenMh;
        public int soluong;
        public float donGia;

        public MatHang()
        {
            this.maMh = -1;
            this.tenMh = "";
            this.soluong = -1;
            this.donGia = -1;
        }
        public MatHang(int maMh, string tenMh, int soluong, float donGia)
        {
            this.maMh = maMh;
            this.tenMh = tenMh;
            this.soluong = soluong;
            this.donGia = donGia;
        }

        public override string ToString()
        {
            return "Id: " + maMh + " Mathang: " + tenMh + "\n Soluong" + soluong + " Gia tien: " + donGia;
        }
    }
}
