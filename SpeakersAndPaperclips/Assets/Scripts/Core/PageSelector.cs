using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct PageData
    {
        public string ID;
        public GameObject PageParent;

        public PageData(string iD, GameObject pageParent)
        {
            ID = iD;
            PageParent = pageParent;
        }
    }

    public class PageSelector : MonoBehaviour
    {
        [SerializeField] private string _defaultPageID = "HomePage";
        [SerializeField] private List<PageData> _pages = new();

        private void Start()
        {
            SelectPageWithString(_defaultPageID);
        }

        public void SelectPageWithString(string pageID)
        {
            int idx = _pages.FindIndex(p => p.ID == pageID);
            if (idx == -1)
            {
                Debug.LogWarning($"Page '{pageID}' not found.");
                return;
            }

            // Turn on the matched page and turn off others
            for (int i = 0; i < _pages.Count; i++)
            {
                var go = _pages[i].PageParent;
                if (go != null)
                    go.SetActive(i == idx);
            }
        }
    }
}