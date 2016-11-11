public interface IOverrideable{

	DeviceState state{get; set;}
	
	void Disable();
	void Activate();
}