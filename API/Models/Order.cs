﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webshopAPI.Models;

[Table("Order")]
public partial class Order
{
    [Key]
    public int IDOrder { get; set; }

    public int UserID { get; set; }

    public int StatusID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal TotalAmount { get; set; }

    [InverseProperty("Order")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("StatusID")]
    [InverseProperty("Orders")]
    public virtual Status Status { get; set; }

    [ForeignKey("UserID")]
    [InverseProperty("Orders")]
    public virtual User User { get; set; }
}