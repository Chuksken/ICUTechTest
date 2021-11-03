using ICUTechTest.Web.Models;
using ICUTechTest.Web.TestServiceReference;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICUTechTest.Web.Services
{
    public class LoginService
    {
        ICUTechClient iCUTechClient = new ICUTechClient();
        public ResponseModel login(UserModel user, string Ip)
       {
            ResponseModel message = null;
            var respObj = iCUTechClient.Login(user.UserName, user.Password, Ip);
            message  = JsonConvert.DeserializeObject<ResponseModel>(respObj);
            
            return message;
       }
    }
}