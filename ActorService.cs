using Akka.Actor;
using whereismytransport.Actors;

namespace whereismytransport
{
	public class ActorService
	{
		public ActorSystem _system;
		public IActorRef VehicleActor { get; }

		public ActorService()
		{
			_system = ActorSystem.Create("actor-system");
			VehicleActor = _system.ActorOf(Props.Create<VehicleActor>(), "vehicle-root");
		}
	}
}
