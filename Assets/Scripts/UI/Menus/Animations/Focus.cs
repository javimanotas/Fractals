using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Fractals.UI
{
    /// <summary> Allows to focus one panel by hiding the rest and removing the blur </summary>
    public class Focus : MonoBehaviour
    {
        MenuAnimator _animator;

        static bool Animating = false;

        PanelAnimation _parent;

        public async void ToggleFocus()
        {
            if (Animating)
            {
                return;
            }

            Animating = true;

            if (_animator == null)
            {
                _animator = FindFirstObjectByType<MenuAnimator>();
            }

            if (_parent == null)
            {
                _parent = GetComponentInParent<PanelAnimation>();
            }

            _animator.ToggleEffects();

            var tasks =  PanelAnimation.Panels
                .Select(x => x.ToggleScale(x == _parent));

            await Task.WhenAll(tasks);

            Animating = false;
        }
    }
}