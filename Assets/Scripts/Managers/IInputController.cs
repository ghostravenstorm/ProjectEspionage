using UnityEngine;

public interface IInputController{

	void OnSubmit();
	void OnUp();
	void OffUp();
	void OnDown();
	void OffDown();
	void OnMoveUp();
	void OffMoveUp();
	void OnMoveDown();
	void OffMoveDown();
	void OnMoveLeft();
	void OffMoveLeft();
	void OnMoveRight();
	void OffMoveRight();
	void OnJump();
	void OffJump();
	void OnSprint();
	void OffSprint();
	void OnSneak();
	void OffSneak();
	void OnAgentInteract();
}

public abstract class InputController : MonoBehaviour, IInputController{

	public virtual void OnSubmit(){}
	public virtual void OnUp(){}
	public virtual void OffUp(){}
	public virtual void OnDown(){}
	public virtual void OffDown(){}
	public virtual void OnMoveUp(){}
	public virtual void OffMoveUp(){}
	public virtual void OnMoveDown(){}
	public virtual void OffMoveDown(){}
	public virtual void OnMoveLeft(){}
	public virtual void OffMoveLeft(){}
	public virtual void OnMoveRight(){}
	public virtual void OffMoveRight(){}
	public virtual void OnJump(){}
	public virtual void OffJump(){}
	public virtual void OnSprint(){}
	public virtual void OffSprint(){}
	public virtual void OnSneak(){}
	public virtual void OffSneak(){}
	public virtual void OnAgentInteract(){}
}