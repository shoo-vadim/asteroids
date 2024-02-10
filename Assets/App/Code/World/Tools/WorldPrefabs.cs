using UnityEngine;

namespace App.Code.World.Tools
{
    public class WorldPrefabs : MonoBehaviour
    {
        [SerializeField] private GameWorld _game;
        [SerializeField] private MenuWorld _menu;

        public GameWorld CreateGameWorld()
        {
            return Instantiate(_game, transform);
        }

        public MenuWorld CreateMenuWorld()
        {
            return Instantiate(_menu, transform);
        }

        public void Remove(MonoBehaviour world)
        {
            Destroy(world.gameObject);
        } 
    }
}