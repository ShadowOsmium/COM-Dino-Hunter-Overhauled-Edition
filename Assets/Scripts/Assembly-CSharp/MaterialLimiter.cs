using UnityEngine;
using System.Collections.Generic;

public class MaterialLimiter : MonoBehaviour
{
    private const int MAX_MATERIAL_VALUE = 45;  // Max material limit
    private const float CHECK_INTERVAL = 1f;    // Time between checks (in seconds)

    private MaterialLimiter materialLoader;
    private float nextCheckTime = 0f;
    private Dictionary<string, int> m_dictMaterials;
    private Dictionary<int, int> m_dictGoods;

    public void LimitMaterialById(int materialId)
    {
        // Define the valid ID range for materials
        int minId = 50001;
        int maxId = 700006;

        // Check if the materialId is within the valid range
        if (materialId >= minId && materialId <= maxId)
        {
            // Material ID is within the valid range, so you can add it or process it
            Debug.Log("Valid material ID: " + materialId);
            // Proceed with material handling here, like adding it to the inventory, etc.
        }
        else
        {
            // Material ID is outside the valid range, handle the invalid case
            Debug.LogError("Invalid material ID: " + materialId);
            // Optionally, show an error or prevent adding it to the inventory
        }
    }


    void Update()
    {
        // Only check periodically to avoid too frequent checks
        if (Time.time >= nextCheckTime)
        {
            nextCheckTime = Time.time + CHECK_INTERVAL;

            if (materialLoader != null)
            {
                CheckAndLimitGoods();
            }
        }
    }

    private void CheckAndLimitGoods()
    {
        Dictionary<int, int> goods = materialLoader.m_dictGoods; // Assuming you are tracking goods in this dictionary
        int total = 0;

        // Create a list of the goods keys to iterate over
        List<int> keys = new List<int>(goods.Keys);

        // Sort keys to ensure consistent order (optional)
        keys.Sort();

        foreach (int key in keys)
        {
            int value = goods[key]; // Get the quantity of the specific good

            // If the good's value exceeds MAX_MATERIAL_VALUE, reset it to 0
            if (value > MAX_MATERIAL_VALUE)
            {
                Debug.LogWarning("Good with ID " + key + " exceeds max value, resetting to 0.");
                goods[key] = 0;  // Reset it to 0
                value = 0;  // Update the value to reflect reset
            }

            // Add the value of the good to the total
            total += value;

            // If the total exceeds MAX_MATERIAL_VALUE, we need to clamp the remaining goods
            if (total > MAX_MATERIAL_VALUE)
            {
                // How much we can still add before reaching the max limit
                int allowed = MAX_MATERIAL_VALUE - (total - value);
                goods[key] = allowed;  // Clamp this good's value to the allowed amount
                total = MAX_MATERIAL_VALUE;  // We have hit the max total
                break;  // Stop processing as we've hit the limit
            }
        }

        // Log the results for debugging
        Debug.Log("Goods validated and limited. Final total = " + total);
    }
}
