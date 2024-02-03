using System.Collections.Generic;
using App.Code.Model.Entities;

namespace App.Code.Model
{
    public class Space
    {
        private readonly List<Asteroid> _asteroids;

        public Space(IEnumerable<Asteroid> asteroids)
        {
            _asteroids = new List<Asteroid>(asteroids);
        }
    }
}