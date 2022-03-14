using UnityEngine;

namespace Es.InkPainter.Sample
{
	[RequireComponent(typeof(Collider), typeof(MeshRenderer))]
	public class CollisionPainter : MonoBehaviour
	{
		[SerializeField]
		private Brush brush = null;

		[SerializeField]
		private int wait = 3;

		public bool canPaint = false;

		private int waitCount;
		Rigidbody rb;

		public void Awake()
		{
			GetComponent<MeshRenderer>().material.color = brush.Color;
			rb = GetComponent<Rigidbody>();
		}

		public void FixedUpdate()
		{
			++waitCount;
		}

		public void OnCollisionStay(Collision collision)
		{
			if(canPaint)
				rb.freezeRotation = true;

			if(waitCount < wait || !canPaint)
				return;

			waitCount = 0;

			foreach(var p in collision.contacts)
			{
				var canvas = p.otherCollider.GetComponent<InkCanvas>();
				if(canvas != null)
					canvas.Paint(brush, p.point);
			}
		}

        public void OnCollisionExit(Collision collision) {
			rb.freezeRotation = false;
        }

        public void CanPaint(bool tf) {
			canPaint = tf;
		}
	}
}