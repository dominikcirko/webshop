﻿namespace webshopAPI.DTOs
{
    public class CartItemDTO
    {
        public int IDCartItem { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public string ItemTitle { get; set; }
    }
}