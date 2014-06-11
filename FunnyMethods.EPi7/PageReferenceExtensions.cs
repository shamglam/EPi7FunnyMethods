using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using log4net.Appender;

namespace FunnyMethods.EPi7
{
    public static class PageReferenceExtensions
    {
        public static bool CreateRandomPage<T>(this PageReference reference, IDateTime datetime = null, EPiServer.IContentRepository repository = null) where T:IContent
        {
            if (repository == null)
            {
                repository = ServiceLocator.Current.GetInstance<IContentRepository>();
            }
            if (datetime == null)
            {
                datetime = new DateTimeWrapper();
            }
            if (datetime.Now.Day%2 == 0)
            {
                T ft = repository.GetDefault<T>(reference);
                ft.Property["PageName"] = new PropertyString("Foo bar");
                var cref = repository.Save(ft, SaveAction.Publish, AccessLevel.NoAccess);
                return true;
            }
            else return false;
        }

    }

    public class DateTimeWrapper : IDateTime
    {
        public virtual DateTime Now { get { return DateTime.Now; } }
    }
}
