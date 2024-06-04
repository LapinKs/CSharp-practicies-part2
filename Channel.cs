using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace rocket_bot;

public class Channel<T> where T : class
{
    private List<T> channel = new List<T> { };
    private object obj = new object();
    /// <summary>
    /// Возвращает элемент по индексу или null, если такого элемента нет.
    /// При присвоении удаляет все элементы после.
    /// Если индекс в точности равен размеру коллекции, работает как Append.
    /// </summary>
    public T this[int index]
    {
        get
        {
            lock (channel)
            {
                return index >= 0 && channel.Count > index ? channel[index] : null;
            }
        }
        set
        {
            lock (channel)
            {
                channel.RemoveRange(index, channel.Count - index);
                channel.Add(value);
            }
        }
    }

    /// <summary>
    /// Возвращает последний элемент или null, если такого элемента нет
    /// </summary>
    public T LastItem()
    {
        lock (channel)
        {
            return channel.LastOrDefault();
        }
    }

    /// <summary>
    /// Добавляет item в конец только если lastItem является последним элементом
    /// </summary>
	public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
    {
        lock (channel)
        {
            if (channel.LastOrDefault() == knownLastItem)
                channel.Add(item);
        }
    }

    /// <summary>
    /// Возвращает количество элементов в коллекции
    /// </summary>
    public int Count
    {
        get
        {
            lock (channel)
            {
                return channel.Count;
            }
        }
    }
}