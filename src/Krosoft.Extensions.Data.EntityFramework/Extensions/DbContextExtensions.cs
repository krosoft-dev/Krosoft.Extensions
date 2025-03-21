﻿using System.Reflection;
using Krosoft.Extensions.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Krosoft.Extensions.Data.EntityFramework.Extensions;

public static class DbContextExtensions
{
    public static void Import<T>(this DbContext db) where T : class
    {
        db.Import<T>(typeof(T).Assembly);
    }

    public static void Import<T>(this DbContext db, Assembly assembly) where T : class
    {
        var entities = JsonHelper.Get<T>(assembly);
        db.Set<T>().AddRange(entities);
    }
}