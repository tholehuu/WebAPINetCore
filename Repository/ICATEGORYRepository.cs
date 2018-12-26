using System;
using System.Linq;
using System.Collections.Generic;
using WebApiNetCore.Models.MAINTE;

namespace WebApiNetCore.Repository
{   
    public interface ICATEGORYRepository 
    {   
        void Add(CATEGORY item);
        IEnumerable<CATEGORY> GetAll();
        CATEGORY Find(int key);
        CATEGORY Remove(int key);
        void Update(CATEGORY item);
    }
}