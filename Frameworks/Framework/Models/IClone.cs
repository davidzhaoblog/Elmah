using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Models
{
    public interface IClone<TItem>
    {
        T GetAClone<T>() where T: TItem, new();
        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true) where T : TItem;
        public void CopyFrom<T>(T source) where T : TItem;
    }
}

