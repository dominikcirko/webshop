﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webshopAPI.Models;

[Table("Cart")]
public partial class Cart
{
    [Key]
    public int IDCart { get; set; }

    public int UserID { get; set; }

    [InverseProperty("Cart")]
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    [ForeignKey("UserID")]
    [InverseProperty("Carts")]
    public virtual User User { get; set; }
}