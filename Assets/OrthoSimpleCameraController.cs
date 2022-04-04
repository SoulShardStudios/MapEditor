using UnityEngine;
public class OrthoSimpleCameraController : MonoBehaviour
{
    [SerializeField] float speed;
    private void Update()
    {
        transform.position += speed * (Vector3)GetMovementVector() * Time.deltaTime;
    }
    Vector2 GetMovementVector()
    {
        Vector2 res = Vector2.zero;
        if (Input.GetKey(KeyCode.LeftArrow))
            res.x -= 1;
        if (Input.GetKey(KeyCode.RightArrow))
            res.x += 1;
        if (Input.GetKey(KeyCode.DownArrow))
            res.y -= 1;
        if (Input.GetKey(KeyCode.UpArrow))
            res.y += 1;
        return res;
    }
}