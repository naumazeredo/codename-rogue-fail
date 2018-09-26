using System.Collections.Generic;
using UnityEngine;

// TODO: Collision handling between clones + originals
// TODO: Shader to render exactly equal clone

public class ScreenWrap : MonoBehaviour {
  public GameObject prefab;

  // Sprite clones to every side for the visual effect
  List<GameObject> clones;
  new Camera camera;

  void Start() {
    camera = Camera.main;

    // Create clones
    clones = new List<GameObject>();
    for (int i = -1; i <= 1; i++) {
      for (int j = -1; j <= 1; j++) {
        if (i == 0 && j == 0) continue;
        var go = new GameObject();

        // FIXME: ...
        var spriteRenderer = go.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = GetComponent<SpriteRenderer>().sprite;

        clones.Add(go);
      }
    }

    Reposition();
  }

  void Reposition() {
    // Actual creen wrap
    Vector3 bounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    transform.position = new Vector3(
      Mathf.Repeat(transform.position.x + bounds.x, 2 * bounds.x) - bounds.x,
      Mathf.Repeat(transform.position.y + bounds.y, 2 * bounds.y) - bounds.y,
      0f
    );

    // Reposition all clones
    int k = 0;
    for (int i = -1; i <= 1; i++) {
      for (int j = -1; j <= 1; j++) {
        if (i == 0 && j == 0) continue;

        Vector3 position = camera.ScreenToWorldPoint(
          camera.WorldToScreenPoint(transform.position) +
          new Vector3(i * Screen.width, j * Screen.height, 0)
        );
        position.z = 0f;

        clones[k].transform.position = position;
        clones[k].transform.rotation = transform.rotation;

        // TODO: change clone sprite to match original object

        k++;
      }
    }
  }

  void LateUpdate() {
    Reposition();
  }
}
