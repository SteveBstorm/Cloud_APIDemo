using BLL.Forms;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapper
{
    public static class UserMapper
    {

        public static User ToUser(this RegisterForm form)
        {
            return new User()
            {
                Email = form.Email,
                Id = 0,
                Password = form.Password,
                Pseudo = form.Pseudo
            };
        }

    }
}
