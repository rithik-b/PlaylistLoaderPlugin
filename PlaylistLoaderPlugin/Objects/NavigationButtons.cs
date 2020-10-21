using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PlaylistLoaderLite.Objects
{
    internal class NavigationButtons
    {
        public static NoTransitionsButton leftPlaylistsButton;
        public static NoTransitionsButton rightPlaylistsButton;

        public static void ButtonsInit()
        {
            GameObject leftPlaylistsButtonObject = new GameObject("LeftPlaylistsButton");
            leftPlaylistsButtonObject.transform.parent = GameObject.FindObjectOfType<AnnotatedBeatmapLevelCollectionsViewController>().transform.Find("TableView").transform.Find("Viewport").transform;
            leftPlaylistsButtonObject.AddComponent<CanvasRenderer>();
            leftPlaylistsButtonObject.AddComponent<Touchable>();
            leftPlaylistsButton = leftPlaylistsButtonObject.AddComponent<NoTransitionsButton>();
            leftPlaylistsButtonObject.transform.localPosition = new Vector3(-59, 0, 0);
            leftPlaylistsButtonObject.transform.localScale = new Vector3(0.2f, 1.5f, 1.5f);
            leftPlaylistsButton.selectionStateDidChangeEvent += LeftPlaylistsButton_selectionStateDidChangeEvent;
            GameObject icon = GameObject.Instantiate(GameObject.Find("BackButton").transform.Find("Icon").gameObject, leftPlaylistsButtonObject.transform, false);
            icon.transform.localScale = new Vector3(6, 1, 1);
            icon.transform.localPosition = new Vector3(40, 0, 0);

            GameObject rightPlaylistsButtonObject = new GameObject("RightPlaylistsButton");
            rightPlaylistsButtonObject.transform.parent = GameObject.FindObjectOfType<AnnotatedBeatmapLevelCollectionsViewController>().transform.Find("TableView").transform.Find("Viewport").transform;
            rightPlaylistsButtonObject.AddComponent<CanvasRenderer>();
            rightPlaylistsButtonObject.AddComponent<Touchable>();
            rightPlaylistsButton = rightPlaylistsButtonObject.AddComponent<NoTransitionsButton>();
            rightPlaylistsButtonObject.transform.localPosition = new Vector3(30, 0, 0);
            rightPlaylistsButtonObject.transform.localScale = new Vector3(0.05f, 1.5f, 1.5f);
            rightPlaylistsButton.selectionStateDidChangeEvent += RightPlaylistsButton_selectionStateDidChangeEvent;
            GameObject icon2 = GameObject.Instantiate(GameObject.Find("BackButton").transform.Find("Icon").gameObject, rightPlaylistsButtonObject.transform, false);
            icon2.transform.localScale = new Vector3(24, 1, 1);
            icon2.transform.localPosition = new Vector3(35, 0, 0);
            icon2.transform.localRotation = Quaternion.Euler(0, 0, 180);
        }

        private static void RightPlaylistsButton_selectionStateDidChangeEvent(NoTransitionsButton.SelectionState obj)
        {
            if (obj == NoTransitionsButton.SelectionState.Pressed)
                Plugin.Log.Critical("Pressed right!");
        }

        private static void LeftPlaylistsButton_selectionStateDidChangeEvent(NoTransitionsButton.SelectionState obj)
        {
            if (obj == NoTransitionsButton.SelectionState.Pressed)
                Plugin.Log.Critical("Pressed left!");
        }
    }
}
