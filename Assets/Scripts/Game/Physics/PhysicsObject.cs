using UnityEngine;

public class PhysicsObject : MonoBehaviour {
  protected CollisionFetcher collisionFetcher;
  // TODO: Auto create ScreenWrap to every PhysicsObject
  protected ScreenWrap screenWrap;

  protected void SetupPhysics() {
    collisionFetcher = gameObject.AddComponent<CollisionFetcher>();
    screenWrap = gameObject.AddComponent<ScreenWrap>();
  }

  protected ContactInfo GetBestContactInfo(Vector2 axis)
  {
    var bestAngle = -1.1f;
    var best = new ContactInfo();
    foreach (ContactInfo x in collisionFetcher)
    {
      float angle = Vector2.Dot(x.normal, axis);
      if (angle > bestAngle)
      {
        best = x;
        bestAngle = angle;
      }
    }
    return best;
  }

  protected int GetContactCount() {
    return collisionFetcher.GetContactCount();
  }
}
