namespace Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore
{
	public partial class Entity
	{
		public Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.MoveDirection MoveDirectionC => GetComponent<Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.MoveDirection>();

		public Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> MoveDirection => MoveDirectionC.Value;

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddMoveDirection()
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.MoveDirection() {Value = new Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() }); 
		}

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddMoveDirection(Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.MoveDirection() {Value = value}); 
		}

		public Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.MoveSpeed MoveSpeedC => GetComponent<Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.MoveSpeed>();

		public Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> MoveSpeed => MoveSpeedC.Value;

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddMoveSpeed()
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.MoveSpeed() {Value = new Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddMoveSpeed(Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.MoveSpeed() {Value = value}); 
		}

		public Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.RotationSpeed RotationSpeedC => GetComponent<Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.RotationSpeed>();

		public Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> RotationSpeed => RotationSpeedC.Value;

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddRotationSpeed()
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.RotationSpeed() {Value = new Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single>() }); 
		}

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddRotationSpeed(Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<System.Single> value)
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.RotationSpeed() {Value = value}); 
		}

		public Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.RotationDirection RotationDirectionC => GetComponent<Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.RotationDirection>();

		public Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> RotationDirection => RotationDirectionC.Value;

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddRotationDirection()
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.RotationDirection() {Value = new Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3>() }); 
		}

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddRotationDirection(Assets._Project.Develop.Runtime.Utilities.Reactive.ReactiveVariable<UnityEngine.Vector3> value)
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Features.MovementFeature.RotationDirection() {Value = value}); 
		}

		public Assets._Project.Develop.Runtime.GameplayMechanics.Common.RigidbodyComponent RigidbodyC => GetComponent<Assets._Project.Develop.Runtime.GameplayMechanics.Common.RigidbodyComponent>();

		public UnityEngine.Rigidbody Rigidbody => RigidbodyC.Value;

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddRigidbody(UnityEngine.Rigidbody value)
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Common.RigidbodyComponent() {Value = value}); 
		}

		public Assets._Project.Develop.Runtime.GameplayMechanics.Common.CharacterControllerComponent CharacterControllerC => GetComponent<Assets._Project.Develop.Runtime.GameplayMechanics.Common.CharacterControllerComponent>();

		public UnityEngine.CharacterController CharacterController => CharacterControllerC.Value;

		public Assets._Project.Develop.Runtime.GameplayMechanics.EntitiesCore.Entity AddCharacterController(UnityEngine.CharacterController value)
		{
		return AddComponent(new Assets._Project.Develop.Runtime.GameplayMechanics.Common.CharacterControllerComponent() {Value = value}); 
		}

	}
}
