using UnityEngine;

public class InputTouch : MonoBehaviour
{
    private enum SwipePhase
    {
        None,
        Moved
    }

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;

    [SerializeField] private float _minSwipeDistance = 100;
    [SerializeField] private float _maxSwipeTime = 1;

    private Vector2 _startSwipePosition;
    private float _startSwipeTime;
    private SwipePhase _swipePhase;

    private Touch _touch;

    private void Update()
    {
        if(Input.touches.Length == 1)
        {
            Swipe();
        }
    }

    private void Swipe()
    {
        BeganPhase();
        MovePhase();
        EndPhase();
        CanceledPhase();
    }

    private void BeganPhase()
    {
        GetTouch();
        if (_touch.phase == TouchPhase.Began && _swipePhase == SwipePhase.None)
        {
            _swipePhase = SwipePhase.Moved;
            _startSwipePosition = _touch.position;
            _startSwipeTime = Time.time;
        }
    }

    private void MovePhase()
    {
        GetTouch();
        if (_touch.phase == TouchPhase.Moved && _swipePhase == SwipePhase.Moved)
        {
            SwipeVertical();
        }
    }

    private void EndPhase()
    {
        GetTouch();
        if (_touch.phase == TouchPhase.Ended && _swipePhase == SwipePhase.Moved)
        {
            float endSwipeTime = Time.time;
            if (endSwipeTime - _startSwipeTime > _maxSwipeTime)
            {
                Debug.Log("Слишком длинный");
                return;
            }
            SwipeHorizontal();
            _swipePhase = SwipePhase.None;
        }
    }

    private void SwipeHorizontal()
    {
        Vector2 endSwipePosition = _touch.position;
        Vector2 swipe = endSwipePosition - _startSwipePosition;

        if (swipe.magnitude < _minSwipeDistance)
        {
            Debug.Log("Слишком короткий");
            return;
        }
        if (swipe.x > 0)
        {
            Vector3 force = new Vector3(_touch.position.x, 0f, _touch.position.y);
            rb.AddForce(force * speed);
            Debug.Log("Свайп вправо!");
        }
        else
        {
            Vector3 forceNew = new Vector3(-_touch.position.y, 0f, -_touch.position.x);
            rb.AddForce(forceNew * speed);
            Debug.Log("Свайп влево!");
        }
    }

    private void SwipeVertical()
    {
        Vector2 endSwipePosition = _touch.position;
        Vector2 swipe = endSwipePosition - _startSwipePosition;

        if (Mathf.Abs(swipe.y) > Mathf.Abs(swipe.x) / 2)
        {
            Debug.Log("Не горизонтальный");
            _swipePhase = SwipePhase.None;

            if (swipe.y < 0)
            {
                Debug.Log("Свайп вниз !");
                Vector3 force = new Vector3(-_touch.position.x, 0f, -_touch.position.y);
                rb.AddForce(force * speed);
            }
            else
            {
                Debug.Log("Свайп вверх !");
                Vector3 force = new Vector3(_touch.position.x, 0f, _touch.position.y);
                rb.AddForce(force * speed);
            }
            return;
        }
    }

    private void CanceledPhase()
    {
        GetTouch();
        if (_touch.phase == TouchPhase.Canceled)
        {
            Debug.Log("Отменен");
            _swipePhase = SwipePhase.None;
        }
    }

    private void GetTouch()
    {
        _touch = Input.GetTouch(0);
    }    
}
