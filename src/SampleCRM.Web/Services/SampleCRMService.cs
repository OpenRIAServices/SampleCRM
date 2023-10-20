using OpenRiaServices.Server;
using SampleCRM.Web.Models;

namespace SampleCRM.Web
{
    public abstract class SampleCRMService: DomainService
    {
        protected SampleCRMEntities _context = new SampleCRMEntities();
    }
}