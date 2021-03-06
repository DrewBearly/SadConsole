﻿#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endif

namespace SadConsole.Themes
{
    using SadConsole.Controls;
    using System.Runtime.Serialization;

    /// <summary>
    /// Has the basic appearances of each control state.
    /// </summary>
    [DataContract]
    public class ThemeStates
    {
        /// <summary>
        /// The normal appearance of the control.
        /// </summary>
        [DataMember]
        public Cell Normal;

        /// <summary>
        /// The appearance of the control when it is disabled.
        /// </summary>
        [DataMember]
        public Cell Disabled;

        /// <summary>
        /// The appearance of the control when it is focused.
        /// </summary>
        [DataMember]
        public Cell Focused;

        /// <summary>
        /// The appearence of the control when it is in a selected state.
        /// </summary>
        [DataMember]
        public Cell Selected;

        /// <summary>
        /// The appearance of the control when the mouse is over it.
        /// </summary>
        [DataMember]
        public Cell MouseOver;

        /// <summary>
        /// THe appearance of the control when a mouse button is held down.
        /// </summary>
        [DataMember]
        public Cell MouseDown;

        /// <summary>
        /// Creates a new instance of the theme states object.
        /// </summary>
        public ThemeStates(Colors themeColors = null)
        {
            themeColors = themeColors ?? Library.Default.Colors;

            RefreshTheme(themeColors);
        }

        /// <summary>
        /// Sets the same foreground color to all theme states.
        /// </summary>
        /// <param name="color">The foreground color.</param>
        public void SetForeground(Color color)
        {
            Normal.Foreground = color;
            Disabled.Foreground = color;
            MouseOver.Foreground = color;
            MouseDown.Foreground = color;
            Selected.Foreground = color;
            Focused.Foreground = color;
        }

        /// <summary>
        /// Sets the same background color to all theme states.
        /// </summary>
        /// <param name="color">The background color.</param>
        public void SetBackground(Color color)
        {
            Normal.Background = color;
            Disabled.Background = color;
            MouseOver.Background = color;
            MouseDown.Background = color;
            Selected.Background = color;
            Focused.Background = color;
        }

        /// <summary>
        /// Sets the same glyph to all theme states.
        /// </summary>
        /// <param name="glyph">The glyph.</param>
        public void SetGlyph(int glyph)
        {
            Normal.Glyph = glyph;
            Disabled.Glyph = glyph;
            MouseOver.Glyph = glyph;
            MouseDown.Glyph = glyph;
            Selected.Glyph = glyph;
            Focused.Glyph = glyph;
        }

        /// <summary>
        /// Sets the same mirror setting to all theme states.
        /// </summary>
        /// <param name="mirror">The mirror setting.</param>
        public void SetMirror(SpriteEffects mirror)
        {
            Normal.Mirror = mirror;
            Disabled.Mirror = mirror;
            MouseOver.Mirror = mirror;
            MouseDown.Mirror = mirror;
            Selected.Mirror = mirror;
            Focused.Mirror = mirror;
        }

        /// <summary>
        /// Gets an apperance defined by this theme from the <paramref name="state" /> parameter.
        /// </summary>
        /// <param name="state">The state to check.</param>
        /// <returns>A cell appearance.</returns>
        public Cell GetStateAppearance(ControlStates state)
        {
            if (Helpers.HasFlag(state, ControlStates.Disabled))
                return Disabled;

            if (Helpers.HasFlag(state, ControlStates.MouseLeftButtonDown) || Helpers.HasFlag(state, ControlStates.MouseRightButtonDown))
                return MouseDown;

            if (Helpers.HasFlag(state, ControlStates.MouseOver))
                return MouseOver;

            if (Helpers.HasFlag(state, ControlStates.Focused))
                return Focused;

            if (Helpers.HasFlag(state, ControlStates.Selected))
                return Selected;

            return Normal;
        }

        /// <summary>
        /// Performs a deep copy of this theme.
        /// </summary>
        /// <returns>A new instance of the theme.</returns>
        public ThemeStates Clone()
        {
            return new ThemeStates()
            {
                Normal = Normal.Clone(),
                Disabled = Disabled.Clone(),
                MouseOver = MouseOver.Clone(),
                MouseDown = MouseDown.Clone(),
                Selected = Selected.Clone(),
                Focused = Focused.Clone(),
            };
        }

        /// <summary>
        /// Reloads the theme values based on the colors provided.
        /// </summary>
        /// <param name="themeColors">The colors to create the theme with.</param>
        public virtual void RefreshTheme(Colors themeColors)
        {
            Normal = themeColors.Appearance_ControlNormal.Clone();
            Disabled = themeColors.Appearance_ControlDisabled.Clone();
            MouseOver = themeColors.Appearance_ControlOver.Clone();
            MouseDown = themeColors.Appearance_ControlMouseDown.Clone();
            Selected = themeColors.Appearance_ControlSelected.Clone();
            Focused = themeColors.Appearance_ControlFocused.Clone();
        }
    }

    /// <summary> 
    /// The base class for a theme.
    /// </summary>
    [DataContract]
    public abstract class ThemeBase : ThemeStates
    {
        /// <summary>
        /// The colors to set for the theme. If <see langword="null"/>, then colors are pulled from the parent control or <see cref="Themes.Library.Default"/>.
        /// </summary>
        public Colors Colors { get; set; }

        /// <summary>
        /// Draws the control state to the control.
        /// </summary>
        /// <param name="control">The control to draw.</param>
        /// <param name="time">The time since the last update frame call.</param>
        public abstract void UpdateAndDraw(ControlBase control, System.TimeSpan time);

        /// <summary>
        /// Called when the theme is attached to a control.
        /// </summary>
        /// <param name="control">The control that will use this theme instance.</param>
        public virtual void Attached(ControlBase control)
        {
            var colors = Colors ?? control.Parent?.Theme.Colors ?? Library.Default.Colors;

            RefreshTheme(colors);
        }

        /// <summary>
        /// Creates a new theme instance based on the current instance.
        /// </summary>
        /// <returns>A new theme instance.</returns>
        public new abstract ThemeBase Clone();
    }
}
