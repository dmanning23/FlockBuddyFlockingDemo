using FlockBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;

namespace FlockBuddyWidgets
{
	public abstract class BaseTab : WidgetScreen
	{
		#region Properties

		protected StackLayout ToolStack { get; private set; }
		
		#endregion //Properties

		#region Methods

		public BaseTab(string tabName) : base(tabName)
		{
			CoveredByOtherScreens = true;
			CoverOtherScreens = true;
		}

		public override void LoadContent()
		{
			base.LoadContent();

			ToolStack = new StackLayout()
			{
				Position = new Point(910, 72),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Alignment = StackAlignment.Top,
			};
		}

		protected Shim AddShim(IScreenItem nextItem = null)
		{
			var shim = new Shim()
			{
				Size = new Vector2(16f, 16f)
			};

			if (null != nextItem)
			{
				ToolStack.InsertItemBefore(shim, nextItem);
			}
			else
			{
				ToolStack.AddItem(shim);
			}
			return shim;
		}

		protected IButton CreateButton(string labelText)
		{
			var button = new RelativeLayoutButton()
			{
				Size = new Vector2(360f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			button.AddItem(new Label(labelText, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center
			});

			ToolStack.AddItem(button);
			AddShim();
			return button;
		}

		protected void AddHeader(FlockManager flock)
		{
			var flockButtons = new StackLayout(StackAlignment.Left)
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};

			int sizeDelta = 360;

			//add a button with the flock name
			var relLayout = new RelativeLayout()
			{
				Size = new Vector2(256f, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				HasOutline = false
			};
			relLayout.AddItem(new TextEdit(flock.Name, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				TextColor = flock.DebugColor,
				Highlightable = false
			});
			flockButtons.AddItem(relLayout);
			sizeDelta -= relLayout.Rect.Width;

			//add a shim
			var shim = new Shim()
			{
				Size = new Vector2(16f, 16f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
			};
			flockButtons.AddItem(shim);
			sizeDelta -= shim.Rect.Width;

			//add a button to delete the flock
			var closeButton = new RelativeLayoutButton()
			{
				Size = new Vector2(sizeDelta, 32f),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				HasOutline = true
			};
			closeButton.AddItem(new Label("X", FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight),
				TextColor = Color.Red
			});
			flockButtons.AddItem(closeButton);
			sizeDelta -= closeButton.Rect.Width;

			//delete this flock and control when this button clicked
			closeButton.OnClick += (obj, e) =>
			{
				ExitScreen();
			};

			ToolStack.AddItem(flockButtons);
			AddShim();
		}

		#endregion //Methods
	}
}
