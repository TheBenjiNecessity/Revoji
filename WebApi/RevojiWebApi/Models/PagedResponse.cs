using System;
namespace RevojiWebApi.Models
{
    public class PagedResponse <T>
    {
        public T[] data;
        public PageMetaData meta;

        public PagedResponse(T[] data, PageMetaData meta)
        {
            this.data = data;
            this.meta = meta;
        }
    }

    public class PageMetaData
    {
        public PageMetaData() {}
    }
}
