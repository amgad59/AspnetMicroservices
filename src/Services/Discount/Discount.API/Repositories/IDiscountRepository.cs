﻿using Discount.Grpc.Entities;

namespace Discount.Grpc.Repositories
{
	public interface IDiscountRepository
	{
		Task<Coupon> GetDiscount(string productName);
		Task<bool> UpdateDiscount(Coupon coupon);
		Task<bool> CreateDiscount(Coupon coupon);
		Task<bool> DeleteDiscount(string productName);
	}
}
