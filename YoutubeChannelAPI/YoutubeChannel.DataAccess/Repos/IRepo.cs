using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeChannelAPI.DataAccess.Repos
{
    public interface IRepo<T>
    {
        public IEnumerable<T> Get();
        public T GetOne(int id);
        public bool Post(T item);
        public bool Put(T item);
        public bool Delete(int id);
    }
}
