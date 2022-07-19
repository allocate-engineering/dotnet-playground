using System;
using System.Collections;
using UserApi.Web.Models;

namespace UserApi.Web.Repositories
{
    public class UserRepository
    {
        private static Guid SYSTEM_ID = new Guid("11111111-1111-1111-1111-111111111111");

        private static Dictionary<Guid, UserDataModel> _userStorage = new Dictionary<Guid, UserDataModel>() { { SYSTEM_ID, new UserDataModel
            {
                Id = SYSTEM_ID,
                Name = "System",
                Email = "system@allocate.build",
            } } };

        public UserRepository()
        {
        }

        public void AddUser(UserRequestModel requestModel)
        {
            if (String.IsNullOrEmpty(requestModel.Email) || String.IsNullOrEmpty(requestModel.Name))
            {
                throw new Exception("Email and Name are required");
            }

            var dataModel = new UserDataModel();

            //from model
            dataModel.Age = requestModel.Age;
            dataModel.Email = requestModel.Email;
            dataModel.Name = requestModel.Name;
            dataModel.Phone = requestModel.Phone;

            //metadata
            dataModel.Id = Guid.NewGuid();
            dataModel.Created = DateTime.Now;
            dataModel.Modified = DateTime.Now;

            _userStorage.Add(dataModel.Id, dataModel);
        }

        public UserResponseModel? GetUser(string email)
        {
            if (_userStorage.Values.Any(user => user.Email == email))
            {
                var dataModel = _userStorage.Values.Where(user => user.Email == email).First();
                var responseModel = new UserResponseModel();
                responseModel.Age = dataModel.Age;
                responseModel.Email = dataModel.Email;
                responseModel.Id = dataModel.Id;
                responseModel.Name = dataModel.Name;
                responseModel.Phone = dataModel.Phone;

                return responseModel;
            }
            return null;
        }

        public UserResponseModel? GetUser(Guid id)
        {
            if (_userStorage.ContainsKey(id))
            {
                var dataModel = _userStorage[id];
                var responseModel = new UserResponseModel();
                responseModel.Age = dataModel.Age;
                responseModel.Email = dataModel.Email;
                responseModel.Id = dataModel.Id;
                responseModel.Name = dataModel.Name;
                responseModel.Phone = dataModel.Phone;

                return responseModel;
            }
            return null;
        }
    }
}