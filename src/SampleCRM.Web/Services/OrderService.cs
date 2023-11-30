﻿using OpenRiaServices.Server;
using SampleCRM.Web.Attributes;
using SampleCRM.Web.Models;
using System;
using System.Linq;

namespace SampleCRM.Web
{
    [EnableClientAccess]
    public class OrderService : SampleCRMService
    {
        [Query]
        public IQueryable<Orders> GetOrders()
        {
            return _context.Orders;
        }

        [Query]
        public IQueryable<Orders> GetLatestOrders(int limit)
        {
            return _context.Orders.OrderByDescending(x => x.OrderDate).Take(limit);
        }

        [Query]
        public IQueryable<Orders> GetOrdersOfCustomer(long customerId)
        {
            return _context.Orders.Where(x => x.CustomerID == customerId);
        }

        [Delete]
        [RestrictAccessDeveloperMode]
        public void DeleteOrder(Orders order)
        {
            var dorder = _context.Orders.FirstOrDefault(x => x.OrderID == order.OrderID);
            if (dorder == null)
                return;

            _context.Orders.Remove(dorder);
            _context.SaveChanges();
        }

        [Insert]
        [RestrictAccessDeveloperMode]
        public void InsertOrder(Orders order)
        {
            order.OrderID = new Random().Next((int)Math.Pow(10, 12), (int)Math.Pow(10, 13) - 1);
            if (order.OrderID < 0)
                order.OrderID *= -1;

            order.OrderDate = order.LastModifiedOn = DateTime.Now.ToString();
            _context.Orders.Add(order);

#if DEBUG_
            var validationResult = _context.Entry(order).GetValidationResult();
            if (validationResult.ValidationErrors.Any())
            {
                Console.WriteLine($"Validation Error in InsertOrder: {validationResult.ValidationErrors.FirstOrDefault().PropertyName} {validationResult.ValidationErrors.FirstOrDefault().ErrorMessage}");
            }
#endif

            _context.SaveChanges();
        }

        [Update]
        [RestrictAccessDeveloperMode]
        public void UpdateOrder(Orders order)
        {
            order.LastModifiedOn = DateTime.Now.ToString();
            _context.Orders.AddOrUpdate(order);
            _context.SaveChanges();
        }
    }
}