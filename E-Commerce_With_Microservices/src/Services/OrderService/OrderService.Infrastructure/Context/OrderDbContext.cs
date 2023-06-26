﻿using Microsoft.EntityFrameworkCore;
using OrderService.Domain.AggregateModels;

namespace OrderService.Infrastructure.Context
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
