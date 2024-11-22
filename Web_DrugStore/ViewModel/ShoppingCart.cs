using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_DrugStore.Models;


// định nghĩa lại giỏ hàng trong đây cho dễ quản lý
namespace Web_DrugStore.ViewModel
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Items { get; set; }
        public ShoppingCart()
        {
            this.Items = new List<ShoppingCartItem>();
        }
        public void AddToCart(ShoppingCartItem item, int quantity)
        {
            var tmp = this.Items.FirstOrDefault(x => x.SanPhamId == item.SanPhamId);
            if (tmp != null)
            {
                tmp.SoLuong += quantity;
                tmp.TongTien = tmp.sanpham.DonGia * tmp.SoLuong;
            }
            else
            {
                Items.Add(item);
            } 
                
        }
        public void Remove(int id)
        {
            var tmp = Items.SingleOrDefault(x => x.SanPhamId == id);
            if (tmp != null)
            {
                Items.Remove(tmp);
            }
        }
        public void UpdateQuantity(int id, int quantity)
        {
            var tmp = Items.SingleOrDefault(x => x.SanPhamId == id);
            if (tmp != null)
            {
                tmp.SoLuong = quantity;
                tmp.TongTien = tmp.sanpham.DonGia * tmp.SoLuong;
            }
        }
        public decimal GetTotalPrice()
        {
            return Items.Sum(x => x.TongTien);
        }
        public decimal GetTotalQuantity()
        {
            return Items.Sum(x => x.SoLuong);
        }
        public void ClearCart()
        {
            Items.Clear();
        }
    }

    public class ShoppingCartItem
    {
        public int SanPhamId { get; set; }
        public SanPham sanpham { get; set; }
        public int SoLuong { get; set; }

        public decimal TongTien { get; set; }
    }
}