using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task_Project.Areas.Administrator.Models
{
    public static class Repository
    {
        static List<UserAdmin> users = new List<UserAdmin>() {

        new UserAdmin() {Login="admin",Password="admin" },
    };
        public static bool GetUserDetails(UserAdmin user)
        {
           var use= users.Where(u => u.Login.ToLower() == user.Login.ToLower() &&
            u.Password == user.Password).FirstOrDefault();
            if (use == null)
                return false;
            else
                return true;
        }
    }
}