using UnityEngine;

namespace _Scripts.Helpers
{
    public class LayerMaskHelper  
    {
        public static bool IsLayerInLayerMask(int layer, LayerMask layerMask)
        {
            return layerMask == (layerMask | (1 << layer));
        }
    }
}