using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddCart/{userId}")]
        public IActionResult AddCart(CartModel cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.cartBL.AddCart(cart, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Added SuccessFully in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart Add Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteCart/{CartId}")]
        public IActionResult RemoveFromCart(int CartId)
        {
            try
            {
                if (this.cartBL.RemoveFromCart(CartId))
                {
                    return this.Ok(new { Success = true, message = "cart Deleted Sucessfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Soory! please Enter Valid cart Id" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetCartDetailsByUserid")]
        public IActionResult GetCartDetailsByUserid()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cart = this.cartBL.GetCartDetailsByUserid(UserId);
                if (cart != null)
                {
                    return this.Ok(new { Success = true, message = "cart Detail Fetched Sucessfully", Response = cart });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Soory! Please Enter Valid UserId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("UpdateCart/{CartId}")]
        public IActionResult UpdateCart(int CartId, CartModel cartModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                CartModel userData = this.cartBL.UpdateCart(CartId, cartModel, UserId);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "Cart Updeted Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "Sorry! Updated Failed" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
