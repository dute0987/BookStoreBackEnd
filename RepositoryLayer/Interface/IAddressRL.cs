using CommonLayer;
using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public string AddAddress(AddAddressModel addAddress, int userId);
        public string DeleteAddress(int AddressId, int UserId);
        public AddressModel UpdateAddress(AddressModel addressModel, int userId);
        public List<AddressModel> GetAllAddresses(int userId);
    }
}
