using Discount.gRPC.Data;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountService (DiscountDbContext dbcontext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var Coupon = await dbcontext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (Coupon == null)
            {
                Coupon = new Models.Coupon
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = "No Discount"
                };
            }

            logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", Coupon.ProductName, Coupon.Amount);

            var couponModel = Coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            return base.CreateDiscount(request, context);
        }

        public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            return base.UpdateDiscount(request, context);
        }

        public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }
    }
}
