using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fractals.Animations
{
    /// <summary> Updates all animations <para>
    /// This class should be used to avoid overusing Unity's animation system for simple UI animations </para> </summary>
    public class AnimationController : Singleton<AnimationController>
    {
        readonly List<AnimatedField> _animations = new();

        public void AddAnimation(AnimatedField field) => _animations.Add(field);

        /// <summary> Adds animations that can be awaited </summary>
        public Task AddAsyncAnimation(AnimatedField field)
        {
            _animations.Add(field);
            return field.Completion.Task;
        }

        void Update()
        {
            for (var i = 0; i < _animations.Count; i++)
            {
                _animations[i].Update(Time.deltaTime);

                if (_animations[i].Finished)
                {
                    _animations.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}