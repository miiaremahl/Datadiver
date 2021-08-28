using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

/*
 https://www.youtube.com/watch?v=I9j3MD7gS5Y&list=PL9z3tc0RL6Z4WenhJiJieCcrPVNxYszod&index=8
 */

// Just for safety as we'll need some references to ARTrackedImage
[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTracking : MonoBehaviour
{
    // Array of game objects which will call placeable prefabs which will be created at runtime
    [SerializeField]
    private GameObject[] placeablePrefabs;

    // Dictionary to call spawned prefabs
    // We'll use <string> to find a placeable prefab from <GameObject> of the same name to control their functionality
    // Whether they're visible and things like that
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        // Getting and storing a reference to the tracked image manager
        trackedImageManager = FindObjectOfType<ARTrackedImageManager>();

        // Now we pre spawn the prefabs from the array (from editor)
        foreach(GameObject prefab in placeablePrefabs)
        {
            // Create new prefab which will instantiate using the current looped prefab
            GameObject newPrefab = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            // Making sure the name is correct for searching it later
            newPrefab.name = prefab.name;
            // Adding to the spawned prefabs dictionary
            spawnedPrefabs.Add(prefab.name, newPrefab);
        }

    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= ImageChanged;
    }

    // This is going to allow us to call functionality based on which images are being tracked/removed/updated
    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // each time an image is added call another function
        // WE CAN CHANGE THIS TO FIT 
        foreach(ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            spawnedPrefabs[trackedImage.name].SetActive(false);
        }

    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        string name = trackedImage.referenceImage.name;
        Vector3 position = trackedImage.transform.position;

        GameObject prefab = spawnedPrefabs[name];
        prefab.transform.position = position;
        prefab.SetActive(true);

        foreach(GameObject go in spawnedPrefabs.Values)
        {
            if(go.name != name)
            {
                go.SetActive(false);
            }
        } 
    }
}
