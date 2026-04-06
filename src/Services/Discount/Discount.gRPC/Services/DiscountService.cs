using Discount.gRPC.Data;
using Discount.gRPC.Models;
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

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var Coupon = request.Coupon.Adapt<Coupon>();

            if (Coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Coupon"));
            }

            dbcontext.Coupons.Add(Coupon);

            await dbcontext.SaveChangesAsync();

            var couponModel = Coupon.Adapt<CouponModel>();

            Console.WriteLine(couponModel);

            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var Coupon = request.Coupon.Adapt<Coupon>();

            if (Coupon is null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Coupon"));
            }

            dbcontext.Coupons.Update(Coupon);

            await dbcontext.SaveChangesAsync();

            var couponModel = Coupon.Adapt<CouponModel>();

            Console.WriteLine(couponModel);

            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var Coupon = await dbcontext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (Coupon is null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "this discount is not found"));
            }

            dbcontext.Coupons.Remove(Coupon);

            await dbcontext.SaveChangesAsync();

            logger.LogInformation("Deleted Coupon Successfully");

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
