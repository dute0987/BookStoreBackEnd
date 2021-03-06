using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddAddress")]
        public IActionResult AddAddress(AddAddressModel addAddress)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.addressBL.AddAddress(addAddress, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Address Added SuccessFully", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Address adding Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteAddress/{AddressId}")]
        public IActionResult DeleteAddress(int AddressId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.addressBL.DeleteAddress(AddressId, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Address Deleted SuccessFully", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Address Delete Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }


        [Authorize (Roles = Role.User)]
        [HttpPost("UpdateAddress")]
        public IActionResult UpdateAddress(AddressModel addressModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.addressBL.UpdateAddress(addressModel, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Address Updated SuccessFully", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Address update Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllAddresses")]
        public IActionResult GetAllAddresses()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartdata = this.addressBL.GetAllAddresses(userId);
                if (cartdata != null)
                {
                    return this.Ok(new { Success = true, message = "Address Detail Fetched Sucessfully", Response = cartdata });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Enter valid Inputs" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}

