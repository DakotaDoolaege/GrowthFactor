using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Assets.Resources.Classes.Theme;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Resources.Classes
{
    /// <summary>
    /// Static Class <c>ApplicationTheme</c> is a class that represents the
    /// theme for the entire application.
    /// </summary>
    public static class ApplicationTheme
    {
        public static IList<GameTheme> Themes;
        public static GameTheme CurrentTheme;
        private const int DEFAULT_THEME_INDEX = 0;

        static ApplicationTheme()
        {
            Themes = new List<GameTheme> {new DefaultGameTheme(), new KnightTheme(), new HalloweenTheme()};

            // Add all game themes to the linked list

            CurrentTheme = Themes[DEFAULT_THEME_INDEX];
        }

        public static void SwitchTheme(int index)
        {
            if (index >= Themes.Count || index < 0)
            {
                throw new ArgumentException("Index is out of range", nameof(index));
            }

            CurrentTheme = Themes[index];
        }

        /// <summary>
        /// Returns the current application theme
        /// </summary>
        /// <returns>
        /// The currently active application theme
        /// </returns>
        public static GameTheme GetTheme()
        {
            return CurrentTheme;
        }

        public static void RefreshThemes()
        {
            for (int i = 0; i < Themes.Count; i++)
            {
                Themes[i].Refresh();
            }
        }

        public static void ScaleBackground(GameObject backgroundObject, Sprite background)
        {
            SpriteRenderer renderer = backgroundObject.GetComponent<SpriteRenderer>();

            float cameraHeight = Camera.main.orthographicSize * 2;
            Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);

            Vector2 spriteSize = renderer.sprite.bounds.size;

            // Determine the scale in order to fill the camera
            Vector2 scale = backgroundObject.transform.localScale;
            if (cameraSize.x >= cameraSize.y)
            {
                // Landscape (or equal)
                scale *= cameraSize.x / spriteSize.x;
            }
            else
            {
                // Portrait
                scale *= cameraSize.y / spriteSize.y;
            }

            backgroundObject.transform.position = Vector2.zero; // Optional
            backgroundObject.transform.localScale = scale;
        }
    }
}
