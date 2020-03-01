using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using whereismytransport.Models;
using System.Threading.Tasks;
using Akka.Actor;
using System;
using static whereismytransport.Protocols.VehicleProtocol;

namespace whereismytransport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : InjectedController
    {
        private readonly ILogger<VehicleController> _logger;
		private IActorRef VehicleActor;

        public VehicleController(ILogger<VehicleController> logger, WIMTDataContext context, ActorService actorService): base(context)
        {
            _logger = logger;
			VehicleActor = actorService.VehicleActor;
        }

		[HttpPost("/addVehicle")]
		public async Task<IActionResult> Add([FromBody] Payload value)
		{
			var result = await VehicleActor.Ask<VehicleActorResponse>(new AddVehicle(value.type), TimeSpan.FromSeconds(10));
			Console.WriteLine(result);
			return Ok("ok");
		}
    }
}
