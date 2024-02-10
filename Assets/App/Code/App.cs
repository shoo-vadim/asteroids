using System;
using System.Linq;
using App.Code.World;
using App.Code.World.Tools;
using UnityEngine;

namespace App.Code
{
    [RequireComponent(typeof(WorldPrefabs))]
    public class App : MonoBehaviour
    {
        private WorldPrefabs _prefabs;

        private GameWorld _game;
        private MenuWorld _menu;

        private void Awake()
        {
            if (!TryGetComponent(out _prefabs))
            {
                throw new InvalidOperationException($"Unable to find {typeof(WorldPrefabs).FullName} component!");
            }

            if (GetComponentsInChildren<IWorld>(false) is { Length: > 0 } worlds)
            {
                switch (worlds.Single())
                {
                    case GameWorld game:
                        RunGame(game);
                        break;
                    case MenuWorld menu:
                        RunMenu(menu);
                        break;
                }
            }
            else
            {
                RunMenu(_prefabs.CreateMenuWorld());
            }
        }

        private void RunGame(GameWorld game)
        {
            if (_menu)
            {
                _menu.GameStart -= OnGameStart;
                _prefabs.Remove(_menu);
            }
            _game = game;
            _game.GameOver += OnGameOver;
        }

        private void RunMenu(MenuWorld menu)
        {
            if (_game)
            {
                _game.GameOver -= OnGameOver;
                _prefabs.Remove(_game);
            }
            _menu = menu;
            _menu.GameStart += OnGameStart;
        }

        private void OnGameStart() => RunGame(_prefabs.CreateGameWorld());
        
        private void OnGameOver() => RunMenu(_prefabs.CreateMenuWorld());
        
    }
}