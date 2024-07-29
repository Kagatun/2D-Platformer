using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private InputDetector _inputDetector;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Hero _hero;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private EnemyDetector _enemyDetector;

    private List<Enemy> _enemiesInZone = new List<Enemy>();
    private WaitForSecondsRealtime _wait;

    private int _damage = 5;
    private int _timeAction = 6;
    private int _timeInterval = 1;
    private int _timeCooldown = 10;
    private int _currentTimeCooldown;
    private int _currentTimeAction;

    private bool _isSkillCooldown;

    public event Action<int, int> OnTime;

    private void Awake()
    {
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
        _currentTimeCooldown = _timeCooldown;
        _isSkillCooldown = true;

        _wait = new WaitForSecondsRealtime(_timeInterval);

        _inputDetector.OnSkillKey += ActivateVampirism;
        _enemyDetector.OnEnemyEnter += AddEnemy;
        _enemyDetector.OnEnemyExit += RemoveEnemy;
    }

    private void OnDisable()
    {
        _inputDetector.OnSkillKey -= ActivateVampirism;
        _enemyDetector.OnEnemyEnter -= AddEnemy;
        _enemyDetector.OnEnemyExit -= RemoveEnemy;
    }

    private void AddEnemy(Enemy enemy)
    {
        if (!_enemiesInZone.Contains(enemy))
        {
            _enemiesInZone.Add(enemy);
        }
    }

    private void RemoveEnemy(Enemy enemy)
    {
        if (_enemiesInZone.Contains(enemy))
        {
            _enemiesInZone.Remove(enemy);
        }
    }

    private void ActivateVampirism()
    {
        if (_isSkillCooldown)
        {
            StartCoroutine(VampirismRoutine());
        }
    }

    private IEnumerator VampirismRoutine()
    {
        _isSkillCooldown = false;
        _currentTimeCooldown = 0;
        _currentTimeAction = _timeAction;

        _collider.enabled = true;
        _spriteRenderer.enabled = true;

        while (_currentTimeAction > 0)
        {
            foreach (var enemy in _enemiesInZone)
            {
                if (enemy != null)
                {
                    enemy.Health.TakeDamage(_damage);
                    _hero.Health.Heal(_damage);

                    if (enemy.Health.Value <= 0)
                    {
                        _enemiesInZone.Remove(enemy);
                    }
                }
            }

            _currentTimeAction = Mathf.Clamp(_currentTimeAction, 0, _timeAction);
            OnTime?.Invoke(_currentTimeAction, _timeAction);

            yield return _wait;

            _currentTimeAction--;
        }

        _collider.enabled = false;
        _spriteRenderer.enabled = false;

        _enemiesInZone.Clear();

        StartCoroutine(SkillRecovering());
    }

    private IEnumerator SkillRecovering()
    {
        while (_currentTimeCooldown <= _timeCooldown)
        {
            _currentTimeCooldown = Mathf.Clamp(_currentTimeCooldown, 0, _timeCooldown);
            OnTime?.Invoke(_currentTimeCooldown, _timeCooldown);

            yield return _wait;

            _currentTimeCooldown++;
        }

        _isSkillCooldown = true;
    }
}

