using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private SpawnTable _spawnTable = null;
    [SerializeField] private int _spawnAmount = 1;
    [SerializeField] private bool _applyForce = false;
    [SerializeField] private float _forceValue = 1f;

    public void Spawn()
    {
        for (int i = 1; i <= _spawnAmount; i++)
        {
            GameObject gameObject = Instantiate((GameObject)_spawnTable.ChooseRandom(),
                                    transform.position,
                                    Quaternion.identity);

            if (_applyForce)
            {
                ApplyForce(gameObject.GetComponent<Rigidbody2D>());
            }
        }
    }

    private void ApplyForce(Rigidbody2D rigidbody)
    {
        if (rigidbody != null)
        {
            Vector2 force = new Vector2(Random.Range(-1, 1),
                                        Random.Range(-1, 1)) * _forceValue;

            rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
