namespace whereismytransport.Protocols
{
    public static class VehicleProtocol
    {

		public class Vehicle
		{
			public string _type { get; set; }

			public Vehicle(string type)
			{
				_type = type;
				/*
					Need to add more properties here as per the spec.
				*/
			}
		}
		public class AddVehicle
		{
			public string VehicleType { get; set; }
			public AddVehicle(string vehicleType)
			{
				VehicleType = vehicleType;
				/*
					Some logic here regarding line selection maybe?
				*/
			}
		}

		public class VehicleActorResponse
		{
			public string _response;

			public VehicleActorResponse(string response)
			{
				_response = $"Vehicle of type {response} has been added";
			}
		}
    }
}
