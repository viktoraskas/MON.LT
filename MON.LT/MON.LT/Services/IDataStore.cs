﻿using MON.LT.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bukimedia.PrestaSharp.Entities;

namespace MON.LT.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        Task<byte[]> GetImageAsync(Item item);
        Task LoadMoreItems();
    }
}
