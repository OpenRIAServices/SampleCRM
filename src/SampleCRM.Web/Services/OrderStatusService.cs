﻿using OpenRiaServices.Server;
using SampleCRM.Web.Attributes;
using SampleCRM.Web.Models;
using System.Linq;

namespace SampleCRM.Web
{
    [EnableClientAccess]
    public class OrderStatusService : SampleCRMService
    {
        [Query]
        public IQueryable<OrderStatus> GetOrderStatus()
        {
            return _context.OrderStatus;
        }

        public OrderStatus GetOrderStatusById(long statusId)
        {
            return _context.OrderStatus.SingleOrDefault(x => x.Status == statusId);
        }

        [Delete]
        [RestrictAccessDeveloperMode]
        public void DeleteStatus(OrderStatus status)
        {
            _context.OrderStatus.Remove(status);
        }

        [Insert]
        [RestrictAccessDeveloperMode]
        public void InsertStatus(OrderStatus status)
        {
            _context.OrderStatus.AddOrUpdate(status);
        }

        [Update]
        [RestrictAccessDeveloperMode]
        public void UpdateStatus(OrderStatus status)
        {
            _context.OrderStatus.AddOrUpdate(status);
        }
    }
}