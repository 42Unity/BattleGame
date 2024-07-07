using System;
using System.Collections.Generic;

public class DisposibleList : IDisposable
{
    private readonly List<IDisposable> list = new();

    public void Add(IDisposable item)
    {
        list.Add(item);
    }

    public void Dispose()
    {
        foreach (var item in list)
        {
            item.Dispose();
        }
        list.Clear();
    }
}
