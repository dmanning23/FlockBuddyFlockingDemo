using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;

namespace FlockBuddyWidgets
{
	/// <summary>
	/// This 
	/// </summary>
	public class BoidsScreen : BaseTab
	{
		readonly FlockManager _flock;

		public BoidsScreen(FlockManager flock) : base("BoidsScreen")
		{
			_flock = flock;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			//Add the header
			AddHeader(_flock);

			var radius = AddBoidOption("Radius", _flock.BoidRadius);
			radius.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidRadius = radius.Number;
			};

			var mass = AddBoidOption("Mass", _flock.BoidMass);
			mass.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidMass = mass.Number;
			};

			var minSpeed = AddBoidOption("Min Speed", _flock.BoidMinSpeed);
			minSpeed.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidMinSpeed = minSpeed.Number;
			};

			var walkSpeed = AddBoidOption("Walk Speed", _flock.BoidWalkSpeed);
			walkSpeed.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidWalkSpeed = walkSpeed.Number;
			};

			var maxSpeed = AddBoidOption("Max Speed", _flock.BoidMaxSpeed);
			maxSpeed.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidMaxSpeed = maxSpeed.Number;
			};

			var maxTurnRate = AddBoidOption("Max Turn Rate", _flock.BoidMaxTurnRate);
			maxTurnRate.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidMaxTurnRate = maxTurnRate.Number;
			};

			var maxForce = AddBoidOption("Max Force", _flock.BoidMaxForce);
			maxForce.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidMaxForce = maxForce.Number;
			};

			var queryRadius0 = AddBoidOption("Neighbor Query Radius", _flock.BoidNeighborQueryRadius);
			queryRadius0.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidNeighborQueryRadius = queryRadius0.Number;
			};

			var queryRadius1 = AddBoidOption("Predator Query Radius", _flock.BoidPredatorQueryRadius);
			queryRadius1.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidPredatorQueryRadius= queryRadius1.Number;
			};

			var queryRadius2 = AddBoidOption("Prey Query Radius", _flock.BoidPreyQueryRadius);
			queryRadius2.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidPreyQueryRadius = queryRadius2.Number;
			};

			var queryRadius3 = AddBoidOption("Vip Query Radius", _flock.BoidVipQueryRadius);
			queryRadius3.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidVipQueryRadius = queryRadius3.Number;
			};

			var queryRadius4 = AddBoidOption("Wall Query Radius", _flock.BoidWallQueryRadius);
			queryRadius4.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidWallQueryRadius = queryRadius4.Number;
			};

			var retargetTime = AddBoidOption("Retarget Time", _flock.BoidRetargetTime);
			retargetTime.OnNumberEdited += (obj, e) =>
			{
				_flock.BoidRetargetTime = retargetTime.Number;
			};

			AddItem(ToolStack);
		}

		private NumEdit AddBoidOption(string option, float current)
		{
			var boidOptionLayout = new StackLayout(StackAlignment.Left)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};

			int sizeDelta = 360;

			var button = new RelativeLayout()
			{
				Size = new Vector2(200f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = false,
			};
			button.AddItem(new Label(option.ToString(), Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				Highlightable = false
			});
			boidOptionLayout.AddItem(button);
			sizeDelta -= button.Rect.Width;
			
			//add a num edit to change the weight
			var weight = new NumEdit(Content, FontSize.Small)
			{
				Size = new Vector2(sizeDelta, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = true,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
				Number = current,

			};
			boidOptionLayout.AddItem(weight);
			ToolStack.AddItem(boidOptionLayout);

			//add a shim
			ToolStack.AddItem(new Shim()
			{
				Size = new Vector2(16f, 16f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			});

			return weight;
		}
	}
}
