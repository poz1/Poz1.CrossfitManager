using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Urho;
using Urho.Actions;
using Urho.Gui;
using Urho.Physics;
using Urho.Shapes;

namespace Poz1.UpdatingNutrition
{
	public class Gym : Application
	{
		bool movementsEnabled;
	    Scene scene;
        public event EventHandler Ready;

        Node body;
        Camera camera;
		Octree octree;

        Node lA, rA, lL, rL;

		[Preserve]
		public Gym(ApplicationOptions options = null) : base(options) { }

		static Gym()
		{
			UnhandledException += (s, e) =>
			{
				if (Debugger.IsAttached)
					Debugger.Break();
				e.Handled = true;
			};
		}

		protected override void Start ()
		{
			base.Start ();
			CreateScene ();
            movementsEnabled = true;
          //  Ready?.Invoke(this, null);

        }

        async void CreateScene ()
		{
			Input.SubscribeToTouchEnd(OnTouched);

			scene = new Scene ();
			octree = scene.CreateComponent<Octree> ();


            body = scene.CreateChild();
            body.Position = new Vector3(0, -1.5f, 4);
            body.Rotation = new Quaternion(0, 180, 0);
            body.SetScale(1f);
   
            AnimatedModel modelObject = body.CreateComponent<AnimatedModel>();
            modelObject.Model = ResourceCache.GetModel("Ninja/Kachujin.mdl");
            modelObject.Material = ResourceCache.GetMaterial("Ninja/Kachujin.xml");

            lA = body.GetChild("LeftArm", true);
            rA = body.GetChild("RightArm", true);
            lL = body.GetChild("LeftUpLeg", true);
            rL = body.GetChild("RightUpLeg", true);
            await lA.RunActionsAsync(new EaseOut(new RotateBy(1f, -80f, 0f, 0f), 3));
            await rA.RunActionsAsync(new EaseOut(new RotateBy(1f, -80f, 0f, 0f), 3));


            Node light = scene.CreateChild(name: "light");
            light.SetDirection(new Vector3(0.4f, -0.5f, 0.3f));
            light.CreateComponent<Light>();

            Node cameraNode = scene.CreateChild(name: "camera");
            camera = cameraNode.CreateComponent<Camera>();

            Renderer.SetViewport(0, new Viewport(scene, camera, null));
            Renderer.GetViewport(0).SetClearColor(Color.White);

        }


        public async void Jump(){
            try
            {
               
                body.RunActionsAsync(new EaseOut(new JumpTo(1f, new Vector3(0, -1.5f, 4), 0.5f, 1), 3));

                lA.RunActionsAsync(new EaseOut(new RotateBy(0.5f, 160f, 0f,0f), 3)).ContinueWith((arg) => 
                    lA.RunActionsAsync(new EaseIn(new RotateBy(0.5f, -160f, 0f, 0f), 3)));
                rA.RunActionsAsync(new EaseOut(new RotateBy(0.5f, 160f, 0f, 0f), 3)).ContinueWith((arg) => 
                      rA.RunActionsAsync(new EaseIn(new RotateBy(0.5f, -160f, 0f, 0f), 3)));

                lL.RunActionsAsync(new EaseOut(new RotateBy(0.5f, 0f, 0f, 20f), 3)).ContinueWith((arg) =>
                      lL.RunActionsAsync(new EaseIn(new RotateBy(0.5f, 0f, 0f, -20f), 3)));
                rL.RunActionsAsync(new EaseOut(new RotateBy(0.5f, 0f, 0f, -20f), 3)).ContinueWith((arg) =>
                              rL.RunActionsAsync(new EaseIn(new RotateBy(0.5f, 0f, 0f, 20f), 3)));


            }
            catch (OperationCanceledException) { }
        }
		void OnTouched(TouchEndEventArgs e)
		{
			Ray cameraRay = camera.GetScreenRay((float)e.X / Graphics.Width, (float)e.Y / Graphics.Height);
			var results = octree.RaycastSingle(cameraRay, RayQueryLevel.Triangle, 100, DrawableFlags.Geometry);
			if (results != null)
			{
				var bar = results.Value.Node?.Parent?.GetComponent<Bar>();
				//if (SelectedBar != bar)
				//{
				//	//SelectedBar?.Deselect();
				//	//SelectedBar = bar;
				//	//SelectedBar?.Select();
				//}
			}
		}

        double prevDiff;
		protected override void OnUpdate(float timeStep)
		{
			if (Input.NumTouches >= 1 && movementsEnabled)
			{
                if (Input.NumTouches == 1)
                {
                    var touch = Input.GetTouch(0);
                    //We need to move the origin of the model to the center 
                    body.Rotate(new Quaternion(-touch.Delta.Y, -touch.Delta.X, 0), TransformSpace.Local);
                } else if (Input.NumTouches == 2){
                    // Calculate the distance between the two pointers
                    var curDiff = Math.Sqrt((Input.GetTouch(0).Position.X - Input.GetTouch(1).Position.X)^2 + (Input.GetTouch(0).Position.Y - Input.GetTouch(1).Position.Y) ^ 2);

                    if (prevDiff > 0)
                    {
                        if (curDiff > prevDiff)
                        {
                            // The distance between the two pointers has increased
                            System.Diagnostics.Debug.WriteLine("Pinch moving OUT -> Zoom in");
                            body.SetScale((float)(0.001 * curDiff) + body.Scale.X);
                        }
                        if (curDiff < prevDiff)
                        {
                            // The distance between the two pointers has decreased
                            System.Diagnostics.Debug.WriteLine("Pinch moving IN -> Zoom out");
                            body.SetScale((float)(0.001 * curDiff) - body.Scale.X);
                        }
                    }

                    // Cache the distance for the next move event 
                    prevDiff = curDiff;
                }

            }
            base.OnUpdate(timeStep);
		}

		public void Rotate(float toValue)
		{
            body.Rotate(new Quaternion(0, toValue, 0), TransformSpace.Local);
        }

        public void Reset()
        {
            body.Position = new Vector3(0, -1.5f, 4);
            body.Rotation = new Quaternion(0, 180, 0);
            body.SetScale(1f);
        }
    }

    class Mover : Component
    {
        float MoveSpeed { get; set; }
        float RotationSpeed { get; }

        public Mover(float moveSpeed, float rotateSpeed)
        {
            MoveSpeed = moveSpeed;
            RotationSpeed = rotateSpeed;
            ReceiveSceneUpdates = true;
        }

        protected override void OnUpdate(float timeStep)
        {
            // This moves the character position
            Node.Translate(Vector3.UnitX * MoveSpeed * timeStep, TransformSpace.Local);

            // If in risk of going outside the plane, rotate the model right
            var pos = Node.Position;

            System.Diagnostics.Debug.WriteLine("La tipa sta qua: X: " + pos.X + " Y: " + pos.Y + " Z: " + pos.Z);
            if (pos.X > 100)
                MoveSpeed = -MoveSpeed;
            //if (pos.X < Bounds.Min.X || pos.X > Bounds.Max.X || pos.Z < Bounds.Min.Z || pos.Z > Bounds.Max.Z)
                //Node.Yaw(RotationSpeed * timeStep, TransformSpace.Local);

            // Get the model's first (only) animation
            // state and advance its time. Note the
            // convenience accessor to other components in
            // the same scene node

            var model = GetComponent<AnimatedModel>();
            if (model.NumAnimationStates > 0)
            {
                var state = model.AnimationStates.First();
                state.AddTime(timeStep);
            }
        }
    }
}