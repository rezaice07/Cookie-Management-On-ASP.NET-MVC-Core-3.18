using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Cookie.TrackingServices
{
    public interface IAddToCartEventTraking
    {
        string GetCartIdentifier();
        void TrackToAddCartIdentifier(string cartIdenfier);
    }
}
