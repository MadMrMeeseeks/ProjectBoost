using Godot;

public partial class Player : RigidBody3D
{
	// How much vertical force to apply when moving
	[Export(PropertyHint.Range, "750.0, 3000.0")]
	float _thrust = 1000.0f;
	
	// How much torque thrust to apply
	[Export] private float _torqueThrust = 100f;

	private bool _isTransitioning = false;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("boost"))
		{
			ApplyCentralForce(Basis.Y * (float)delta * _thrust);
		}

		if (Input.IsActionPressed("rotate_left"))
		{
			ApplyTorque(new Vector3(0.0f, 0.0f, _torqueThrust * (float)delta));
		}
		
		if (Input.IsActionPressed("rotate_right"))
		{
			ApplyTorque(new Vector3(0.0f, 0.0f, -_torqueThrust * (float)delta));
		}
	}


	// Signal handler method
	private void OnBodyEntered(Node body)
	{
		if (_isTransitioning == false)
		{
			if (body.IsInGroup("Goal"))
			{
				var landingPad = body as Landingpad;
				if (landingPad != null)
				{
					CompleteLevel(landingPad.FilePath);
				}
			}

			if (body.IsInGroup("Hazard"))
			{
				CrashSequence();
			}
		}
	}

	private void CrashSequence()
	{
		GD.Print("KABOOM!");
		// Use this method to disable the process function, rendering the ship controls off
		SetProcess(false);
		_isTransitioning = true;
		Tween tween = CreateTween();
		tween.TweenInterval(1.0);
		tween.TweenCallback((Callable.From(GetTree().ReloadCurrentScene)));
	}


	private void CompleteLevel(string nextLevelFile)
	{
		GD.Print("You Win!");
		SetProcess(false);
		_isTransitioning = true;
		Tween tween = CreateTween();
		tween.TweenInterval(1.0);
		tween.TweenCallback(Callable.From(() => GetTree().ChangeSceneToFile(nextLevelFile)));
	}
}
