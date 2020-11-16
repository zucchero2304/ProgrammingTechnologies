using System;
using System.Collections.Generic;
using System.Text;

namespace ShopSystem.Data
{
    public interface IContentGenerator
    {
        DataContext GenerateContent();
    }
}
