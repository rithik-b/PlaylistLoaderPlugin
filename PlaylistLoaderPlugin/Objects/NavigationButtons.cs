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
        private static NoTransitionsButton _leftPlaylistsButton;
        private static NoTransitionsButton _rightPlaylistsButton;

        private static AnnotatedBeatmapLevelCollectionsViewController _annotatedBeatmapLevelCollectionsViewController;
        private static AnnotatedBeatmapLevelCollectionsTableView _annotatedBeatmapLevelCollectionsTableView;
        private static TableView _tableView;
        private static GameObject _content;
        private static List<GameObject> _disabledCells;

        public const int MAX_PLAYLISTS_PER_PAGE = 6;

        public static void ButtonsInit()
        {
            _content = GameObject.FindObjectOfType<AnnotatedBeatmapLevelCollectionsViewController>().transform.Find("TableView").transform.Find("Viewport").transform.Find("Content").gameObject;

            GameObject leftPlaylistsButtonObject = new GameObject("LeftPlaylistsButton");
            leftPlaylistsButtonObject.transform.parent = GameObject.FindObjectOfType<AnnotatedBeatmapLevelCollectionsViewController>().transform.Find("TableView").transform.Find("Viewport").transform;
            leftPlaylistsButtonObject.AddComponent<CanvasRenderer>();
            leftPlaylistsButtonObject.AddComponent<Touchable>();
            _leftPlaylistsButton = leftPlaylistsButtonObject.AddComponent<NoTransitionsButton>();
            leftPlaylistsButtonObject.transform.localPosition = new Vector3(-59, 0, 0);
            leftPlaylistsButtonObject.transform.localScale = new Vector3(0.2f, 1.5f, 1.5f);
            leftPlaylistsButtonObject.layer = 5;
            _leftPlaylistsButton.selectionStateDidChangeEvent += LeftPlaylistsButton_selectionStateDidChangeEvent;
            GameObject icon = GameObject.Instantiate(GameObject.Find("BackButton").transform.Find("Icon").gameObject, leftPlaylistsButtonObject.transform, false);
            icon.transform.localScale = new Vector3(6, 1, 1);
            icon.transform.localPosition = new Vector3(40, 0, 0);

            GameObject rightPlaylistsButtonObject = new GameObject("RightPlaylistsButton");
            rightPlaylistsButtonObject.transform.parent = GameObject.FindObjectOfType<AnnotatedBeatmapLevelCollectionsViewController>().transform.Find("TableView").transform.Find("Viewport").transform;
            rightPlaylistsButtonObject.AddComponent<CanvasRenderer>();
            rightPlaylistsButtonObject.AddComponent<Touchable>();
            _rightPlaylistsButton = rightPlaylistsButtonObject.AddComponent<NoTransitionsButton>();
            rightPlaylistsButtonObject.transform.localPosition = new Vector3(30, 0, 0);
            rightPlaylistsButtonObject.transform.localScale = new Vector3(0.05f, 1.5f, 1.5f);
            rightPlaylistsButtonObject.layer = 5;
            _rightPlaylistsButton.selectionStateDidChangeEvent += RightPlaylistsButton_selectionStateDidChangeEvent;
            GameObject icon2 = GameObject.Instantiate(GameObject.Find("BackButton").transform.Find("Icon").gameObject, rightPlaylistsButtonObject.transform, false);
            icon2.transform.localScale = new Vector3(24, 1, 1);
            icon2.transform.localPosition = new Vector3(35, 0, 0);
            icon2.transform.localRotation = Quaternion.Euler(0, 0, 180);

            _annotatedBeatmapLevelCollectionsViewController = GameObject.FindObjectOfType<AnnotatedBeatmapLevelCollectionsViewController>();
            _annotatedBeatmapLevelCollectionsTableView = _annotatedBeatmapLevelCollectionsViewController.transform.Find("TableView").GetComponent<AnnotatedBeatmapLevelCollectionsTableView>();
            _tableView = _annotatedBeatmapLevelCollectionsTableView.GetComponent<TableView>();

            PostHandleOverflow();
        }

        public static void EnableButtons()
        {
            if (_leftPlaylistsButton != null)
            {
                _leftPlaylistsButton.gameObject.SetActive(true);
                _rightPlaylistsButton.gameObject.SetActive(true);
            }
            else
                ButtonsInit();
        }

        public static void DisableButtons()
        {
            if (_leftPlaylistsButton != null)
            {
                _leftPlaylistsButton.gameObject.SetActive(false);
                _rightPlaylistsButton.gameObject.SetActive(false);
            }
        }

        private static void PreHandleOverFlow()
        {
            foreach (GameObject cell in _disabledCells)
                cell.SetActive(true);
        }

        private static void PostHandleOverflow()
        {
            _disabledCells = new List<GameObject>();
            foreach (Transform child in _content.transform)
            {
                if (child.localPosition.x >= MAX_PLAYLISTS_PER_PAGE * 13 && child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(false);
                    _disabledCells.Add(child.gameObject);
                }
            }
        }

        private static void RightPlaylistsButton_selectionStateDidChangeEvent(NoTransitionsButton.SelectionState state)
        {
            if (state == NoTransitionsButton.SelectionState.Pressed)
            {
                PreHandleOverFlow();
                _tableView.ScrollToCellWithIdx(8, TableViewScroller.ScrollPositionType.Beginning, true);
                PostHandleOverflow();
            }
        }

        private static void LeftPlaylistsButton_selectionStateDidChangeEvent(NoTransitionsButton.SelectionState state)
        {
            if (state == NoTransitionsButton.SelectionState.Pressed)
            {
                PreHandleOverFlow(); 
                _tableView.ScrollToCellWithIdx(0, TableViewScroller.ScrollPositionType.Beginning, true);
                PostHandleOverflow();
            }
        }
    }
}
