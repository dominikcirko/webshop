﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace webshopAPI.Models;

[Table("User")]
[Index("Email", Name = "UQ_User_Email", IsUnique = true)]
[Index("Username", Name = "UQ_User_Username", IsUnique = true)]
public partial class User
{
    [Key]
    public int IDUser { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Password { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string PhoneNumber { get; set; }

    public bool IsAdmin { get; set; }

    [MaxLength(1024)]
    public byte[] PasswordSalt { get; set; }

    [StringLength(255)]
    public string PasswordHash { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}