using OpenRiaServices.Server;
using SampleCRM.Web.Models;

namespace SampleCRM.Web
{
    public abstract class SampleCRMService: DomainService //DbDomainService<SampleCRMEntities>
    {
        protected SampleCRMContext _context = new SampleCRMContext();
    }
}