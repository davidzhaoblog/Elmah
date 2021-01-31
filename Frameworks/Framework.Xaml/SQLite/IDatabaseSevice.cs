using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml.SQLite
{
    public interface IDatabaseSevice<out T>
    {
        bool Init();
        T GetDatabase(string databaseName);
    }
}

