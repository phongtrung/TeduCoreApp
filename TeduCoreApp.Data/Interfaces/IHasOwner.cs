using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Data.Interfaces
{
    interface IHasOwner<T>
    {
        T OwnerId { get; set; }
    }
}
