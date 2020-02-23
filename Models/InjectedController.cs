using Microsoft.AspNetCore.Mvc;

namespace whereismytransport
{
	public class InjectedController: ControllerBase
    {
        protected readonly WIMTDataContext db;

		// This would be better if i didnt have to inject a specific DbContext here, not sure how though.
        public InjectedController(WIMTDataContext context)
        {
            db = context;
        }
    }
}
