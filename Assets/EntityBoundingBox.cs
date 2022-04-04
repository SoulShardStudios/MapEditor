using UnityEngine;
public class EntityBoundingBox
{
    Vector3 _min, _max;
    Transform _referencedEntity;
    public EntityBoundingBox(Vector2Int min, Vector2Int max, Transform entity)
    {
        if (min.x < max.x && min.y < max.y)
        {
            _min = (Vector2)min;
            _max = (Vector2)max;
        }
        else
        {
            _min = new Vector3(min.x < max.x ? min.x : max.x, min.y < max.y ? min.y : max.y);
            _max = new Vector3(min.x > max.x ? min.x : max.x, min.y > max.y ? min.y : max.y);
        }
        _referencedEntity = entity;
    }
    public bool IsInside(Vector3Int position)
    {
        Vector3 transMin = _referencedEntity.position + _min;
        Vector3 transMax = _referencedEntity.position + _max;
        return (position.x > transMin.x) && (position.x < transMax.x) && (position.y > transMin.y) && (position.y < transMax.y);
    }
    public void DrawDebugBox(float Duration)
    {

    }
}