using JWT_NEW.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT_NEW.Repos
{
    public interface IJWTManagerRepo
    {
        Tokens Authenticate(Users user);
    }
}
