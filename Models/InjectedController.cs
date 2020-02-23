using Microsoft.AspNetCore.Mvc;

namespace whereismytransport
{
	public class InjectedController: ControllerBase
    {
        protected readonly TokenContext db;

        public InjectedController(TokenContext context)
        {
            db = context;
        }
    }
}
