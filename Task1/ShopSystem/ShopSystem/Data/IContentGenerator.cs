using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    interface IContentGenerator
    {
        void GenerateContent(DataContext context);
    }
}
