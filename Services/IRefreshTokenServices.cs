using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apbd_int_cw5.Services
{
    interface IRefreshTokenServices
    {
        void SetToken(Guid token);
        bool CheckToken(Guid token);
    }
}
