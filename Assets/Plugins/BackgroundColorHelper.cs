using UnityEngine;

public static class BackgroundColorHelper
{
    static readonly Color _default = new(0.2196f, 0.2196f, 0.2196f);

    static readonly Color _selected = new(0.1725f, 0.3647f, 0.5294f);

    static readonly Color _selectedUnfocused = new(0.3f, 0.3f, 0.3f);

    static readonly Color _hovered = new(0.2706f, 0.2706f, 0.2706f);

    public static Color Get(bool isSelected, bool isHovered, bool isWindowFocused)
    {
        if (isSelected)
        {
            if (isWindowFocused)
            {
                return _selected;
            }

            return _selectedUnfocused;
        }

        if (isHovered)
        {
            return _hovered;
        }

        return _default;
    }
}