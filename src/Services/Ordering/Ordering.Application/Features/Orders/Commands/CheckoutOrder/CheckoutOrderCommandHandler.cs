﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistance;
using Ordering.Application.Models;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;
        private readonly IEmailService _emailService;
        public CheckoutOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, 
            IEmailService emailService, ILogger<CheckoutOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }
        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(order);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");

            await SendMail(newOrder);

            return newOrder.Id;
        }

        public async Task SendMail(Order order)
        {
            var email = new Email { To = "amgad@gmail.com", Body = $"Order Created", Subject = "New order" };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with mail service:{ex.Message}");
            }
        }
    }
}
