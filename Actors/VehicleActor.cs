using Akka.Actor;
using System.Collections;
using System.Collections.Generic;
using static whereismytransport.Protocols.VehicleProtocol;

namespace whereismytransport.Actors
{
    public class VehicleActor : ReceiveActor
    {
		private bool _initialized;

		private List<Vehicle> _vehicles = new List<Vehicle>();
		public VehicleActor()
		{
			Receive<AddVehicle>(_ => !_initialized, msg =>
            {
                _initialized = true;
				_vehicles.Add(new Vehicle(msg.VehicleType));
				Sender.Tell(new VehicleActorResponse(msg.VehicleType));
            });
		}
    }
}
